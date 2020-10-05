using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicely.Models;
namespace Servicely.Controllers
{
    public class infoCertificateFaculty
    {
        public string facultyName { get; set; }
        public string facultyNameArabic { get; set; }
        public string universityName { get; set; }
        public string universityNameArabic { get; set; }
        public string gradeName { get; set; }
        public string gradeNameArabic { get; set; }
        public string specializationName { get; set; }
        public string specializationNameArabic { get; set; }
        public string date { get; set; }



    }
    public class healthCCare
    {

        public String code { get; set; }
        public String firstName { get; set; }
        public String secondName { get; set; }
        public String thirdName { get; set; }
        public String fourthName { get; set; }
        public String firstNameArabic { get; set; }
        public String secondNameArabic { get; set; }
        public String thirdNameArabic { get; set; }
        public String fourthNameArabic { get; set; }
        public String hospitalName { get; set; }
        public String hospitalNameArabic { get; set; }
        public String TransactionDate { get; set; }
        public String date { get; set; }
        public String cancel { get; set; }



    }
    public class Vaccreservationnn
    {

        public String code { get; set; }
        public String firstName { get; set; }
        public String secondName { get; set; }
        public String thirdName { get; set; }
        public String fourthName { get; set; }
        public String firstNameArabic { get; set; }
        public String secondNameArabic { get; set; }
        public String thirdNameArabic { get; set; }
        public String fourthNameArabic { get; set; }
        public String hospitalName { get; set; }
        public String hospitalNameArabic { get; set; }
        public String TransactionDate { get; set; }
        public String date { get; set; }
        public String cancel { get; set; }
        public String typeName { get; set; }
        public String typeNameArabic { get; set; }



    }

    public class Reservationnnn
    {
        public string TrasactionDate { get; set; }
        public string ServiceName { get; set; }
        public string ServiceNameArabic { get; set; }
        public string OfficeName { get; set; }
        public string OfficeNameArbic { get; set; }
        public string ServicePrice { get; set; }
        public string Date { get; set; }
       

    }

    public class CustomCitizenInfo
    {
        public String firstName       { get; set; }
        public String secondName       { get; set; }
        public String thirdName        { get; set; }
        public String fourthName       { get; set; }
        public String firstNameArabic    { get; set; }
        public String secondNameArabic { get; set; }
        public String thirdNameArabic    { get; set; }
        public String fourthNameArabic    { get; set; }
        public String Age                  { get; set; }
        public String Relegion            { get; set; }
        public String gender              { get; set; }
        public String motherName           { get; set; }
        public String birthDate           { get; set; }
        public String DateTRansactionCitizen           { get; set; }
      


    }
    public class CustomCitizeSocialStatus
    {
        public String firstName { get; set; }
        public String secondName { get; set; }
        public String thirdName { get; set; }
        public String fourthName { get; set; }
        public String firstNameArabic { get; set; }
        public String secondNameArabic { get; set; }
        public String thirdNameArabic { get; set; }
        public String fourthNameArabic { get; set; }
        public String wfirstName { get; set; }
        public String wsecondName { get; set; }
        public String wthirdName { get; set; }
        public String wfourthName { get; set; }
        public String wFourthNameArabic { get; set; }
        public String wfirstNameArabic{ get; set; }
        public String wsecondNameArabic { get; set; }
        public String wthirdNameArabic{ get; set; }
        public String typeName { get; set; }
        public String typeNameArabic { get; set; }
        public String from { get; set; }
        public String to { get; set; }

        public String still { get; set; }

    }
    public class GetAddress
    {
        public String districtName { get; set; }
        public String districtNameArabic { get; set; }
        public String regionName { get; set; }
        public String regionNameArabic { get; set; }
        public String cityName { get; set; }
        public String cityNameArabic { get; set; }
        public String stateName { get; set; }
        public String stateNameArabic { get; set; }
        public String CurrentAddressDate { get; set; }
    }

