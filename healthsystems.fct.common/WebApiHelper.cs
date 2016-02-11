using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace healthsystems.fct.common
{
    public class WebApiHelper
    {
		public static HttpResponseMessage StringToHttpResponseMessage(string jsonString, System.Net.HttpStatusCode statusCode)
        {
            var content = new StringContent(jsonString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			var response = new HttpResponseMessage { Content = content, StatusCode = statusCode };

            return response;
        }

        public static string ObjectToJsonString(object obj)
        {
            return JsonConvert.SerializeObject(
                obj,
                Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ContractResolver = new NHibernateExtensions.NHibernateContractResolver()
                }
                );
        }

		public static HttpResponseMessage ObjectToHttpResponseMessage(object obj, System.Net.HttpStatusCode statusCode = System.Net.HttpStatusCode.OK)
        {
            var objString = ObjectToJsonString(obj);
			return StringToHttpResponseMessage(objString, statusCode);
        }
    }
}
