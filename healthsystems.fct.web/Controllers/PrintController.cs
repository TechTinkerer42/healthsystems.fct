using System.Web.Mvc;
using System.IO;
using healthsystems.fct.data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using healthsystems.fct.data.Common;
using NReco.PdfGenerator;


namespace healthsystems.fct.web
{
    public class PrintController : Controller
    {

		string BootstrapCss
		{
			get
			{
				var cssPath = System.Web.Hosting.HostingEnvironment.MapPath("~/bower_components/bootstrap/dist/css/bootstrap.min.css");
				return System.IO.File.ReadAllText(cssPath);
			}
		}

		public ActionResult Registration(int? id)
		{
			var workStream = new MemoryStream();

			// get the record to which this registation id belongs to
			using (var session = NHibernateHelper.CreateSessionFactory())
			{
				using (var transaction = session.BeginTransaction())
				{
					// get the html template content

					var path = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/templates/registration.html");
					var htmlContent = System.IO.File.ReadAllText(path);

					var registrations =
						new List<Registration>(session.CreateCriteria(typeof (Registration)).List<Registration>());

					var r = registrations.FirstOrDefault(x => x.Id == id);


					// do string replacements to htmlContent here
					var parsedHtmlContent = htmlContent;

					// Head details
					if (r != null)
					{
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$BootstrapCss$$", BootstrapCss);

						parsedHtmlContent = Replacement(parsedHtmlContent, "$$PHERMC_REG_NO$$", r.PhermcRegistrationNumber);
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$LAST_RENEWAL_DATE$$", r.LastRenewalDate.HasValue ?  r.LastRenewalDate.Value.ToString("dd/MM/yyyy") : "");
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$CAC_NUMBER$$", r.CacNumber);
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$REGISTRATION_DATE$$", r.RegistrationDate.ToString("dd/MM/yyyy"));

						// Proprietor details
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$PROPRIETOR_NAME$$", string.Concat(r.ProprietorFirstName, " ", r.ProprietorLastName));

						parsedHtmlContent = Replacement(parsedHtmlContent, "$$PROPRIETOR_GENDER_MALE$$", r.ProprietorGender.Equals("Male") ? "X" : "");
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$PROPRIETOR_GENDER_FEMALE$$", r.ProprietorGender.Equals("Female") ? "X" : "");

						parsedHtmlContent = Replacement(parsedHtmlContent, "$$PROPRIETOR_NIN_NUMBER$$", r.ProprietorNinNumber);
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$PROPRIETOR_MOBILE1$$", r.ProprietorMobile1);
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$PROPRIETOR_MOBILE2$$", r.ProprietorMobile2);
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$PROPRIETOR_EMAIL$$", r.ProprietorEmailAddress);
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$PROPRIETOR_IS_MD_NO$$", !r.ProprietorIsMedicalDirector ? "X" : "");
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$PROPRIETOR_IS_MD_YES$$", r.ProprietorIsMedicalDirector ? "X" : "");

						// Medical Director details
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$MD_NAME$$", string.Concat(r.MedicalDirectorFirstName, " ", r.MedicalDirectorLastName));
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$MD_GENDER_MALE$$", r.MedicalDirectorGender == null ? "" : r.MedicalDirectorGender.Equals("Male") ? "X" : "");
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$MD_GENDER_FEMALE$$", r.MedicalDirectorGender == null ? "" : r.MedicalDirectorGender.Equals("Female") ? "X" : "");
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$MD_NIN_NUMBER$$", r.MedicalDirectorNinNumber);
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$MD_MOBILE1$$", r.MedicalDirectorMobile1);
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$MD_MOBILE2$$", r.MedicalDirectorMobile2);
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$MD_EMAIL$$", r.MedicalDirectorEmailAddress);

						// Establishment details
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$ESTABLISHMENT_NAME$$", r.EstablishmentName);
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$ESTABLISHMENT_TYPE_HOSPITAL$$", r.TypeOfEstablishment.Equals("Hospital") ? "X" : "");
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$ESTABLISHMENT_TYPE_CLINIC$$", r.TypeOfEstablishment.Equals("Clinic") ? "X" : "");
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$ESTABLISHMENT_TYPE_LABARATORY$$", r.TypeOfEstablishment.Equals("Laboratory") ? "X" : "");
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$ESTABLISHMENT_TYPE_MATERNITY$$", r.TypeOfEstablishment.Equals("Marernity") ? "X" : "");
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$ESTABLISHMENT_TYPE_PHYSIOTHERAPY$$", r.TypeOfEstablishment.Equals("Physiotherapy") ? "X" : "");
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$ESTABLISHMENT_TYPE_RADIODIAGNOSTIC$$", r.TypeOfEstablishment.Equals("Radiodiagnostic") ? "X" : "");
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$ESTABLISHMENT_TYPE_OTHER$$", r.TypeOfEstablishment.Equals("Other") ? "X" : "");
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$ESTABLISHMENT_TYPE_OTHER_NAME$$", r.TypeOfEstablishment.Equals("Hospital") ? "X" : "");

						// Establishment address
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$ESTABLISHMENT_ADDRESS$$", string.Concat(r.AddressLine1, " ", r.AddressLine2));
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$ESTABLISHMENT_LGA$$", r.Lga);
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$ESTABLISHMENT_LANDMARK$$", r.LandMark);

						// Numbers of staff
						var staffing = new StringBuilder();
						for (var i = 0; i < r.RegistrationStaffing.Count; i++)
						{
							var staff = r.RegistrationStaffing[i];

							staffing.AppendFormat("{0} {1}", staff.NumberOfStaff, staff.Staffing.Name);

							if (i >= 0 && i < r.RegistrationStaffing.Count - 1)
							{
								staffing.Append(", ");
							}
						}
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$STAFFING_PROFILE$$", staffing.ToString());

						// Services
						var services = new StringBuilder();
						for (var i = 0; i < r.RegistrationServices.Count; i++)
						{
							var service = r.RegistrationServices[i];

							services.AppendFormat("{0} {1}", service.Selected, service.Service.Name);

							if (i >= 0 && i < r.RegistrationServices.Count - 1)
							{
								services.Append(", ");
							}
						}
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$SERVICES$$", services.ToString());

						// Administrator details
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$ADMIN_NAME$$", string.Concat(r.AdministratorFirstName, " ", r.AdministratorLastName));
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$ADMIN_MOBILE1$$", r.AdministratorMobile1);
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$ADMIN_MOBILE2$$", r.AdministratorMobile2);

						// Professional Body details
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$PBODY_ATT_20$$", r.ProfessionalBodyAttendance.Equals("20 Units") ? "X" : "");
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$PBODY_ATT_LESS20$$", r.ProfessionalBodyAttendance.Equals("Less than 20 Units") ? "X" : "");
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$PBODY_ATT_NOTATALL$$", r.ProfessionalBodyAttendance.Equals("Not at all") ? "X" : "");
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$PBODY_INVOLVE_REGULAR$$", r.ProfessionalBodyInvolvement.Equals("Regular") ? "X" : "");
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$PBODY_INVOLVE_IRREGULAR$$", r.ProfessionalBodyInvolvement.Equals("Irregular") ? "X" : "");
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$PBODY_INVOLVE_NOT$$", r.ProfessionalBodyInvolvement.Equals("Not Involved") ? "X" : "");
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$PBODY_NAME$$", string.Concat(r.ProfessionalBodyFirstName, " ", r.ProfessionalBodyLastName));
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$PBODY_POSITION$$", r.ProfessionalBodyPosition);
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$PBODY_REMARKS$$", r.ProfessionalBodyRemarks);

						// Acceptance details
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$ACCEPTANCE_NO$$", !r.AcceptanceDetailsAccepted ? "X" : "");
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$ACCEPTANCE_YES$$", r.AcceptanceDetailsAccepted ? "X" : "");
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$ACCEPTANCE_REASON$$", r.AcceptanceDetailsReason);

						// create instance of pdf api
						var htmlToPdf = new HtmlToPdfConverter();

						// convert theget a byte array 
						var pdfBytes = htmlToPdf.GeneratePdf(parsedHtmlContent);

						var byteInfo = pdfBytes;
						workStream.Write(byteInfo, 0, byteInfo.Length);
						workStream.Position = 0;
					}
				}
			}

			//return new FileStreamResult(workStream, "application/pdf");
			return File(workStream, "application/pdf", string.Format("Establishment_{0}_{1}.pdf", id, DateTime.Now.ToString("yyyyMMddhhmmss")) );
		}

