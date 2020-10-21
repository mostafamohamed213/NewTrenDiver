using Layers.Base.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Data.Contracts.Contracts
{
    /// <summary>
    /// Repository responsible for write operations
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public interface IWriteRepository<TEntity, TId> : IDisposable where TEntity : class, IEntity<TId>, IWriteEntity
                                                     where TId : IEquatable<TId>
    {
        /// <summary>
        /// Get UnitOfWork to save changes
        /// </summary>
        IWriteUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Get image from TEntity in order to update it. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="include1"></param>
        /// <param name="include2"></param>
        /// <returns></returns>
        TEntity GetImage(TId id, Expression<Func<TEntity, object>> include1 = null, Expression<Func<TEntity, object>> include2 = null);

        /// <summary>
        /// Get image from TEntity in order to update it.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="include1"></param>
        /// <param name="include2"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetImage(Expression<Func<TEntity, bool>> query, Expression<Func<TEntity, object>> include1 = null, Expression<Func<TEntity, object>> include2 = null);

        /// <summary>
        /// Attach entity to current context
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="enableHardDelete"></param>
        void Attach(TEntity entity, bool enableHardDelete = false);

        /// <summary>
        /// Delete entity using it's identifer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(TId id);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Delete(TEntity entity);

        /// <summary>
        /// Deattche all entites from current Context
        /// </summary>
        void DeattachAll();

        /// <summary>
        /// Deattach entity from current context
        /// </summary>
        /// <param name="entity"></param>
        void Deattach(TEntity entity);
    }
}
