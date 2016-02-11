using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace healthsystems.fct.data
{
    [Serializable]
    [DataContract]
    public class State
    {
        [DataMember]
        public virtual int Id { get; protected set; }
        [DataMember]
        public virtual string Name { get; set; }
        [DataMember]
        public virtual string Description { get; set; }
        [DataMember]
        public virtual string Color { get; set; }
        [DataMember]
        public virtual string Logo { get; set; }
        [DataMember]
        public virtual bool Active { get; set; }
        [DataMember]
        public virtual bool Deleted { get; set; }
        [DataMember]
        public virtual IList<Location> Locations { get; set; }
        [DataMember]
        public virtual IList<Costing> Costings { get; set; }

        public State()
        {
            Locations = new List<Location>();
            Costings = new List<Costing>();
        }

        public virtual void AddLocation(Location location)
        {
            location.State = this;
            Locations.Add(location);
        }

        public virtual void AddCosting(Costing costing)
        {
            costing.State = this;
            Costings.Add(costing);
        }

    }
}