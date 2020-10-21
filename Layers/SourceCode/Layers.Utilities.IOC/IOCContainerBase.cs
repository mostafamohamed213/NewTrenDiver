using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Utilities.IOC
{
    public interface IIOCContainer
    {
        /// <summary>
        /// Return provider's container
        /// </summary>
        object InternalContainer { get; }

        /// <summary>
        /// Configure IOC Container
        /// </summary>
        /// <param name="contextInfo"></param>
        void Configure(object contextInfo = null);

        /// <summary>
        /// Resolve Type of type TEntity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        TEntity ResolveType<TEntity>();

        /// <summary>
        /// Resolve Type of type TEntity by provide entity name
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entityName"></param>
        /// <returns></returns>
        TEntity ResolveType<TEntity>(string entityName);


    }
}
