using System;
using System.Runtime.Serialization;

namespace healthsystems.fct.data
{
    [Serializable]
    [DataContract]
    public class Payment
    {
        [DataMember]
        public virtual int Id { get; protected set; }
        [DataMember]
        public virtual string PaymentMethod { get; set; }
        [DataMember]
        public virtual string ReferenceNumber { get; set; }
        [DataMember]
        public virtual decimal AmountPaid { get; set; }
        [DataMember]
        public virtual string ReceivedFrom { get; set; }
        [DataMember]
        public virtual DateTime Created { get; set; }
        [DataMember]
        public virtual DateTime Modified { get; set; }
        [DataMember]
        public virtual Registration Registration { get; set; }
        [DataMember]
        public virtual int ReceivedById { get; set; }
        [DataMember]
        public virtual string ReceivedByName { get; set; }
    }
}