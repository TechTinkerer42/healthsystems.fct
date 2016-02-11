using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using healthsystems.fct.data;
using healthsystems.fct.common;
using healthsystems.fct.data.Common;

namespace healthsystems.fct.web
{
    public class PermissionsOfRoleController : ApiController
    {
		public HttpResponseMessage Get(int id)
		{
			using (var session = NHibernateHelper.CreateSessionFactory())
			{
				using (var transaction = session.BeginTransaction())
				{
					List<RolePermission> entity;

					if (id == 0)
					{
						entity = new List<RolePermission>();
					}
					else
					{
						// collect objects
						var data = new List<RolePermission>(session.CreateCriteria(typeof(RolePermission)).List<RolePermission>());

						// collect single object
						entity = data.Where (x => x.Role.Id == id).ToList();
					}

					// trim excess
					entity.ForEach(x =>
						{
							if (x.Role != null) x.Role.Users = null;
						});

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
					foreach (var item in entity) {
						session.SaveOrUpdate(entity);
					}

					transaction.Commit();

					// return as HttpResponseMessage
					return WebApiHelper.ObjectToHttpResponseMessage(entity);
				}
			}
		}

    } // end class
} // end namespace
