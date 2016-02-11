using System.Web.Http;

namespace healthsystems.fct.web
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "Api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

		}
	}
}
