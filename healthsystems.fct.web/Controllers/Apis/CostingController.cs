using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using healthsystems.fct.data;
using healthsystems.fct.common;
using healthsystems.fct.data.Common;

namespace healthsystems.fct.web
{
    public class CostingController : ApiController
    {
        public HttpResponseMessage Get()
        {
            using (var session = NHibernateHelper.CreateSessionFactory())
            {
                using (var transaction = session.BeginTransaction())
                {
                    // collect objects
                    var data = new List<Costing>(session.CreateCriteria(typeof(Costing)).List<Costing>());

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
                    Costing entity;

                    if (id == 0)
                    {
                        entity = new Costing();
                    }
                    else
                    {
                        // collect objects
                        var data = new List<Costing>(session.CreateCriteria(typeof(Costing)).List<Costing>());

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

        public HttpResponseMessage Post(Costing entity)
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
