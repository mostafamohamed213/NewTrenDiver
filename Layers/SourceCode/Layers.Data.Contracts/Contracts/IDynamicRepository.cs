using Layers.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Data.Contracts.Contracts
{
    public interface IDynamicRepository
    {

        /// <summary>
        /// Exectue sql query and return list of TReturnType
        /// </summary>
        /// <typeparam name="TReturnType"></typeparam>
        /// <param name="query"></param>
        /// <param name="paramters"></param>
        /// <returns></returns>
        List<TReturnType> ExecuteQuery<TReturnType>(string query, Dictionary<string, object> paramters = null);

        /// <summary>
        /// Exectue sql query and return list of object
        /// </summary>
        /// <param name="returnType"></param>
        /// <param name="query"></param>
        /// <param name="paramters"></param>
        /// <returns></returns>
        List<object> ExecuteQuery(Type returnType,string query, Dictionary<string, object> paramters = null);

        /// <summary>
        /// Check if the column is existing at object name.
        /// </summary>
        /// <param name="objectName"></param>
        /// <returns></returns>
        List<DbColumn> GetColumns(string objectName);

        /// <summary>
        /// Execute SQL Command
        /// </summary>
        /// <param name="query"></param>
        /// <param name="paramters"></param>
        /// <returns></returns>
        int ExecuteCommand(string query, Dictionary<string, object> paramters = null);



    }
}
