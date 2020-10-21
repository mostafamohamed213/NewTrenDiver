using Layers.Base.Contracts;
using Layers.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Data.Contracts.Contracts
{
    /// <summary>
    /// Repository responsible for read operations
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public interface IReadRepository<TEntity, TId> : IDisposable where TEntity : class, IEntity<TId>, IReadEntity
                                                    where TId : IEquatable<TId>
    {
        /// <summary>
        /// Get all items
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Get all items for specific paging criteria 
        /// </summary>
        /// <param name="pageCriteria"></param>
        /// <param name="sortCriteria"></param>
        /// <returns></returns>
        FilteredCollection<TEntity> GetAll(FilterPagingCriteria pageCriteria, FilterSortCriteria sortCriteria = null);

        /// <summary>
        /// Get all items that match query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        /// 
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> query);

        /// <summary>
        /// Get all Items after appling filters
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        FilteredCollection<TEntity> Find(Filtration filter);

        /// <summary>
        /// Get all items that match query and filters
        /// </summary>
        /// <param name="query"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        FilteredCollection<TEntity> Find(Expression<Func<TEntity, bool>> query, Filtration filter);

        /// <summary>
        /// Get single entity using identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetSingleOrDefault(TId id);

        /// <summary>
        /// Get single entity that match query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        TEntity GetSingleOrDefault(Expression<Func<TEntity, bool>> query);

        /// <summary>
        /// Get single entity that match filters
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        TEntity GetSingleOrDefault(Filtration filter);

        /// <summary>
        /// Get single entity that match query and filters
        /// </summary>
        /// <param name="query"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        TEntity GetSingleOrDefault(Expression<Func<TEntity, bool>> query, Filtration filter);

        /// <summary>
        /// Count number of entites 
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Count based on filters
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        int Count(Filtration filter);

        /// <summary>
        /// Count number of entites that match query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> query);

        /// <summary>
        /// Count number of entites that match query and filters 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> query, Filtration filter);

        /// <summary>
        /// Check existence of entity
        /// </summary>
        /// <returns></returns>
        bool Any();

        /// <summary>
        /// check existence of entity based on filters
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        bool Any(Filtration filter);


        /// <summary>
        /// check existence of entity based on  query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        bool Any(Expression<Func<TEntity, bool>> query);

        /// <summary>
        /// check existence of entity based on query and filter
        /// </summary>
        /// <param name="query"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        bool Any(Expression<Func<TEntity, bool>> query, Filtration filter);

    }
}
