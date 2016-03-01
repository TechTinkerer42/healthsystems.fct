using FluentNHibernate.Mapping;

namespace healthsystems.fct.data.DataMappings
{
	public class TypeOfEstablishmentMap : ClassMap<TypeOfEstablishment>
	{
		public TypeOfEstablishmentMap ()
		{
			Id(x => x.Id);
			Map(x => x.Name);
			Map(x => x.Description);
            HasManyToMany(x => x.Staffings)
                .Cascade.All()
                .Table("TypeOfEstablishmentStaffings");
		}
	}
}

