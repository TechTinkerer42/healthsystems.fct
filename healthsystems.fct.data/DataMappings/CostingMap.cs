using FluentNHibernate.Mapping;

namespace healthsystems.fct.data
{
    public class CostingMap : ClassMap<Costing>
    {
        public CostingMap()
        {
            Id(x => x.Id);
            Map(x => x.EffectiveDate);
            Map(x => x.RegistrationCost);
            Map(x => x.RenewalCost);
            References(x => x.Category);
            References(x => x.State);
        }
    }
}
