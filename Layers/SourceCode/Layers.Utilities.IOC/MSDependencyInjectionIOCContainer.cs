using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Utilities.IOC
{
    public class MSDependencyInjectionIOCContainer : IIOCContainer
    {
        #region Members 

        private IServiceProvider _serviceProvider;
        private static MSDependencyInjectionIOCContainer _instance;

        #endregion


        #region Properties

        public static MSDependencyInjectionIOCContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MSDependencyInjectionIOCContainer();
                }

                return _instance;
            }
        }

        #endregion


        #region Ctor
        private MSDependencyInjectionIOCContainer()
        {

        }

        #endregion

        #region IIOCContainer
        public object InternalContainer
        {
            get
            {
                return _serviceProvider;
            }
        }

        public void Configure(object contextInfo = null)
        {
            if (contextInfo == null)
            {
                throw new NullReferenceException("ContextInfo can not be null!!");
            }

            // Cast contextInfo to IServiceCollection
            IServiceCollection serviceCollection = contextInfo as IServiceCollection;

            if (serviceCollection == null)
            {
                throw new Exception("Context info should be of type IserviceCollection!");
            }

            //Build Service Provider
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public TEntity ResolveType<TEntity>()
        {
            // If service provider is null
            if (_serviceProvider == null)
            {
                throw new Exception("Service provider is null!. CALL configure method before use the container!");
            }

            //Resolve Type
            return _serviceProvider.GetService<TEntity>();
        }

        public TEntity ResolveType<TEntity>(string entityName)
        {
            // If service provider is null
            if (_serviceProvider == null)
            {
                throw new Exception("Service provider is null!. CALL configure method before use the container!");
            }

            //Resolve Type
            return _serviceProvider.GetService<TEntity>();
        }

        #endregion
    }
}
