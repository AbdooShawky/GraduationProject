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
    public class VaccinationTypesController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: VaccinationTypes
        public ActionResult Index()
        {
            return View(db.VaccinationTypes.Where(a=>a.vaccination_isDeleted!= true).ToList());
        }

       

        // GET: VaccinationTypes/Create
        public ActionResult Create()
        {
            return View();
        }

     
        [HttpPost]
       
        public ActionResult Create( VaccinationType vaccinationType)
        {
            var data = db.VaccinationTypes.Where(a=> a.vaccination_type_name ==  vaccinationType.vaccination_type_name && a.vaccination_isDeleted !=true).SingleOrDefault();
            if (data == null)
            {
                db.VaccinationTypes.Add(vaccinationType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.errmsg = Servicely.Languages.Language.VaccinationType__errorMsg;
            return View(vaccinationType);
         }

        // GET: VaccinationTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VaccinationType vaccinationType = db.VaccinationTypes.Find(id);
            if (vaccinationType == null)
            {
                return HttpNotFound();
            }
            return View(vaccinationType);
        }

        // POST: VaccinationTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Edit(VaccinationType vaccinationType)
        {
            var data = db.VaccinationTypes.Where(a => a.vaccination_type_name == vaccinationType.vaccination_type_name && a.vaccination_isDeleted != true&&a.vaccination_type_id !=vaccinationType.vaccination_type_id).SingleOrDefault();
            if (data == null)
            {
              
                if (ModelState.IsValid)
                {
                    var old = db.VaccinationTypes.Find(vaccinationType.vaccination_type_id);
                    old.vaccination_type_name = vaccinationType.vaccination_type_name;
                    old.vaccination_type_name_arabic = vaccinationType.vaccination_type_name_arabic;
                    old.vaccination_type_period = vaccinationType.vaccination_type_period;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.errmsg = Servicely.Languages.Language.VaccinationType__errorMsg;
            return View(vaccinationType);
        }

        // GET: VaccinationTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VaccinationType vaccinationType = db.VaccinationTypes.Find(id);
            if (vaccinationType == null)
            {
                return HttpNotFound();
            }
            return View(vaccinationType);
        }

        // POST: VaccinationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            VaccinationType vaccinationType = db.VaccinationTypes.Find(id);
            vaccinationType.vaccination_isDeleted = true;
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
