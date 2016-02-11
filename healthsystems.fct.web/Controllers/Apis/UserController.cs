using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using NHibernate.Util;
using healthsystems.fct.data;
using healthsystems.fct.common;
using healthsystems.fct.data.Common;
using System;

namespace healthsystems.fct.web
{
    public class UserController : ApiController
    {
        public HttpResponseMessage Get()
        {
            using (var session = NHibernateHelper.CreateSessionFactory())
            {
                using (var transaction = session.BeginTransaction())
                {
                    // collect objects
                    var data = session.CreateCriteria(typeof(User)).List<User>();

                    data.ForEach(user =>
                    {
                        if (user.State != null) user.State.Locations = null;
                        if (user.Roles != null) user.Roles.ForEach(role => role.Users = null);
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
                    // collect objects
                    var dataList = new List<User>(session.CreateCriteria(typeof(User)).List<User>());
                    
                    // collect single object
                    var dataEntry = dataList.FirstOrDefault(x => x.Id == id);

                    // trim excess
                    if (dataEntry != null)
                    {
                        if (dataEntry.State != null) dataEntry.State.Locations = null;
                        if (dataEntry.Roles != null) dataEntry.Roles.ForEach(role => role.Users = null);
                    }

                    // return as HttpResponseMessage
                    return WebApiHelper.ObjectToHttpResponseMessage(dataEntry);
                    
                }
            }
        }

        // POST api/user
        public HttpResponseMessage Post([FromBody] User userRequest)
        {
            using (var session = NHibernateHelper.CreateSessionFactory())
            {
                using (var transaction = session.BeginTransaction())
                {
                    // select the record
                    var dataList = new List<User>(session.CreateCriteria(typeof (User)).List<User>());
					var user = dataList.FirstOrDefault (x => x.Username.ToLower ().Equals (userRequest.Username.ToLower ()));

					if (user != null) {
						if (user.Id != userRequest.Id) {
							var error = new ErrorResponse {
								Error = true,
								ErrorMessage = "This username already exist, please use another one"
							};
							return WebApiHelper.ObjectToHttpResponseMessage (error);
						}
					}

					if (user == null) {
						user = new User ();
						user.Created = DateTime.Now;
					}

                    user.Username = userRequest.Username;
                    user.Password = userRequest.Password;
                    user.FirstName = userRequest.FirstName;
                    user.LastName = userRequest.LastName;
                    user.EmailAddress = userRequest.EmailAddress;
                    user.Mobile = userRequest.Mobile;
                    user.State = userRequest.State;
                    user.Roles = userRequest.Roles;
					user.Active = userRequest.Active;
					user.Modified = DateTime.Now;

                    // save back to db
                    session.SaveOrUpdate(user);
                    transaction.Commit();

                    return WebApiHelper.ObjectToHttpResponseMessage(user);
                }
            }
        }

    } // end class
} // end namespace
