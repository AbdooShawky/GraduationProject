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
    public class RealStateRegistryInterestBranchesController : Controller
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: RealStateRegistryInterestBranches
        public ActionResult Index()
        {
            var realStateRegistryInterestBranches = db.RealStateRegistryInterestBranches.Include(r => r.Citizen).Include(r => r.District).Include(r => r.RealStateRegistryInterest);
            return View(realStateRegistryInterestBranches.ToList());
        }

        // GET: RealStateRegistryInterestBranches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealStateRegistryInterestBranch realStateRegistryInterestBranch = db.RealStateRegistryInterestBranches.Find(id);
            if (realStateRegistryInterestBranch == null)
            {
                return HttpNotFound();
            }
            return View(realStateRegistryInterestBranch);
        }

        // GET: RealStateRegistryInterestBranches/Create
        public ActionResult Create()
        {
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            ViewBag.realStateRegistryInterest_branch_technical_member_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id");
            ViewBag.realStateRegistryInterest_branch_district_id = new SelectList(db.Districts, "district_id", "district_name");
            ViewBag.realStateRegistryInterest_branch_realstate_id = new SelectList(db.RealStateRegistryInterests, "realStateRegistryInterest_id", "realStateRegistryInterest_name");
            return View();
        }

        // POST: RealStateRegistryInterestBranches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "realStateRegistryInterest_branch_id,realStateRegistryInterest_branch_name,realStateRegistryInterest_branch_realstate_id,realStateRegistryInterest_branch_technical_member_id,realStateRegistryInterest_branch_district_id,realStateRegistryInterest_branch_isDeleted")] RealStateRegistryInterestBranch realStateRegistryInterestBranch)
        {
            if (ModelState.IsValid)
            {
                ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

                db.RealStateRegistryInterestBranches.Add(realStateRegistryInterestBranch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            ViewBag.realStateRegistryInterest_branch_technical_member_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", realStateRegistryInterestBranch.realStateRegistryInterest_branch_technical_member_id);
            ViewBag.realStateRegistryInterest_branch_district_id = new SelectList(db.Districts, "district_id", "district_name", realStateRegistryInterestBranch.realStateRegistryInterest_branch_district_id);
            ViewBag.realStateRegistryInterest_branch_realstate_id = new SelectList(db.RealStateRegistryInterests, "realStateRegistryInterest_id", "realStateRegistryInterest_name", realStateRegistryInterestBranch.realStateRegistryInterest_branch_realstate_id);
            return View(realStateRegistryInterestBranch);
        }

        // GET: RealStateRegistryInterestBranches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealStateRegistryInterestBranch realStateRegistryInterestBranch = db.RealStateRegistryInterestBranches.Find(id);
            if (realStateRegistryInterestBranch == null)
            {
                return HttpNotFound();
            }
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            ViewBag.realStateRegistryInterest_branch_technical_member_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", realStateRegistryInterestBranch.realStateRegistryInterest_branch_technical_member_id);
            ViewBag.realStateRegistryInterest_branch_district_id = new SelectList(db.Districts, "district_id", "district_name", realStateRegistryInterestBranch.realStateRegistryInterest_branch_district_id);
            ViewBag.realStateRegistryInterest_branch_realstate_id = new SelectList(db.RealStateRegistryInterests, "realStateRegistryInterest_id", "realStateRegistryInterest_name", realStateRegistryInterestBranch.realStateRegistryInterest_branch_realstate_id);
            return View(realStateRegistryInterestBranch);
        }

        // POST: RealStateRegistryInterestBranches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "realStateRegistryInterest_branch_id,realStateRegistryInterest_branch_name,realStateRegistryInterest_branch_realstate_id,realStateRegistryInterest_branch_technical_member_id,realStateRegistryInterest_branch_district_id,realStateRegistryInterest_branch_isDeleted")] RealStateRegistryInterestBranch realStateRegistryInterestBranch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(realStateRegistryInterestBranch).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            ViewBag.realStateRegistryInterest_branch_technical_member_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", realStateRegistryInterestBranch.realStateRegistryInterest_branch_technical_member_id);
            ViewBag.realStateRegistryInterest_branch_district_id = new SelectList(db.Districts, "district_id", "district_name", realStateRegistryInterestBranch.realStateRegistryInterest_branch_district_id);
            ViewBag.realStateRegistryInterest_branch_realstate_id = new SelectList(db.RealStateRegistryInterests, "realStateRegistryInterest_id", "realStateRegistryInterest_name", realStateRegistryInterestBranch.realStateRegistryInterest_branch_realstate_id);
            return View(realStateRegistryInterestBranch);
        }

        // GET: RealStateRegistryInterestBranches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealStateRegistryInterestBranch realStateRegistryInterestBranch = db.RealStateRegistryInterestBranches.Find(id);
            if (realStateRegistryInterestBranch == null)
            {
                return HttpNotFound();
            }
            return View(realStateRegistryInterestBranch);
        }

        // POST: RealStateRegistryInterestBranches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RealStateRegistryInterestBranch realStateRegistryInterestBranch = db.RealStateRegistryInterestBranches.Find(id);
            db.RealStateRegistryInterestBranches.Remove(realStateRegistryInterestBranch);
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
