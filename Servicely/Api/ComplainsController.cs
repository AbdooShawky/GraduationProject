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

namespace Servicely.Api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ComplainsController : ApiController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: api/Complains
        public IQueryable<Complain> GetComplains()
        {
            return db.Complains;
        }

        // GET: api/Complains/5
        [ResponseType(typeof(Complain))]
        public IHttpActionResult GetComplain(int cId)
        {
            IEnumerable<Complain> data = db.Complains.Where(a => a.Is_Deleted != true && a.CitizenId == cId);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);

        }
        public string GetNationalId(int Id)
        {
            var data = db.Citizens.Find(Id);
            string a = data.citizen_national_id;
            return a;
        }
        public string PostComplaint (Complain c)
        {
            c.Date = DateTime.Now;
            db.Complains.Add(c);
            db.SaveChanges();
            return "1";

        }
        
        [ResponseType(typeof(Complain))]
        public string GetComplain(string cId, string NId , string GovId, string ComText, string Ar)
        {

            
            Complain complain = new Complain();
            complain.CitizenId = int.Parse(cId);
            complain.CitizenNationalId = NId;
            complain.ComplainText = ComText;
            complain.GovernementId = int.Parse( GovId);
            complain.Date = DateTime.Now;

            db.Complains.Add(complain);
            db.SaveChanges();
            if(Boolean.Parse(Ar)== true)
            {
                return "تم ارسال الشكوي بنجاح";
            }
            return "Complaint has been sent successfully";
        }

        // PUT: api/Complains/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutComplain(int id, Complain complain)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != complain.Id)
            {
                return BadRequest();
            }

            db.Entry(complain).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComplainExists(id))
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
/*
        // POST: api/Complains
        [ResponseType(typeof(Complain))]
        public IHttpActionResult PostComplain(Complain complain)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Complains.Add(complain);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = complain.Id }, complain);
        }
        */
        // DELETE: api/Complains/5
        [ResponseType(typeof(Complain))]
        public IHttpActionResult DeleteComplain(int id)
        {
            Complain complain = db.Complains.Find(id);
            if (complain == null)
            {
                return NotFound();
            }

            db.Complains.Remove(complain);
            db.SaveChanges();

            return Ok(complain);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ComplainExists(int id)
        {
            return db.Complains.Count(e => e.Id == id) > 0;
        }
    }
}