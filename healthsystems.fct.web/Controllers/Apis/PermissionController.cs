using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using healthsystems.fct.data;
using healthsystems.fct.common;
using healthsystems.fct.data.Common;

namespace healthsystems.fct.web
{
    public class PermissionController : ApiController
    {
        public HttpResponseMessage Get()
        {
            using (var session = NHibernateHelper.CreateSessionFactory())
            {
                using (var transaction = session.BeginTransaction())
                {
                    // collect objects
                    var data = new List<Permission>(session.CreateCriteria(typeof(Permission)).List<Permission>());

                    data.ForEach(x =>
                    {
                        //if (x.Registrations != null) x.Registrations = null;
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
                    Permission entity;

                    if (id == 0)
                    {
                        entity = new Permission();
                    }
                    else
                    {
                        // collect objects
                        var data = new List<Permission>(session.CreateCriteria(typeof(Permission)).List<Permission>());

                        // collect single object
                        entity = data.FirstOrDefault(x => x.Id == id);
                    }

                    // trim excess
                    if (entity != null)
                    {
                        //if (entity.Registrations != null) entity.Registrations = null;
                    }

                    // return as HttpResponseMessage
                    return WebApiHelper.ObjectToHttpResponseMessage(entity);
                }
            }
        }

        public HttpResponseMessage Post(Permission entity)
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
