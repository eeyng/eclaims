using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EClaim
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var jsonSetting = new JsonSerializerSettings();
            jsonSetting.Converters.Add(new StringEnumConverter());
            config.Formatters.JsonFormatter.SerializerSettings = jsonSetting;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "api",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { controller = "Login", id = RouteParameter.Optional }
            );
        }
    }
}