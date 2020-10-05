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
    public class Healthcare_DoctorController : ApiController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: api/Healthcare_Doctor
        public IEnumerable<CustomDoctors> GetHealthcare_Doctor()
        {
            return db.Healthcare_Doctor.Where(a=> a.doctor_isDeleted != true).Select(a=> new CustomDoctors { doctor_id =a.doctor_id ,citizen_first_name= a.Citizen.citizen_first_name , citizen_fourth_name =a.Citizen.citizen_fourth_name, citizen_first_name_arabic = a.Citizen.citizen_first_name_arabic, citizen_fourth_name_arabic = a.Citizen.citizen_fourth_name_arabic });
        }

        // GET: api/Healthcare_Doctor/5
        [ResponseType(typeof(Healthcare_Doctor))]
        public IHttpActionResult GetHealthcare_Doctor(int id)
        {
            Healthcare_Doctor healthcare_Doctor = db.Healthcare_Doctor.Find(id);
            if (healthcare_Doctor == null)
            {
                return NotFound();
            }

            return Ok(healthcare_Doctor);
        }

        // PUT: api/Healthcare_Doctor/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHealthcare_Doctor(int id, Healthcare_Doctor healthcare_Doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != healthcare_Doctor.doctor_id)
            {
                return BadRequest();
            }

            //db.Entry(healthcare_Doctor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Healthcare_DoctorExists(id))
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

        // POST: api/Healthcare_Doctor
        [ResponseType(typeof(Healthcare_Doctor))]
        public IHttpActionResult PostHealthcare_Doctor(Healthcare_Doctor healthcare_Doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Healthcare_Doctor.Add(healthcare_Doctor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = healthcare_Doctor.doctor_id }, healthcare_Doctor);
        }

        // DELETE: api/Healthcare_Doctor/5
        [ResponseType(typeof(Healthcare_Doctor))]
        public IHttpActionResult DeleteHealthcare_Doctor(int id)
        {
            Healthcare_Doctor healthcare_Doctor = db.Healthcare_Doctor.Find(id);
            if (healthcare_Doctor == null)
            {
                return NotFound();
            }

            db.Healthcare_Doctor.Remove(healthcare_Doctor);
            db.SaveChanges();

            return Ok(healthcare_Doctor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Healthcare_DoctorExists(int id)
        {
            return db.Healthcare_Doctor.Count(e => e.doctor_id == id) > 0;
        }
    }
}