    public class RequestsOfCitizen
    {
        public string ServiceName { get; set; }
        public string ServiceNameArabic { get; set; }
        public string GovName { get; set; }
        public string GovNameArabic { get; set; }
        public string TypeName { get; set; }
        public string TypeNameArabic { get; set; }
        public string TransactionDate { get; set; }
        public string Language { get; set; }
        public string Quantity { get; set; }
        

    }
   public class carLicenceViolation
    {
        public String carCode { get; set; }
        public String carModel { get; set; }
        public String carName { get; set; }
        public String carNameArabic { get; set; }
        public String violationName { get; set; }
        public String violationNameArabic { get; set; }
        public String date { get; set; }
        public String violationPrice { get; set; }
        public String LicenceStartDate { get; set; }
        public String LicenceEndDate { get; set; }
 


    }
    public class CitizenLiveCycleController : BaseController
    {
        // GET: CitizenLiveCycle
        DbMasterEntities1 db = new DbMasterEntities1();
        public ActionResult Index()
        {
            ViewBag.zero = "0";
            ViewBag.NationalId = new SelectList(db.Citizens.Where(a=>a.citizen_isDeleted!=true),"citizen_id", "citizen_national_id");
            return View();
        }

        public JsonResult CitizenInfo(int Id)
        {
            var birthdate = db.Citizens.Find(Id).citizen_birthDate;
            var motherId = db.Citizens.Find(Id).citizen_mother_id;
            var motherName = db.Citizens.Find(motherId);

            var mothertNamee= motherName.citizen_first_name + " " + motherName.citizen_second_name + " " + motherName.citizen_third_name + " " + motherName.citizen_fourth_name;

           int agee =DateTime.Now.Subtract(birthdate).Days  ;
            int agee1 = agee / 365;
            var data = db.Citizens.Where(a => a.citizen_id == Id).Select(a => new CustomCitizenInfo
            {

                firstName = a.citizen_first_name,
                secondName = a.citizen_second_name,
                thirdName = a.citizen_third_name,
                fourthName = a.citizen_fourth_name,
                firstNameArabic = a.citizen_first_name_arabic,
                secondNameArabic = a.citizen_second_name_arabic,
                thirdNameArabic = a.citizen_third_name_arabic,
                fourthNameArabic = a.citizen_fourth_name_arabic,
                gender = a.citizen_gender,
                Relegion = a.citizen_relegion,
                Age = agee1.ToString(),
                motherName = mothertNamee.ToString(),
                birthDate = a.citizen_birthDate.ToString(),
                DateTRansactionCitizen = a.TransactionDate.ToString()


            }) ;
            db.Configuration.ProxyCreationEnabled = false;
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getAddress (int Id)
        {

            var data = db.AddressCitizens.Where(a => a.CA_CitizenId == Id && a.Address.address_isCurrent == true).Select(a=>new GetAddress{ 
             districtName=   a.Address.District.district_name,
            
         districtNameArabic=   a.Address.District.district_arabic_name,
         regionName=   a.Address.District.Region.region_name,
          regionNameArabic=  a.Address.District.Region.region_arabic_name,
           cityName= a.Address.District.Region.City.city_name,
          cityNameArabic=  a.Address.District.Region.City.city_arabic_name,
          stateName=  a.Address.District.Region.City.State.state_name,
          stateNameArabic=  a.Address.District.Region.City.State.state_arabic_name,
          CurrentAddressDate = a.Address.transactionDate.ToString()
            
            
            });
            db.Configuration.ProxyCreationEnabled = false;
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getSocialStatus(int Id)
        {
            var data = db.Social_status.Where(a=> a.social_status_isDeleted!= true && (a.socialStatus_citizenId_Wife == Id||a.socialStatus_citizenId_Husband== Id)).Select(a=> new CustomCitizeSocialStatus {


                firstName = a.Citizen.citizen_first_name,
                secondName = a.Citizen.citizen_second_name,
                thirdName = a.Citizen.citizen_third_name,
                fourthName = a.Citizen.citizen_fourth_name,
                firstNameArabic = a.Citizen.citizen_first_name_arabic,
                secondNameArabic = a.Citizen.citizen_second_name_arabic,
                thirdNameArabic = a.Citizen.citizen_third_name_arabic,
                fourthNameArabic = a.Citizen.citizen_fourth_name_arabic,
                wfirstName = a.Citizen1.citizen_first_name,
                wsecondName = a.Citizen1.citizen_second_name,
                wthirdName = a.Citizen1.citizen_third_name,
                wfourthName = a.Citizen1.citizen_fourth_name,
                wfirstNameArabic = a.Citizen1.citizen_first_name_arabic,
                wsecondNameArabic = a.Citizen1.citizen_second_name_arabic,
                wthirdNameArabic = a.Citizen1.citizen_third_name_arabic,
                wFourthNameArabic = a.Citizen1.citizen_fourth_name_arabic,
                typeName = a.Social_Status_Type.social_status_type_name,
                typeNameArabic = a.Social_Status_Type.social_status_type_name_arabic,
                from = a.social_status_from.ToString(),
                to = a.social_status_to.ToString(),
                still = a.social_status_isStill.ToString()

            }).ToList();
            
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllRequestsByCitizenId(int Id)
        {
            var data = db.Requests.Where(a => a.Is_Deleted != true && a.request_citizenId == Id).Select(a=> new RequestsOfCitizen { 
            
            
           ServiceName= a.service1.service_name,
           ServiceNameArabic= a.service1.service_name_arabic,
           GovName= a.governement_body.governement_name,
            GovNameArabic= a.governement_body.governement_name_arabic,
           TransactionDate= a.TransactionDate.ToString(),
            TypeName= a.TypeRequest1.typeReaquest_name,
            TypeNameArabic =a.TypeRequest1.typeReaquest_name_arabic,
           Quantity= a.quantity.ToString(),
           Language= a.language,
            
            
            }).ToList();
            db.Configuration.ProxyCreationEnabled = false;
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getCarLicenceAndViolation(int Id)
        {

            var data = db.Violation_CarLicenceM_M.Where(a => a.CarLicence.CitizenId == Id).Select(a=>new carLicenceViolation { 
            
          carCode=  a.CarLicence.CarCode,
        carModel=    a.CarLicence.CarModel.ToString(),
         carName=   a.CarLicence.Car.CarName,
        carNameArabic=    a.CarLicence.Car.CarNameArabic,
        violationName=    a.Violation.ViolationName,
         violationNameArabic=   a.Violation.ViolationNameArabic,
         violationPrice=a.Violation.ViolationPrice.ToString(),
         date=   a.Date.ToString(),
            LicenceStartDate=a.CarLicence.StartDate.ToString(),
            LicenceEndDate=a.CarLicence.EndDate.ToString()
            
            
            });
            db.Configuration.ProxyCreationEnabled = false;
            return Json(data,JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllReservationyBId(int Id)
        {
            var data = db.Reservations.Where(a => a.reservation_isDeleted != true && a.reservation_citizen_id == Id).Select(a => new Reservationnnn
            {
               TrasactionDate= a.TransactionDate.ToString(),
               ServiceName= a.service.service_name,
               ServiceNameArabic= a.service.service_name_arabic,
              OfficeName=  a.Office.office_name,
               OfficeNameArbic= a.Office.office_name_arabic,
               Date= a.reservation_date.ToString(),
               ServicePrice= a.service.service_price.ToString(),
            });

           
            db.Configuration.ProxyCreationEnabled = false;
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getReservationHealthCareByCitizenId(int Id)
        {


            var data = db.HealthcareReservations.Where(a=>a.healthcareReservation_isDeleted!=true && a.healthcareReservation_citizen_id == Id).Select(a=>new healthCCare{ 
            
           code=   a.healthcareReservation_Code,
           firstName= a.Healthcare_Doctor.Citizen.citizen_first_name,
           secondName= a.Healthcare_Doctor.Citizen.citizen_second_name,
           thirdName=   a.Healthcare_Doctor.Citizen.citizen_third_name,
           fourthName=  a.Healthcare_Doctor.Citizen.citizen_fourth_name,
           firstNameArabic=  a.Healthcare_Doctor.Citizen.citizen_first_name_arabic,
           secondNameArabic= a.Healthcare_Doctor.Citizen.citizen_second_name_arabic,
           thirdNameArabic=   a.Healthcare_Doctor.Citizen.citizen_third_name_arabic,
           fourthNameArabic=  a.Healthcare_Doctor.Citizen.citizen_fourth_name_arabic,
           hospitalName=  a.HealthCare.hospital_name,
           hospitalNameArabic=   a.HealthCare.hospital_name_arabic,
           TransactionDate=   a.TransactionDate.ToString(),
           date= a.Reservation_date.ToString(),
           cancel=   a.healthcareReservation_cancel.ToString()
            
            
            });
            db.Configuration.ProxyCreationEnabled = false;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getReservationHealthCareByCitizenId2(int Id , DateTime from , DateTime to)
        {


            var data = db.HealthcareReservations.Where(a => a.healthcareReservation_isDeleted != true && a.healthcareReservation_citizen_id == Id && a.Reservation_date >= from && a.Reservation_date <= to).Select(a => new healthCCare
            {

                code = a.healthcareReservation_Code,
                firstName = a.Healthcare_Doctor.Citizen.citizen_first_name,
                secondName = a.Healthcare_Doctor.Citizen.citizen_second_name,
                thirdName = a.Healthcare_Doctor.Citizen.citizen_third_name,
                fourthName = a.Healthcare_Doctor.Citizen.citizen_fourth_name,
                firstNameArabic = a.Healthcare_Doctor.Citizen.citizen_first_name_arabic,
                secondNameArabic = a.Healthcare_Doctor.Citizen.citizen_second_name_arabic,
                thirdNameArabic = a.Healthcare_Doctor.Citizen.citizen_third_name_arabic,
                fourthNameArabic = a.Healthcare_Doctor.Citizen.citizen_fourth_name_arabic,
                hospitalName = a.HealthCare.hospital_name,
                hospitalNameArabic = a.HealthCare.hospital_name_arabic,
                TransactionDate = a.TransactionDate.ToString(),
                date = a.Reservation_date.ToString(),
                cancel = a.healthcareReservation_cancel.ToString()


            });
            db.Configuration.ProxyCreationEnabled = false;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllVaccintionReservationBychId(int Id)
        {
            var data = db.VaccinationReservations.Where(a => a.VaccReservation_isDeleted != true && a.VaccReservation_child_id == Id).Select(a => new Vaccreservationnn
            {
               firstName= a.Citizen.citizen_first_name,
               secondName= a.Citizen.citizen_second_name,
               thirdName= a.Citizen.citizen_third_name,
               fourthName= a.Citizen.citizen_fourth_name,
               firstNameArabic= a.Citizen.citizen_first_name_arabic,
               secondNameArabic= a.Citizen.citizen_second_name_arabic,
               thirdNameArabic= a.Citizen.citizen_third_name_arabic,
               fourthNameArabic= a.Citizen.citizen_fourth_name_arabic,
               cancel= a.VaccReservation_cancel.ToString(),
               code= a.VaccReservation_Code,
               typeName= a.VaccinationType.vaccination_type_name,
               typeNameArabic= a.VaccinationType.vaccination_type_name_arabic,
               hospitalName= a.HealthCare.hospital_name,
               hospitalNameArabic= a.HealthCare.hospital_name_arabic,
               TransactionDate= a.TransactionDate.ToString(),
               date = a.VaccReservation_date.ToString() ,
                


            });
            db.Configuration.ProxyCreationEnabled = false;

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getUniversityCertificate(int Id)
        {
            var data1 = db.Students.Where(a => a.CitizenId == Id && a.IsGraduatedU == true).SingleOrDefault().Faculty.Id;
            var data = db.FacultyCertificateM_M.Where(a=>a.FacultyId == data1).SingleOrDefault().CertificateId;
            var data2 = db.CertificateStudentM_M.Where(a => a.CertificateId == data && a.Student.CitizenId == Id).Select(a=>new infoCertificateFaculty { 
            
        facultyName=    a.Student.Faculty.FacultyName,
          facultyNameArabic=  a.Student.Faculty.FacultyNameArabic,
          universityName=  a.Student.University.UniversityName,
          universityNameArabic=  a.Student.University.UniversityNameArabic,
         gradeName=   a.Grade.GradeName,
          gradeNameArabic=  a.Grade.GradeNameArabic,
           specializationName= a.Student.SpecializationOfFaculty.SpecializationName,
          specializationNameArabic=  a.Student.SpecializationOfFaculty.SpecializationNameArabic,
         date=   a.Date.ToString()

            
            
            
            });
            
        
            

            
         
            db.Configuration.ProxyCreationEnabled = false;
            return Json(data2, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPhotoByStudentId(int Id)
        {
       
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.Photos.Where(a => a.Photo_isDeleted != true && a.Photo_citizen_id == Id && a.Photo_isCurrent == true).SingleOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllDataByStudentId(int Id)
        {
            var data1 = db.Students.Where(a => a.CitizenId == Id && a.IsGraduatedU == true).SingleOrDefault().FacultyId;

            
           
            var cirtificate = db.FacultyCertificateM_M.Where(a => a.FacultyId == data1).SingleOrDefault().CertificateId;
            var NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate = db.CertificateStudentM_M.Where(a => a.Student.CitizenId == Id && a.CertificateId == cirtificate).Select(a => new {a.Student.University.UniversityLogo,a.Student.Citizen.citizen_first_name, a.Student.Citizen.citizen_second_name, a.Student.Citizen.citizen_third_name, a.Student.Citizen.citizen_fourth_name, a.Student.Citizen.citizen_first_name_arabic, a.Student.Citizen.citizen_second_name_arabic, a.Student.Citizen.citizen_third_name_arabic, a.Student.Citizen.citizen_fourth_name_arabic, a.Student.Citizen.citizen_birthDate, a.Student.Faculty.FacultyName, a.Student.Faculty.FacultyNameArabic, a.Student.University.UniversityName, a.Student.University.UniversityNameArabic, a.Grade.GradeName, a.Grade.GradeNameArabic, a.Student.SpecializationOfFaculty.SpecializationName, a.Student.SpecializationOfFaculty.SpecializationNameArabic, a.Date }).SingleOrDefault();
            var address = db.AddressCitizens.Where(a => a.CA_CitizenId == Id).Select(a => new { a.Address.District.Region.City.State.state_name, a.Address.District.Region.City.State.state_arabic_name }).FirstOrDefault();
            aaa aa = new aaa();
            aa.state_name = address.state_name;
            aa.state_arabic_name = address.state_arabic_name;
            aa.citizen_birthDate = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.citizen_birthDate.ToShortDateString();
            aa.SpecializationName = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.SpecializationName;
            aa.SpecializationNameArabic = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.SpecializationNameArabic;
            aa.UniversityName = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.UniversityName;
            aa.UniversityNameArabic = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.UniversityNameArabic;
            aa.citizen_first_name = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.citizen_first_name;
            aa.citizen_first_name_arabic = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.citizen_first_name_arabic;
            aa.citizen_second_name = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.citizen_second_name;
            aa.citizen_second_name_arabic = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.citizen_second_name_arabic;
            aa.citizen_third_name = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.citizen_third_name;
            aa.citizen_third_name_arabic = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.citizen_third_name_arabic;
            aa.citizen_fourth_name = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.citizen_fourth_name;
            aa.citizen_fourth_name_arabic = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.citizen_fourth_name_arabic;
            aa.FacultyName = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.FacultyName;
            aa.FacultyNameArabic = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.FacultyNameArabic;
            aa.Date = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.Date.Value.ToShortDateString();
            aa.GradeName = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.GradeName;
            aa.GradeNameArabic = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.GradeNameArabic;
            aa.UniversityLogo = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.UniversityLogo;


            List<string> c = new List<string>();

            c.Add(NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.ToString());
            c.Add(address.ToString());
            db.Configuration.ProxyCreationEnabled = false;
            return Json(aa, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAddressByStudentId(int Id)
        {
           
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.AddressCitizens.Where(a => a.CA_CitizenId == Id).FirstOrDefault().CA_AddressId;//Address.District.Region.City.State.state_name;
            var state = db.Addresses.Where(a => a.address_id == data).Select(a => a.District.Region.City.State.state_name).SingleOrDefault();
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    var data1 = db.AddressCitizens.Where(a => a.CA_CitizenId == Id).FirstOrDefault().Address.District.Region.City.State.state_arabic_name;
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(state, JsonRequestBehavior.AllowGet);
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            //your handling logic here
            filterContext.ExceptionHandled = true;
            filterContext.Result = RedirectToAction("errorpage", "home");
        }
    }
}