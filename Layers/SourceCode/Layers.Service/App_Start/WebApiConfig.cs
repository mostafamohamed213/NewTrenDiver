using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Configuration;
using System.Web.Http.Cors;
using System.Web.ModelBinding;
using Layers.Service.Filters;
using System.Web.Http.Routing;
using System.Web.Http.Controllers;
using Layers.Utilities.IOC;
using Layers.Service.App_Start;
using Unity;

namespace Layers.Service
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Get allowed O=origins
            string allowOrigins = ConfigurationManager.AppSettings["allowOrigins"];

            // AllowOrigins exist 
            if (!string.IsNullOrEmpty(allowOrigins))
            {
                // Enable Cors attribute
                var cors = new EnableCorsAttribute(allowOrigins, "*", "*");

                // Enable Cors
                config.EnableCors(cors);
            }

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.3.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            ModelValidatorProviders.Providers.Clear();
            ModelValidatorProviders.Providers.Add(new DataAnnotationsModelValidatorProvider());
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;

            config.Filters.Add(new ValidateModelAttribute());

            // Configure IOCContainer
            IOCConfigurationManager.Instance.Configure();

            // Dependency Resolver
            config.DependencyResolver = new UnityResolver((IUnityContainer)IOCConfigurationManager.Instance.ProviderContainer);

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Web API routes
            config.MapHttpAttributeRoutes(new CustomDirectRouteProvider());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }

        public class CustomDirectRouteProvider : DefaultDirectRouteProvider
        {
            protected override IReadOnlyList<IDirectRouteFactory> GetActionRouteFactories(HttpActionDescriptor actionDescriptor)
            {
                return actionDescriptor.GetCustomAttributes<IDirectRouteFactory>(true);
            }
        }
    }
}
