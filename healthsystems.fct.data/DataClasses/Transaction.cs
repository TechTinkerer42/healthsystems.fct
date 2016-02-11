using System;
using System.Runtime.Serialization;

namespace healthsystems.fct.data
{
    [Serializable]
    [DataContract]
    public class Transaction
    {
        [DataMember]
        public virtual int Id { get; protected set; }
        [DataMember]
        public virtual Renewal Renewal { get; set; }
        [DataMember]
        public virtual DateTime Date { get; set; }
        [DataMember]
        public virtual decimal Amount { get; set; }
        [DataMember]
        public virtual string ReceivedFrom { get; set; }
        [DataMember]
        public virtual bool Printed { get; set; }
        [DataMember]
        public virtual User ReceivedBy { get; set; }
        [DataMember]
        public virtual PaymentType PaymentType { get; set; }
        [DataMember]
        public virtual TransactionType TransactionType { get; set; }

    }
}
