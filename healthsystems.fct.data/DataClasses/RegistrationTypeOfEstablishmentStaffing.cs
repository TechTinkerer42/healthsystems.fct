using System;
using System.Runtime.Serialization;

namespace healthsystems.fct.data
{
	[Serializable]
	[DataContract]
	public class RegistrationTypeOfEstablishmentStaffing
	{
		[DataMember]
		public virtual int Id { get; protected set; }
		[DataMember]
		public virtual Registration Registration { get; set; }
		[DataMember]
		public virtual TypeOfEstablishment TypeOfEstablishment { get; set; }
		[DataMember]
		public virtual Staffing Staffing { get; set; }
		[DataMember]
		public virtual int NumberOfStaff { get; set; }
	}
}

