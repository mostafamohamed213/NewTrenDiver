using Layers.Data.Contracts.Contracts;
using Layers.Data.DataAccess.Repository;
using Layers.Base.Consts;
using Layers.Base.Entities;
using Layers.Base.Enums;
using Layers.Utilities.Logging;
using Layers.Utilities.Types;
using Layers.Utilities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Layers.Business.Managers
{
    public class LookupManger
    {
        #region Public Methods
        public DescriptiveResponse<List<LookupObject<TId, TUId>>> GetAll<TId, TUId>(string lookupName, bool includeExtraColumns = false, bool includeDeleted = false) where TId : IEquatable<TId>
                                                                                                                                                                where TUId : struct
        {
            return FilterLookup<TId,TUId>(lookupName, null, includeExtraColumns, includeDeleted);
        }

        public DescriptiveResponse<List<LookupObject<TId, TUId>>> Find<TId, TUId>(string lookupName,List<FilterSearchCriteria> filters, bool includeExtraColumns = false, bool includeDeleted = false) where TId : IEquatable<TId>
                                                                                                                                                               where TUId : struct
        {
            return FilterLookup<TId, TUId>(lookupName, filters, includeExtraColumns, includeDeleted);
        }
        #endregion

        #region Private Methods
        private DescriptiveResponse<TResponse> PreventSQLInjection<TResponse>(string lookupName)
        {
            if (lookupName.Any(x => x != '_' && Char.IsPunctuation(x) || lookupName.Any(char.IsWhiteSpace)))
            {
                return new DescriptiveResponse<TResponse>
                {
                    IsErrorState = true,
                    ErrorMetaData = ErrorStatus.INPUT_INVAILD,
                    ErrorDescription = ErrorStatus.INPUT_INVAILD.ToString()
                };
            }

            return null;
        }

        private DescriptiveResponse<List<LookupObject<TId, TUId>>> FilterLookup<TId, TUId>(string lookupName, List<FilterSearchCriteria> filters = null, bool includeExtraColumns = false, bool includeDeleted = false) where TId : IEquatable<TId>
                                                                                                                                                                                                                        where TUId : struct
        {
            var injected = PreventSQLInjection<List<LookupObject<TId, TUId>>>(lookupName);

            if (injected != null)
            {
                return injected;
            }

            try
            {
                // New UnitOfWork
                using (IUnitOfWork unitOfWork = new UnitOfWork())
                {

                    // New dynmic repository
                    IDynamicRepository repository = new DynamicRepository(unitOfWork);

                    // Get all Coulmns
                    List<DbColumn> columns = repository.GetColumns($"dbo.LK_{lookupName}");

                    StringBuilder query = new StringBuilder();

                    // Create Query
                    CreateQuery(query, columns, lookupName);

                    // Apply Filter on query
                    ApplyFilters<TUId>(query, includeDeleted, columns.Any(col => col.Name.ToLower() == LookUpConsts.CreatedByField.ToLower()), filters, columns);

                    // Execute SQL Query
                    List<LookupObject<TId, TUId>> lookupCollection = repository.ExecuteQuery<LookupObject<TId, TUId>>(query.ToString());

                    lookupCollection.ForEach(item => item.LookupObjectType = item.UserId.HasValue ? LookupObjectType.User : LookupObjectType.System);

                    if (includeExtraColumns)
                    {
                        GetExtraColumns(repository, lookupCollection, columns, lookupName, includeDeleted, filters);
                    }

                    return DescriptiveResponse<List<LookupObject<TId, TUId>>>.Success(lookupCollection);
                }

            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return DescriptiveResponse<List<LookupObject<TId, TUId>>>.Error(ErrorStatus.UNEXPECTED_ERROR);
            }
        }

        private void CreateQuery(StringBuilder query, List<DbColumn> columns, string lookupName)
        {
            // Check if display name column exist in lookup table
            if (columns.Any(col => col.Name.ToLower() == LookUpConsts.DisplayNameField.ToLower()))
            {
                // Construct Query
                query.Append($"SELECT * FROM dbo.LK_{lookupName} L");
            }
            else // If Lookup table does not contain DisplayName so it has loclized table 
            {
                // Get Current Culture
                string currentCultue = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

                // Construct query based on existing lookup_local table contains locallization strings per language
                // Returns createdby to check if lookup object type defiend by system or user
                if (columns.Any(col => col.Name.ToLower() == LookUpConsts.CreatedByField.ToLower()))
                {
                    query.Append($"SELECT E.Id, E.UniqueKey, L.Name AS '{LookUpConsts.DisplayNameField}', E.CreatedBy AS UserId, E.IsDeleted FROM dbo.LK_{lookupName} E JOIN dbo.LK_Language G ON G.Abbreviation=N'{currentCultue}' JOIN dbo.LK_{lookupName}_Local L ON E.Id = L.{lookupName}Id and L.LangId = G.Id");
                }
                else
                {
                    query.Append($"SELECT E.Id, E.UniqueKey, L.Name AS '{LookUpConsts.DisplayNameField}', E.IsDeleted FROM dbo.LK_{lookupName} E JOIN dbo.LK_Language G ON G.Abbreviation=N'{currentCultue}' JOIN dbo.LK_{lookupName}_Local L ON E.Id = L.{lookupName}Id and L.LangId = G.Id");
                }
            }
        }

        private void ApplyFilters<TUId>(StringBuilder query, bool includeDeleted, bool isUserIdExist, List<FilterSearchCriteria> filters = null, List<DbColumn> columns = null) where TUId : struct
        {
            // If deleted records not included
            if (!includeDeleted)
            {
                query.Append(" WHERE E.IsDeleted = 0");
            }

            // Filter lookupData if userId exists
            if (isUserIdExist)
            {
                if (!includeDeleted)
                {
                    query.Append($" AND (E.CreatedBy IS NULL OR E.CreatedBy = {UserUtility<TUId>.CurrentUser.UserId}");
                }
                else
                {
                    query.Append($" WHERE (E.CreatedBy IS NULL OR E.CreatedBy = {UserUtility<TUId>.CurrentUser.UserId}");

                }
            }

            if (filters != null)
            {
                // Filter lookup data with search criteria
                if (includeDeleted && !isUserIdExist)
                {
                    query.Append(" WHERE");
                }

                filters.ForEach(filter =>
                {
                    DbColumn column = columns.Find(col => col.Name.ToLower() == filter.Field.ToLower());

                    if (column != null)
                    {
                        query.Append($" AND E.{column.Name} = N'{filter.SearchKey}'");
                    }
                });

            }

        }

        private void GetExtraColumns<TId, TUId>(IDynamicRepository repository, List<LookupObject<TId, TUId>> lookupCollection, List<DbColumn> columns, string lookupName, bool includeDeleted, List<FilterSearchCriteria> filters = null) where TId : IEquatable<TId>
                                                 where TUId : struct
        {
            // Get Lookup object properties
            var properties = typeof(LookupObject<TId, TUId>).GetProperties();

            StringBuilder extraQuery = new StringBuilder();

            // Get extra Columns
            List<DbColumn> extraColumns = columns.Where(col => !properties.Any(prop => prop.Name == col.Name)).ToList();

            // If there are extra columns
            if (extraColumns != null && extraColumns.Count>0)
            {
                // Join all columns name seprated by ,
                string columnStr = string.Join(",", extraColumns.Select(col => col.Name));

                extraQuery.Append($"SELECT Id, {columnStr} FROM dbo.LK_{lookupName} AS E");
                 
                // Apply filter on extra query
                ApplyFilters<TUId>(extraQuery, includeDeleted, columns.Any(col => col.Name.ToLower() == LookUpConsts.CreatedByField.ToLower()), filters, columns);

                // Build dynmic type
                TypeBuilder builder = DynmicTypeBuilder.CreateTypeBuilder("MyDynmicAssembly", "MyModule", "MyType");

                // Find Id Column
                DbColumn idColumn = columns.First(x => x.Name.ToLower() == LookUpConsts.IdField.ToLower());

                // If id not exist
                if (idColumn == null)
                {
                    throw new Exception("Can not find Id column for lookup entity.");
                }

                // Add id property.
                DynmicTypeBuilder.CreateAutoImplementedProperty(builder, idColumn.Name, idColumn.ColumnType);

                // Add columns as propreties at dynmic type.
                extraColumns.ForEach(col => DynmicTypeBuilder.CreateAutoImplementedProperty(builder, col.Name, col.ColumnType));

                // Create Type of builder.
                Type resultType = builder.CreateType();

                List<dynamic> extraFields = repository.ExecuteQuery(resultType, extraQuery.ToString());

                foreach (var item in lookupCollection)
                {
                    var dynmicObject = extraFields.FirstOrDefault(x => item.Id == x.Id);

                    item.ExtraFields = new Dictionary<string, object>();

                    extraColumns.ForEach(col => 
                    {
                        item.ExtraFields.Add(col.Name, resultType.GetProperty(col.Name).GetValue(dynmicObject));
                    });
                }
            }
        }

        #endregion
    }
}
