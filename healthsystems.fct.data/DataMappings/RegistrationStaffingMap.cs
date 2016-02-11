using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace healthsystems.fct.data
{
    public class RegistrationStaffingMap : ClassMap<RegistrationStaffing>
    {
        public RegistrationStaffingMap()
        {
            Id(x => x.Id);
            Map(x => x.NumberOfStaff);
            References(x => x.Registration);
            References(x => x.Staffing);
        }
    }
}
