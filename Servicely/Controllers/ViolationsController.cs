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
    public class ViolationsController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: Violations
        public ActionResult Index()
        {
            return View(db.Violations.Where(a=>a.Is_Deleted !=true).ToList());
        }



        // GET: Violations/Create
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        
        public ActionResult Create( Violation violation)
        {
            if (ModelState.IsValid)
            {
          



                var data = db.Violations.Where(a => a.Is_Deleted != true && a.ViolationName == violation.ViolationName).SingleOrDefault();
                if (data == null)
                {


                    db.Violations.Add(violation);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.errorMessage =Servicely.Languages.Language.violationError;
                    return View(violation);
                }
                }

            return View(violation);
        }

        // GET: Violations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorpage","home");
            }
            Violation violation = db.Violations.Find(id);
            if (violation == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            return View(violation);
        }

        // POST: Violations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        public ActionResult Edit(Violation violation)
        {
            if (ModelState.IsValid)
            {
                var data = db.Violations.Where(a => a.Is_Deleted != true && a.ViolationName != violation.ViolationName);
                foreach (var item in data)
                {
                    if(item.ViolationName == violation.ViolationName)
                    {


                        ViewBag.errorMessage = Servicely.Languages.Language.violationError;
                        return View(violation);
                    }
                }
                var old = db.Violations.Find(violation.Id);
                old.ViolationName = violation.ViolationName;
                old.ViolationPrice = violation.ViolationPrice;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(violation);
        }

        // GET: Violations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            Violation violation = db.Violations.Find(id);
            if (violation == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            return View(violation);
        }

        // POST: Violations/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            Violation violation = db.Violations.Find(id);
            violation.Is_Deleted = true;
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
