using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Servicely.Models;

namespace Servicely.Api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HealthcareReservationsController : ApiController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();
        [ResponseType(typeof(VaccinationReservation))]
        // GET: api/VaccinationReservations
        public IHttpActionResult GetDoctorReservations(int citizenid)
        {
            IEnumerable<CustomListofDoctorReservation> data = db.HealthcareReservations.Where(a => a.healthcareReservation_isDeleted != true && a.healthcareReservation_cancel != true && a.healthcareReservation_citizen_id == citizenid).Select(a => new CustomListofDoctorReservation { healthcareReservation_id = a.healthcareReservation_id, healthcareReservation_doctor_id = a.healthcareReservation_doctor_id, healthcareReservation_hospital_id = a.healthcareReservation_hospital_id, healthcareReservation_Code = a.healthcareReservation_Code, Reservation_date = a.Reservation_date, specialization_name = a.Healthcare_Doctor.HealthCareSpecialization.specialization_name, specialization_name_arabic = a.Healthcare_Doctor.HealthCareSpecialization.specialization_name_arabic });

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);


        }

        public IHttpActionResult GetDoctorReservationsCancelation(int citizenid, int Reservation_id ,Boolean Ar)
        {
            var data = db.HealthcareReservations.Where(a => a.healthcareReservation_isDeleted != true && a.healthcareReservation_citizen_id == citizenid && a.healthcareReservation_id == Reservation_id && a.Reservation_date > DateTime.Now).SingleOrDefault();

            if (data == null)
            {
                if(Ar== true)
                {
                    return Ok("لا يمكن الغاء حجز قديم");
                }
                return Ok("An old reservation cannot be canceled");
            }
            data.healthcareReservation_cancel = true;
            db.SaveChanges();
            if (Ar == true)
            {
                return Ok("تم الغاء الحجز بنجاح");
            }
            return Ok("Reservation Canceled");


        }

        // GET: api/HealthcareReservations
        public IQueryable<HealthcareReservation> GetHealthcareReservations()
        {
            return db.HealthcareReservations;
        }

        // GET: api/HealthcareReservations/5
        [ResponseType(typeof(HealthcareReservation))]
        public string GetHealthcareReservation(int healthcareReservation_id, int healthcareReservation_citizen_id , int healthcareReservation_doctor_id , int healthcareReservation_hospital_id , int schedule ,Boolean Ar)
        {

          
            
            HealthcareReservation healthcareReservation = new HealthcareReservation();
            var data = db.HealthcareReservations.Where(a => a.healthcareReservation_citizen_id == healthcareReservation_citizen_id && a.healthcareReservation_doctor_id == healthcareReservation_doctor_id && a.healthcareReservation_hospital_id == healthcareReservation_hospital_id && a.healthcareReservation_cancel !=true && a.healthcareReservation_isDeleted != true ).Join(db.ReservationScheduleM_M,a=>a.healthcareReservation_id,b=> b.reservationSchedule_reservation , (a,b)=> new { b}).Where(y => y.b.reservationSchedule_schedule ==schedule).FirstOrDefault();
            if (data != null)
            {
                if(Ar==true)
                {
                    return "لقد حجزت بالفعل في هذا اليوم";
                }
                
                return Servicely.Languages.Language.you_are_already_reserved_on_this_day;

            }



            int? reservationmax = db.ScheduleHealthCares.Find(schedule).checkup_number;

            int maxreserv = db.ReservationScheduleM_M.Where(a => a.reservationSchedule_schedule == schedule).Join(db.HealthcareReservations, a => a.reservationSchedule_reservation, b => b.healthcareReservation_id, (a, b) => new { b }).Where(a => a.b.healthcareReservation_hospital_id == healthcareReservation.healthcareReservation_hospital_id && a.b.Reservation_date == healthcareReservation.Reservation_date && a.b.healthcareReservation_doctor_id == healthcareReservation_doctor_id && a.b.healthcareReservation_cancel!= true).Count();

            if (maxreserv == reservationmax)
            {
                if (Ar == true)
                {
                    return "هذا اليوم ممتلئ اختر يوم اخر";
                }

                return Servicely.Languages.Language.day_is_full_choose_another_date;

            }


            string cod = Guid.NewGuid().ToString().Substring(0, 5) + "-" + Guid.NewGuid().ToString().Substring(0, 5);


            healthcareReservation.healthcareReservation_citizen_id = healthcareReservation_citizen_id;
            healthcareReservation.healthcareReservation_hospital_id = healthcareReservation_hospital_id;
            healthcareReservation.healthcareReservation_doctor_id = healthcareReservation_doctor_id;
            healthcareReservation.healthcareReservation_Code =cod;
            healthcareReservation.Reservation_date = db.ScheduleHealthCares.Find(schedule).checkup_date;
            db.HealthcareReservations.Add(healthcareReservation);
            db.SaveChanges();
            ReservationScheduleM_M r = new ReservationScheduleM_M();
            r.reservationSchedule_reservation = healthcareReservation.healthcareReservation_id;
            r.reservationSchedule_schedule = schedule;
            db.ReservationScheduleM_M.Add(r);
            db.SaveChanges();
            /*
            var father = db.Citizens.Find(healthcareReservation_citizen_id);
            var vaccSchedule = db.ScheduleHealthCares.Where(a => a.schedule_id == schedule).FirstOrDefault();
            var email = db.Contacts.Where(a => a.contact_citizen_id == healthcareReservation_citizen_id).Join(db.Contact_Type, a => a.contact_contactType_id, b => b.contact_type_id, (a, b) => new { a, b }).Where(b => b.b.contact_type_name == "Email").Select(a => a.a).FirstOrDefault();
            int code = maxreserv;
            var HealthCare = db.HealthCares.Find(healthcareReservation.healthcareReservation_hospital_id);
            if (email != null)
            {
                string Title = "Dear " + father.citizen_first_name + " " + father.citizen_second_name + " " + father.citizen_third_name + " " + father.citizen_fourth_name + " \n  " +
                  "You have reserved an appointment on " + vaccSchedule.checkup_date.Value.ToShortDateString() + " from : " + vaccSchedule.checkup_start + " to : " + vaccSchedule.checkup_end + " \n\n" +
                  " Health Care Name: " + HealthCare.hospital_name + " \n Health Care Address: " + HealthCare.District.Region.City.State.state_name + " " + HealthCare.District.Region.City.city_name + " " + HealthCare.District.Region.region_name + " " + HealthCare.District.district_name + " \n" + "Reservation Code: "
                   + (code += 1) + " \n yours sincerely , ";
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("CSCE4502@gmail.com", "csce4502f16");
                smtp.Send("CSCE4502@gmail.com", email.contact_data, "Vaccination", Title);
               
            }
            */
            if (Ar == true)
            {
                return "تم الحجز بنجاح , راجع بريدك الالكتروني";
            }
            return Servicely.Languages.Language.successfully_reserved_checkemail;





        }

        // PUT: api/HealthcareReservations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHealthcareReservation(int id, HealthcareReservation healthcareReservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != healthcareReservation.healthcareReservation_id)
            {
                return BadRequest();
            }

           // db.Entry(healthcareReservation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HealthcareReservationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/HealthcareReservations
        [ResponseType(typeof(HealthcareReservation))]
        public IHttpActionResult PostHealthcareReservation(HealthcareReservation healthcareReservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HealthcareReservations.Add(healthcareReservation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = healthcareReservation.healthcareReservation_id }, healthcareReservation);
        }

        // DELETE: api/HealthcareReservations/5
        [ResponseType(typeof(HealthcareReservation))]
        public IHttpActionResult DeleteHealthcareReservation(int id)
        {
            HealthcareReservation healthcareReservation = db.HealthcareReservations.Find(id);
            if (healthcareReservation == null)
            {
                return NotFound();
            }

            db.HealthcareReservations.Remove(healthcareReservation);
            db.SaveChanges();

            return Ok(healthcareReservation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HealthcareReservationExists(int id)
        {
            return db.HealthcareReservations.Count(e => e.healthcareReservation_id == id) > 0;
        }
    }
}