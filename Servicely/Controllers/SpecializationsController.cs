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
    public class SpecializationsController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: Specializations
        public ActionResult Index()
        {
            return View(db.HealthCareSpecializations.Where(a => a.specialization_isDeleted != true).ToList());
        }

        // GET: Specializations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HealthCareSpecialization specialization = db.HealthCareSpecializations.Find(id);
            if (specialization == null)
            {
                return HttpNotFound();
            }
            return View(specialization);
        }

        // GET: Specializations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Specializations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create( HealthCareSpecialization specialization)
        {
            var data = db.HealthCareSpecializations.Where(a => a.specialization_name == specialization.specialization_name && a.specialization_isDeleted!=true).SingleOrDefault();
            if (data != null)
            {
                ViewBag.msg =Languages.Language.Specialization_already_exist ;
                return View(specialization);
            }
            db.HealthCareSpecializations.Add(specialization);
                db.SaveChanges();
                return RedirectToAction("Index");
            

        
        }

        // GET: Specializations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HealthCareSpecialization specialization = db.HealthCareSpecializations.Find(id);
            if (specialization == null)
            {
                return HttpNotFound();
            }
            return View(specialization);
        }

        // POST: Specializations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(HealthCareSpecialization specialization)
        {

            var data = db.HealthCareSpecializations.Where(a => a.specialization_name == specialization.specialization_name && a.specialization_isDeleted != true && a.specialization_id != specialization.specialization_id).SingleOrDefault();
            if (data != null)
            {
                ViewBag.msg = Languages.Language.Specialization_already_exist;
                return View(specialization);
            }
            if (ModelState.IsValid)
            {
                db.Entry(specialization).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(specialization);
        }

        // GET: Specializations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HealthCareSpecialization specialization = db.HealthCareSpecializations.Find(id);
            if (specialization == null)
            {
                return HttpNotFound();
            }
            return View(specialization);
        }

        // POST: Specializations/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {

            HealthCareSpecialization specialization = db.HealthCareSpecializations.Find(id);
            specialization.specialization_isDeleted = true;
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
