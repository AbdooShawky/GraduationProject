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
    public class CarLicencesController : ApiController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: api/CarLicences
        public IQueryable<CarLicence> GetCarLicences()
        {
            return db.CarLicences;
        }

    
        [ResponseType(typeof(CarLicence))]
        public IHttpActionResult GetCarLicence(int cId)
        {
            IEnumerable<CarLicence> carLicence = db.CarLicences.Where(a => a.CitizenId == cId && a.Is_Deleted != true);
            if (carLicence == null)
            {
                return NotFound();
            }

            return Ok(carLicence);
        }

        // PUT: api/CarLicences/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCarLicence(int id, CarLicence carLicence)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != carLicence.Id)
            {
                return BadRequest();
            }

            db.Entry(carLicence).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarLicenceExists(id))
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

        // POST: api/CarLicences
        [ResponseType(typeof(CarLicence))]
        public IHttpActionResult PostCarLicence(CarLicence carLicence)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CarLicences.Add(carLicence);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = carLicence.Id }, carLicence);
        }

        // DELETE: api/CarLicences/5
        [ResponseType(typeof(CarLicence))]
        public IHttpActionResult DeleteCarLicence(int id)
        {
            CarLicence carLicence = db.CarLicences.Find(id);
            if (carLicence == null)
            {
                return NotFound();
            }

            db.CarLicences.Remove(carLicence);
            db.SaveChanges();

            return Ok(carLicence);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CarLicenceExists(int id)
        {
            return db.CarLicences.Count(e => e.Id == id) > 0;
        }
    }
}