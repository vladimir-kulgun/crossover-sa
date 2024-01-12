using System.Web.Http;
using Journals.WebPortal.Controllers;
using Journals.WebPortal.Filters;

namespace Journals.WebPortal
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new ArgumentNullExceptionFilterAttribute());
            config.Filters.Add(new ValidateModelAttribute());
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
