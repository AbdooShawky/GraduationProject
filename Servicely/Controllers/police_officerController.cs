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
    public class police_officerController : Controller
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: police_officer
        public ActionResult Index()
        {
            var police_officer = db.police_officer.Include(p => p.Citizen).Include(p => p.PoliceDepartment);
            return View(police_officer.ToList());
        }

       
      
        // GET: police_officer/Create
        public ActionResult Create()
        {
            ViewBag.police_officer_citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id");
            ViewBag.police_officer_policeDepartmentId = new SelectList(db.PoliceDepartments, "police_department_id", "police_department_id");
            return View();
        }

        // POST: police_officer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        public ActionResult Create( police_officer police_officer)
        {
            if (ModelState.IsValid)
            {
                db.police_officer.Add(police_officer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.police_officer_citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", police_officer.police_officer_citizen_id);
            ViewBag.police_officer_policeDepartmentId = new SelectList(db.PoliceDepartments, "police_department_id", "police_department_id", police_officer.police_officer_policeDepartmentId);
            return View(police_officer);
        }

        // GET: police_officer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            police_officer police_officer = db.police_officer.Find(id);
            if (police_officer == null)
            {
                return HttpNotFound();
            }
            ViewBag.police_officer_citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", police_officer.police_officer_citizen_id);
            ViewBag.police_officer_policeDepartmentId = new SelectList(db.PoliceDepartments, "police_department_id", "police_department_id", police_officer.police_officer_policeDepartmentId);
            return View(police_officer);
        }

        // POST: police_officer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Edit( police_officer police_officer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(police_officer).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.police_officer_citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", police_officer.police_officer_citizen_id);
            ViewBag.police_officer_policeDepartmentId = new SelectList(db.PoliceDepartments, "police_department_id", "police_department_id", police_officer.police_officer_policeDepartmentId);
            return View(police_officer);
        }

        // GET: police_officer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            police_officer police_officer = db.police_officer.Find(id);
            if (police_officer == null)
            {
                return HttpNotFound();
            }
            return View(police_officer);
        }

        // POST: police_officer/Delete/5
        [HttpPost, ActionName("Delete")]
       
        public ActionResult DeleteConfirmed(int id)
        {
            police_officer police_officer = db.police_officer.Find(id);
            db.police_officer.Remove(police_officer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            //your handling logic here
            filterContext.ExceptionHandled = true;
            filterContext.Result = RedirectToAction("errorpage", "home");
        }

    }
}
