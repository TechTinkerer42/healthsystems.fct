using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace healthsystems.fct.data
{
    [Serializable]
    [DataContract]
    public class Renewal
    {
        [DataMember]
        public virtual int Id { get; protected set; }
        [DataMember]
        public virtual Registration Registration { get; set; }
        [DataMember]
        public virtual decimal Amount { get; set; }
        [DataMember]
        public virtual DateTime Date { get; set; }
        [DataMember]
        public virtual bool Paid { get; set; }
        [DataMember]
        public virtual IList<Transaction> Transactions { get; protected set; }
        [DataMember]
        public virtual RenewalType RenewalType { get; set; }

        public Renewal()
        {
            Transactions = new List<Transaction>();
        }
    }
}
