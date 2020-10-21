using Layers.Data.Contracts.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Layers.Base.Entities;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace Layers.Data.DataAccess.Repository
{
    public class DynamicRepository : IDynamicRepository
    {
        #region Members

        private IUnitOfWork _unitOfWork;

        #endregion

        #region Ctor
        public DynamicRepository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("UnitOfWork can not be null!");
            }
            this._unitOfWork = unitOfWork;
        }

        #endregion

        #region IDynmicRepository

        public List<object> ExecuteQuery(Type returnType, string query, Dictionary<string, object> paramters = null)
        {
            // No query
            if (String.IsNullOrEmpty(query))
            {
                throw new ArgumentNullException("Query can not be null!");
            }

            // New Sql paramter List
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            // If Paramters exist
            if (paramters != null)
            {
                // ForEach param in dictionary add to sql parameter list
                foreach (KeyValuePair<string, object> param in paramters)
                {
                    sqlParams.Add(new SqlParameter(param.Key, param.Value));
                }
            }

            //Execute Sql Command
            return _unitOfWork.Context.Database.SqlQuery(returnType, query, sqlParams.ToArray()).ToListAsync().Result;
        }

        public List<TReturnType> ExecuteQuery<TReturnType>(string query, Dictionary<string, object> paramters = null)
        {
            // No query
            if (String.IsNullOrEmpty(query))
            {
                throw new ArgumentNullException("Query can not be null!");
            }

            // New Sql paramter List
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            // If Paramters exist
            if (paramters != null)
            {
                // ForEach param in dictionary add to sql parameter list
                foreach (KeyValuePair<string, object> param in paramters)
                {
                    sqlParams.Add(new SqlParameter(param.Key, param.Value));
                }
            }

            //Execute Sql Command
            return _unitOfWork.Context.Database.SqlQuery<TReturnType>(query, sqlParams.ToArray()).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TReturnType"></typeparam>
        /// <param name="functionName">function or stored name</param>
        /// <param name="parameters"></param>
        /// <returns>result list</returns>
        public IQueryable<TReturnType> ExecuteFunction<TReturnType>(string functionName, Dictionary<string, object> parameters = null, params string[] includes)
        {
            //No Query
            if (string.IsNullOrEmpty(functionName))
            {
                //Throw Null Ref Exception
                throw new ArgumentNullException("functionName");
            }

            //New Sql Parameter list
            List<ObjectParameter> sqlParams = new List<ObjectParameter>();

            //if params Dictionary is not null
            StringBuilder queryString = new StringBuilder(functionName);
            queryString.Append("(");
            if (parameters != null)
            {
                int i = 1;
                //For Each item in Dictionary add to sql parameter list
                foreach (KeyValuePair<string, object> item in parameters)
                {

                    sqlParams.Add(new ObjectParameter(item.Key, item.Value));
                    queryString.Append("@").Append(item.Key);
                    if (i != parameters.Count)
                        queryString.Append(",");
                    i++;
                }
            }

            queryString.Append(")");

            var query = ((IObjectContextAdapter)_unitOfWork.Context).ObjectContext.CreateQuery<TReturnType>(queryString.ToString(), sqlParams.ToArray());

            if (query != null && includes != null)
            {

                foreach (string include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
            //Execute Sql Command
            //return ((IObjectContextAdapter)_unitOfWork.Context).ObjectContext.ExecuteFunction<TReturnType>(functionName, sqlParams.ToArray()).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="functionName">function or stored name</param>
        /// <param name="parameters"></param>
        /// <returns>number of affected rows</returns>
        public bool ExecuteFunction(string functionName, Dictionary<string, object> parameters = null)
        {
            //No Query
            if (string.IsNullOrEmpty(functionName))
            {
                //Throw Null Ref Exception
                throw new ArgumentNullException("functionName");
            }

            //New Sql Parameter list
            List<ObjectParameter> sqlParams = new List<ObjectParameter>();

            //if params Dictionary is not null
            if (parameters != null)
            {
                //For Each item in Dictionary add to sql parameter list
                foreach (KeyValuePair<string, object> item in parameters)
                {
                    sqlParams.Add(new ObjectParameter(item.Key, item.Value));
                }
            }

            //Execute Sql Command
            return ((IObjectContextAdapter)_unitOfWork.Context).ObjectContext.ExecuteFunction(functionName, sqlParams.ToArray()) > 0;
        }
        public List<DbColumn> GetColumns(string objectName)
        {
            //if ObjectName or columnName is null
            if (string.IsNullOrEmpty(objectName))
            {
                throw new ArgumentNullException("ObjectName can not be null!");
            }

            //if objectName contains punctuation or whitespce characters, throw new Exception
            if (objectName.Any(x => x != '_' && x != '.' && char.IsPunctuation(x) || char.IsWhiteSpace(x)))
            {
                throw new ArgumentException("Invaild object name");
            }

            //Construct Query
            string query = $"SELECT c.name AS 'Name', t.name AS 'DbTypeName', t.is_nullable as IsNullable FROM sys.columns c INNER JOIN sys.types t on c.user_type_id = t.user_type_id WHERE Object_ID = Object_ID(N'{objectName}')";

            //#xecute query
            var result = _unitOfWork.Context.Database.SqlQuery<DbColumn>(query).ToList();

            // retun list of DbColumn
            return result;
        }

        public int ExecuteCommand(string query, Dictionary<string, object> paramters = null)
        {
            // No query
            if (String.IsNullOrEmpty(query))
            {
                throw new ArgumentNullException("Query can not be null!");
            }

            // New Sql paramter List
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            // If Paramters exist
            if (paramters != null)
            {
                // ForEach param in dictionary add to sql parameter list
                foreach (KeyValuePair<string, object> param in paramters)
                {
                    sqlParams.Add(new SqlParameter(param.Key, param.Value));
                }
            }

            //Execute Sql Command
            return _unitOfWork.Context.Database.ExecuteSqlCommand(query, sqlParams.ToArray());
        }

        #endregion

    }
}
