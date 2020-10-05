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
  

    public class Health
    {
        public int healthcareReservation_id { get; set; }
        public Nullable<int> healthcareReservation_hospital_id { get; set; }
        public Nullable<System.DateTime> Reservation_date { get; set; }
        public Nullable<bool> healthcareReservation_isDeleted { get; set; }
        public Nullable<int> healthcareReservation_doctor_id { get; set; }
        public Nullable<int> healthcareReservation_citizen_id { get; set; }
        public string healthcareReservation_Code { get; set; }
        public Nullable<bool> healthcareReservation_cancel { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public bool Ar { get; set; }
        public int schedule { get; set; }
    }
    public class reservation
    {

        public string checkup_end { get; set; }
        public string checkup_start { get; set; }
        public string citizen_first_name1 { get; set; }
        public string citizen_second_name1 { get; set; }
        public string citizen_third_name1 { get; set; }
        public string citizen_fourth_name1 { get; set; }
        public string citizen_first_name_arabic1 { get; set; }
        public string citizen_second_name_arabic1 { get; set; }
        public string citizen_third_name_arabic1 { get; set; }
        public string citizen_fourth_name_arabic1 { get; set; }
        public string citizen_first_name { get; set; }
        public string citizen_second_name { get; set; }
        public string citizen_third_name { get; set; }
        public string citizen_fourth_name { get; set; }
        public string citizen_first_name_arabic { get; set; }
        public string citizen_second_name_arabic { get; set; }
        public string citizen_third_name_arabic { get; set; }
        public string citizen_fourth_name_arabic { get; set; }
        public string hospital_name { get; set; }
        public string hospital_name_arabic { get; set; }
        public string district_name { get; set; }
        public string district_arabic_name { get; set; }
        public string Reservation_date { get; set; }
        public string checkup_date { get; set; }
        public int healthcareReservation_id { get; set; }


    }
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DoctorReservationController : ApiController
    {
        DbMasterEntities1 db = new DbMasterEntities1();
        [HttpPost]
        public string PostHealthcareReservation( Health health)
        {

            //int cid = 9;//(int) Session["citizenID"] ;

            HealthcareReservation healthcareReservation = new HealthcareReservation();
            var data = db.HealthcareReservations.Where(a => a.healthcareReservation_citizen_id == health.healthcareReservation_citizen_id && a.healthcareReservation_doctor_id == health.healthcareReservation_doctor_id && a.healthcareReservation_hospital_id == health.healthcareReservation_hospital_id && a.healthcareReservation_cancel != true).FirstOrDefault();
            if (data != null)
            {
                if (health.Ar == true)
                {
                    return "لقد حجزت بالفعل في هذا اليوم";
                }

                return Servicely.Languages.Language.you_are_already_reserved_on_this_day;

            }



            int? reservationmax = db.ScheduleHealthCares.Find(health.schedule).checkup_number;

            int maxreserv = db.ReservationScheduleM_M.Where(a => a.reservationSchedule_schedule == health.schedule).Join(db.HealthcareReservations, a => a.reservationSchedule_reservation, b => b.healthcareReservation_id, (a, b) => new { b }).Where(a => a.b.healthcareReservation_hospital_id == healthcareReservation.healthcareReservation_hospital_id && a.b.Reservation_date == healthcareReservation.Reservation_date && a.b.healthcareReservation_doctor_id == health.healthcareReservation_doctor_id && a.b.healthcareReservation_cancel != true).Count();

            if (maxreserv == reservationmax)
            {
                if (health.Ar == true)
                {
                    return "هذا اليوم ممتلئ اختر يوم اخر";
                }

                return Servicely.Languages.Language.day_is_full_choose_another_date;

            }


            string cod = Guid.NewGuid().ToString().Substring(0, 5) + "-" + Guid.NewGuid().ToString().Substring(0, 5);


            healthcareReservation.healthcareReservation_citizen_id = health.healthcareReservation_citizen_id;
            healthcareReservation.healthcareReservation_hospital_id = health.healthcareReservation_hospital_id;
            healthcareReservation.healthcareReservation_doctor_id = health.healthcareReservation_doctor_id;
            healthcareReservation.healthcareReservation_Code = cod;
            healthcareReservation.Reservation_date = db.ScheduleHealthCares.Find(health.schedule).checkup_date;
            db.HealthcareReservations.Add(healthcareReservation);
            db.SaveChanges();
            ReservationScheduleM_M r = new ReservationScheduleM_M();
            r.reservationSchedule_reservation = healthcareReservation.healthcareReservation_id;
            r.reservationSchedule_schedule = health.schedule;
            db.ReservationScheduleM_M.Add(r);
            db.SaveChanges();

            var father = db.Citizens.Find(health.healthcareReservation_citizen_id);
            var vaccSchedule = db.ScheduleHealthCares.Where(a => a.schedule_id == health.schedule).FirstOrDefault();
            var email = db.Contacts.Where(a => a.contact_citizen_id == health.healthcareReservation_citizen_id).Join(db.Contact_Type, a => a.contact_contactType_id, b => b.contact_type_id, (a, b) => new { a, b }).Where(b => b.b.contact_type_name == "Email").Select(a => a.a).FirstOrDefault();
            int code = maxreserv;
            var HealthCare = db.HealthCares.Find(healthcareReservation.healthcareReservation_hospital_id);
            if (email != null)
            {
            //    string Title = "Dear " + father.citizen_first_name + " " + father.citizen_second_name + " " + father.citizen_third_name + " " + father.citizen_fourth_name + " \n  " +
            //      "You have reserved an appointment on " + vaccSchedule.checkup_date.Value.ToShortDateString() + " from : " + vaccSchedule.checkup_start + " to : " + vaccSchedule.checkup_end + " \n\n" +
            //      " Health Care Name: " + HealthCare.hospital_name + " \n Health Care Address: " + HealthCare.District.Region.City.State.state_name + " " + HealthCare.District.Region.City.city_name + " " + HealthCare.District.Region.region_name + " " + HealthCare.District.district_name + " \n" + "Reservation Code: "
            //       + (code += 1) + " \n yours sincerely , ";
            //    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            //    smtp.EnableSsl = true;
            //    smtp.UseDefaultCredentials = false;
            //    smtp.Credentials = new NetworkCredential("CSCE4502@gmail.com", "csce4502f16");
            //    smtp.Send("CSCE4502@gmail.com", email.contact_data, "Vaccination", Title);

            }

            if (health.Ar == true)
            {
                return "تم الحجز بنجاح , راجع بريدك الالكتروني";
            }
            return Servicely.Languages.Language.successfully_reserved_checkemail;





        }
        public IEnumerable<reservation> GetIndexOfUser(int Id)
        {

            int cid = Id;
            db.Configuration.ProxyCreationEnabled = false;
            var healthcareReservations = db.ReservationScheduleM_M.Join(db.ScheduleHealthCares, a => a.reservationSchedule_schedule, b => b.schedule_id, (a, b) => new { a, b }).Join(db.HealthcareReservations, a => a.a.reservationSchedule_reservation, b => b.healthcareReservation_id, (x, y) => new { x, y }).Where(a => a.y.healthcareReservation_citizen_id == cid && a.y.healthcareReservation_isDeleted != true && a.y.healthcareReservation_id == a.x.a.reservationSchedule_reservation && a.x.a.reservationSchedule_schedule == a.x.b.schedule_id).Select(a => a.x.a).Select(a =>
            new reservation
            {
                healthcareReservation_id = a.HealthcareReservation.healthcareReservation_id,
                Reservation_date = a.HealthcareReservation.Reservation_date.ToString(),
                checkup_date = a.ScheduleHealthCare.checkup_date.ToString(),
                citizen_first_name = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_first_name,
                citizen_second_name = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_second_name,
                citizen_third_name = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_third_name,
                citizen_fourth_name = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_fourth_name,
                citizen_first_name_arabic = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_first_name_arabic,
                citizen_second_name_arabic = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_second_name_arabic,
                citizen_third_name_arabic = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_third_name_arabic,
                citizen_fourth_name_arabic = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_fourth_name_arabic,
                hospital_name = a.HealthcareReservation.HealthCare.hospital_name,
                hospital_name_arabic = a.HealthcareReservation.HealthCare.hospital_name_arabic,
                district_name = a.HealthcareReservation.HealthCare.District.district_name,
                district_arabic_name = a.HealthcareReservation.HealthCare.District.district_arabic_name,
                checkup_end = a.ScheduleHealthCare.checkup_end.Value.Hours.ToString() + ":" + a.ScheduleHealthCare.checkup_end.Value.Minutes.ToString(),
                checkup_start = a.ScheduleHealthCare.checkup_start.Value.Hours.ToString() + ":" + a.ScheduleHealthCare.checkup_start.Value.Minutes.ToString(),

            }

            );

            return healthcareReservations;
        }
       
        public string GetDelete(int DId)
        {
            HealthcareReservation healthcareReservation = db.HealthcareReservations.Find(DId);
            healthcareReservation.healthcareReservation_isDeleted = true;
            db.SaveChanges();
            return "success";
        }

        public reservation GetD(int DId1)
        {
            var healthcareReservations = db.ReservationScheduleM_M.Join(db.ScheduleHealthCares, a => a.reservationSchedule_schedule, b => b.schedule_id, (a, b) => new { a, b }).Join(db.HealthcareReservations, a => a.a.reservationSchedule_reservation, b => b.healthcareReservation_id, (x, y) => new { x, y }).Where(a =>  a.y.healthcareReservation_isDeleted != true && a.y.healthcareReservation_id == DId1).Select(a => a.x.a).Select(a =>
            new reservation
            {
                healthcareReservation_id = a.HealthcareReservation.healthcareReservation_id,
                Reservation_date = a.HealthcareReservation.Reservation_date.ToString(),
                checkup_date = a.ScheduleHealthCare.checkup_date.ToString(),
                citizen_first_name = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_first_name,
                citizen_second_name = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_second_name,
                citizen_third_name = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_third_name,
                citizen_fourth_name = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_fourth_name,
                citizen_first_name_arabic = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_first_name_arabic,
                citizen_second_name_arabic = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_second_name_arabic,
                citizen_third_name_arabic = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_third_name_arabic,
                citizen_fourth_name_arabic = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_fourth_name_arabic,
                hospital_name = a.HealthcareReservation.HealthCare.hospital_name,
                hospital_name_arabic = a.HealthcareReservation.HealthCare.hospital_name_arabic,
                district_name = a.HealthcareReservation.HealthCare.District.district_name,
                district_arabic_name = a.HealthcareReservation.HealthCare.District.district_arabic_name,
                checkup_end = a.ScheduleHealthCare.checkup_end.Value.Hours.ToString() + ":" + a.ScheduleHealthCare.checkup_end.Value.Minutes.ToString(),
                checkup_start = a.ScheduleHealthCare.checkup_start.Value.Hours.ToString() + ":" + a.ScheduleHealthCare.checkup_start.Value.Minutes.ToString(),

            }).SingleOrDefault();
            return healthcareReservations;
        }
        public IEnumerable<reservation> GetIndexOfUserU( int Id, DateTime from, DateTime to)
    {

        int cid = Id;
        db.Configuration.ProxyCreationEnabled = false;
        var healthcareReservations = db.ReservationScheduleM_M.Join(db.ScheduleHealthCares, a => a.reservationSchedule_schedule, b => b.schedule_id, (a, b) => new { a, b }).Join(db.HealthcareReservations, a => a.a.reservationSchedule_reservation, b => b.healthcareReservation_id, (x, y) => new { x, y }).Where(a => a.y.healthcareReservation_citizen_id == cid && a.y.healthcareReservation_isDeleted != true && a.y.healthcareReservation_id == a.x.a.reservationSchedule_reservation && a.x.a.reservationSchedule_schedule == a.x.b.schedule_id && a.y.Reservation_date >= from && a.y.Reservation_date <= to && a.y.healthcareReservation_citizen_id == cid).Select(a => a.x.a).Select(a =>
        new reservation
        {
            healthcareReservation_id = a.HealthcareReservation.healthcareReservation_id,
            Reservation_date = a.HealthcareReservation.Reservation_date.ToString(),
            checkup_date = a.ScheduleHealthCare.checkup_date.ToString(),
            citizen_first_name = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_first_name,
            citizen_second_name = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_second_name,
            citizen_third_name = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_third_name,
            citizen_fourth_name = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_fourth_name,
            citizen_first_name_arabic = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_first_name_arabic,
            citizen_second_name_arabic = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_second_name_arabic,
            citizen_third_name_arabic = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_third_name_arabic,
            citizen_fourth_name_arabic = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_fourth_name_arabic,
            hospital_name = a.HealthcareReservation.HealthCare.hospital_name,
            hospital_name_arabic = a.HealthcareReservation.HealthCare.hospital_name_arabic,
            district_name = a.HealthcareReservation.HealthCare.District.district_name,
            district_arabic_name = a.HealthcareReservation.HealthCare.District.district_arabic_name,
            checkup_end = a.ScheduleHealthCare.checkup_end.Value.Hours.ToString() + ":" + a.ScheduleHealthCare.checkup_end.Value.Minutes.ToString(),
            checkup_start = a.ScheduleHealthCare.checkup_start.Value.Hours.ToString() + ":" + a.ScheduleHealthCare.checkup_start.Value.Minutes.ToString(),

        }

        );

        return healthcareReservations;
    }


}
}
