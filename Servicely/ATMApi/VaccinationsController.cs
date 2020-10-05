using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Servicely.Models;

namespace Servicely.ATMApi
{
    public class vacc
    {
        public int VaccReservation_id { get; set; }
        public Nullable<int> VaccReservation_HealthCare_id { get; set; }
        public Nullable<int> VaccReservation_Citizen_id { get; set; }
        public Nullable<int> VaccReservation_child_id { get; set; }
        public Nullable<System.DateTime> VaccReservation_date { get; set; }
        public Nullable<bool> VaccReservation_checked { get; set; }
        public string VaccReservation_Code { get; set; }
        public Nullable<int> VaccReservation_VaccinationType_id { get; set; }
        public Nullable<bool> VaccReservation_isDeleted { get; set; }
        public Nullable<bool> VaccReservation_cancel { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public int schedule { get; set; }
        public string lang { get; set; }
    }
   
    
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class VaccinationsController : ApiController
    {
        DbMasterEntities1 db = new DbMasterEntities1();

        public IEnumerable<reservation> GetReservations(int Id)
        {
            var data = db.ScheduleVaccReservationM_M.Where(a => a.VaccinationReservation.VaccReservation_Citizen_id == Id && a.VaccinationReservation.VaccReservation_isDeleted != true).Select(a => new reservation
            {
               
                 Reservation_date = a.VaccinationReservation.VaccReservation_date.ToString(),
                 checkup_date = a.ScheduleVaccination.checkup_date.ToString(),
                 checkup_end = a.ScheduleVaccination.checkup_end.Value.Hours.ToString() + ":" + a.ScheduleVaccination.checkup_end.Value.Minutes.ToString()+"0",
                 checkup_start = a.ScheduleVaccination.checkup_start.Value.Hours.ToString() + ":" + a.ScheduleVaccination.checkup_start.Value.Minutes.ToString()+"0",
                 citizen_first_name = a.VaccinationReservation.Citizen1.citizen_first_name,
                 citizen_second_name = a.VaccinationReservation.Citizen1.citizen_second_name,
                 citizen_third_name = a.VaccinationReservation.Citizen1.citizen_third_name,
                 citizen_fourth_name = a.VaccinationReservation.Citizen1.citizen_fourth_name,
                 citizen_first_name_arabic = a.VaccinationReservation.Citizen1.citizen_first_name_arabic,
                 citizen_second_name_arabic = a.VaccinationReservation.Citizen1.citizen_second_name_arabic,
                 citizen_third_name_arabic = a.VaccinationReservation.Citizen1.citizen_third_name_arabic,
                 citizen_fourth_name_arabic = a.VaccinationReservation.Citizen1.citizen_fourth_name,
                 healthcareReservation_id =a.VaccinationReservation.VaccReservation_id,
                 hospital_name= a.VaccinationReservation.HealthCare.hospital_name,
                 hospital_name_arabic= a.VaccinationReservation.HealthCare.hospital_name_arabic,
                 district_name = a.VaccinationReservation.VaccinationType.vaccination_type_name,
                 district_arabic_name = a.VaccinationReservation.VaccinationType.vaccination_type_name,
                 
            }) ; 
            return data;
        }
           

    
        public string PostVaccinationReservation(vacc vaccinationReservation )
        {
            var CitizenAge = db.Citizens.Find(vaccinationReservation.VaccReservation_Citizen_id).citizen_birthDate;
            var AgeonVacc = db.VaccinationTypes.Find(vaccinationReservation.VaccReservation_VaccinationType_id);



            

            vaccinationReservation.VaccReservation_date = db.ScheduleVaccinations.Find(vaccinationReservation.schedule).checkup_date;
            vaccinationReservation.VaccReservation_isDeleted = false;



            //---------------------------------------------------------------------



            int? reservationmax = db.ScheduleVaccinations.Find(vaccinationReservation.schedule).checkup_number;

            int maxreserv = db.ScheduleVaccReservationM_M.Where(a => a.ScheduleVaccReservation_schedule_id == vaccinationReservation.schedule).Join(db.VaccinationReservations, a => a.ScheduleVaccReservation_reservationVacc_id, b => b.VaccReservation_id, (a, b) => new { b }).Where(a => a.b.VaccReservation_VaccinationType_id == vaccinationReservation.VaccReservation_VaccinationType_id && a.b.VaccReservation_date == vaccinationReservation.VaccReservation_date && a.b.VaccReservation_HealthCare_id == vaccinationReservation.VaccReservation_HealthCare_id && a.b.VaccReservation_cancel != true).Count();

            if (maxreserv == reservationmax)
            {

                
                return Servicely.Languages.Language.day_is_full_choose_another_date;

            }





            var data = db.VaccinationReservations.Where(a => a.VaccReservation_Citizen_id == vaccinationReservation.VaccReservation_Citizen_id && a.VaccReservation_VaccinationType_id == vaccinationReservation.VaccReservation_VaccinationType_id && a.VaccReservation_checked == true && a.VaccReservation_date < DateTime.Now).FirstOrDefault();
            if (data != null)
            {

                

                return Servicely.Languages.Language.your_child_is_vaccinated;

            }
            var data1 = db.VaccinationReservations.Where(a => a.VaccReservation_Citizen_id == vaccinationReservation.VaccReservation_Citizen_id && a.VaccReservation_child_id == vaccinationReservation.VaccReservation_child_id && a.VaccReservation_VaccinationType_id == vaccinationReservation.VaccReservation_VaccinationType_id && ((a.VaccReservation_date.Value.Year >= DateTime.Now.Year && a.VaccReservation_date.Value.Month > DateTime.Now.Month) || (a.VaccReservation_date.Value.Year >= DateTime.Now.Year && a.VaccReservation_date.Value.Month == DateTime.Now.Month && a.VaccReservation_date.Value.Day >= DateTime.Now.Day)) && a.VaccReservation_cancel != true && a.VaccReservation_isDeleted != true).FirstOrDefault();

            if (data1 != null)
            {


                return Servicely.Languages.Language.you_are_already_reserved_on_this_day;
            }

            string cod = Guid.NewGuid().ToString().Substring(0, 5) + "-" + Guid.NewGuid().ToString().Substring(0, 5);

            VaccinationReservation vv = new VaccinationReservation();
            vv.VaccReservation_child_id = vaccinationReservation.VaccReservation_child_id;
            vv.VaccReservation_Citizen_id = vaccinationReservation.VaccReservation_Citizen_id;
            vv.VaccReservation_HealthCare_id = vaccinationReservation.VaccReservation_HealthCare_id;
            vv.VaccReservation_VaccinationType_id = vaccinationReservation.VaccReservation_VaccinationType_id;
            vv.VaccReservation_date = vaccinationReservation.VaccReservation_date;
            vv.VaccReservation_cancel = false;
            vv.VaccReservation_Code = cod;
            vv.VaccReservation_checked = false;
            vv.TransactionDate = DateTime.Now;
            db.VaccinationReservations.Add(vv);
            db.SaveChanges();
            ScheduleVaccReservationM_M mm = new ScheduleVaccReservationM_M();
            mm.ScheduleVaccReservation_reservationVacc_id = vv.VaccReservation_id;
            mm.ScheduleVaccReservation_schedule_id = vaccinationReservation.schedule;
            db.ScheduleVaccReservationM_M.Add(mm);
            db.SaveChanges();

            var father = db.Citizens.Find(vaccinationReservation.VaccReservation_Citizen_id);
            var child = db.Citizens.Find(vaccinationReservation.VaccReservation_child_id);
            // var address = db.Addresses.Where(a => a.address_isCurrent == true).Join(db.AddressCitizens, a => a.address_id, b => b.CA_AddressId, (a, b) => b).Where(a => a.CA_CitizenId == itemm.citizen_id).Select(a => a.Address.address_district_id).SingleOrDefault();
            //var hospital = db.HealthCares.Where(a => a.hospital_district_id == address).FirstOrDefault();
            var vaccSchedule = db.ScheduleVaccM_M.Where(a => a.scheduleVacc_vaccType_id == vaccinationReservation.VaccReservation_VaccinationType_id).Join(db.ScheduleVaccinations, a => a.scheduleVacc_schedule_id, b => b.schedule_id, (a, b) => new { b }).FirstOrDefault();
            var email = db.Contacts.Where(a => a.contact_citizen_id == vaccinationReservation.VaccReservation_Citizen_id).Join(db.Contact_Type, a => a.contact_contactType_id, b => b.contact_type_id, (a, b) => new { a, b }).Where(b => b.b.contact_type_name == "Email").Select(a => a.a).FirstOrDefault();
            int code = maxreserv;
            var HealthCare = db.HealthCares.Find(vaccinationReservation.VaccReservation_HealthCare_id);
            //if (email != null)
            //{
            //    //string Title = "Dear " + father.citizen_first_name + " " + father.citizen_second_name + " " + father.citizen_third_name + " " + father.citizen_fourth_name + " \n  " +
            //    //  "You have reserved an appointment to your chilled " + child.citizen_first_name + "( " + child.citizen_national_id + " ) on " + vaccSchedule.b.checkup_date.Value.ToShortDateString() + " from : " + vaccSchedule.b.checkup_start + "to : " + vaccSchedule.b.checkup_end + " \n\n" +
            //    //  " Health Care Name: " + HealthCare.hospital_name + " \n Health Care Address: " + HealthCare.District.Region.City.State.state_name + " " + HealthCare.District.Region.City.city_name + " " + HealthCare.District.Region.region_name + " " + HealthCare.District.district_name + " \n" + "Reservation Code: "
            //    //   + (code += 1) + " \n yours sincerely , ";
            //    //SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            //    //smtp.EnableSsl = true;
            //    //smtp.UseDefaultCredentials = false;
            //    //smtp.Credentials = new NetworkCredential("CSCE4502@gmail.com", "csce4502f16");
            //    //smtp.Send("CSCE4502@gmail.com", email.contact_data, "Vaccination", "Dear " + father.citizen_first_name + " " + father.citizen_second_name + " " + father.citizen_third_name + " " + father.citizen_fourth_name + " \n  " +
            //    //  "You have reserved an appointment to your chilled " + child.citizen_first_name + "( " + child.citizen_national_id + " ) on " + vaccSchedule.b.checkup_date.Value.ToShortDateString() + " from : " + vaccSchedule.b.checkup_start + "to : " + vaccSchedule.b.checkup_end + " \n\n" +
            //    //  " Health Care Name: " + HealthCare.hospital_name + " \n Health Care Address: " + HealthCare.District.Region.City.State.state_name + " " + HealthCare.District.Region.City.city_name + " " + HealthCare.District.Region.region_name + " " + HealthCare.District.district_name + " \n" + "Reservation Code: "
            //    //   + (code += 1) + " \n yours sincerely , ");

               
            //    return Servicely.Languages.Language.successfully_reserved_checkemail;
            //}




            return Servicely.Languages.Language.successfully_reserved;



        }
        public IHttpActionResult GetVaccinationReservationsCancelation(int citizenid, int VaccReservation_id, Boolean Ar)
        {
            var data = db.VaccinationReservations.Where(a => a.VaccReservation_isDeleted != true && a.VaccReservation_Citizen_id == citizenid && a.VaccReservation_id == VaccReservation_id && a.VaccReservation_date > DateTime.Now).SingleOrDefault();

            if (data == null)
            {
                if (Ar == true)
                {
                    return Ok("لا يمكن الغاء حجز قديم");
                }
                return Ok("Reservation appointment missed");

            }
            data.VaccReservation_cancel = true;
            db.SaveChanges();
            if (Ar == true)
            {
                return Ok("تم الغاء الحجز بنجاح");
            }
            return Ok("Reservation Canceled");


        }

        public reservation GetD(int DId1)
        {
            var data = db.ScheduleVaccReservationM_M.Where(a => a.VaccinationReservation.VaccReservation_id == DId1 && a.VaccinationReservation.VaccReservation_isDeleted != true).Select(a => new reservation
            {

                Reservation_date = a.VaccinationReservation.VaccReservation_date.ToString(),
                checkup_date = a.ScheduleVaccination.checkup_date.ToString(),
                checkup_end = a.ScheduleVaccination.checkup_end.Value.Hours.ToString() + ":" + a.ScheduleVaccination.checkup_end.Value.Minutes.ToString() + "0",
                checkup_start = a.ScheduleVaccination.checkup_start.Value.Hours.ToString() + ":" + a.ScheduleVaccination.checkup_start.Value.Minutes.ToString() + "0",
                citizen_first_name = a.VaccinationReservation.Citizen1.citizen_first_name,
                citizen_second_name = a.VaccinationReservation.Citizen1.citizen_second_name,
                citizen_third_name = a.VaccinationReservation.Citizen1.citizen_third_name,
                citizen_fourth_name = a.VaccinationReservation.Citizen1.citizen_fourth_name,
                citizen_first_name_arabic = a.VaccinationReservation.Citizen1.citizen_first_name_arabic,
                citizen_second_name_arabic = a.VaccinationReservation.Citizen1.citizen_second_name_arabic,
                citizen_third_name_arabic = a.VaccinationReservation.Citizen1.citizen_third_name_arabic,
                citizen_fourth_name_arabic = a.VaccinationReservation.Citizen1.citizen_fourth_name,
                healthcareReservation_id = a.VaccinationReservation.VaccReservation_id,
                hospital_name = a.VaccinationReservation.HealthCare.hospital_name,
                hospital_name_arabic = a.VaccinationReservation.HealthCare.hospital_name_arabic,
                district_name = a.VaccinationReservation.VaccinationType.vaccination_type_name,
                district_arabic_name = a.VaccinationReservation.VaccinationType.vaccination_type_name,

            }).SingleOrDefault();

            return data;
        }
        public string GetDelete(int DId)
        {
            VaccinationReservation healthcareReservation = db.VaccinationReservations.Find(DId);
            healthcareReservation.VaccReservation_isDeleted = true;
            db.SaveChanges();
            return "success";
        }
    }
}
