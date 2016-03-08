using System;
using System.Runtime.Serialization;

namespace healthsystems.fct.data.DataClasses
{
    [Serializable]
    [DataContract]
    public class SurveyQuestion
    {
        [DataMember]
        public virtual int Id { get; protected set; }
        [DataMember]
        public virtual Survey Survey { get; set; }
        [DataMember]
        public virtual Registration Registration { get; set; }
        [DataMember]
        public virtual string Question { get; set; }
        [DataMember]
        public virtual double Rating { get; set; }
    }
}
