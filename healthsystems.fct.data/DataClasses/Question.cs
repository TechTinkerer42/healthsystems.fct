using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace healthsystems.fct.data.DataClasses
{
    [Serializable]
    [DataContract]
    public class Question
    {
        [DataMember]
        public virtual int Id { get; protected set; }
        [DataMember]
        public virtual string Query { get; set; }
        [DataMember]
        public virtual double Rating { get; set; }
    }
}
