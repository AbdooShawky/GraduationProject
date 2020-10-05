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
    public class LogRequestsController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: LogRequests
        public ActionResult Index()
        {
            var logRequests = db.LogRequests.Include(l => l.Citizen).Include(l => l.Request).Include(l => l.TypeRequest);
            return View(logRequests.ToList());
        }

        // GET: LogRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogRequest logRequest = db.LogRequests.Find(id);
            if (logRequest == null)
            {
                return HttpNotFound();
            }
            return View(logRequest);
        }

        // GET: LogRequests/Create
        public ActionResult Create()
        {
            ViewBag.log_citizenId = new SelectList(db.Citizens, "citizen_id", "citizen_national_id");
            ViewBag.log_requestId = new SelectList(db.Requests, "request_id", "address");
            ViewBag.log_request_typeId = new SelectList(db.TypeRequests, "typeReaquest_id", "typeReaquest_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.log_citizenId = new SelectList(db.Citizens, "citizen_id", "citizen_national_id");
                    ViewBag.log_requestId = new SelectList(db.Requests, "request_id", "address_arabic");
                    ViewBag.log_request_typeId = new SelectList(db.TypeRequests, "typeReaquest_id", "typeReaquest_name_arabic");

                }
            }
            return View();
        }

        // POST: LogRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "log_request_id,log_requestId,log_citizenId,log_date,log_request_typeId")] LogRequest logRequest)
        {
            if (ModelState.IsValid)
            {
                db.LogRequests.Add(logRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.log_citizenId = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", logRequest.log_citizenId);
            ViewBag.log_requestId = new SelectList(db.Requests, "request_id", "address", logRequest.log_requestId);
            ViewBag.log_request_typeId = new SelectList(db.TypeRequests, "typeReaquest_id", "typeReaquest_name", logRequest.log_request_typeId);
         
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {

                    ViewBag.log_citizenId = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", logRequest.log_citizenId);
                    ViewBag.log_requestId = new SelectList(db.Requests, "request_id", "address_arabic", logRequest.log_requestId);
                    ViewBag.log_request_typeId = new SelectList(db.TypeRequests, "typeReaquest_id", "typeReaquest_name_arabic", logRequest.log_request_typeId);


                }
            }

            return View(logRequest);
        }

        // GET: LogRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogRequest logRequest = db.LogRequests.Find(id);
            if (logRequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.log_citizenId = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", logRequest.log_citizenId);
            ViewBag.log_requestId = new SelectList(db.Requests, "request_id", "address", logRequest.log_requestId);
            ViewBag.log_request_typeId = new SelectList(db.TypeRequests, "typeReaquest_id", "typeReaquest_name", logRequest.log_request_typeId);


            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.log_citizenId = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", logRequest.log_citizenId);
                    ViewBag.log_requestId = new SelectList(db.Requests, "request_id", "address_arabic", logRequest.log_requestId);
                    ViewBag.log_request_typeId = new SelectList(db.TypeRequests, "typeReaquest_id", "typeReaquest_name_arabic", logRequest.log_request_typeId);
                }
            }

            return View(logRequest);
        }

        // POST: LogRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "log_request_id,log_requestId,log_citizenId,log_date,log_request_typeId")] LogRequest logRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(logRequest).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.log_citizenId = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", logRequest.log_citizenId);
            ViewBag.log_requestId = new SelectList(db.Requests, "request_id", "address", logRequest.log_requestId);
            ViewBag.log_request_typeId = new SelectList(db.TypeRequests, "typeReaquest_id", "typeReaquest_name", logRequest.log_request_typeId);

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {

                    ViewBag.log_citizenId = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", logRequest.log_citizenId);
                    ViewBag.log_requestId = new SelectList(db.Requests, "request_id", "address_arabic", logRequest.log_requestId);
                    ViewBag.log_request_typeId = new SelectList(db.TypeRequests, "typeReaquest_id", "typeReaquest_name_arabic", logRequest.log_request_typeId);
                }
            }
            return View(logRequest);
        }

        // GET: LogRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogRequest logRequest = db.LogRequests.Find(id);
            if (logRequest == null)
            {
                return HttpNotFound();
            }
            return View(logRequest);
        }

        // POST: LogRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LogRequest logRequest = db.LogRequests.Find(id);
            db.LogRequests.Remove(logRequest);
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
