using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Data.Contracts.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Get Context
        /// </summary>
        DbContext Context { get; }

        /// <summary>
        /// save all changes in context
        /// </summary>
        /// <returns></returns>
        bool SaveChanges();
        /// <summary>
        /// Start new transaction
        /// </summary>
        void StartTransaction();

        /// <summary>
        /// Commit Changes
        /// </summary>
        void Commit();

        /// <summary>
        /// RollBack changes
        /// </summary>
        void RollBack();

    }
}
