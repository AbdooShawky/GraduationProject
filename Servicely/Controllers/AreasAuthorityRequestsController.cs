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
    public class AreasAuthorityRequestsController : Controller
    {
        private DbMasterEntities1 db = new DbMasterEntities1();


        // GET: AreasAuthorityRequests
        public ActionResult Index()
        {
            var areasAuthorityRequests = db.AreasAuthorityRequests.Include(a => a.District);
            return View(areasAuthorityRequests.ToList());
        }

        // GET: AreasAuthorityRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreasAuthorityRequest areasAuthorityRequest = db.AreasAuthorityRequests.Find(id);
            if (areasAuthorityRequest == null)
            {
                return HttpNotFound();
            }
            return View(areasAuthorityRequest);
        }

        // GET: AreasAuthorityRequests/Create
        public ActionResult Create()
        {
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            ViewBag.AreasAuthorityRequests_District_id = new SelectList(db.Districts, "district_id", "district_name");
            return View();
        }

        // POST: AreasAuthorityRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AreasAuthorityRequest_id,AreasAuthorityRequest_number,AreasAuthorityRequests_District_id,AreasAuthorityRequests_length,AreasAuthorityRequests_width,AreasAuthorityRequests_LWnumber")] AreasAuthorityRequest areasAuthorityRequest)
        {
            if (ModelState.IsValid)
            {
                ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

                db.AreasAuthorityRequests.Add(areasAuthorityRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            ViewBag.AreasAuthorityRequests_District_id = new SelectList(db.Districts, "district_id", "district_name", areasAuthorityRequest.AreasAuthorityRequests_District_id);
            return View(areasAuthorityRequest);
        }

        // GET: AreasAuthorityRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreasAuthorityRequest areasAuthorityRequest = db.AreasAuthorityRequests.Find(id);
            if (areasAuthorityRequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            ViewBag.AreasAuthorityRequests_District_id = new SelectList(db.Districts, "district_id", "district_name", areasAuthorityRequest.AreasAuthorityRequests_District_id);
            return View(areasAuthorityRequest);
        }

        // POST: AreasAuthorityRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AreasAuthorityRequest_id,AreasAuthorityRequest_number,AreasAuthorityRequests_District_id,AreasAuthorityRequests_length,AreasAuthorityRequests_width,AreasAuthorityRequests_LWnumber")] AreasAuthorityRequest areasAuthorityRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(areasAuthorityRequest).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            ViewBag.AreasAuthorityRequests_District_id = new SelectList(db.Districts, "district_id", "district_name", areasAuthorityRequest.AreasAuthorityRequests_District_id);
            return View(areasAuthorityRequest);
        }

        // GET: AreasAuthorityRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreasAuthorityRequest areasAuthorityRequest = db.AreasAuthorityRequests.Find(id);
            if (areasAuthorityRequest == null)
            {
                return HttpNotFound();
            }
            return View(areasAuthorityRequest);
        }

        // POST: AreasAuthorityRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AreasAuthorityRequest areasAuthorityRequest = db.AreasAuthorityRequests.Find(id);
            db.AreasAuthorityRequests.Remove(areasAuthorityRequest);
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
