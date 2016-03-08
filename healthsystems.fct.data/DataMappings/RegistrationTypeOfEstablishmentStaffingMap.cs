using System;
using FluentNHibernate.Mapping;

namespace healthsystems.fct.data
{
	public class RegistrationTypeOfEstablishmentStaffingMap : ClassMap<RegistrationTypeOfEstablishmentStaffing>
	{
		public RegistrationTypeOfEstablishmentStaffingMap ()
		{
			Id(x => x.Id);
			References(x => x.Registration);
			References(x => x.TypeOfEstablishment);
			References(x => x.Staffing);
			Map(x => x.NumberOfStaff);
		}
	}
}

