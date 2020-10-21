using Layers.Base.Contracts;
using Layers.Base.Entities;
using Layers.Base.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Layers.Base.Consts;
using System.ComponentModel;
using Layers.Utilitize.Extensions;

namespace Layers.Utilities.Extensions
{
    public static class QuerableExtensions
    {

        #region Members

        private static int _pageSize;

        #endregion

        #region Properties

        private static int PageSize
        {
            get
            {
                if (_pageSize == 0)
                {
                    int.TryParse(ConfigurationManager.AppSettings["PageSize"], out _pageSize);
                }
                return _pageSize != 0 ? _pageSize : SystemConsts.defaultPageSiaze;
            }
        }

        #endregion
        public static IQueryable<TEntity> AppendFilterSortCriteria<TEntity, TId>(this IQueryable<TEntity> query, FilterSortCriteria sortCriteria) where TEntity : IEntity<TId>
        {
            // Determine order command
            string command = sortCriteria != null ? (sortCriteria.Direction == SortDirection.DESC ? "OrderByDescending" : "OrderBy") : "OrderBy";

            // Type of TEntity
            Type type = typeof(TEntity);

            // Get property by it's name
            PropertyInfo property = type.GetProperty(sortCriteria != null ? sortCriteria.Field : "Id");

            // Create expression paramter
            ParameterExpression paramter = Expression.Parameter(type, "p");

            // Create member expression
            MemberExpression propertyAccess = Expression.MakeMemberAccess(paramter, property);

            // Create lambda expression
            LambdaExpression orderByExpression = Expression.Lambda(propertyAccess, paramter);

            // Calling expression
            MethodCallExpression resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
                                                                                       query.Expression, Expression.Quote(orderByExpression));

            // Construct the query based on order command
            return query.Provider.CreateQuery<TEntity>(resultExpression);

        }

        /// <summary>
        /// applying paging criteria on collection.
        /// To prevent applying this set pageingCriteria.PageNumber = -1
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TId"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageingCriteria"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> AppendFilterPagingCriteria<TEntity, TId>(this IQueryable<TEntity> query, FilterPagingCriteria pageingCriteria) where TEntity : IEntity<TId>
        {
            if (pageingCriteria != null)
            {
                // If paging enabled
                if (pageingCriteria.PageNumber >= 0)
                {
                    int pageSize = pageingCriteria.PageSize > 0 ? pageingCriteria.PageSize : PageSize;

                    // Apply paging
                    query = query.Skip(pageingCriteria.PageNumber * pageingCriteria.PageSize).Take(pageSize);
                }
            }

            return query;
        }

        /// <summary>
        /// Append filter criteria to strong typed querable
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TId"></typeparam>
        /// <param name="query"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> AppendFilterCriteria<TEntity, TId>(this IQueryable<TEntity> query, Filtration filter) where TEntity : IEntity<TId>
        {
            // No search criteria
            if (filter == null)
            {
                // Return the orignal query
                return query;
            }

            // Append search criteria
            query = AppendFilterSearchCriteria<TEntity, TId>(query, filter.SearchCriteria);

            // Append sort criteria
            query = AppendFilterSortCriteria<TEntity, TId>(query, filter.SortCriteria);

            // Append paging criteria
            query = AppendFilterPagingCriteria<TEntity, TId>(query, filter.PageCriteria);

            // Return result query
            return query;
        }

        private static IQueryable<TEntity> AppendFilterSearchCriteria<TEntity, TId>(IQueryable<TEntity> query, List<FilterSearchCriteria> searchCriteriaCollection) where TEntity : IEntity<TId>
        {
            // If search criteria not exist
            if (searchCriteriaCollection == null)
            {
                // Return orignal query
                return query;
            }

            Expression<Func<TEntity, bool>> filterExpression;
            RelationalLogicalOperator relation;

            // For each criteria at search criteria collection
            if (searchCriteriaCollection.Count > 0)
            {
                filterExpression = GetExpression<TEntity>(searchCriteriaCollection[0]);

                relation = searchCriteriaCollection[0].RelationOperator;
                for (int i = 1; i < searchCriteriaCollection.Count; i++)
                {
                    Expression<Func<TEntity, bool>> expression = GetExpression<TEntity>(searchCriteriaCollection[i]);
                    if (expression != null)
                    {
                        if (relation == RelationalLogicalOperator.And)
                        {
                            filterExpression = filterExpression.And(expression);

                        }
                        else if (relation == RelationalLogicalOperator.Or)
                        {
                            filterExpression = filterExpression.Or(expression);
                        }
                    }
                    relation = searchCriteriaCollection[i].RelationOperator;
                }

                return query.Where(filterExpression);
            }

            // return query
            return query;
        }

