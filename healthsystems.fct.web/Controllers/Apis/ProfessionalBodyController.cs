using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Net.Http;
using healthsystems.fct.data.Common;
using healthsystems.fct.data;
using healthsystems.fct.common;

namespace healthsystems.fct.web.Controllers
{
    public class ProfessionalBodyController : ApiController
    {
		public HttpResponseMessage Get()
		{
			using (var session = NHibernateHelper.CreateSessionFactory())
			{
				using (var transaction = session.BeginTransaction())
				{
					// collect objects
					var data = new List<ProfessionalBody>(session.CreateCriteria(typeof(ProfessionalBody)).List<ProfessionalBody>());

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
					ProfessionalBody entity;

					if (id == 0)
					{
						entity = new ProfessionalBody();
					}
					else
					{
						// collect objects
						var data = new List<ProfessionalBody>(session.CreateCriteria(typeof(ProfessionalBody)).List<ProfessionalBody>());

						// collect single object
						entity = data.FirstOrDefault(x => x.Id == id);
					}

					// trim excess
					if (entity != null)
					{
						//if (entity.Costings!= null) entity.Costings = null;
					}

					// return as HttpResponseMessage
					return WebApiHelper.ObjectToHttpResponseMessage(entity);
				}
			}
		}

		public HttpResponseMessage Post(ProfessionalBody entity)
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
