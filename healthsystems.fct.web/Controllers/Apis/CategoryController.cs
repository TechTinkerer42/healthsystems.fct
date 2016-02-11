using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using healthsystems.fct.data;
using healthsystems.fct.common;
using healthsystems.fct.data.Common;

namespace healthsystems.fct.web
{
    public class CategoryController : ApiController
    {
        public HttpResponseMessage Get()
        {
            using (var session = NHibernateHelper.CreateSessionFactory())
            {
                using (var transaction = session.BeginTransaction())
                {
                    // collect objects
                    var data = new List<Category>(session.CreateCriteria(typeof(Category)).List<Category>());

                    data.ForEach(x =>
                    {
                        if (x.Costings != null) x.Costings = null;
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
                    Category entity;

                    if (id == 0)
                    {
                        entity = new Category();
                    }
                    else
                    {
                        // collect objects
                        var data = new List<Category>(session.CreateCriteria(typeof(Category)).List<Category>());

                        // collect single object
                        entity = data.FirstOrDefault(x => x.Id == id);
                    }

                    // trim excess
                    if (entity != null)
                    {
                        if (entity.Costings!= null) entity.Costings = null;
                    }

                    // return as HttpResponseMessage
                    return WebApiHelper.ObjectToHttpResponseMessage(entity);
                }
            }
        }

        public HttpResponseMessage Post(Category entity)
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
