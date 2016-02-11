using FluentNHibernate.Mapping;

namespace healthsystems.fct.data
{
    public class RenewalTypeMap : ClassMap<RenewalType>
    {
        public RenewalTypeMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Description);
        }
    }
}