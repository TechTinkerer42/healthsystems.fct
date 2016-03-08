using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using healthsystems.fct.common;
using healthsystems.fct.data;
using healthsystems.fct.data.Common;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Linq;
using System.Net;
using Newtonsoft.Json;
using healthsystems.fct.data.DataClasses;

namespace healthsystems.fct.web.Controllers.Apis
{
    public class SurveyController : ApiController
    {
        public HttpResponseMessage Post([FromBody]JToken body)
        {
            using (var session = NHibernateHelper.CreateSessionFactory())
            {
                using (var transaction = session.BeginTransaction())
                {
                    // Survey
                    
                    var survey = new Survey();
                    survey.Name = body["Name"].ToObject<string>();
                    survey.Surname = body["Surname"].ToObject<string>();
                    survey.EmailAddress = body["EmailAddress"].ToObject<string>();
                    survey.MobileNumber = body["MobileNumber"].ToObject<string>();

                    var registrations = new List<Registration>(session.CreateCriteria(typeof(Registration)).List<Registration>());
                    var registration = registrations.FirstOrDefault(x => x.Id == body["RegistrationId"].ToObject<int>());
                    survey.Registration = registration;

                    session.SaveOrUpdate(survey);

                    // SurveyQuestion

                    // loop thru all the questions and ratings
                    var categories = body["Categories"].ToObject<List<SurveyCategory>>();
                    foreach(SurveyCategory c in categories){
                        foreach(Question q in c.Questions)
                        {
                            var surveyQuestion = new SurveyQuestion();
                            surveyQuestion.Survey = survey;
                            surveyQuestion.Registration = registration;
                            surveyQuestion.Question = q.Query;
                            surveyQuestion.Rating = q.Rating;
                            session.SaveOrUpdate(surveyQuestion);
                        }
                    }
                    
                    transaction.Commit();

                    // return as HttpResponseMessage
                    return WebApiHelper.ObjectToHttpResponseMessage(survey);
                }
            }
        }
    }
}