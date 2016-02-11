using System;
using System.Runtime.Serialization;

namespace healthsystems.fct.data
{
    [Serializable]
    [DataContract]
    public class Setting
    {
        [DataMember]
        public virtual int Id { get; protected set; }
        [DataMember]
        public virtual string Name { get; set; }
        [DataMember]
        public virtual string Description { get; set; }
        [DataMember]
        public virtual string Value { get; set; }
        [DataMember]
        public virtual bool Enabled { get; set; }
        [DataMember]
        public virtual bool Deleted { get; set; }
    }
}