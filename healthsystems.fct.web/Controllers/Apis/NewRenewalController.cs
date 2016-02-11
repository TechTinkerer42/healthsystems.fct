using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Net.Http;
using healthsystems.fct.data;
using healthsystems.fct.common;
using healthsystems.fct.data.Common;

namespace healthsystems.fct.web
{
    public class NewRenewalController : ApiController
    {
		public HttpResponseMessage Get(int id)
		{
			using (var session = NHibernateHelper.CreateSessionFactory())
			{
				using (var transaction = session.BeginTransaction())
				{
					Renewal entity;

					if (id == 0)
					{
						entity = new Renewal();
					}
					else
					{
						// collect objects
						var data = new List<Renewal>(session.CreateCriteria(typeof(Renewal)).List<Renewal>());

						// collect single object
						entity = data.FirstOrDefault(x => x.Id == id);
					}

					// trim excess
					if (entity != null)
					{
						entity.Transactions.Clear();
						entity.RenewalType = null;
					}

					// return as HttpResponseMessage
					return WebApiHelper.ObjectToHttpResponseMessage(entity);
				}
			}
		}
    }
}
