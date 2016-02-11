using FluentNHibernate.Mapping;

namespace healthsystems.fct.data
{
    public class RolePermissionMap : ClassMap<RolePermission>
    {
        public RolePermissionMap()
        {
            Id(x => x.Id);
            References(x => x.Role);
            References(x => x.Permission);
        }
    }
}
