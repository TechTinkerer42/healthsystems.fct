using FluentNHibernate.Mapping;

namespace healthsystems.fct.data
{
    public class RenewalMap : ClassMap<Renewal>
    {
        public RenewalMap()
        {
            Id(x => x.Id);
            References(x => x.Registration).Not.LazyLoad();
            Map(x => x.Amount);
            Map(x => x.Date);
            Map(x => x.Paid);
            HasMany(x => x.Transactions)
              .Inverse()
              .Cascade.All();
            References(x => x.RenewalType);
        }
    }
}
