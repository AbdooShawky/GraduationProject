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
    public class ViolationsController : ApiController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: api/Violations
        public IQueryable<Violation> GetViolations()
        {
            return db.Violations;
        }

        // GET: api/Violations/5
        [ResponseType(typeof(Violation))]
        public IHttpActionResult GetViolation(int id)
        {
            Violation violation = db.Violations.Find(id);
            if (violation == null)
            {
                return NotFound();
            }

            return Ok(violation);
        }

        // PUT: api/Violations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutViolation(int id, Violation violation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != violation.Id)
            {
                return BadRequest();
            }

            //db.Entry(violation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ViolationExists(id))
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

        // POST: api/Violations
        [ResponseType(typeof(Violation))]
        public IHttpActionResult PostViolation(Violation violation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Violations.Add(violation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = violation.Id }, violation);
        }

        // DELETE: api/Violations/5
        [ResponseType(typeof(Violation))]
        public IHttpActionResult DeleteViolation(int id)
        {
            Violation violation = db.Violations.Find(id);
            if (violation == null)
            {
                return NotFound();
            }

            db.Violations.Remove(violation);
            db.SaveChanges();

            return Ok(violation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ViolationExists(int id)
        {
            return db.Violations.Count(e => e.Id == id) > 0;
        }
    }
}