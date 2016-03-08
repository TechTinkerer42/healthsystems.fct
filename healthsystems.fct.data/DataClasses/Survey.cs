using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace healthsystems.fct.data.DataClasses
{
    [Serializable]
    [DataContract]
    public class Survey
    {
        [DataMember]
        public virtual int Id { get; protected set; }
        [DataMember]
        public virtual Registration Registration { get; set; }
        [DataMember]
        public virtual string Name { get; set; }
        [DataMember]
        public virtual string Surname { get; set; }
        [DataMember]
        public virtual string EmailAddress { get; set; }
        [DataMember]
        public virtual string MobileNumber { get; set; }
        [DataMember]
        public virtual IList<SurveyQuestion> SurveyQuestions { get; set; }

        public Survey()
		{
            Registration = new Registration();
            SurveyQuestions = new List<SurveyQuestion>();
		}

        public virtual void AddSurveyQuestion(SurveyQuestion surveyQuestion)
		{
            surveyQuestion.Survey = this;
            SurveyQuestions.Add(surveyQuestion);
		}
    }
}
