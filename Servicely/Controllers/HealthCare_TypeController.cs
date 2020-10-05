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
    public class HealthCare_TypeController : Controller
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: HealthCare_Type
        public ActionResult Index()
        {
            return View(db.HealthCare_Type.ToList());
        }

        // GET: HealthCare_Type/Details/5
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

        // GET: HealthCare_Type/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HealthCare_Type/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "healthcare_type_id,healthcare_type_name,healthcare_isDeleted")] HealthCare_Type healthCare_Type)
        {
            if (ModelState.IsValid)
            {
                db.HealthCare_Type.Add(healthCare_Type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(healthCare_Type);
        }

        // GET: HealthCare_Type/Edit/5
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

        // POST: HealthCare_Type/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "healthcare_type_id,healthcare_type_name,healthcare_isDeleted")] HealthCare_Type healthCare_Type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(healthCare_Type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(healthCare_Type);
        }

        // GET: HealthCare_Type/Delete/5
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

        // POST: HealthCare_Type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HealthCare_Type healthCare_Type = db.HealthCare_Type.Find(id);
            db.HealthCare_Type.Remove(healthCare_Type);
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
    }
}
