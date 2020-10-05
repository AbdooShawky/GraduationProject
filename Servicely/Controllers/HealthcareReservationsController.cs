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
    public class HealthcareReservationsController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: HealthcareReservations
        public ActionResult Index()
        {
            var healthcareReservations = db.HealthcareReservations.Include(h => h.HealthCare);
            return View(healthcareReservations.ToList());
        }

        // GET: HealthcareReservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HealthcareReservation healthcareReservation = db.HealthcareReservations.Find(id);
            if (healthcareReservation == null)
            {
                return HttpNotFound();
            }
            return View(healthcareReservation);
        }

        // GET: HealthcareReservations/Create
        public ActionResult Create()
        {
            ViewBag.healthcareReservation_hospital_id = new SelectList(db.HealthCares, "hospital_id", "hospital_name");
            ViewBag.healthcareReservation_service_type_id = new SelectList(db.services, "service_id", "service_name");
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.healthcareReservation_hospital_id = new SelectList(db.HealthCares, "hospital_id", "hospital_name_arabic");
                    ViewBag.healthcareReservation_service_type_id = new SelectList(db.services, "service_id", "service_name_arabic");
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");


                }
            }


            return View();
        }

        // POST: HealthcareReservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "healthcareReservation_id,healthcareReservation_service_type_id,healthcareReservation_hospital_id,Reservation_date,healthcareReservation_isDeleted")] HealthcareReservation healthcareReservation)
        {
            if (ModelState.IsValid)
            {
                db.HealthcareReservations.Add(healthcareReservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.healthcareReservation_hospital_id = new SelectList(db.HealthCares, "hospital_id", "hospital_name", healthcareReservation.healthcareReservation_hospital_id);


            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                  

                    ViewBag.healthcareReservation_hospital_id = new SelectList(db.HealthCares, "hospital_id", "hospital_name_arabic", healthcareReservation.healthcareReservation_hospital_id);


                }
            }

            return View(healthcareReservation);
        }

        // GET: HealthcareReservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HealthcareReservation healthcareReservation = db.HealthcareReservations.Find(id);
            if (healthcareReservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.healthcareReservation_hospital_id = new SelectList(db.HealthCares, "hospital_id", "hospital_name", healthcareReservation.healthcareReservation_hospital_id);

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {

                    ViewBag.healthcareReservation_hospital_id = new SelectList(db.HealthCares, "hospital_id", "hospital_name_arabic", healthcareReservation.healthcareReservation_hospital_id);



                }
            }
            return View(healthcareReservation);
        }

        // POST: HealthcareReservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "healthcareReservation_id,healthcareReservation_service_type_id,healthcareReservation_hospital_id,Reservation_date,healthcareReservation_isDeleted")] HealthcareReservation healthcareReservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(healthcareReservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.healthcareReservation_hospital_id = new SelectList(db.HealthCares, "hospital_id", "hospital_name", healthcareReservation.healthcareReservation_hospital_id);

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {

                    ViewBag.healthcareReservation_hospital_id = new SelectList(db.HealthCares, "hospital_id", "hospital_name_arabic", healthcareReservation.healthcareReservation_hospital_id);



                }
            }
            return View(healthcareReservation);
        }

        // GET: HealthcareReservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HealthcareReservation healthcareReservation = db.HealthcareReservations.Find(id);
            if (healthcareReservation == null)
            {
                return HttpNotFound();
            }
            return View(healthcareReservation);
        }

        // POST: HealthcareReservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HealthcareReservation healthcareReservation = db.HealthcareReservations.Find(id);
            db.HealthcareReservations.Remove(healthcareReservation);
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
