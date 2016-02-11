using FluentNHibernate.Mapping;

namespace healthsystems.fct.data
{
    public class TransactionMap : ClassMap<Transaction>
    {
        public TransactionMap()
        {
            Id(x => x.Id);
            References(x => x.PaymentType);
            References(x => x.TransactionType);
            References(x => x.Renewal).Not.LazyLoad();
            References(x => x.ReceivedBy);
			Map(x => x.Printed);
            Map(x => x.ReceivedFrom);
            Map(x => x.Date);
            Map(x => x.Amount);
        }
    }
}
