﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Servicely.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DbMasterEntities1 : DbContext
    {
        public DbMasterEntities1()
            : base("name=DbMasterEntities1")
        {
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AddressCitizen> AddressCitizens { get; set; }
        public virtual DbSet<AreasAuthorityRequest> AreasAuthorityRequests { get; set; }
        public virtual DbSet<CarLicence> CarLicences { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Certificate> Certificates { get; set; }
        public virtual DbSet<CertificateStudentM_M> CertificateStudentM_M { get; set; }
        public virtual DbSet<CertificateType> CertificateTypes { get; set; }
        public virtual DbSet<Citizen> Citizens { get; set; }
        public virtual DbSet<Citizen_id> Citizen_id { get; set; }
        public virtual DbSet<CitizenBalance> CitizenBalances { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Complain> Complains { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Contact_Type> Contact_Type { get; set; }
        public virtual DbSet<Coordination> Coordinations { get; set; }
        public virtual DbSet<Deceased> Deceaseds { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<DoctorScheduleM_M> DoctorScheduleM_M { get; set; }
        public virtual DbSet<DoctorType> DoctorTypes { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Document_Detail> Document_Detail { get; set; }
        public virtual DbSet<Document_Type> Document_Type { get; set; }
        public virtual DbSet<EducationalOut> EducationalOuts { get; set; }
        public virtual DbSet<EducationDirectorate> EducationDirectorates { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<FacultyCertificateM_M> FacultyCertificateM_M { get; set; }
        public virtual DbSet<governement_body> governement_body { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<GradesCertificateM_M> GradesCertificateM_M { get; set; }
        public virtual DbSet<HealthCare> HealthCares { get; set; }
        public virtual DbSet<Healthcare_Doctor> Healthcare_Doctor { get; set; }
        public virtual DbSet<HealthCare_Type> HealthCare_Type { get; set; }
        public virtual DbSet<HealthCareDoctorM_M> HealthCareDoctorM_M { get; set; }
        public virtual DbSet<HealthcareReservation> HealthcareReservations { get; set; }
        public virtual DbSet<HealthCareSheduleM_M> HealthCareSheduleM_M { get; set; }
        public virtual DbSet<HealthCareSpecializationM_M> HealthCareSpecializationM_M { get; set; }
        public virtual DbSet<HealthCareSpecialization> HealthCareSpecializations { get; set; }
        public virtual DbSet<KindOfSchool> KindOfSchools { get; set; }
        public virtual DbSet<Live_Status> Live_Status { get; set; }
        public virtual DbSet<Live_Status_Type> Live_Status_Type { get; set; }
        public virtual DbSet<LoginCitizen> LoginCitizens { get; set; }
        public virtual DbSet<LogRequest> LogRequests { get; set; }
        public virtual DbSet<Office> Offices { get; set; }
        public virtual DbSet<PhasesOfSchoole> PhasesOfSchooles { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<police_officer> police_officer { get; set; }
        public virtual DbSet<police_report> police_report { get; set; }
        public virtual DbSet<PoliceDepartment> PoliceDepartments { get; set; }
        public virtual DbSet<PoliceReport_type> PoliceReport_type { get; set; }
        public virtual DbSet<PoliceReportClassification> PoliceReportClassifications { get; set; }
        public virtual DbSet<RealStateRegistryInterest> RealStateRegistryInterests { get; set; }
        public virtual DbSet<RealStateRegistryInterestBranch> RealStateRegistryInterestBranches { get; set; }
        public virtual DbSet<RealStateRegistryInterestDepartment> RealStateRegistryInterestDepartments { get; set; }
        public virtual DbSet<RealStateRegistryInterestPopularContract> RealStateRegistryInterestPopularContracts { get; set; }
        public virtual DbSet<RealStateRegistryInterestPropertyTax> RealStateRegistryInterestPropertyTaxes { get; set; }
        public virtual DbSet<RealStateRegistryInterestReport> RealStateRegistryInterestReports { get; set; }
        public virtual DbSet<RealStateRegistryInterestReportsSubject> RealStateRegistryInterestReportsSubjects { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Request_Ambulance> Request_Ambulance { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<ReservationScheduleM_M> ReservationScheduleM_M { get; set; }
        public virtual DbSet<Respond> Responds { get; set; }
        public virtual DbSet<RSRIPersonalRegistry> RSRIPersonalRegistries { get; set; }
        public virtual DbSet<RSRISpecificRegistry> RSRISpecificRegistries { get; set; }
        public virtual DbSet<ScheduleHealthCare> ScheduleHealthCares { get; set; }
        public virtual DbSet<ScheduleVaccination> ScheduleVaccinations { get; set; }
        public virtual DbSet<ScheduleVaccM_M> ScheduleVaccM_M { get; set; }
        public virtual DbSet<ScheduleVaccReservationM_M> ScheduleVaccReservationM_M { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<SchoolCertificateM_M> SchoolCertificateM_M { get; set; }
        public virtual DbSet<SchoolPhasesM_M> SchoolPhasesM_M { get; set; }
        public virtual DbSet<SchoolStudentM_M> SchoolStudentM_M { get; set; }
        public virtual DbSet<service> services { get; set; }
        public virtual DbSet<Social_status> Social_status { get; set; }
        public virtual DbSet<Social_Status_Type> Social_Status_Type { get; set; }
        public virtual DbSet<SpecializationOfFaculty> SpecializationOfFaculties { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<TypeRequest> TypeRequests { get; set; }
        public virtual DbSet<University> Universities { get; set; }
        public virtual DbSet<UniversityFacultyM_M> UniversityFacultyM_M { get; set; }
        public virtual DbSet<UniversityStudentM_M> UniversityStudentM_M { get; set; }
        public virtual DbSet<UniversityType> UniversityTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<User_Type> User_Type { get; set; }
        public virtual DbSet<UserCitizen> UserCitizens { get; set; }
        public virtual DbSet<VaccinationReservation> VaccinationReservations { get; set; }
        public virtual DbSet<VaccinationType> VaccinationTypes { get; set; }
        public virtual DbSet<VacinationHealthCareM_M> VacinationHealthCareM_M { get; set; }
        public virtual DbSet<Violation> Violations { get; set; }
        public virtual DbSet<Violation_CarLicenceM_M> Violation_CarLicenceM_M { get; set; }
    }
}
