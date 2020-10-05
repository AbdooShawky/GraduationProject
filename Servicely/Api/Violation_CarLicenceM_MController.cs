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
    public class Violation_CarLicenceM_MController : ApiController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: api/Violation_CarLicenceM_M
        public IQueryable<Violation_CarLicenceM_M> GetViolation_CarLicenceM_M()
        {
            return db.Violation_CarLicenceM_M;
        }

        // GET: api/Violation_CarLicenceM_M/5
     
        [ResponseType(typeof(Violation_CarLicenceM_M))]
        public IHttpActionResult GetViolation_CarLicenceM_M(int cId)
        {
            /*  IEnumerable<Violation_CarLicenceM_M> violation_CarLicenceM_M = db.Violation_CarLicenceM_M.Where(a => a.CarLicenceId == cId && a.Is_Deleted != true);
              if (violation_CarLicenceM_M == null)
              {
                  return NotFound();
              }

              return Ok(violation_CarLicenceM_M);
              */
            db.Configuration.ProxyCreationEnabled = false;

            var data = db.Violation_CarLicenceM_M.Where(a => a.Is_Deleted != true && a.CarLicence.CitizenId == cId).Select(a => new {
                a.Id,
                a.Date,
                a.CarLicence.CarCode,
                a.CarLicence.Citizen.citizen_national_id,
                a.CarLicence.Citizen.citizen_first_name,
                a.CarLicence.Citizen.citizen_second_name,
                a.CarLicence.Citizen.citizen_third_name,
                a.CarLicence.Citizen.citizen_fourth_name,
                a.CarLicence.Citizen.citizen_first_name_arabic,
                a.CarLicence.Citizen.citizen_second_name_arabic,
                a.CarLicence.Citizen.citizen_third_name_arabic,
                a.CarLicence.Citizen.citizen_fourth_name_arabic,

                a.Violation.ViolationName,
                a.Violation.ViolationNameArabic,
                a.Violation.ViolationPrice,
                a.Is_Paid


            }).ToList();

            return Json(data);
        }

        // PUT: api/Violation_CarLicenceM_M/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutViolation_CarLicenceM_M(int id, Violation_CarLicenceM_M violation_CarLicenceM_M)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != violation_CarLicenceM_M.Id)
            {
                return BadRequest();
            }

           // db.Entry(violation_CarLicenceM_M).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Violation_CarLicenceM_MExists(id))
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

        // POST: api/Violation_CarLicenceM_M
        [ResponseType(typeof(Violation_CarLicenceM_M))]
        public IHttpActionResult PostViolation_CarLicenceM_M(Violation_CarLicenceM_M violation_CarLicenceM_M)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Violation_CarLicenceM_M.Add(violation_CarLicenceM_M);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = violation_CarLicenceM_M.Id }, violation_CarLicenceM_M);
        }

        // DELETE: api/Violation_CarLicenceM_M/5
        [ResponseType(typeof(Violation_CarLicenceM_M))]
        public IHttpActionResult DeleteViolation_CarLicenceM_M(int id)
        {
            Violation_CarLicenceM_M violation_CarLicenceM_M = db.Violation_CarLicenceM_M.Find(id);
            if (violation_CarLicenceM_M == null)
            {
                return NotFound();
            }

            db.Violation_CarLicenceM_M.Remove(violation_CarLicenceM_M);
            db.SaveChanges();

            return Ok(violation_CarLicenceM_M);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Violation_CarLicenceM_MExists(int id)
        {
            return db.Violation_CarLicenceM_M.Count(e => e.Id == id) > 0;
        }
    }
}