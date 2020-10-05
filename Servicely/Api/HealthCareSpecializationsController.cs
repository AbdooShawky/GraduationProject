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
    public class HealthCareSpecializationsController : ApiController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: api/HealthCareSpecializations
        public IQueryable<HealthCareSpecialization> GetHealthCareSpecializations()
        {
            return db.HealthCareSpecializations.Where(a=> a.specialization_isDeleted != true);
        }

        // GET: api/HealthCareSpecializations/5
        [ResponseType(typeof(HealthCareSpecialization))]
        public IHttpActionResult GetHealthCareSpecialization(int id)
        {
            HealthCareSpecialization healthCareSpecialization = db.HealthCareSpecializations.Find(id);
            if (healthCareSpecialization == null)
            {
                return NotFound();
            }

            return Ok(healthCareSpecialization);
        }

        // PUT: api/HealthCareSpecializations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHealthCareSpecialization(int id, HealthCareSpecialization healthCareSpecialization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != healthCareSpecialization.specialization_id)
            {
                return BadRequest();
            }

            db.Entry(healthCareSpecialization).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HealthCareSpecializationExists(id))
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

        // POST: api/HealthCareSpecializations
        [ResponseType(typeof(HealthCareSpecialization))]
        public IHttpActionResult PostHealthCareSpecialization(HealthCareSpecialization healthCareSpecialization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HealthCareSpecializations.Add(healthCareSpecialization);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = healthCareSpecialization.specialization_id }, healthCareSpecialization);
        }

        // DELETE: api/HealthCareSpecializations/5
        [ResponseType(typeof(HealthCareSpecialization))]
        public IHttpActionResult DeleteHealthCareSpecialization(int id)
        {
            HealthCareSpecialization healthCareSpecialization = db.HealthCareSpecializations.Find(id);
            if (healthCareSpecialization == null)
            {
                return NotFound();
            }

            db.HealthCareSpecializations.Remove(healthCareSpecialization);
            db.SaveChanges();

            return Ok(healthCareSpecialization);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HealthCareSpecializationExists(int id)
        {
            return db.HealthCareSpecializations.Count(e => e.specialization_id == id) > 0;
        }
    }
}