using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using healthsystems.fct.common;
using healthsystems.fct.data;
using healthsystems.fct.data.Common;
using System.Web.Mvc;

namespace healthsystems.fct.web.Controllers
{
    public class TrainingController : Controller
    {
        public ActionResult Index()
        {

			using (var s = NHibernateHelper.CreateSessionFactory())
			{
				using (var t = s.BeginTransaction())
				{
					var r1 = s.CreateCriteria(typeof(Registration)).List<Registration>();
					var r = r1.FirstOrDefault(x => x.Id >= 1);

					return View (r);
				}


			}

			
            
        }
    }
}
