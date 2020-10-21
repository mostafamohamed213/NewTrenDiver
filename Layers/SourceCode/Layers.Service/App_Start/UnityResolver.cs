using Layers.Utilities.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Unity;
using Unity.Exceptions;

namespace Layers.Service.App_Start
{
    public class UnityResolver : IDependencyResolver
    {
        #region Members

        protected IUnityContainer container;

        #endregion

        #region Ctor
        public UnityResolver(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("Unity container can not be null!");
            }
            this.container = container;
        }

        #endregion

        #region IDependencyResolver
        public IDependencyScope BeginScope()
        {
            return new UnityResolver(container.CreateChildContainer());
        }

        public void Dispose()
        {
            container.Dispose();
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException ex)
            {
                Logger.Log(ex);
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException ex)
            {
                Logger.Log(ex);
                return null;
            }
        }

        #endregion
    }
}