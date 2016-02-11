using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace healthsystems.fct.data
{
    [Serializable]
    [DataContract]
    public class User
    {
        [DataMember]
        public virtual int Id { get; protected set; }

        [DataMember]
        public virtual string FirstName { get; set; }

        [DataMember]
        public virtual string LastName { get; set; }

        [DataMember]
        public virtual string Username { get; set; }

        [DataMember]
        public virtual string EmailAddress { get; set; }

        [DataMember]
        public virtual string Mobile { get; set; }

        [DataMember]
        public virtual string Password { get; set; }

        [DataMember]
        public virtual Guid ConfirmationHash { get; set; }

        [DataMember]
        public virtual Guid OneTimeHash { get; set; }

        [DataMember]
        public virtual DateTime Created { get; set; }

        [DataMember]
        public virtual DateTime Modified { get; set; }

        [DataMember]
        public virtual bool Deleted { get; set; }

        [DataMember]
        public virtual bool Active { get; set; }

        [DataMember]
        public virtual string SessionHash { get; set; }

        [DataMember]
        public virtual State State { get; set; }

        [DataMember]
        public virtual IList<Role> Roles { get; set; }

        public User()
        {
            Roles = new List<Role>();
        }
    }
}