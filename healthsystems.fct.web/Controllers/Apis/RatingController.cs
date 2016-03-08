using healthsystems.fct.common;
using healthsystems.fct.data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace healthsystems.fct.web
{
    public class RatingController : ApiController
    {
        public HttpResponseMessage Get()
        {
            using (var session = NHibernateHelper.CreateSessionFactory())
            {
                using (var transaction = session.BeginTransaction())
                {
                    // collect objects
                    var sqlPath = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/sql/rating.txt");
                    var sql = System.IO.File.ReadAllText(sqlPath);

                    var data =
                        from x in session.CreateSQLQuery(sql).DynamicList()
                        select new
                        {
                            x.RegistrationId,
                            x.EstablishmentName,
                            x.NumberOfQuestions,
                            x.TotalOfRatings
                        };

                    // return as HttpResponseMessage
                    return WebApiHelper.ObjectToHttpResponseMessage(data);
                }
            }
        }
    }
}
