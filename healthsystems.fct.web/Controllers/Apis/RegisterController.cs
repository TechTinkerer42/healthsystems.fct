using System.Collections.Generic;
using System.Web.Http;

namespace healthsystems.fct.web
{
    public class RegisterController : ApiController
    {
        // GET api/register
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/register/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/register
        public RegistrationResponse Post([FromBody]RegistrationRequest request)
        {
            var response = new RegistrationResponse
            {
                StatusCode = "2",
                Message = "You've been acquired!"
            };

            return response;
        }

        // PUT api/register/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/register/5
        public void Delete(int id)
        {
        }
    }

    public class RegistrationRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class RegistrationResponse
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }
    }
}
