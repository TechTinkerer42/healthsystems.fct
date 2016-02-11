using System.Net.Http;
using System.Web.Http;
using healthsystems.fct.common;
using healthsystems.fct.data;
using healthsystems.fct.data.Common;

namespace healthsystems.fct.web
{
    public class RegistrationServiceController : ApiController
    {
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage();
        }

        public HttpResponseMessage Get(int id)
        {

            using (var session = NHibernateHelper.CreateSessionFactory())
            {
                using (var transaction = session.BeginTransaction())
                {
                    /*
                    var registrationList = new List<Registration>(session.CreateCriteria(typeof(Registration)).List<Registration>());

                    var registration = registrationList.FirstOrDefault(x => x.Id == id);

                    if (registration != null)
                    {
                        var registrationServices = registration.Services;

                        var rowData =
                            from x in registrationServices
                            select new
                            {
                                serviceName = x. Description
                            };
                    }
                    else
                    {
                        
                    }

                    */

                    return WebApiHelper.ObjectToHttpResponseMessage("");
                }
            }
        }

    }
}
