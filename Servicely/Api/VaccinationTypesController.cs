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
    public class VaccinationTypesController : ApiController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: api/VaccinationTypes
        public IQueryable<VaccinationType> GetVaccinationTypes()
        {
            return db.VaccinationTypes;
        }

        // GET: api/VaccinationTypes/5
        [ResponseType(typeof(VaccinationType))]
        public IHttpActionResult GetVaccinationType(int id)
        {
            VaccinationType vaccinationType = db.VaccinationTypes.Find(id);
            if (vaccinationType == null)
            {
                return NotFound();
            }

            return Ok(vaccinationType);
        }

        // PUT: api/VaccinationTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVaccinationType(int id, VaccinationType vaccinationType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vaccinationType.vaccination_type_id)
            {
                return BadRequest();
            }

           // db.Entry(vaccinationType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VaccinationTypeExists(id))
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

        // POST: api/VaccinationTypes
        [ResponseType(typeof(VaccinationType))]
        public IHttpActionResult PostVaccinationType(VaccinationType vaccinationType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VaccinationTypes.Add(vaccinationType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vaccinationType.vaccination_type_id }, vaccinationType);
        }

        // DELETE: api/VaccinationTypes/5
        [ResponseType(typeof(VaccinationType))]
        public IHttpActionResult DeleteVaccinationType(int id)
        {
            VaccinationType vaccinationType = db.VaccinationTypes.Find(id);
            if (vaccinationType == null)
            {
                return NotFound();
            }

            db.VaccinationTypes.Remove(vaccinationType);
            db.SaveChanges();

            return Ok(vaccinationType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VaccinationTypeExists(int id)
        {
            return db.VaccinationTypes.Count(e => e.vaccination_type_id == id) > 0;
        }
    }
}