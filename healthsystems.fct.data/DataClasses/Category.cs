using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace healthsystems.fct.data
{
    [Serializable]
    [DataContract]
    public class Category
    {
        [DataMember]
        public virtual int Id { get; protected set; }
        [DataMember]
        public virtual string Name { get; set; }
        [DataMember]
        public virtual string Description { get; set; }
        [DataMember]
        public virtual IList<Costing> Costings { get; set; }

        public Category()
        {
            Costings = new List<Costing>();
        }

        public virtual void AddCosting(Costing costing)
        {
            costing.Category = this;
            Costings.Add(costing);
        }

    }
}
