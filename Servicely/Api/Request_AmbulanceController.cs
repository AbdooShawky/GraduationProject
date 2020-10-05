using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Servicely.Models;

namespace Servicely.Api
{
    public class Request_AmbulanceController : ApiController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();


        // GET: api/Request_Ambulance
        public IEnumerable<Request_Ambulance> GetRequest_Ambulance()
        {
          return db.Request_Ambulance.Where(a => a.request_isDeleted != true && a.request_isFinshed != true);
       }

        // GET: api/Request_Ambulance/5
        [ResponseType(typeof(Request_Ambulance))]
        public IHttpActionResult GetRequest_Ambulance(int id)
        {
            Request_Ambulance Request_Ambulance = db.Request_Ambulance.Find(id);
            if (Request_Ambulance == null)
            {
                return NotFound();
            }

            return Ok(Request_Ambulance);
        }

        public IHttpActionResult GetRequests_Ambulance( int RId, Boolean Ar)
        {
            var data = db.Request_Ambulance.Where(a => a.request_isDeleted != true && a.Id == RId && a.request_isFinshed != true).SingleOrDefault();

           if (data == null)
              {
                if (Ar == true)
             {
                    return Ok("لا يمكن الاستجابة للطلب");
               }
               return Ok("Request missed");

           }
           data.request_isFinshed = true;
            db.SaveChanges();
            if (Ar == true)
            {
                return Ok("تم انهاء الطلب بنجاح ");
            }
            return Ok("Request Finshed");


       }


        //// GET: api/Cities/5
        [ResponseType(typeof(Request_Ambulance))]
        public IHttpActionResult GetRequests_Ambulance(Boolean Ar)
        {
            IEnumerable<Request_Ambulance> request_Ambulance = db.Request_Ambulance.Where(a => a.request_isDeleted != true && a.request_isFinshed == true);
          if (request_Ambulance == null)
          {
                return NotFound();
          }

        return Ok(request_Ambulance);
        }

        // GET: api/Cities/5
        [ResponseType(typeof(Request_Ambulance))]
        public IHttpActionResult GetRequests_Ambulance(int cId)
        {
               //City city = db.Cities.Find(id);
               IEnumerable<Request_Ambulance> request_Ambulance = db.Request_Ambulance.Where(a => a.request_citizenId == cId && a.request_isDeleted != true);
               if (request_Ambulance == null)
               {
                   return NotFound();
               }

              return Ok(request_Ambulance);
        }

        [ResponseType(typeof(Request_Ambulance))]
        public string GetRequest_Ambulance(int cId, decimal lat, decimal lon, string details, string Ar)
        {


            Request_Ambulance request_Ambulance = new Request_Ambulance();
            request_Ambulance.request_citizenId = cId;
            request_Ambulance.location_latitude =lat;
            request_Ambulance.location_longitude =lon;
            request_Ambulance.request_details = details;
            request_Ambulance.request_Date = DateTime.Now;

            db.Request_Ambulance.Add(request_Ambulance);
            db.SaveChanges();
            if (Boolean.Parse(Ar) == true)
            {
                return "تم ارسال الطلب بنجاح ,سوف نصل في اقرب وقت";
            }
            return "The request has been sent successfully. We will arrive soon";
        }




        // PUT: api/Request_Ambulance/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRequest_Ambulance(int id, Request_Ambulance request_Ambulance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request_Ambulance.Id)
            {
                return BadRequest();
            }

            db.Entry(request_Ambulance).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Request_AmbulanceExists(id))
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

        // POST: api/Request_Ambulance
        [ResponseType(typeof(Request_Ambulance))]
        public IHttpActionResult PostRequest_Ambulance(Request_Ambulance request_Ambulance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Request_Ambulance.Add(request_Ambulance);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Request_AmbulanceExists(request_Ambulance.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = request_Ambulance.Id }, request_Ambulance);
        }

        // DELETE: api/Request_Ambulance/5
        [ResponseType(typeof(Request_Ambulance))]
        public IHttpActionResult DeleteRequest_Ambulance(int id)
        {
            Request_Ambulance request_Ambulance = db.Request_Ambulance.Find(id);
            if (request_Ambulance == null)
            {
                return NotFound();
            }

            db.Request_Ambulance.Remove(request_Ambulance);
            db.SaveChanges();

            return Ok(request_Ambulance);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Request_AmbulanceExists(int id)
        {
            return db.Request_Ambulance.Count(e => e.Id == id) > 0;
        }
    }
}