        #region PrivateMethods
        /// <summary>
        /// Get filter expression to set in where method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="criteria"></param>
        /// <returns></returns>
        private static Expression<Func<TEntity, bool>> GetExpression<TEntity>(FilterSearchCriteria criteria)
        {
            // If no criteria, return thesame query
            if (criteria == null)
            {
                return null;
            }

            // Create expresiion param called "field"
            ParameterExpression field = Expression.Parameter(typeof(TEntity), "field");

            // Expression Reference
            Expression fieldExpression;

            Type fieldType = GetPropertyType<TEntity>(criteria.Field);

            switch (criteria.Operator)
            {
                case LogicalOperator.Equal:
                    {
                        // Construct equal's expression between field and searchKey  (field => field.property == SearchKey)
                        fieldExpression = Expression.Equal(CreateParamter(field, criteria.Field), Expression.Constant(GetValue(criteria.SearchKey, fieldType), fieldType));
                        break;
                    }
                case LogicalOperator.NotEqual:
                    {
                        fieldExpression = Expression.NotEqual(CreateParamter(field, criteria.Field), Expression.Constant(GetValue(criteria.SearchKey, fieldType), fieldType));
                        break;
                    }
                case LogicalOperator.GreaterThan:
                    {
                        fieldExpression = Expression.GreaterThan(CreateParamter(field, criteria.Field), Expression.Constant(GetValue(criteria.SearchKey, fieldType), fieldType));
                        break;
                    }
                case LogicalOperator.GreaterThanOrEqual:
                    {
                        fieldExpression = Expression.GreaterThanOrEqual(CreateParamter(field, criteria.Field), Expression.Constant(GetValue(criteria.SearchKey, fieldType), fieldType));
                        break;
                    }
                case LogicalOperator.LessThan:
                    {
                        fieldExpression = Expression.LessThan(CreateParamter(field, criteria.Field), Expression.Constant(GetValue(criteria.SearchKey, fieldType), fieldType));
                        break;
                    }
                case LogicalOperator.LessThanOrEqual:
                    {
                        fieldExpression = Expression.LessThanOrEqual(CreateParamter(field, criteria.Field), Expression.Constant(GetValue(criteria.SearchKey, fieldType), fieldType));
                        break;
                    }
                case LogicalOperator.Contains:
                    {
                        fieldType = typeof(string);

                        // Pointer to Contains Method
                        MethodInfo contains = fieldType.GetMethod("Contains");

                        // Create paramter of function like field.propertyName
                        Expression propertyExpression = CreateParamter(field, criteria.Field);

                        // Apply ToString() method to function Paramter (field.propertyName.ToString())
                        MethodCallExpression convertedExpression = Expression.Call(Expression.Convert(propertyExpression, typeof(object)), typeof(object).GetMethod("ToString"));

                        // Create constent of searchkey of type string
                        ConstantExpression someValue = Expression.Constant(criteria.SearchKey.Trim(), fieldType);

                        // Call expression (Like field => field.propertyName.ToString().contains(someValue))
                        fieldExpression = Expression.Call(convertedExpression, contains, someValue);

                        break;
                    }
                default:
                    throw new NotSupportedException("Logical operator is not supported");
            }

            // Create Where Expression
            Expression<Func<TEntity, bool>> conditionExpression = Expression.Lambda<Func<TEntity, bool>>(fieldExpression, field);

            // Return Query
            return conditionExpression;
        }

        /// <summary>
        /// Cast searchKey to it's certain type 
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="destinsionType"></param>
        /// <returns></returns>
        private static object GetValue(string searchKey, Type destinsionType)
        {
            return TypeDescriptor.GetConverter(destinsionType).ConvertFromInvariantString(searchKey);
        }

        /// <summary>
        /// Create Expresion body like field.propertyName
        /// </summary>
        /// <param name="param"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private static Expression CreateParamter(ParameterExpression param, string propertyName)
        {
            Expression body = param;
            foreach (var member in propertyName.Split('.'))
            {
                body = Expression.PropertyOrField(body, member);
            }

            return body;
        }

        /// <summary>
        /// Get property type to create constant with this type
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private static Type GetPropertyType<TEntity>(string propertyName)
        {
            string[] props = propertyName.Split('.');

            Type propertyType = typeof(TEntity);

            foreach (var prop in props)
            {
                PropertyInfo pi = propertyType.GetProperty(prop);
                propertyType = pi.PropertyType;
            }

            return propertyType;


        }

        #endregion
    }
}
