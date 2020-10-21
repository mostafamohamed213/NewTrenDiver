using Layers.Base.Contracts;
using Layers.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Business.Contracts.Base
{
    /// <summary>
    /// Contract for all bussiness manager
    /// </summary>
    /// <typeparam name="TRead"></typeparam>
    /// <typeparam name="TWrite"></typeparam>
    /// <typeparam name="TId"></typeparam>
   public interface IManager<TRead,TWrite,TId> : IDisposable where TRead : class , IEntity<TId> ,IReadEntity 
                                                             where TWrite : class, IEntity<TId>, IWriteEntity
                                                             where TId : IEquatable<TId>

    {
        /// <summary>
        /// Get all items that match filtration
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        DescriptiveResponse<FilteredCollection<TRead>> FilterCollection(Filtration filter);

        /// <summary>
        /// Get item by it's identity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DescriptiveResponse<TRead> GetItem(TId id);

        /// <summary>
        /// Add or update an item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        DescriptiveResponse<TId> Save(TWrite item);
        
        /// <summary>
        /// Delete item by it's identity 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DescriptiveResponse<bool> Delete(TId id);
    }
}
