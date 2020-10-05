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
    public class ScheduleHealthCaresController : ApiController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: api/ScheduleHealthCares
        public IQueryable<ScheduleHealthCare> GetScheduleHealthCares()
        {
            return db.ScheduleHealthCares.Where(a=> a.schedule_isDeleted!= true);
        }

        // GET: api/ScheduleHealthCares/5
        [ResponseType(typeof(ScheduleHealthCare))]
        public IHttpActionResult GetScheduleHealthCare(int id)
        {
            ScheduleHealthCare scheduleHealthCare = db.ScheduleHealthCares.Find(id);
            if (scheduleHealthCare == null)
            {
                return NotFound();
            }

            return Ok(scheduleHealthCare);
        }

        // PUT: api/ScheduleHealthCares/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutScheduleHealthCare(int id, ScheduleHealthCare scheduleHealthCare)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != scheduleHealthCare.schedule_id)
            {
                return BadRequest();
            }

            //db.Entry(scheduleHealthCare).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleHealthCareExists(id))
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

        // POST: api/ScheduleHealthCares
        [ResponseType(typeof(ScheduleHealthCare))]
        public IHttpActionResult PostScheduleHealthCare(ScheduleHealthCare scheduleHealthCare)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ScheduleHealthCares.Add(scheduleHealthCare);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = scheduleHealthCare.schedule_id }, scheduleHealthCare);
        }

        // DELETE: api/ScheduleHealthCares/5
        [ResponseType(typeof(ScheduleHealthCare))]
        public IHttpActionResult DeleteScheduleHealthCare(int id)
        {
            ScheduleHealthCare scheduleHealthCare = db.ScheduleHealthCares.Find(id);
            if (scheduleHealthCare == null)
            {
                return NotFound();
            }

            db.ScheduleHealthCares.Remove(scheduleHealthCare);
            db.SaveChanges();

            return Ok(scheduleHealthCare);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ScheduleHealthCareExists(int id)
        {
            return db.ScheduleHealthCares.Count(e => e.schedule_id == id) > 0;
        }
    }
}