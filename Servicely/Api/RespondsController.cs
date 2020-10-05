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
    public class RespondsController : ApiController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: api/Complains
        public IQueryable<Respond> GetResponds()
        {
            return db.Responds;
        }

        [ResponseType(typeof(Respond))]
        public IHttpActionResult GetResponds(int cId)
        {
            IEnumerable<Respond> data = db.Responds.Where(a => a.Is_Deleted != true && a.ComplainsId == cId);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);

        }

        // PUT: api/Responds/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRespond(int id, Respond respond)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != respond.Id)
            {
                return BadRequest();
            }

           // db.Entry(respond).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RespondExists(id))
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

        // POST: api/Responds
        [ResponseType(typeof(Respond))]
        public IHttpActionResult PostRespond(Respond respond)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Responds.Add(respond);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = respond.Id }, respond);
        }

        // DELETE: api/Responds/5
        [ResponseType(typeof(Respond))]
        public IHttpActionResult DeleteRespond(int id)
        {
            Respond respond = db.Responds.Find(id);
            if (respond == null)
            {
                return NotFound();
            }

            db.Responds.Remove(respond);
            db.SaveChanges();

            return Ok(respond);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RespondExists(int id)
        {
            return db.Responds.Count(e => e.Id == id) > 0;
        }
    }
}