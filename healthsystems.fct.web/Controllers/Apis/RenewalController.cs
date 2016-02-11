using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Newtonsoft.Json;
using healthsystems.fct.data;
using healthsystems.fct.common;
using healthsystems.fct.data.Common;

namespace healthsystems.fct.web
{
    public class RenewalController : ApiController
    {
        readonly string _sqlPath = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/sql/renewals.txt");

        public HttpResponseMessage Get()
        {
            using (var session = NHibernateHelper.CreateSessionFactory())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var sql = System.IO.File.ReadAllText(_sqlPath);

                    var payments =
                        from x in session.CreateSQLQuery(sql).DynamicList()
                        select new
                        {
                            x.RenewalId,
                            x.CacNumber,
                            x.EstablishmentName,
                            x.Date,
                            x.RenewalTypeId,
                            x.AmountDue,
                            x.TotalPaid,
                            x.Balance
                        };

                    var stringResult = JsonConvert.SerializeObject(payments);
                    return StringToJsonActionResult(stringResult);

                }
            }

        }

        public HttpResponseMessage Get(int id)
        {
            using (var session = NHibernateHelper.CreateSessionFactory())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var sql = System.IO.File.ReadAllText(_sqlPath);

                    var payments =
                        from x in session.CreateSQLQuery(sql).DynamicList()
                        where x.RenewalId.Equals(id)
                        select new
                        {
                            x.RenewalId,
                            x.CacNumber,
                            x.EstablishmentName,
                            x.Date,
                            x.RenewalTypeId,
                            x.AmountDue,
                            x.TotalPaid,
                            x.Balance
                        };

                    var stringResult = JsonConvert.SerializeObject(payments);
                    return StringToJsonActionResult(stringResult);
                }
            }
        }

        HttpResponseMessage StringToJsonActionResult(string jsonString)
        {
            var content = new StringContent(jsonString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = new HttpResponseMessage { Content = content };

            return response;
        }

        public void Post([FromBody]Renewal renewal)
        {

        }
    }
}
