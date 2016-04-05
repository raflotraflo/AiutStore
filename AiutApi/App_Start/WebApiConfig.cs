using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;

namespace Test.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
               name: "AiutApi",
               routeTemplate: "api/{controller}/{id}",
               defaults: new { id = RouteParameter.Optional }
           );

            SetupXml();
            SetupJsonNet(config);
        }

        private static void SetupXml()
        {
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.MediaTypeMappings.Add(
                new QueryStringMapping("format", "xml", "text/xml"));
        }

        private static void SetupJsonNet(HttpConfiguration configuration)
        {
            var json = configuration.Formatters.JsonFormatter;
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            };

            settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            json.SerializerSettings = settings;
        }
    }
}