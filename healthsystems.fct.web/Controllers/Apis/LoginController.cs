using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using healthsystems.fct.common;
using healthsystems.fct.data;
using healthsystems.fct.data.Common;
using NLog;

namespace healthsystems.fct.web.Controllers.Apis
{
    public class LoginController : ApiController
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]LoginRequest loginRequest)
        {
            LoginResponse loginResponse = null;
            var statusCode = System.Net.HttpStatusCode.OK;

            try
            {
                _logger.Info("Starting Api/Login POST");
                
                using (var session = NHibernateHelper.CreateSessionFactory())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        _logger.Info("Inside session and transaction");

                        // does this user exist? (db-call)
                        _logger.Info("About to select user from database");
                        var users = session.CreateCriteria(typeof (User)).List<User>();
                        var user = users.FirstOrDefault(x => x.Username == loginRequest.Username);

                        _logger.Info("Selected user from users table");

                        if (user != null)
                        {
                            _logger.Info("User does exist");

                            // if yes, validate him (db-call)
                            var isValid = user.Password.Equals(loginRequest.Password);

                            // is it correct?
                            if (isValid)
                            {
                                _logger.Info("Password is correct");

                                // create a hash
                                var token = JwtHelper.Encode(user.Id, user.Username, user.State.Id);

                                _logger.Info("Generated token: " + token);

                                // return success w/ hash to user
                                loginResponse = new LoginResponse
                                {
                                    StatusCode = 3,
                                    Reason = "Login success",
                                    Username = loginRequest.Username,
                                    Token = token
                                };
                            }
                            else
                            {
                                _logger.Info("User does not exist");

                                // no, tell him its wrong password
                                loginResponse = new LoginResponse
                                {
                                    StatusCode = 1,
                                    Reason = "The password is incorrect. Please try again",
                                    Username = loginRequest.Username
                                };
                            }
                        }
                        else
                        {
                            // if no
                            // tell him this user doesnt exist
                            loginResponse = new LoginResponse
                            {
                                StatusCode = 2,
                                Reason =
                                    "This username is not on the system. Please try again, or register if you don't have an account",
                                Username = loginRequest.Username
                            };

                        }

                    }
                } //end using session

                if (loginResponse.StatusCode != 3)
                {
                    statusCode = System.Net.HttpStatusCode.Unauthorized;
                }

                _logger.Info("About to return login response");

                return WebApiHelper.ObjectToHttpResponseMessage(loginResponse, statusCode);
                
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            _logger.Warn("Api/Login returned null");
            return WebApiHelper.ObjectToHttpResponseMessage(loginResponse, statusCode);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        public static string Md5(string itemToHash = "")
        {
            var guid = new Guid();
            itemToHash = string.IsNullOrEmpty(itemToHash) ? guid.ToString() : "";
            return string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(itemToHash)).Select(s => s.ToString("x2")));
        }

    }
}