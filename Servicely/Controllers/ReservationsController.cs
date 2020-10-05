using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Servicely.Models;

namespace Servicely.Controllers
{


    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ReservationsController : ApiController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: api/Reservations
        public IQueryable<Reservation> GetReservations()
        {
            return db.Reservations;
        }

        // GET: api/Reservations/5
        [ResponseType(typeof(Reservation))]
        public IHttpActionResult GetReservation(int reservation_citizen_id)
        {
           IEnumerable< Reservation> reservation = db.Reservations.Where(a=> a.reservation_citizen_id == reservation_citizen_id&&a.reservation_isDeleted!=true);
            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        // PUT: api/Reservations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReservation(int id, Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reservation.reservation_id)
            {
                return BadRequest();
            }

            //db.Entry(reservation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
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

        // POST: api/Reservations
        [ResponseType(typeof(Reservation))]
        public String GetReservation(int reservation_id, Nullable<System.DateTime>  reservation_date, int reservation_office_id, int reservation_citizen_id, int service_id,Boolean Ar)
        {


            //if( db.Reservations.Where(a => a.reservation_date == reservation_date && a.reservation_document_type_id == reservation_document_type_id && a.reservation_office_id == reservation_office_id  ).Count() >50 )
            //{
            //    return "the day is full please select another day";
            //}


            //var data = db.Reservations.Where(a=> a.reservation_citizen_id == reservation_citizen_id && a.reservation_date==reservation_date && a.reservation_office_id==reservation_office_id).SingleOrDefault();

            //if( data != null)
            //{
            //    return "Already reserved on this day ";
            //}


            var d = db.services.Find(service_id);
            int? sumService = d.service_time;
            //Session["CitizenId"] = 1 ;
            //u.reservation_citizen_id = (int)Session["CitizenId"];
            var data = db.Reservations.Where(a => a.reservation_date == reservation_date && a.reservation_office_id == reservation_office_id).Join(db.services, a => a.service_id, b => b.service_id, (a, b) => new { b.service_time });
            foreach (var item in data)
            {
                sumService += item.service_time;
            }

            var govern = db.services.Where(a => a.service_id == service_id).Select(a => a.goverenemnt_id).SingleOrDefault();
            var gov = db.governement_body.Find(d.goverenemnt_id);

            TimeSpan? ss = gov.end_time - gov.start_time;
            double time = ss.Value.Hours;
            double ddd = (double)sumService / 60;
            if (ddd > time)
            {


                if(Ar==true)
                {
                    return "هذا اليوم ممتلئ";
                }

                return "*day full, choose another day";
            }


            int cID = reservation_citizen_id;
            var dataCitizen = db.Reservations.Where(a => a.reservation_date == reservation_date && a.reservation_office_id == reservation_office_id && a.reservation_citizen_id == cID).SingleOrDefault();

            if (dataCitizen != null)
            {

                if (Ar == true)
                {
                    return "بالفعل لديك حجز في هذا اليوم ";
                }
                return "you have reserved in this office once at that date";
            }




            Reservation reservation = new Reservation();
            reservation.reservation_id = reservation_id;
            reservation.reservation_citizen_id = reservation_citizen_id;
            reservation.service_id = service_id;
           // reservation.reservation_document_type_id = reservation_document_type_id;
            reservation.reservation_office_id = reservation_office_id;
            reservation.reservation_date = reservation_date;
            if (!ModelState.IsValid)
            {
               // return BadRequest(ModelState);
            }

            db.Reservations.Add(reservation);
            db.SaveChanges();
            if (Ar == true)
            {
                return "تم الحجز بنجاح";
            }
            return "Successful Reserved";
            //  return CreatedAtRoute("DefaultApi", new { id = reservation.reservation_id }, reservation);
        }



        // atm api 
        [ResponseType(typeof(Reservation))]
        public String PostReservation(Reservation r)
        {




            var d = db.services.Find(r.service_id);
            int? sumService = d.service_time;
            //Session["CitizenId"] = 1 ;
            //u.reservation_citizen_id = (int)Session["CitizenId"];
            var data = db.Reservations.Where(a => a.reservation_date == r.reservation_date && a.reservation_office_id == r.reservation_office_id).Join(db.services, a => a.service_id, b => b.service_id, (a, b) => new { b.service_time });
            foreach (var item in data)
            {
                sumService += item.service_time;
            }

            var govern = db.services.Where(a => a.service_id == r.service_id).Select(a => a.goverenemnt_id).SingleOrDefault();
            var gov = db.governement_body.Find(d.goverenemnt_id);

            TimeSpan? ss = gov.end_time - gov.start_time;
            double time = ss.Value.Hours;
            double ddd = (double)sumService / 60;

            if (ddd > 2)
            {

                return "1";//"*day full";
            }


            int? cID = r.reservation_citizen_id;
            var dataCitizen = db.Reservations.Where(a => a.reservation_date == r.reservation_date && a.reservation_office_id == r.reservation_office_id && a.reservation_citizen_id == cID).SingleOrDefault();

            if (dataCitizen != null)
            {

                return "2";//"you have reserved in this office once at that date";
            }

            r.TransactionDate = DateTime.Now;
            db.Reservations.Add(r);
            db.SaveChanges();

            return "3"; // "Successful Reserved";
            //  return CreatedAtRoute("DefaultApi", new { id = reservation.reservation_id }, reservation);
        }

        // DELETE: api/Reservations/5
        [ResponseType(typeof(Reservation))]
        public IHttpActionResult DeleteReservation(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return NotFound();
            }

            db.Reservations.Remove(reservation);
            db.SaveChanges();

            return Ok(reservation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReservationExists(int id)
        {
            return db.Reservations.Count(e => e.reservation_id == id) > 0;
        }
    }
}