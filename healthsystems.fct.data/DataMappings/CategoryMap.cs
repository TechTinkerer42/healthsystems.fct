using FluentNHibernate.Mapping;

namespace healthsystems.fct.data
{
    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Description);
            HasMany(x => x.Costings)
              .Inverse()
              .Cascade.All();
        }
    }
}
