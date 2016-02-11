using FluentNHibernate.Mapping;

namespace healthsystems.fct.data
{
    public class PaymentMap : ClassMap<Payment>
    {
        public PaymentMap()
        {
            Id(x => x.Id);
            Map(x => x.PaymentMethod);
            Map(x => x.ReferenceNumber);
            Map(x => x.AmountPaid);
            Map(x => x.ReceivedFrom);
            Map(x => x.Created);
            Map(x => x.Modified);
            References(x => x.Registration);
            Map(x => x.ReceivedById);
            Map(x => x.ReceivedByName);
        }
    }
}