using Microsoft.Practices.Unity.Configuration;
using System;
using System.Configuration;
using Unity;

namespace Layers.Utilities.IOC
{
    public class UnityIOCContainer : IIOCContainer
    {
        #region Members

        private readonly UnityContainer _container = new UnityContainer();

        private static UnityIOCContainer _instance;

        #endregion


        #region Propreties

        public IUnityContainer Container
        {
            get
            {
                return _container;
            }
        }

        public static UnityIOCContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UnityIOCContainer();
                }

                return _instance;
            }
        }

        #endregion

        #region Ctor

        private UnityIOCContainer()
        {

        }

        #endregion

        #region IIOCContainer
        public object InternalContainer
        {
            get
            {
                return Container;
            }
        }

        public void Configure(object contextInfo = null)
        {
            UnityConfigurationSection configSection = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");

            configSection.Configure(_container, "unityConfig");
        }

        public TEntity ResolveType<TEntity>()
        {
            return Container.Resolve<TEntity>();
        }

        public TEntity ResolveType<TEntity>(string entityName)
        {
            return Container.Resolve<TEntity>(entityName);
        }
        #endregion
    }
}