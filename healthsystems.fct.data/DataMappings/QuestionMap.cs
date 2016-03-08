using FluentNHibernate.Mapping;
using healthsystems.fct.data.DataClasses;

namespace healthsystems.fct.data.DataMappings
{
    public class QuestionMap : ClassMap<Question>
    {
        public QuestionMap()
        {
            Id(x => x.Id);
            Map(x => x.Query);
        }
    }
}
