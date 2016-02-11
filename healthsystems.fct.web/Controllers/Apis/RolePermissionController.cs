using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using FluentNHibernate.Conventions.Helpers;
using healthsystems.fct.data;
using healthsystems.fct.common;
using healthsystems.fct.data.Common;
using NHibernate.Criterion;
using WebGrease.Css.Extensions;

namespace healthsystems.fct.web
{
	public class RolePermissionController : ApiController
    {
        public HttpResponseMessage Get()
        {
            using (var session = NHibernateHelper.CreateSessionFactory())
            {
                using (var transaction = session.BeginTransaction())
                {
                    // collect objects
					var data = new List<RolePermission>(session.CreateCriteria(typeof(RolePermission)).List<RolePermission>());

                    data.ForEach(x =>
                    {
							if (x.Role != null) x.Role.Users = null;
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
					RolePermission entity;

                    if (id == 0)
                    {
						entity = new RolePermission();
                    }
                    else
                    {
                        // collect objects
						var data = new List<RolePermission>(session.CreateCriteria(typeof(RolePermission)).List<RolePermission>());

                        // collect single object
                        entity = data.FirstOrDefault(x => x.Id == id);
                    }

                    // trim excess
                    if (entity != null)
                    {
						if (entity.Role != null) entity.Role.Users = null;
                    }

                    // return as HttpResponseMessage
                    return WebApiHelper.ObjectToHttpResponseMessage(entity);
                }
            }
        }

        public HttpResponseMessage Post(IEnumerable<RolePermission> entity)
        {
            using (var session = NHibernateHelper.CreateSessionFactory())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var queryString = Request
                        .GetQueryNameValuePairs()
                        .ToDictionary(x => x.Key, x => x.Value);

                    var roleId = Convert.ToInt32(queryString["roleId"]);

                    var existingData = session.CreateCriteria(typeof (RolePermission)).List<RolePermission>();
                    var filteredData = existingData.Where(x => x.Role.Id.Equals(roleId));

                    foreach (var n in filteredData)
                    {
                        RolePermission n1 = n;
                        var contains = entity.Where(x => x.Permission.Id == n1.Permission.Id);
                        if (!contains.Any())
                        {
                            session.Delete(n1);
                        }
                    }

                    if (entity != null)
                    {
                         foreach (var n in entity)
                        {
                            RolePermission n1 = n;
                            var contains = filteredData.Where(x => x.Permission.Id == n1.Permission.Id);
                            if (!contains.Any())
                            {
                                session.SaveOrUpdate(n1);
                            }
                        }                       
                    }

                    transaction.Commit();                        
                    

                    // return as HttpResponseMessage
                    return WebApiHelper.ObjectToHttpResponseMessage(entity);
                }
            }
        }
    }
}
