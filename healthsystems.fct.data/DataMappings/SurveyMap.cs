using FluentNHibernate.Mapping;
using healthsystems.fct.data.DataClasses;

namespace healthsystems.fct.data.DataMappings
{
    public class SurveyMap : ClassMap<Survey>
    {
        public SurveyMap()
        {
            Id(x => x.Id);
            References(x => x.Registration);
            Map(x => x.Name);
            Map(x => x.Surname);
            Map(x => x.EmailAddress);
            Map(x => x.MobileNumber);
        }
    }
}
