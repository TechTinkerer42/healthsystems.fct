using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using healthsystems.fct.data;
using healthsystems.fct.common;
using healthsystems.fct.data.Common;

namespace healthsystems.fct.web
{
    public class StaffingController : ApiController
    {
        public HttpResponseMessage Get()
        {
            using (var session = NHibernateHelper.CreateSessionFactory())
            {
                using (var transaction = session.BeginTransaction())
                {
                    // collect objects
                    var data = new List<Staffing>(session.CreateCriteria(typeof(Staffing)).List<Staffing>());

                    data.ForEach(x =>
                    {
                        
                    });

                    // return as HttpResponseMessage
                    return WebApiHelper.ObjectToHttpResponseMessage(data);
                }
            }
        }

        public HttpResponseMessage Get(int id)
        {
            using (var session = NHibernateHelper.CreateSessionFactory())
            {
                using (var transaction = session.BeginTransaction())
                {
                    Staffing entity;

                    if (id == 0)
                    {
                        entity = new Staffing();
                    }
                    else
                    {
                        // collect objects
                        var data = new List<Staffing>(session.CreateCriteria(typeof(Staffing)).List<Staffing>());

                        // collect single object
                        entity = data.FirstOrDefault(x => x.Id == id);
                    }

                    // trim excess
                    if (entity != null)
                    {
                        
                    }

                    // return as HttpResponseMessage
                    return WebApiHelper.ObjectToHttpResponseMessage(entity);
                }
            }
        }

        public HttpResponseMessage Post(Staffing entity)
        {
            using (var session = NHibernateHelper.CreateSessionFactory())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(entity);
                    transaction.Commit();

                    // return as HttpResponseMessage
                    return WebApiHelper.ObjectToHttpResponseMessage(entity);
                }
            }
        }
    }
}
