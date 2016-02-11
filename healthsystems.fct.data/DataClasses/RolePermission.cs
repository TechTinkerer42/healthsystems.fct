
using System;
using System.Runtime.Serialization;

namespace healthsystems.fct.data
{
    [Serializable]
    [DataContract]
    public class RolePermission
    {
        [DataMember]
        public virtual int Id { get; protected set; }
        [DataMember]
        public virtual Role Role { get; set; }
        [DataMember]
        public virtual Permission Permission { get; set; }
    }
}
