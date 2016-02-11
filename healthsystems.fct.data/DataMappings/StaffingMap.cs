using FluentNHibernate.Mapping;

namespace healthsystems.fct.data
{
    public class StaffingMap : ClassMap<Staffing>
    {
        public StaffingMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Description);
        }
    }
}
