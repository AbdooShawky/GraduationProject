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
    public class StatesController : ApiController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: api/States
        public IQueryable<State> GetStates()
        {
            return db.States.Where(a=> a.state_isDeleted != true);
        }

        // GET: api/States/5
        [ResponseType(typeof(State))]
        public IHttpActionResult GetState(int id)
        {
            State state = db.States.Find(id);
            if (state == null)
            {
                return NotFound();
            }

            return Ok(state);
        }

        // PUT: api/States/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutState(int id, State state)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != state.state_id)
            {
                return BadRequest();
            }

            //db.Entry(state).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StateExists(id))
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

        // POST: api/States
        [ResponseType(typeof(State))]
        public IHttpActionResult PostState(State state)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.States.Add(state);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = state.state_id }, state);
        }

        // DELETE: api/States/5
        [ResponseType(typeof(State))]
        public IHttpActionResult DeleteState(int id)
        {
            State state = db.States.Find(id);
            if (state == null)
            {
                return NotFound();
            }

            db.States.Remove(state);
            db.SaveChanges();

            return Ok(state);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StateExists(int id)
        {
            return db.States.Count(e => e.state_id == id) > 0;
        }
    }
}