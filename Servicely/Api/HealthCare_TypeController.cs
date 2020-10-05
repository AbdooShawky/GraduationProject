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
    public class HealthCare_TypeController : ApiController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: api/HealthCare_Type
        public IQueryable<HealthCare_Type> GetHealthCare_Type()
        {
            return db.HealthCare_Type;
        }

        // GET: api/HealthCare_Type/5
        [ResponseType(typeof(HealthCare_Type))]
        public IHttpActionResult GetHealthCare_Type(int id)
        {
            HealthCare_Type healthCare_Type = db.HealthCare_Type.Find(id);
            if (healthCare_Type == null)
            {
                return NotFound();
            }

            return Ok(healthCare_Type);
        }

        // PUT: api/HealthCare_Type/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHealthCare_Type(int id, HealthCare_Type healthCare_Type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != healthCare_Type.healthcare_type_id)
            {
                return BadRequest();
            }

           // db.Entry(healthCare_Type).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HealthCare_TypeExists(id))
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

        // POST: api/HealthCare_Type
        [ResponseType(typeof(HealthCare_Type))]
        public IHttpActionResult PostHealthCare_Type(HealthCare_Type healthCare_Type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HealthCare_Type.Add(healthCare_Type);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = healthCare_Type.healthcare_type_id }, healthCare_Type);
        }

        // DELETE: api/HealthCare_Type/5
        [ResponseType(typeof(HealthCare_Type))]
        public IHttpActionResult DeleteHealthCare_Type(int id)
        {
            HealthCare_Type healthCare_Type = db.HealthCare_Type.Find(id);
            if (healthCare_Type == null)
            {
                return NotFound();
            }

            db.HealthCare_Type.Remove(healthCare_Type);
            db.SaveChanges();

            return Ok(healthCare_Type);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HealthCare_TypeExists(int id)
        {
            return db.HealthCare_Type.Count(e => e.healthcare_type_id == id) > 0;
        }
    }
}