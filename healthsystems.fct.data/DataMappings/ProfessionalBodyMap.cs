using FluentNHibernate.Mapping;

namespace healthsystems.fct.data
{
	public class ProfessionalBodyMap : ClassMap<ProfessionalBody>
	{
		public ProfessionalBodyMap ()
		{
			Id(x => x.Id);
			Map(x => x.Name);
			Map(x => x.Description);
		}
	}
}

