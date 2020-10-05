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
    public class PinNumber : ApiController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: api/PinNumber
        public IQueryable<LoginCitizen> GetLoginCitizens()
        {
            return db.LoginCitizens;
        }

        // GET: api/PinNumber/5
        [ResponseType(typeof(LoginCitizen))]
        public IHttpActionResult GetLoginCitizen(int id)
        {
            LoginCitizen loginCitizen = db.LoginCitizens.Find(id);
            if (loginCitizen == null)
            {
                return NotFound();
            }

            return Ok(loginCitizen);
        }

        // PUT: api/PinNumber/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLoginCitizen(int id, LoginCitizen loginCitizen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != loginCitizen.LoginId)
            {
                return BadRequest();
            }

            db.Entry(loginCitizen).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoginCitizenExists(id))
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

        // POST: api/PinNumber
        [ResponseType(typeof(LoginCitizen))]
        public IHttpActionResult PostLoginCitizen(LoginCitizen loginCitizen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LoginCitizens.Add(loginCitizen);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = loginCitizen.LoginId }, loginCitizen);
        }

        // DELETE: api/PinNumber/5
        [ResponseType(typeof(LoginCitizen))]
        public IHttpActionResult DeleteLoginCitizen(int id)
        {
            LoginCitizen loginCitizen = db.LoginCitizens.Find(id);
            if (loginCitizen == null)
            {
                return NotFound();
            }

            db.LoginCitizens.Remove(loginCitizen);
            db.SaveChanges();

            return Ok(loginCitizen);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LoginCitizenExists(int id)
        {
            return db.LoginCitizens.Count(e => e.LoginId == id) > 0;
        }
    }
}