		public ActionResult Receipt(int? id)
		{
			var workStream = new MemoryStream();

			// get the record to which this registation id belongs to
			using (var session = NHibernateHelper.CreateSessionFactory())
			{
				using (var transaction = session.BeginTransaction())
				{
					// get the html template content
					var path = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/templates/receipt.html");
					var htmlContent = System.IO.File.ReadAllText(path);

					var registrations = new List<Renewal>(session.CreateCriteria(typeof(Renewal)).List<Renewal>());


					var ts = new List<Transaction>(session.CreateCriteria(typeof(Transaction)).List<Transaction>());
					var t = ts.FirstOrDefault(x => x.Id == id);

					var r = t.Renewal.Registration;

					// do string replacements to htmlContent here
					var parsedHtmlContent = htmlContent;

					// Head details
					if (r != null)
					{
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$BootstrapCss$$", BootstrapCss);

						parsedHtmlContent = Replacement(parsedHtmlContent, "$$PHERMC_REG_NO$$", r.PhermcRegistrationNumber);

						var lastRenewalDate = r.LastRenewalDate == null || r.LastRenewalDate.Equals(DateTime.MinValue)
							? ""
							: r.LastRenewalDate.Value.ToString("dd/MM/yyyy");

						parsedHtmlContent = Replacement(parsedHtmlContent, "$$ESTABLISHMENT_NAME$$", r.EstablishmentName);
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$CAC_NUMBER$$", r.CacNumber);
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$REGISTRATION_DATE$$", r.RegistrationDate.ToString("dd/MM/yyyy"));

						// create $$TBODY_CONTENT$$ here
						var totalPaid = 0.0M;
						var tbodyRows = new StringBuilder();

						var payments = t.Renewal.Transactions;

						if (payments.Any())
						{
							foreach (var p in payments)
							{
								totalPaid += p.Amount;
							}
						}

						var lastPayment = t;

						var receiptTitle = "PAYMENT RECEIPT";
						if (lastPayment.Printed)
						{
							receiptTitle = "COPY OF RECEIPT";
						}
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$RECEIPT_TITLE$$", receiptTitle);

						tbodyRows.Append("<tr>");

						tbodyRows.AppendFormat("<td>{0}</td>", lastPayment.Date.ToString("dd/MM/yyyy"));
						tbodyRows.AppendFormat("<td>{0}</td>", lastPayment.PaymentType.Name);
						tbodyRows.AppendFormat("<td>{0}</td>", "");
						tbodyRows.AppendFormat("<td>{0}</td>", lastPayment.ReceivedFrom);
						tbodyRows.AppendFormat("<td>{0}</td>", lastPayment.ReceivedBy.Username);
						tbodyRows.AppendFormat("<td class=\"right\">&#x20a6;{0}</td>", lastPayment.Amount.ToString("#,##0.00"));

						tbodyRows.Append("</tr>");

						parsedHtmlContent = Replacement(parsedHtmlContent, "$$TBODY_CONTENT$$", tbodyRows.ToString());

						// Update totals
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$TOTAL$$", lastPayment.Amount.ToString("N2"));
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$TOTAL_DUE$$", t.Renewal.Amount.ToString("#,##0.00"));
						var amountOutstanding = t.Renewal.Amount - totalPaid;
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$OUTSTANDING$$", amountOutstanding.ToString("#,##0.00"));

						// create instance of pdf api
						var htmlToPdf = new HtmlToPdfConverter();

						// convert theget a byte array 
						var pdfBytes = htmlToPdf.GeneratePdf(parsedHtmlContent);

						var byteInfo = pdfBytes;
						workStream.Write(byteInfo, 0, byteInfo.Length);
						workStream.Position = 0;

						lastPayment.Printed = true;
						transaction.Commit();

					}

				}
			}

			return File(workStream, "application/pdf", string.Format("Receipt_{0}_{1}.pdf", id, DateTime.Now.ToString("yyyyMMddhhmmss")));

		}

