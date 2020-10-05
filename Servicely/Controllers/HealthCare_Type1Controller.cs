using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using Servicely.Models;

namespace Servicely.Controllers
{
    public class HealthCare_Type1Controller : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: HealthCare_Type1
        public ActionResult Index()
        {
            
            
            
            

            return View(db.HealthCare_Type.Where(a=> a.healthcare_isDeleted != true).ToList());
        }

        // GET: HealthCare_Type1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HealthCare_Type healthCare_Type = db.HealthCare_Type.Find(id);
            if (healthCare_Type == null)
            {
                return HttpNotFound();
            }
            return View(healthCare_Type);
        }

        // GET: HealthCare_Type1/Create
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
       
        public ActionResult Create( HealthCare_Type healthCare_Type)
        {

            var data = db.HealthCare_Type.Where(a => a.healthcare_type_name == healthCare_Type.healthcare_type_name && a.healthcare_isDeleted != true).SingleOrDefault();

            if(data != null)
            {
                ViewBag.health = Languages.Language.This_type_already_exist;
                return View();
            }


                db.HealthCare_Type.Add(healthCare_Type);
                db.SaveChanges();
                return RedirectToAction("Index");
            

            return View(healthCare_Type);
        }

        // GET: HealthCare_Type1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HealthCare_Type healthCare_Type = db.HealthCare_Type.Find(id);
            if (healthCare_Type == null)
            {
                return HttpNotFound();
            }
            return View(healthCare_Type);
        }

        // POST: HealthCare_Type1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
      
        public ActionResult Edit(HealthCare_Type healthCare_Type)
        {
            var data = db.HealthCare_Type.Where(a => a.healthcare_type_name == healthCare_Type.healthcare_type_name && healthCare_Type.healthcare_type_id !=a.healthcare_type_id && a.healthcare_isDeleted !=true).SingleOrDefault();

            if (data != null)
            {
                ViewBag.health = Servicely.Languages.Language.This_type_already_exist;
                return View(healthCare_Type);
            }
            db.Entry(healthCare_Type).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
                return RedirectToAction("Index");
            
          
        }

        // GET: HealthCare_Type1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HealthCare_Type healthCare_Type = db.HealthCare_Type.Find(id);
            if (healthCare_Type == null)
            {
                return HttpNotFound();
            }
            return View(healthCare_Type);
        }

        // POST: HealthCare_Type1/Delete/5
        [HttpPost, ActionName("Delete")]
      
        public ActionResult DeleteConfirmed(int id)
        {
            HealthCare_Type healthCare_Type = db.HealthCare_Type.Find(id);
            healthCare_Type.healthcare_isDeleted = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            //your handling logic here
            filterContext.ExceptionHandled = true;
            filterContext.Result = RedirectToAction("errorpage", "home");
        }
    }
}
