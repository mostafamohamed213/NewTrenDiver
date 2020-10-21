using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Data.Contracts.Contracts
{
    public interface IWriteUnitOfWork : IDisposable
    {
        /// <summary>
        /// Refernce store internal context
        /// </summary>
        object Context { get; }
        /// <summary>
        /// Save changes on Context
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
