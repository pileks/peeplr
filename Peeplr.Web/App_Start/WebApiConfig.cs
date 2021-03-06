﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Peeplr.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(name: "Tags_GetAll", routeTemplate: "api/tags", defaults: new { controller = "TagsApi", action = "GetAll" });

            config.Routes.MapHttpRoute(name: "Contacts_GetAll", routeTemplate: "api/contacts", defaults: new { controller = "ContactsApi", action = "GetAll" });
            config.Routes.MapHttpRoute(name: "Contacts_Get_forQuery", routeTemplate: "api/contacts/search", defaults: new { controller = "ContactsApi", action = "Get_forQuery" });
            
            config.Routes.MapHttpRoute(name: "Contacts_Create", routeTemplate: "api/contacts/create", defaults: new { controller = "ContactsApi", action = "Create" });
            config.Routes.MapHttpRoute(name: "Contacts_Update", routeTemplate: "api/contacts/update/{id}", defaults: new { controller = "ContactsApi", action = "Update" });
            config.Routes.MapHttpRoute(name: "Contacts_Delete", routeTemplate: "api/contacts/delete/{id}", defaults: new { controller = "ContactsApi", action = "Delete" });

            config.Routes.MapHttpRoute(name: "Contacts_GetSingle", routeTemplate: "api/contacts/{id}", defaults: new { controller = "ContactsApi", action = "GetSingle" });
        }
    }
}
