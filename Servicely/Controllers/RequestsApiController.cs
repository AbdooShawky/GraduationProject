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

    public class RequestsApiController : ApiController
    {

        private DbMasterEntities1 db = new DbMasterEntities1();
       
        // GET: api/RequestsApi
        public IEnumerable<Request> GetRequests()
        {
            return db.Requests ;
        }

        // GET: api/RequestsApi/5
        [ResponseType(typeof(Request))]
        public IHttpActionResult GetRequest(int request_citizenId)
        {
           IEnumerable< Request> request = db.Requests.Where(a=>a.request_citizenId  == request_citizenId && a.Is_Deleted !=true );
            if (request == null)
            {
                return NotFound();
            }

            return Ok(request);
        }

        // PUT: api/RequestsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRequest(int id, Request request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.request_id)
            {
                return BadRequest();
            }

           // db.Entry(request).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
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

        // POST: api/RequestsApi
        [ResponseType(typeof(Request))]
        public String GetRequest( int request_id , int  request_citizenId, string address, int governmentAgency , int service, string language, int quantity, int typeRequest, System.DateTime date,Boolean Ar )
        {


            var requestData = db.Requests.Where(a=>a.typeRequest !=5 && a.service==service && a.request_citizenId == request_citizenId&&a.Is_Deleted!=true).SingleOrDefault();

            if(requestData != null)
            {
                if(Ar==true)
                {
                    return " بالفعل لقد طلبت هذه الخدمة وهي قيد التنفيذ ";
                }
                return "Already requested the same service";

            }
            Request request = new Request();
            request.request_id = request_id;
            request.request_citizenId = request_citizenId;
            request.address = address;
            request.service = service;
            request.governmentAgency = governmentAgency;
            request.quantity = quantity;
            request.language = language;
            request.typeRequest = typeRequest;
            request.date = date;
            if (!ModelState.IsValid)
            {
               // return BadRequest(ModelState);
            }

            db.Requests.Add(request);
            db.SaveChanges();
            if (Ar == true)
            {
                return "تم ارسال الطلب بنجاح ";
            }
            return "Successful send";
         //   return CreatedAtRoute("DefaultApi", new { id = request.request_id }, request);
        }

        // DELETE: api/RequestsApi/5
        [ResponseType(typeof(Request))]
        public IHttpActionResult DeleteRequest(int id)
        {
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return NotFound();
            }

            db.Requests.Remove(request);
            db.SaveChanges();

            return Ok(request);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RequestExists(int id)
        {
            return db.Requests.Count(e => e.request_id == id) > 0;
        }
    }
}