using FluentNHibernate.Mapping;

namespace healthsystems.fct.data
{
    public class SettingMap : ClassMap<Setting>
    {
        public SettingMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.Value);
            Map(x => x.Enabled);
            Map(x => x.Deleted);
        }
    }
}