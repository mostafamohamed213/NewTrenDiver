using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Utilities.IOC
{
    public class IOCConfigurationManager
    {

        #region Members

        private static IOCConfigurationManager _instance;
        private static object locker = new object();

        #endregion

        #region Properties

        public static IOCConfigurationManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (locker)
                    {

                        if (_instance == null)
                        {
                            _instance = new IOCConfigurationManager();
                        }
                    }
                }

                return _instance;
            }
        }

        public IOCContainerType ContanierType
        {
            get
            {
                string type = ConfigurationManager.AppSettings["ContainerType"];
                if (type == IOCContainerType.Unity.ToString())
                {
                    return IOCContainerType.Unity;
                }
                else
                    return IOCContainerType.MS_DependencyInjection;
            }
        }

        public IIOCContainer IOCContainer
        {
            get
            {
                switch (ContanierType)
                {
                    case IOCContainerType.Unity:
                        return UnityIOCContainer.Instance;
                    case IOCContainerType.MS_DependencyInjection:
                        return MSDependencyInjectionIOCContainer.Instance;
                    default:
                        throw new Exception("UnSupported Container.");
                }
            }
        }

        public object ProviderContainer
        {
            get
            {
                return IOCContainer.InternalContainer;
            }
        }

        #endregion

        #region Ctor
        private IOCConfigurationManager()
        {

        }

        #endregion

        #region PublicMethods

        public void Configure(object contextInfo = null)
        {
            IOCContainer.Configure(contextInfo);
        }

        public TEntity ResolveType<TEntity>()
        {
            return IOCContainer.ResolveType<TEntity>();
        }
        public TEntity ResolveType<TEntity>(string name)
        {
            return IOCContainer.ResolveType<TEntity>(name);
        }

        #endregion

    }


}
