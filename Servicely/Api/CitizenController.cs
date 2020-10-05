using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Servicely.Models;

namespace Servicely.Api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CitizenController : ApiController
    {

        DbMasterEntities1 db = new DbMasterEntities1();

        public string GetNationalId(int IdCC)
        {
            var data = db.Citizens.Find(IdCC);
            string a = data.citizen_national_id;
            return a;
        }
        public string GetFirstName(int id , string lang)
        {
            if(lang == "en")
            {
                return db.Citizens.Find(id).citizen_first_name;
            }
            else
            {
                return db.Citizens.Find(id).citizen_first_name_arabic;
            }

        }
       
        [HttpGet]
        public IEnumerable<Citizen> GetAllChilled(int FId)
        {
            return db.Citizens.Where(a => a.citizen_father_id == FId && a.citizen_isDeleted != true);
        }

        public User GetuserType(int CitizenId)
        {
            return db.Users.Where(a => a.user_citizen_id == CitizenId && a.user_isDeleted != true).SingleOrDefault();
        }

        public CitizenBalance GetCitizenBalance(int ctId)
        {
            return db.CitizenBalances.Where(a => a.CitizenBalance_citizen_id == ctId && a.CitizenBalance_isDeleted != true).SingleOrDefault();
        }
        public IEnumerable<CustomHealthCare> GetAllHospitalsByTypesAndDistrictId(int tid, int dis)
        {
            
            var data = db.HealthCares.Where(a => a.healthcare_type_id == tid && a.hospital_district_id == dis).Select(a => new CustomHealthCare { hospital_id= a.hospital_id, hospital_name= a.hospital_name , hospital_name_arabic = a.hospital_name_arabic }) ;
            return data;
        }

        public IEnumerable<CustomSpecializations> GetAllSepcByHospitalId(int hid)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.HealthCareSpecializationM_M.Where(a => a.hospitalSpecialization_hospital_id == hid).Join(db.HealthCareSpecializations.Where(a => a.specialization_isDeleted != true), a => a.hospitalSpecialization_specialization_id, b => b.specialization_id, (a, b) => new CustomSpecializations { specialization_id =b.specialization_id, specialization_name= b.specialization_name, specialization_name_arabic = b.specialization_name_arabic });
            return data;
        }

        public IEnumerable<HealthCareSpecialization> GetAllSepc()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.HealthCareSpecializations.Where(a => a.specialization_isDeleted != true);
            return data;
        }

        //change
        public IEnumerable<Date> GetscheduleDoctor(int docId , int healtId)
        {
            // .Where(a => Convert.ToDateTime(a.checkup_date) > DateTime.Now)

            var data = db.HealthCareSheduleM_M.Where(a => a.DoctorId == docId && a.hospitalSchedule_hospital == healtId).Join(db.ScheduleHealthCares.Where(a => a.schedule_isDeleted != true && a.checkup_date > DateTime.Now), x => x.hospitalSchedule_shedule, a => a.schedule_id, (x, a) => new Date { checkup_date = a.checkup_date.ToString(), checkup_end = a.checkup_end.ToString(), checkup_start = a.checkup_start.ToString(), schedule_id = a.schedule_id });
            //     var data = db.HealthCareSheduleM_M.Where(a => a.DoctorId == docId && a.hospitalSchedule_hospital == healtId).Join(db.ScheduleHealthCares, b => b.hospitalSchedule_shedule, a => a.schedule_id, (b, a) => new CustomVaccSchedule {checkup_date= a.checkup_date.ToString(), checkup_end = a.checkup_end,checkup_start = a.checkup_start, schedule_id = a.schedule_id });
            db.Configuration.ProxyCreationEnabled = false;
            return data ;

        }

        //change
        public IEnumerable<CustomDoctors> GetAlldoctorsBySpecId(int sid, int hoss)
        {
            
            var data = (from mm in db.HealthCareDoctorM_M from hos in db.HealthCares from hmm in db.HealthCareSpecializationM_M from doc in db.Healthcare_Doctor.Where(a => a.doctor_specialization_id == sid && hos.hospital_id == hoss && mm.hospitalDoctor_hospital == hos.hospital_id && a.doctor_id == mm.hospitalDoctor_doctor && hmm.hospitalSpecialization_specialization_id == sid && hos.hospital_id == hmm.hospitalSpecialization_hospital_id) select new CustomDoctors {doctor_id= doc.doctor_id, citizen_first_name= doc.Citizen.citizen_first_name, citizen_fourth_name = doc.Citizen.citizen_fourth_name, citizen_first_name_arabic = doc.Citizen.citizen_first_name_arabic, citizen_fourth_name_arabic = doc.Citizen.citizen_fourth_name_arabic });


            return data;
        }

        //change
        [HttpGet]
        public IEnumerable<addd> GetAllHealtCaresByVaccinationType_HealthcareTypeAndDistrictIds(int VtId, int DId, int HCTId)
        {


            var data = db.HealthCares.Where(a => a.healthcare_type_id == HCTId && a.hospital_district_id == DId).Join(db.VacinationHealthCareM_M, a => a.hospital_id, b => b.vaccinationhealthcare_hospital, (a, b) => new { a, b }).Where(x => x.b.vacinationhealthcare_vaccination == x.b.VaccinationType.vaccination_type_id && x.b.vacinationhealthcare_vaccination == VtId).Select(a => new addd { hospital_id = a.a.hospital_id, hospital_name = a.a.hospital_name, hospital_name_arabic = a.a.hospital_name_arabic });

            return data;
        }
        [HttpGet]

        //change
        public IEnumerable<Date> GetAllVaccinationAppointmentByHealthCareId(int hos , int vacType)
        {
            var appointment = db.ScheduleVaccM_M.Where(a => a.HealthCareId == hos && a.scheduleVacc_vaccType_id == vacType).Join(db.ScheduleVaccinations.Where(a => a.schedule_isDeleted != true&&a.checkup_date>DateTime.Now), x => x.scheduleVacc_schedule_id, a => a.schedule_id, (x, a) => new Date { checkup_date = a.checkup_date.ToString(), checkup_end = a.checkup_end.ToString(), checkup_start = a.checkup_start.ToString(), schedule_id = a.schedule_id });

            //db.Configuration.ProxyCreationEnabled = false;
            //var data = (
            //    from health in db.HealthCares
            //    from homm in db.VacinationHealthCareM_M
            //    from smm in db.ScheduleVaccM_M
            //    from VaccType in db.VaccinationTypes
            //    from sc in db.ScheduleVaccinations.Where(a => health.hospital_id == hos && health.hospital_id == homm.vaccinationhealthcare_hospital
            //    && VaccType.vaccination_type_id == homm.vacinationhealthcare_vaccination
            //    && a.schedule_id == smm.scheduleVacc_schedule_id && VaccType.vaccination_type_id == smm.scheduleVacc_vaccType_id && a.checkup_date.Value.Year >= DateTime.Now.Year
            //    && a.checkup_date.Value.Month >= DateTime.Now.Month&& ((a.checkup_date.Value.Day > DateTime.Now.Day && a.checkup_date.Value.Month >= DateTime.Now.Month) ||(a.checkup_date.Value.Day <= DateTime.Now.Day && a.checkup_date.Value.Month > DateTime.Now.Month))

            //    )
            //    select new CustomVaccSchedule
            //    {
            //       checkup_date= sc.checkup_date.ToString(),
            //        checkup_end= sc.checkup_end,
            //        checkup_start = sc.checkup_start,
                    
            //        schedule_id = sc.schedule_id

            //    }

            //    );

            return appointment;
        }

    
        public IEnumerable<VaccinationType> GetAllVaccinationTypeByCitizenId(int cti)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var citizen = db.Citizens.Find(cti).citizen_birthDate;
            var vccType = db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true);
            var vaccinated = db.VaccinationReservations.Where(a => a.VaccReservation_child_id == cti && a.VaccReservation_isDeleted != true && a.VaccReservation_checked == true).Select(a => a.VaccinationType.vaccination_type_id);
            foreach (var item in vccType)
            {
                item.vaccination_type_period += 60;
            }



            List<VaccinationType> tb = new List<VaccinationType>();
            foreach (var item in vccType)
            {
                TimeSpan t = DateTime.Now.Subtract(Convert.ToDateTime(citizen));
                if (t.Days < item.vaccination_type_period)
                {
                    int i = 0;
                    foreach (var item1 in vaccinated)
                    {
                        if (item1 == item.vaccination_type_id)
                        {
                            i = 1;
                        }
                    }

                    if (i == 0)
                    {
                        tb.Add(item);
                    }


                }

            }

            var data = tb.ToList();



            return data;
        }

        //change

        public IEnumerable<CustomHealth> GetAllHEaltcaresByDistrictAndSpecAndType(int dis, int htype, int spec)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.HealthCareSpecializationM_M.Where(a => a.HealthCare.healthcare_type_id == htype && a.HealthCare.hospital_district_id == dis && a.hospitalSpecialization_specialization_id == spec && a.HealthCare.hospital_id == a.hospitalSpecialization_hospital_id).Select(a => new CustomHealth { hospital_id = a.HealthCare.hospital_id, hospital_name = a.HealthCare.hospital_name, hospital_name_arabic = a.HealthCare.hospital_name_arabic });

            return data;
        }

        public IEnumerable<CustomCertificate> GetAllCertificationByCitizenId(int citId)
        {
            var Student = db.Students.Where(a => a.Is_Deleted != true && a.CitizenId == citId).SingleOrDefault();
            var data = db.CertificateStudentM_M.Where(a => a.Is_Deleted != true && a.StudentId == Student.Id).Select(a => new CustomCertificate {Date =a.Date.ToString(), GradeName=a.Grade.GradeName,GradeNameArabic= a.Grade.GradeNameArabic,CirtificateTypeName= a.Certificate.CirtificateTypeName,CirtificateTypeNameArabic= a.Certificate.CirtificateTypeNameArabic,CirtificateId=a.Certificate.Id });
            return data;
        }

        public HttpResponseMessage GetPhoto(int Id)
        {
            var respons = Request.CreateResponse(HttpStatusCode.OK);

            var data = db.Photos.Where(a => a.Photo_citizen_id == Id && a.Photo_isCurrent == true).SingleOrDefault();
            string path = "~/CitizenPhotos/" + data.Photo_Url;

            path = System.Web.Hosting.HostingEnvironment.MapPath(path);
            var extension = System.IO.Path.GetExtension(path);

            var content = System.IO.File.ReadAllBytes(path);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(content);
            respons.Content = new StreamContent(ms);
            respons.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/" + extension);

            return respons;

        }



    }
}
