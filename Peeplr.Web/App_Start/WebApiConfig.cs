using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Peeplr.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            config.Routes.MapHttpRoute(name: "Contacts_GetAll", routeTemplate: "api/contacts", defaults: new { controller = "ContactsApi", action = "GetAll" });
        }
    }
}
