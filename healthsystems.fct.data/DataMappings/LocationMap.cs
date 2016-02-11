using FluentNHibernate.Mapping;

namespace healthsystems.fct.data
{
    public class LocationMap : ClassMap<Location>
    {
        public LocationMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Description);
            References(x => x.State);
        }
    }
}
