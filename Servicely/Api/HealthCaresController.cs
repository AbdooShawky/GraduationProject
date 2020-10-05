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
    public class HealthCaresController : ApiController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: api/HealthCares
        public IEnumerable<HealthCare> GetHealthCares()
        {
           var data = db.HealthCares.Where(a => a.hospital_isDeleted != true).ToList().Select(a => new HealthCare { hospital_id =a.hospital_id,hospital_name= a.hospital_name , hospital_name_arabic = a.hospital_name_arabic });
            return data;
        }

        // GET: api/HealthCares/5
        [ResponseType(typeof(HealthCare))]
        public IHttpActionResult GetHealthCare(int id)
        {
            HealthCare healthCare = db.HealthCares.Find(id);
            if (healthCare == null)
            {
                return NotFound();
            }

            return Ok(healthCare);
        }

        // PUT: api/HealthCares/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHealthCare(int id, HealthCare healthCare)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != healthCare.hospital_id)
            {
                return BadRequest();
            }

            //db.Entry(healthCare).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HealthCareExists(id))
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

        // POST: api/HealthCares
        [ResponseType(typeof(HealthCare))]
        public IHttpActionResult PostHealthCare(HealthCare healthCare)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HealthCares.Add(healthCare);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = healthCare.hospital_id }, healthCare);
        }

        // DELETE: api/HealthCares/5
        [ResponseType(typeof(HealthCare))]
        public IHttpActionResult DeleteHealthCare(int id)
        {
            HealthCare healthCare = db.HealthCares.Find(id);
            if (healthCare == null)
            {
                return NotFound();
            }

            db.HealthCares.Remove(healthCare);
            db.SaveChanges();

            return Ok(healthCare);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HealthCareExists(int id)
        {
            return db.HealthCares.Count(e => e.hospital_id == id) > 0;
        }
    }
}