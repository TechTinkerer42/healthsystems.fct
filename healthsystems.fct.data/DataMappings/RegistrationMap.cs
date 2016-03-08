using FluentNHibernate.Mapping;

namespace healthsystems.fct.data
{
    public class RegistrationMap : ClassMap<Registration>
    {
        public RegistrationMap()
        {
            Id(x => x.Id);
            Map(x => x.PhermcRegistrationNumber);
            Map(x => x.CacNumber);
            Map(x => x.LastRenewalDate).Nullable();
            Map(x => x.RegistrationDate);

            Map(x => x.ProprietorFirstName);
            Map(x => x.ProprietorLastName);
            Map(x => x.ProprietorGender);
            Map(x => x.ProprietorNinNumber);
            Map(x => x.ProprietorMobile1);
            Map(x => x.ProprietorMobile2);
            Map(x => x.ProprietorEmailAddress);
            Map(x => x.ProprietorIsMedicalDirector);

            Map(x => x.MedicalDirectorFirstName);
            Map(x => x.MedicalDirectorLastName);
            Map(x => x.MedicalDirectorGender);
            Map(x => x.MedicalDirectorNinNumber);
            Map(x => x.MedicalDirectorMobile1);
            Map(x => x.MedicalDirectorMobile2);
            Map(x => x.MedicalDirectorEmailAddress);

            Map(x => x.AdministratorFirstName);
            Map(x => x.AdministratorLastName);
            Map(x => x.AdministratorMobile1);
            Map(x => x.AdministratorMobile2);

            Map(x => x.EstablishmentName);
            
			References(x => x.TypeOfEstablishment);

			HasMany(x => x.RegistrationTypeOfEstablishmentStaffing)
				.Inverse()
				.Cascade.All();

            Map(x => x.NoOfBeds);
            Map(x => x.AddressLine1);
            Map(x => x.AddressLine2);
            References(x => x.Location);
            References(x => x.Category);
            Map(x => x.Lga);
            Map(x => x.LandMark);

            Map(x => x.Latitude);
            Map(x => x.Longitude);
            
            HasMany(x => x.RegistrationServices)
              .Inverse()
              .Cascade.All();

            HasMany(x => x.Renewals)
              .Inverse()
              .Cascade.All();

			References(x => x.ProfessionalBody);
            Map(x => x.ProfessionalBodyAttendance);
            Map(x => x.ProfessionalBodyInvolvement);
            Map(x => x.ProfessionalBodyRemarks);
            Map(x => x.ProfessionalBodyFirstName);
            Map(x => x.ProfessionalBodyLastName);
            Map(x => x.ProfessionalBodySignatureDate);
            Map(x => x.ProfessionalBodyPosition);

            Map(x => x.AcceptanceDetailsAccepted);
            Map(x => x.AcceptanceDetailsReason);

            Map(x => x.RegistrationCosts);
            Map(x => x.PaymentMethod);
            Map(x => x.ReferenceNumber);
            Map(x => x.AmountPaid);

            Map(x => x.Deleted);
        }
    }
}
