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
using NHibernate.Util;

namespace healthsystems.fct.web.Controllers
{
    public class TypeOfEstablishmentController : ApiController
    {
		public HttpResponseMessage Get()
		{
			using (var session = NHibernateHelper.CreateSessionFactory())
			{
				using (var transaction = session.BeginTransaction())
				{
					// collect objects
					var data = new List<TypeOfEstablishment>(session.CreateCriteria(typeof(TypeOfEstablishment)).List<TypeOfEstablishment>());

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
					TypeOfEstablishment entity;

					if (id == 0)
					{
						entity = new TypeOfEstablishment();
					}
					else
					{
						// collect objects
						var data = new List<TypeOfEstablishment>(session.CreateCriteria(typeof(TypeOfEstablishment)).List<TypeOfEstablishment>());

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

		public HttpResponseMessage Post(TypeOfEstablishment entity)
		{
			using (var session = NHibernateHelper.CreateSessionFactory())
			{
				using (var transaction = session.BeginTransaction())
				{
				    TypeOfEstablishment typeOfEstablishment;

				    if (entity.Id == 0)
				    {
                        typeOfEstablishment = new TypeOfEstablishment();
				    }
				    else
				    {
				        var typeOfEstablishments = new List<TypeOfEstablishment>(session.CreateCriteria(typeof(TypeOfEstablishment)).List<TypeOfEstablishment>());
				        typeOfEstablishment = typeOfEstablishments.First(x => x.Id == entity.Id);
				    }

				    typeOfEstablishment.Name = entity.Name;
				    typeOfEstablishment.Description = entity.Description;
				    typeOfEstablishment.Staffings = entity.Staffings;

                    session.SaveOrUpdate(typeOfEstablishment);
					transaction.Commit();

					// return as HttpResponseMessage
					return WebApiHelper.ObjectToHttpResponseMessage(entity);
				}
			}
		}
    }
}
