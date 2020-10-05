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
    public class police_reportController : Controller
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: police_report
        public ActionResult Index()
        {
            var police_report = db.police_report.Include(p => p.Citizen).Include(p => p.Citizen1).Include(p => p.PoliceReport_type).Include(p => p.PoliceDepartment).Include(p => p.PoliceReportClassification);
            return View(police_report.ToList());
        }

        // GET: police_report/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            police_report police_report = db.police_report.Find(id);
            if (police_report == null)
            {
                return HttpNotFound();
            }
            return View(police_report);
        }

        // GET: police_report/Create
        public ActionResult Create()
        {
            ViewBag.police_report_accused_signature = new SelectList(db.Citizens, "citizen_id", "citizen_national_id");
            ViewBag.police_report_police_signature = new SelectList(db.Citizens, "citizen_id", "citizen_national_id");
            ViewBag.police_report_type = new SelectList(db.PoliceReport_type, "police_report_type_id", "police_report_type_name");
            ViewBag.police_report_policeDepartment = new SelectList(db.PoliceDepartments, "police_department_id", "police_department_name");
            ViewBag.police_report_classification_id = new SelectList(db.PoliceReportClassifications, "police_report_classification_id", "police_report_classification_name");
            return View();
        }

        // POST: police_report/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "police_report_id,police_report_number,police_report_classification_id,police_report_policeDepartment,police_report_body,police_report_police_signature,police_report_accused_signature,police_report_DateAndTime,police_report_type")] police_report police_report)
        {
            if (ModelState.IsValid)
            {
                db.police_report.Add(police_report);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.police_report_accused_signature = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", police_report.police_report_accused_signature);
            ViewBag.police_report_police_signature = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", police_report.police_report_police_signature);
            ViewBag.police_report_type = new SelectList(db.PoliceReport_type, "police_report_type_id", "police_report_type_name", police_report.police_report_type);
            ViewBag.police_report_policeDepartment = new SelectList(db.PoliceDepartments, "police_department_id", "police_department_name", police_report.police_report_policeDepartment);
            ViewBag.police_report_classification_id = new SelectList(db.PoliceReportClassifications, "police_report_classification_id", "police_report_classification_name", police_report.police_report_classification_id);
            return View(police_report);
        }

        // GET: police_report/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            police_report police_report = db.police_report.Find(id);
            if (police_report == null)
            {
                return HttpNotFound();
            }
            ViewBag.police_report_accused_signature = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", police_report.police_report_accused_signature);
            ViewBag.police_report_police_signature = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", police_report.police_report_police_signature);
            ViewBag.police_report_type = new SelectList(db.PoliceReport_type, "police_report_type_id", "police_report_type_name", police_report.police_report_type);
            ViewBag.police_report_policeDepartment = new SelectList(db.PoliceDepartments, "police_department_id", "police_department_name", police_report.police_report_policeDepartment);
            ViewBag.police_report_classification_id = new SelectList(db.PoliceReportClassifications, "police_report_classification_id", "police_report_classification_name", police_report.police_report_classification_id);
            return View(police_report);
        }

        // POST: police_report/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "police_report_id,police_report_number,police_report_classification_id,police_report_policeDepartment,police_report_body,police_report_police_signature,police_report_accused_signature,police_report_DateAndTime,police_report_type")] police_report police_report)
        {
            if (ModelState.IsValid)
            {
                db.Entry(police_report).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.police_report_accused_signature = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", police_report.police_report_accused_signature);
            ViewBag.police_report_police_signature = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", police_report.police_report_police_signature);
            ViewBag.police_report_type = new SelectList(db.PoliceReport_type, "police_report_type_id", "police_report_type_name", police_report.police_report_type);
            ViewBag.police_report_policeDepartment = new SelectList(db.PoliceDepartments, "police_department_id", "police_department_name", police_report.police_report_policeDepartment);
            ViewBag.police_report_classification_id = new SelectList(db.PoliceReportClassifications, "police_report_classification_id", "police_report_classification_name", police_report.police_report_classification_id);
            return View(police_report);
        }

        // GET: police_report/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            police_report police_report = db.police_report.Find(id);
            if (police_report == null)
            {
                return HttpNotFound();
            }
            return View(police_report);
        }

        // POST: police_report/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            police_report police_report = db.police_report.Find(id);
            db.police_report.Remove(police_report);
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
