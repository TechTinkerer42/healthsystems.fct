using FluentNHibernate.Mapping;

namespace healthsystems.fct.data
{
    public class RegistrationServiceMap : ClassMap<RegistrationService>
    {
        public RegistrationServiceMap()
        {
            Id(x => x.Id);
            Map(x => x.Selected);
            References(x => x.Registration);
            References(x => x.Service);
        }
    }
}
