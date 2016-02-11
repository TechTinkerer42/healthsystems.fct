using FluentNHibernate.Mapping;

namespace healthsystems.fct.data
{
    public class TransactionTypeMap : ClassMap<TransactionType>
    {
        public TransactionTypeMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Description);
        }
    }
}
