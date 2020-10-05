﻿using System;
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
    public class ScheduleVaccinationsController : ApiController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: api/ScheduleVaccinations
        public IEnumerable<CustomVaccSchedule> GetScheduleVaccinations()
        {
            var data = db.ScheduleVaccinations.Where(a => a.schedule_isDeleted != true).Select(a=> new CustomVaccSchedule { checkup_date=a.checkup_date.ToString(), checkup_end=a.checkup_end,checkup_start=a.checkup_start,schedule_id=a.schedule_id });
            return data;
        }



        // GET: api/ScheduleVaccinations/5
        [ResponseType(typeof(ScheduleVaccination))]
        public IHttpActionResult GetScheduleVaccination(int id)
        {
            ScheduleVaccination scheduleVaccination = db.ScheduleVaccinations.Find(id);
            if (scheduleVaccination == null)
            {
                return NotFound();
            }

            return Ok(scheduleVaccination);
        }

        // PUT: api/ScheduleVaccinations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutScheduleVaccination(int id, ScheduleVaccination scheduleVaccination)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != scheduleVaccination.schedule_id)
            {
                return BadRequest();
            }

            //db.Entry(scheduleVaccination).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleVaccinationExists(id))
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

        // POST: api/ScheduleVaccinations
        [ResponseType(typeof(ScheduleVaccination))]
        public IHttpActionResult PostScheduleVaccination(ScheduleVaccination scheduleVaccination)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ScheduleVaccinations.Add(scheduleVaccination);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = scheduleVaccination.schedule_id }, scheduleVaccination);
        }

        // DELETE: api/ScheduleVaccinations/5
        [ResponseType(typeof(ScheduleVaccination))]
        public IHttpActionResult DeleteScheduleVaccination(int id)
        {
            ScheduleVaccination scheduleVaccination = db.ScheduleVaccinations.Find(id);
            if (scheduleVaccination == null)
            {
                return NotFound();
            }

            db.ScheduleVaccinations.Remove(scheduleVaccination);
            db.SaveChanges();

            return Ok(scheduleVaccination);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ScheduleVaccinationExists(int id)
        {
            return db.ScheduleVaccinations.Count(e => e.schedule_id == id) > 0;
        }
    }
}