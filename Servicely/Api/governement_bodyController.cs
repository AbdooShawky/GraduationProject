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
    public class governement_bodyController : ApiController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: api/governement_body
        public IQueryable<governement_body> Getgovernement_body()
        {
            return db.governement_body.Where(a => a.governement_isDeleted != true) ;
        }

        // GET: api/governement_body/5
        [ResponseType(typeof(governement_body))]
        public IHttpActionResult Getgovernement_body(int id)
        {
            governement_body governement_body = db.governement_body.Find(id);
            if (governement_body == null)
            {
                return NotFound();
            }

            return Ok(governement_body);
        }

        // PUT: api/governement_body/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putgovernement_body(int id, governement_body governement_body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != governement_body.id)
            {
                return BadRequest();
            }

           // db.Entry(governement_body).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!governement_bodyExists(id))
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

        // POST: api/governement_body
        [ResponseType(typeof(governement_body))]
        public IHttpActionResult Postgovernement_body(governement_body governement_body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.governement_body.Add(governement_body);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = governement_body.id }, governement_body);
        }

        // DELETE: api/governement_body/5
        [ResponseType(typeof(governement_body))]
        public IHttpActionResult Deletegovernement_body(int id)
        {
            governement_body governement_body = db.governement_body.Find(id);
            if (governement_body == null)
            {
                return NotFound();
            }

            db.governement_body.Remove(governement_body);
            db.SaveChanges();

            return Ok(governement_body);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool governement_bodyExists(int id)
        {
            return db.governement_body.Count(e => e.id == id) > 0;
        }
    }
}