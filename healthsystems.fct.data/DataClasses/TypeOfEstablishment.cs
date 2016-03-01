using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace healthsystems.fct.data
{
	[Serializable]
	[DataContract]
	public class TypeOfEstablishment
	{
		[DataMember]
		public virtual int Id { get; protected set; }
		[DataMember]
		public virtual string Name { get; set; }
		[DataMember]
		public virtual string Description { get; set; }
		[DataMember]
		public virtual IList<Staffing> Staffings { get; set; }

		public TypeOfEstablishment()
		{
			Staffings = new List<Staffing>();
		}

		public virtual void AddStaff(Staffing staff)
		{
			staff.TypeOfEstablishment = this;
			Staffings.Add(staff);
		}
	}
}

