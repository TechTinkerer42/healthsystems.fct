using System;
using System.Runtime.Serialization;

namespace healthsystems.fct.data
{
    [Serializable]
    [DataContract]
    public class RegistrationService
    {
        [DataMember]
        public virtual int Id { get; protected set; }
        [DataMember]
        public virtual Registration Registration { get; set; }
        [DataMember]
        public virtual Service Service { get; set; }
        [DataMember]
        public virtual bool Selected { get; set; }
    }
}