		public ActionResult ReceiptX(int? id)
		{
			var workStream = new MemoryStream();

			// get the record to which this registation id belongs to
			using (var session = NHibernateHelper.CreateSessionFactory())
			{
				using (var transaction = session.BeginTransaction())
				{
					// get the html template content

					var path = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/templates/receipt.html");
					var htmlContent = System.IO.File.ReadAllText(path);

					var registrations =
						new List<Registration>(session.CreateCriteria(typeof(Registration)).List<Registration>());

					var r = registrations.FirstOrDefault(x => x.Id == id);


					// do string replacements to htmlContent here
					var parsedHtmlContent = htmlContent;

					// Head details
					if (r != null)
					{
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$BootstrapCss$$", BootstrapCss);

						parsedHtmlContent = Replacement(parsedHtmlContent, "$$PHERMC_REG_NO$$", r.PhermcRegistrationNumber);
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$LAST_RENEWAL_DATE$$", r.LastRenewalDate.Value.ToString("dd/MM/yyyy"));
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$CAC_NUMBER$$", r.CacNumber);
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$REGISTRATION_DATE$$", r.RegistrationDate.ToString("dd/MM/yyyy"));

						// create $$TBODY_CONTENT$$ here
						var totalPaid = 0.0M;
						var tbodyRows = new StringBuilder();

						var payments =
							new List<Payment>(session.CreateCriteria(typeof(Payment)).List<Payment>());

						var paymentEntries =
							from x in payments
								where x.Registration.Id == id
							select x;

						if (paymentEntries.Any())
						{
							foreach (var p in paymentEntries)
							{
								totalPaid += p.AmountPaid;
							}
						}

						var lastPayment = paymentEntries.LastOrDefault();

						tbodyRows.Append("<tr>");

						tbodyRows.AppendFormat("<td>{0}</td>", lastPayment.Created.ToString("dd/MM/yyyy"));
						tbodyRows.AppendFormat("<td>{0}</td>", lastPayment.PaymentMethod);
						tbodyRows.AppendFormat("<td>{0}</td>", lastPayment.ReferenceNumber);
						tbodyRows.AppendFormat("<td>{0}</td>", lastPayment.ReceivedFrom);
						tbodyRows.AppendFormat("<td>{0}</td>", lastPayment.ReceivedByName);
						tbodyRows.AppendFormat("<td class=\"right\">&#x20a6;{0}</td>", lastPayment.AmountPaid.ToString("#,##0.00"));

						tbodyRows.Append("</tr>");

						parsedHtmlContent = Replacement(parsedHtmlContent, "$$TBODY_CONTENT$$", tbodyRows.ToString());

						// Update totals
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$TOTAL$$", lastPayment.AmountPaid.ToString("N2"));
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$TOTAL_DUE$$", r.RegistrationCosts.ToString("#,##0.00"));
						var amountOutstanding = r.RegistrationCosts - totalPaid;
						parsedHtmlContent = Replacement(parsedHtmlContent, "$$OUTSTANDING$$", amountOutstanding.ToString("#,##0.00"));

						// create instance of pdf api
						var htmlToPdf = new HtmlToPdfConverter();

						// convert theget a byte array 
						var pdfBytes = htmlToPdf.GeneratePdf(parsedHtmlContent);

						var byteInfo = pdfBytes;
						workStream.Write(byteInfo, 0, byteInfo.Length);
						workStream.Position = 0;
					}
				}
			}

			return new FileStreamResult(workStream, "application/pdf");
		}

		private string Replacement(string stringContent, string tag, string value)
		{
			if (value == null || value.Trim() == "")
			{
				value = "&nbsp;";
			}
			return stringContent.Replace(tag, value);
		}

    }
}
