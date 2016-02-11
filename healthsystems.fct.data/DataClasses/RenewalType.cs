using System;
using System.Runtime.Serialization;

namespace healthsystems.fct.data
{
    [Serializable]
    [DataContract]
    public class RenewalType
    {
        [DataMember]
        public virtual int Id { get; protected set; }
        [DataMember]
        public virtual string Name { get; set; }
        [DataMember]
        public virtual string Description { get; set; }
    }
}