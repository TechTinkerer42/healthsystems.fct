using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace healthsystems.fct.data
{
    [Serializable]
    [DataContract]
    public class Role
    {
        [DataMember]
        public virtual int Id { get; protected set; }
        [DataMember]
        public virtual string Name { get; set; }
        [DataMember]
        public virtual string Description { get; set; }
        /*[DataMember]
        public virtual IList<RolePermission> RolePermissions { get; set; }*/
        [DataMember]
        public virtual IList<User> Users { get; set; }

        public Role()
        {
            /*RolePermissions = new List<RolePermission>();*/
            Users = new List<User>();
        }


    }
}
