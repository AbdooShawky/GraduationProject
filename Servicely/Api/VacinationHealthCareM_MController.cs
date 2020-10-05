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
    public class VacinationHealthCareM_MController : ApiController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: api/VacinationHealthCareM_M
        public IQueryable<VacinationHealthCareM_M> GetVacinationHealthCareM_M()
        {
            return db.VacinationHealthCareM_M;
        }

        // GET: api/VacinationHealthCareM_M/5
        [ResponseType(typeof(VacinationHealthCareM_M))]
        public IHttpActionResult GetVacinationHealthCareM_M(int id)
        {
            VacinationHealthCareM_M vacinationHealthCareM_M = db.VacinationHealthCareM_M.Find(id);
            if (vacinationHealthCareM_M == null)
            {
                return NotFound();
            }

            return Ok(vacinationHealthCareM_M);
        }

        // PUT: api/VacinationHealthCareM_M/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVacinationHealthCareM_M(int id, VacinationHealthCareM_M vacinationHealthCareM_M)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vacinationHealthCareM_M.vaccinationhealthcare_id)
            {
                return BadRequest();
            }

            //db.Entry(vacinationHealthCareM_M).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VacinationHealthCareM_MExists(id))
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

        // POST: api/VacinationHealthCareM_M
        [ResponseType(typeof(VacinationHealthCareM_M))]
        public IHttpActionResult PostVacinationHealthCareM_M(VacinationHealthCareM_M vacinationHealthCareM_M)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VacinationHealthCareM_M.Add(vacinationHealthCareM_M);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vacinationHealthCareM_M.vaccinationhealthcare_id }, vacinationHealthCareM_M);
        }

        // DELETE: api/VacinationHealthCareM_M/5
        [ResponseType(typeof(VacinationHealthCareM_M))]
        public IHttpActionResult DeleteVacinationHealthCareM_M(int id)
        {
            VacinationHealthCareM_M vacinationHealthCareM_M = db.VacinationHealthCareM_M.Find(id);
            if (vacinationHealthCareM_M == null)
            {
                return NotFound();
            }

            db.VacinationHealthCareM_M.Remove(vacinationHealthCareM_M);
            db.SaveChanges();

            return Ok(vacinationHealthCareM_M);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VacinationHealthCareM_MExists(int id)
        {
            return db.VacinationHealthCareM_M.Count(e => e.vaccinationhealthcare_id == id) > 0;
        }
    }
}