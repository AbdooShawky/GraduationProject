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
    public class VaccinationReservationsController : Controller
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: VaccinationReservations
        public ActionResult Index()
        {
            var vaccinationReservations = db.VaccinationReservations.Include(v => v.Citizen).Include(v => v.HealthCare).Include(v => v.VaccinationType).Include(a=>a.HealthCare.District).Include(a=> a.HealthCare.District.Region).Include(a => a.HealthCare.District.Region.City).Include(a => a.HealthCare.District.Region.City.State);
            return View(vaccinationReservations.ToList());
        }

        // GET: VaccinationReservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VaccinationReservation vaccinationReservation = db.VaccinationReservations.Find(id);
            if (vaccinationReservation == null)
            {
                return HttpNotFound();
            }
            return View(vaccinationReservation);
        }

        // GET: VaccinationReservations/Create
        public ActionResult Create()
        {
            ViewBag.VaccReservation_Citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id");
            ViewBag.VaccReservation_HealthCare_id = new SelectList(db.HealthCares, "hospital_id", "hospital_name");
            ViewBag.VaccReservation_VaccinationType_id = new SelectList(db.VaccinationTypes, "vaccination_type_id", "vaccination_type_name");
            return View();
        }

        // POST: VaccinationReservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VaccReservation_id,VaccReservation_HealthCare_id,VaccReservation_Citizen_id,VaccReservation_child_id,VaccReservation_date,VaccReservation_checked,VaccReservation_Code,VaccReservation_VaccinationType_id,VaccReservation_isDeleted,VaccReservation_cancel,TransactionDate")] VaccinationReservation vaccinationReservation)
        {
            if (ModelState.IsValid)
            {
                db.VaccinationReservations.Add(vaccinationReservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VaccReservation_Citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", vaccinationReservation.VaccReservation_Citizen_id);
            ViewBag.VaccReservation_HealthCare_id = new SelectList(db.HealthCares, "hospital_id", "hospital_name", vaccinationReservation.VaccReservation_HealthCare_id);
            ViewBag.VaccReservation_VaccinationType_id = new SelectList(db.VaccinationTypes, "vaccination_type_id", "vaccination_type_name", vaccinationReservation.VaccReservation_VaccinationType_id);
            return View(vaccinationReservation);
        }

        // GET: VaccinationReservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VaccinationReservation vaccinationReservation = db.VaccinationReservations.Find(id);
            if (vaccinationReservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.VaccReservation_Citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", vaccinationReservation.VaccReservation_Citizen_id);
            ViewBag.VaccReservation_HealthCare_id = new SelectList(db.HealthCares, "hospital_id", "hospital_name", vaccinationReservation.VaccReservation_HealthCare_id);
            ViewBag.VaccReservation_VaccinationType_id = new SelectList(db.VaccinationTypes, "vaccination_type_id", "vaccination_type_name", vaccinationReservation.VaccReservation_VaccinationType_id);
            return View(vaccinationReservation);
        }

        // POST: VaccinationReservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VaccReservation_id,VaccReservation_HealthCare_id,VaccReservation_Citizen_id,VaccReservation_child_id,VaccReservation_date,VaccReservation_checked,VaccReservation_Code,VaccReservation_VaccinationType_id,VaccReservation_isDeleted,VaccReservation_cancel,TransactionDate")] VaccinationReservation vaccinationReservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vaccinationReservation).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VaccReservation_Citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", vaccinationReservation.VaccReservation_Citizen_id);
            ViewBag.VaccReservation_HealthCare_id = new SelectList(db.HealthCares, "hospital_id", "hospital_name", vaccinationReservation.VaccReservation_HealthCare_id);
            ViewBag.VaccReservation_VaccinationType_id = new SelectList(db.VaccinationTypes, "vaccination_type_id", "vaccination_type_name", vaccinationReservation.VaccReservation_VaccinationType_id);
            return View(vaccinationReservation);
        }

        // GET: VaccinationReservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VaccinationReservation vaccinationReservation = db.VaccinationReservations.Find(id);
            if (vaccinationReservation == null)
            {
                return HttpNotFound();
            }
            return View(vaccinationReservation);
        }

        // POST: VaccinationReservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VaccinationReservation vaccinationReservation = db.VaccinationReservations.Find(id);
            db.VaccinationReservations.Remove(vaccinationReservation);
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
