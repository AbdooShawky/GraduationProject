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
    public class OfficesController : ApiController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: api/Offices
        public IEnumerable<Office> GetOffices()
        {
            return db.Offices;
        }

        // GET: api/Offices/5
        [ResponseType(typeof(Office))]
        public IHttpActionResult GetOffice(int DId,int GovId)
        {
            //Office office = db.Offices.Find(id);
            IEnumerable<Office> office = db.Offices.Where(a => a.office_district_id == DId&&a.governemet_id==GovId);
            if (office == null)
            {
                return NotFound();
            }

            return Ok(office);
        }

        // PUT: api/Offices/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOffice(int id, Office office)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != office.office_id)
            {
                return BadRequest();
            }

           // db.Entry(office).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfficeExists(id))
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

        // POST: api/Offices
        [ResponseType(typeof(Office))]
        public IHttpActionResult PostOffice(Office office)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Offices.Add(office);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = office.office_id }, office);
        }

        // DELETE: api/Offices/5
        [ResponseType(typeof(Office))]
        public IHttpActionResult DeleteOffice(int id)
        {
            Office office = db.Offices.Find(id);
            if (office == null)
            {
                return NotFound();
            }

            db.Offices.Remove(office);
            db.SaveChanges();

            return Ok(office);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OfficeExists(int id)
        {
            return db.Offices.Count(e => e.office_id == id) > 0;
        }
    }
}