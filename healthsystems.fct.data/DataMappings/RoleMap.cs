using FluentNHibernate.Mapping;

namespace healthsystems.fct.data
{
    public class RoleMap : ClassMap<Role>
    {
        public RoleMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Description);

            /*HasMany(x => x.RolePermissions)
                .Inverse()
                .Cascade.All();*/

            HasManyToMany(x => x.Users)
                .Inverse()
                .Cascade.All()
                .Table("UserRole");
        }
    }
}
