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
    public class TypeRequestsController : ApiController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: api/TypeRequests
        public IQueryable<TypeRequest> GetTypeRequests()
        {
            return db.TypeRequests;
        }

        // GET: api/TypeRequests/5
        [ResponseType(typeof(TypeRequest))]
        public IHttpActionResult GetTypeRequest(int id)
        {
            TypeRequest typeRequest = db.TypeRequests.Find(id);
            if (typeRequest == null)
            {
                return NotFound();
            }

            return Ok(typeRequest);
        }

        // PUT: api/TypeRequests/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTypeRequest(int id, TypeRequest typeRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != typeRequest.typeReaquest_id)
            {
                return BadRequest();
            }

           // db.Entry(typeRequest).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeRequestExists(id))
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

        // POST: api/TypeRequests
        [ResponseType(typeof(TypeRequest))]
        public IHttpActionResult PostTypeRequest(TypeRequest typeRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TypeRequests.Add(typeRequest);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TypeRequestExists(typeRequest.typeReaquest_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = typeRequest.typeReaquest_id }, typeRequest);
        }

        // DELETE: api/TypeRequests/5
        [ResponseType(typeof(TypeRequest))]
        public IHttpActionResult DeleteTypeRequest(int id)
        {
            TypeRequest typeRequest = db.TypeRequests.Find(id);
            if (typeRequest == null)
            {
                return NotFound();
            }

            db.TypeRequests.Remove(typeRequest);
            db.SaveChanges();

            return Ok(typeRequest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TypeRequestExists(int id)
        {
            return db.TypeRequests.Count(e => e.typeReaquest_id == id) > 0;
        }
    }
}