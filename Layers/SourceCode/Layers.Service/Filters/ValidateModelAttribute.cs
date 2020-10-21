using Layers.Utilities.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Layers.Service.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            bool hasPrimtiveType = actionContext.ActionDescriptor.GetParameters()
                                                .Any(p => p.IsOptional && p.ParameterType.BaseType == typeof(ValueType));

            if (!hasPrimtiveType && !actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Model_Validation_Error"); //actionContext.ModelState

                string errors = JsonConvert.SerializeObject(actionContext.ModelState.Select(p => new
                {
                    key = p.Key,
                    errors = p.Value.Errors.Select(e => e.ErrorMessage),
                }).ToDictionary(kv => kv.key,kv =>kv.errors));

                Logger.Log($"Model Validation Error\nUrl:{actionContext.Request.GetRequestContext().RouteData.Route.RouteTemplate}\n{errors}");
            }
        }
    }
}