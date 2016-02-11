using System;
using System.Runtime.Serialization;

namespace healthsystems.fct.data
{
    [Serializable]
    [DataContract]
    public class Costing
    {
        [DataMember]
        public virtual int Id { get; protected set; }
        [DataMember]
        public virtual DateTime EffectiveDate { get; set; }
        [DataMember]
        public virtual decimal RegistrationCost { get; set; }
        [DataMember]
        public virtual decimal RenewalCost { get; set; }
        [DataMember]
        public virtual Category Category { get; set; }
        [DataMember]
        public virtual State State { get; set; }

    }
}
