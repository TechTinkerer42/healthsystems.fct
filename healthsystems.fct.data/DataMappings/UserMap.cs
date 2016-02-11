using FluentNHibernate.Mapping;

namespace healthsystems.fct.data
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.Username);
            Map(x => x.EmailAddress);
			Map(x => x.Mobile);
            Map(x => x.Password);
            Map(x => x.ConfirmationHash);
            Map(x => x.OneTimeHash);
            Map(x => x.Created);
            Map(x => x.Modified);
            Map(x => x.Deleted);
            Map(x => x.Active);
            Map(x => x.SessionHash);
            References(x => x.State);
            HasManyToMany(x => x.Roles)
                .Cascade.All()
                .Table("UserRole");
        }
    }
}
