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
    public class RealStateRegistryInterestsController : Controller
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: RealStateRegistryInterests
        public ActionResult Index()
        {
            var realStateRegistryInterests = db.RealStateRegistryInterests.Include(r => r.District).Include(r => r.RealStateRegistryInterestDepartment);
            return View(realStateRegistryInterests.ToList());
        }

        // GET: RealStateRegistryInterests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealStateRegistryInterest realStateRegistryInterest = db.RealStateRegistryInterests.Find(id);
            if (realStateRegistryInterest == null)
            {
                return HttpNotFound();
            }
            return View(realStateRegistryInterest);
        }

        // GET: RealStateRegistryInterests/Create
        public ActionResult Create()
        {
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            ViewBag.realStateRegistryInterest_address = new SelectList(db.Districts, "district_id", "district_name");
            ViewBag.realStateRegistryInterest_Departments_id = new SelectList(db.RealStateRegistryInterestDepartments, "realStateRegistryInterestDepartments_id", "realStateRegistryInterestDepartments_name");
            return View();
        }

        // POST: RealStateRegistryInterests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "realStateRegistryInterest_id,realStateRegistryInterest_name,realStateRegistryInterest_address,realStateRegistryInterest_Departments_id,realStateRegistryInterest_isDeleted")] RealStateRegistryInterest realStateRegistryInterest)
        {
            if (ModelState.IsValid)
            {
                db.RealStateRegistryInterests.Add(realStateRegistryInterest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            ViewBag.realStateRegistryInterest_address = new SelectList(db.Districts, "district_id", "district_name", realStateRegistryInterest.realStateRegistryInterest_address);
            ViewBag.realStateRegistryInterest_Departments_id = new SelectList(db.RealStateRegistryInterestDepartments, "realStateRegistryInterestDepartments_id", "realStateRegistryInterestDepartments_name", realStateRegistryInterest.realStateRegistryInterest_Departments_id);
            return View(realStateRegistryInterest);
        }

        // GET: RealStateRegistryInterests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealStateRegistryInterest realStateRegistryInterest = db.RealStateRegistryInterests.Find(id);
            if (realStateRegistryInterest == null)
            {
                return HttpNotFound();
            }
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            ViewBag.realStateRegistryInterest_address = new SelectList(db.Districts, "district_id", "district_name", realStateRegistryInterest.realStateRegistryInterest_address);
            ViewBag.realStateRegistryInterest_Departments_id = new SelectList(db.RealStateRegistryInterestDepartments, "realStateRegistryInterestDepartments_id", "realStateRegistryInterestDepartments_name", realStateRegistryInterest.realStateRegistryInterest_Departments_id);
            return View(realStateRegistryInterest);
        }

        // POST: RealStateRegistryInterests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(RealStateRegistryInterest realStateRegistryInterest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(realStateRegistryInterest).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            ViewBag.realStateRegistryInterest_address = new SelectList(db.Districts, "district_id", "district_name", realStateRegistryInterest.realStateRegistryInterest_address);
            ViewBag.realStateRegistryInterest_Departments_id = new SelectList(db.RealStateRegistryInterestDepartments, "realStateRegistryInterestDepartments_id", "realStateRegistryInterestDepartments_name", realStateRegistryInterest.realStateRegistryInterest_Departments_id);
            return View(realStateRegistryInterest);
        }

        // GET: RealStateRegistryInterests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealStateRegistryInterest realStateRegistryInterest = db.RealStateRegistryInterests.Find(id);
            if (realStateRegistryInterest == null)
            {
                return HttpNotFound();
            }
            return View(realStateRegistryInterest);
        }

        // POST: RealStateRegistryInterests/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            RealStateRegistryInterest realStateRegistryInterest = db.RealStateRegistryInterests.Find(id);
            db.RealStateRegistryInterests.Remove(realStateRegistryInterest);
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
