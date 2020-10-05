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
    public class Document_TypeController : ApiController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: api/Document_Type
        public IQueryable<Document_Type> GetDocument_Type()
        {
            return db.Document_Type.Where(a=> a.document_type_isDeleted!= true);
        }

        // GET: api/Document_Type/5
        [ResponseType(typeof(Document_Type))]
        public IHttpActionResult GetDocument_Type(int id)
        {
            Document_Type document_Type = db.Document_Type.Find(id);
            if (document_Type == null)
            {
                return NotFound();
            }

            return Ok(document_Type);
        }

        // PUT: api/Document_Type/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDocument_Type(int id, Document_Type document_Type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != document_Type.document_type_id)
            {
                return BadRequest();
            }

            db.Entry(document_Type).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Document_TypeExists(id))
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

        // POST: api/Document_Type
        [ResponseType(typeof(Document_Type))]
        public IHttpActionResult PostDocument_Type(Document_Type document_Type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Document_Type.Add(document_Type);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = document_Type.document_type_id }, document_Type);
        }

        // DELETE: api/Document_Type/5
        [ResponseType(typeof(Document_Type))]
        public IHttpActionResult DeleteDocument_Type(int id)
        {
            Document_Type document_Type = db.Document_Type.Find(id);
            if (document_Type == null)
            {
                return NotFound();
            }

            db.Document_Type.Remove(document_Type);
            db.SaveChanges();

            return Ok(document_Type);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Document_TypeExists(int id)
        {
            return db.Document_Type.Count(e => e.document_type_id == id) > 0;
        }
    }
}