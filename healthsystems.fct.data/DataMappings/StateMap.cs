using FluentNHibernate.Mapping;

namespace healthsystems.fct.data
{
    public class StateMap : ClassMap<State>
    {
        public StateMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.Color);
            Map(x => x.Logo);
            Map(x => x.Active);
            Map(x => x.Deleted);
            HasMany(x => x.Locations)
              .Inverse()
              .Cascade.All();
        }
    }
}
