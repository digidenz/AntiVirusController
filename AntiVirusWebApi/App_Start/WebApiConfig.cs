using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AntiVirusWebApi
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services

			// Web API routes
			config.MapHttpAttributeRoutes();
			config.Formatters.JsonFormatter.SerializerSettings = CreateJsonSerializerSettings();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}

		/// <summary>
		///		Create an instance of the JSON serialiser settings.
		/// </summary>
		/// <returns>
		///		The JSON serialiser settings.
		/// </returns>
		static JsonSerializerSettings CreateJsonSerializerSettings()
		{
			return new JsonSerializerSettings
			{
				Converters =
				{
					new StringEnumConverter()
				},
				DateFormatHandling = DateFormatHandling.IsoDateFormat,
				DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind,
				ObjectCreationHandling = ObjectCreationHandling.Auto,
				DefaultValueHandling = DefaultValueHandling.Include,
				NullValueHandling = NullValueHandling.Include,
				TypeNameHandling = TypeNameHandling.Auto,
			};
		}
	}
}
