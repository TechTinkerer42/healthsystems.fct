using FluentNHibernate.Mapping;
using healthsystems.fct.data.DataClasses;

namespace healthsystems.fct.data.DataMappings
{
    public class SurveyQuestionMap : ClassMap<SurveyQuestion>
    {
        public SurveyQuestionMap()
        {
            Id(x => x.Id);
            References(x => x.Survey);
            References(x => x.Registration);
            Map(x => x.Question);
            Map(x => x.Rating);
        }

    }
}
