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
    public class VaccinationReservationsController : ApiController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();
        




        [ResponseType(typeof(VaccinationReservation))]
        // GET: api/VaccinationReservations
        public IHttpActionResult GetVaccinationReservations(int citizenid)
        {
            IEnumerable<VaccinationReservation>  data = db.VaccinationReservations.Where(a => a.VaccReservation_isDeleted != true && a.VaccReservation_Citizen_id == citizenid && a.VaccReservation_cancel != true);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);

            
        }



     
        // GET: api/VaccinationReservations
        public IHttpActionResult GetVaccinationReservationsCancelation(int citizenid, int VaccReservation_id, Boolean Ar)
        {
            var data = db.VaccinationReservations.Where(a => a.VaccReservation_isDeleted != true && a.VaccReservation_Citizen_id == citizenid && a.VaccReservation_id==VaccReservation_id&& a.VaccReservation_date>DateTime.Now).SingleOrDefault();

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
        // GET: api/VaccinationReservations/5
        [ResponseType(typeof(VaccinationReservation))]



        public string GetVaccinationReservation(int VaccReservation_id  , int VaccReservation_HealthCare_id ,int VaccReservation_VaccinationType_id , int VaccReservation_Citizen_id , int schedule, int VaccReservation_child_id,Boolean Ar)
        {
            var CitizenAge = db.Citizens.Find(VaccReservation_Citizen_id).citizen_birthDate;
            var AgeonVacc = db.VaccinationTypes.Find(VaccReservation_VaccinationType_id);



            VaccinationReservation vaccinationReservation = new VaccinationReservation();


            vaccinationReservation.VaccReservation_VaccinationType_id = VaccReservation_VaccinationType_id;
            vaccinationReservation.VaccReservation_Citizen_id = VaccReservation_Citizen_id;
            vaccinationReservation.VaccReservation_date = db.ScheduleVaccinations.Find(schedule).checkup_date;
            vaccinationReservation.VaccReservation_child_id = VaccReservation_child_id;
            vaccinationReservation.VaccReservation_HealthCare_id = VaccReservation_HealthCare_id;
            vaccinationReservation.VaccReservation_isDeleted = false;
            vaccinationReservation.VaccReservation_VaccinationType_id = VaccReservation_VaccinationType_id;
           


            //---------------------------------------------------------------------



            int? reservationmax = db.ScheduleVaccinations.Find(schedule).checkup_number;

            int maxreserv = db.ScheduleVaccReservationM_M.Where(a => a.ScheduleVaccReservation_schedule_id == schedule).Join(db.VaccinationReservations, a => a.ScheduleVaccReservation_reservationVacc_id, b => b.VaccReservation_id, (a, b) => new { b }).Where(a => a.b.VaccReservation_VaccinationType_id == VaccReservation_VaccinationType_id&& a.b.VaccReservation_date == vaccinationReservation.VaccReservation_date && a.b.VaccReservation_HealthCare_id == vaccinationReservation.VaccReservation_HealthCare_id && a.b.VaccReservation_cancel != true).Count();

            if (maxreserv == reservationmax)
            {

                 if(Ar== true)
                {
                    return "هذا اليوم ممتلئ اختر يوم اخر";
                }
                return Servicely.Languages.Language.day_is_full_choose_another_date;

            }





            var data = db.VaccinationReservations.Where(a => a.VaccReservation_Citizen_id == vaccinationReservation.VaccReservation_Citizen_id && a.VaccReservation_VaccinationType_id == VaccReservation_VaccinationType_id && a.VaccReservation_checked == true && a.VaccReservation_date < DateTime.Now).FirstOrDefault();
            if (data != null)
            {

                if (Ar == true)
                {
                    return "طفلك تم تطعيمة";
                }

                return Servicely.Languages.Language.your_child_is_vaccinated;

            }
            var data1 = db.VaccinationReservations.Where(a => a.VaccReservation_Citizen_id == vaccinationReservation.VaccReservation_Citizen_id && a.VaccReservation_child_id == vaccinationReservation.VaccReservation_child_id && a.VaccReservation_VaccinationType_id == VaccReservation_VaccinationType_id && ((a.VaccReservation_date.Value.Year >= DateTime.Now.Year&& a.VaccReservation_date.Value.Month > DateTime.Now.Month) ||(a.VaccReservation_date.Value.Year >= DateTime.Now.Year && a.VaccReservation_date.Value.Month == DateTime.Now.Month && a.VaccReservation_date.Value.Day >= DateTime.Now.Day ))&& a.VaccReservation_cancel!=true&& a.VaccReservation_isDeleted != true).FirstOrDefault();
            if (data1 != null)
            {

                if (Ar == true)
                {
                    return "لقد حجزت هذا اليوم بالفعل";
                }

                return Servicely.Languages.Language.you_are_already_reserved_on_this_day;
            }

            string cod = Guid.NewGuid().ToString().Substring(0, 5) + "-" + Guid.NewGuid().ToString().Substring(0, 5);

            vaccinationReservation.VaccReservation_Code =cod;
            vaccinationReservation.VaccReservation_checked = false;
            db.VaccinationReservations.Add(vaccinationReservation);
            db.SaveChanges();
            ScheduleVaccReservationM_M mm = new ScheduleVaccReservationM_M();
            mm.ScheduleVaccReservation_reservationVacc_id = vaccinationReservation.VaccReservation_id;
            mm.ScheduleVaccReservation_schedule_id = schedule;
            db.ScheduleVaccReservationM_M.Add(mm);
            db.SaveChanges();

            var father = db.Citizens.Find(VaccReservation_Citizen_id);
            var child = db.Citizens.Find(VaccReservation_child_id);
            // var address = db.Addresses.Where(a => a.address_isCurrent == true).Join(db.AddressCitizens, a => a.address_id, b => b.CA_AddressId, (a, b) => b).Where(a => a.CA_CitizenId == itemm.citizen_id).Select(a => a.Address.address_district_id).SingleOrDefault();
            //var hospital = db.HealthCares.Where(a => a.hospital_district_id == address).FirstOrDefault();
            var vaccSchedule = db.ScheduleVaccM_M.Where(a => a.scheduleVacc_vaccType_id == vaccinationReservation.VaccReservation_VaccinationType_id).Join(db.ScheduleVaccinations, a => a.scheduleVacc_schedule_id, b => b.schedule_id, (a, b) => new { b }).FirstOrDefault();
            var email = db.Contacts.Where(a => a.contact_citizen_id == VaccReservation_Citizen_id).Join(db.Contact_Type, a => a.contact_contactType_id, b => b.contact_type_id, (a, b) => new { a, b }).Where(b => b.b.contact_type_name == "Email").Select(a => a.a).FirstOrDefault();
            int code = maxreserv;
            var HealthCare = db.HealthCares.Find(vaccinationReservation.VaccReservation_HealthCare_id);
            if (email != null)
            {
                //string Title = "Dear " + father.citizen_first_name + " " + father.citizen_second_name + " " + father.citizen_third_name + " " + father.citizen_fourth_name + " \n  " +
                //  "You have reserved an appointment to your chilled " + child.citizen_first_name + "( " + child.citizen_national_id + " ) on " + vaccSchedule.b.checkup_date.Value.ToShortDateString() + " from : " + vaccSchedule.b.checkup_start + "to : " + vaccSchedule.b.checkup_end + " \n\n" +
                //  " Health Care Name: " + HealthCare.hospital_name + " \n Health Care Address: " + HealthCare.District.Region.City.State.state_name + " " + HealthCare.District.Region.City.city_name + " " + HealthCare.District.Region.region_name + " " + HealthCare.District.district_name + " \n" + "Reservation Code: "
                //   + (code += 1) + " \n yours sincerely , ";
                //SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                //smtp.EnableSsl = true;
                //smtp.UseDefaultCredentials = false;
                //smtp.Credentials = new NetworkCredential("CSCE4502@gmail.com", "csce4502f16");
                //smtp.Send("CSCE4502@gmail.com", email.contact_data, "Vaccination", "Dear " + father.citizen_first_name + " " + father.citizen_second_name + " " + father.citizen_third_name + " " + father.citizen_fourth_name + " \n  " +
                //  "You have reserved an appointment to your chilled " + child.citizen_first_name + "( " + child.citizen_national_id + " ) on " + vaccSchedule.b.checkup_date.Value.ToShortDateString() + " from : " + vaccSchedule.b.checkup_start + "to : " + vaccSchedule.b.checkup_end + " \n\n" +
                //  " Health Care Name: " + HealthCare.hospital_name + " \n Health Care Address: " + HealthCare.District.Region.City.State.state_name + " " + HealthCare.District.Region.City.city_name + " " + HealthCare.District.Region.region_name + " " + HealthCare.District.district_name + " \n" + "Reservation Code: "
                //   + (code += 1) + " \n yours sincerely , ");

                if (Ar == true)
                {
                    return "تم الحجز بنجاح ,افحص البريد الالكتروني الخاص بك";
                }
                return Servicely.Languages.Language.successfully_reserved_checkemail;
            }


            if (Ar == true)
            {
                return "تم الحجز بنجاح";
            }


            return Servicely.Languages.Language.successfully_reserved;


            
        }

        // PUT: api/VaccinationReservations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVaccinationReservation(int id, VaccinationReservation vaccinationReservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vaccinationReservation.VaccReservation_id)
            {
                return BadRequest();
            }

            //db.Entry(vaccinationReservation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VaccinationReservationExists(id))
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

        // POST: api/VaccinationReservations
        [ResponseType(typeof(VaccinationReservation))]
        public IHttpActionResult PostVaccinationReservation(VaccinationReservation vaccinationReservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VaccinationReservations.Add(vaccinationReservation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vaccinationReservation.VaccReservation_id }, vaccinationReservation);
        }

        // DELETE: api/VaccinationReservations/5
        [ResponseType(typeof(VaccinationReservation))]
        public IHttpActionResult DeleteVaccinationReservation(int id)
        {
            VaccinationReservation vaccinationReservation = db.VaccinationReservations.Find(id);
            if (vaccinationReservation == null)
            {
                return NotFound();
            }

            db.VaccinationReservations.Remove(vaccinationReservation);
            db.SaveChanges();

            return Ok(vaccinationReservation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VaccinationReservationExists(int id)
        {
            return db.VaccinationReservations.Count(e => e.VaccReservation_id == id) > 0;
        }
    }
}