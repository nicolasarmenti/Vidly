using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace Vidly {
    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {
			var serializerSettings = config.Formatters.JsonFormatter.SerializerSettings;
			serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			serializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

			config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}