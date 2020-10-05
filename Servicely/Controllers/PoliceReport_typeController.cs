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
    public class PoliceReport_typeController : Controller
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: PoliceReport_type
        public ActionResult Index()
        {
            return View(db.PoliceReport_type.ToList());
        }

        // GET: PoliceReport_type/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoliceReport_type policeReport_type = db.PoliceReport_type.Find(id);
            if (policeReport_type == null)
            {
                return HttpNotFound();
            }
            return View(policeReport_type);
        }

        // GET: PoliceReport_type/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PoliceReport_type/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create( PoliceReport_type policeReport_type)
        {
            if (ModelState.IsValid)
            {
                db.PoliceReport_type.Add(policeReport_type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(policeReport_type);
        }

        // GET: PoliceReport_type/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoliceReport_type policeReport_type = db.PoliceReport_type.Find(id);
            if (policeReport_type == null)
            {
                return HttpNotFound();
            }
            return View(policeReport_type);
        }

        // POST: PoliceReport_type/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit( PoliceReport_type policeReport_type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(policeReport_type).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(policeReport_type);
        }

        // GET: PoliceReport_type/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoliceReport_type policeReport_type = db.PoliceReport_type.Find(id);
            if (policeReport_type == null)
            {
                return HttpNotFound();
            }
            return View(policeReport_type);
        }

        // POST: PoliceReport_type/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PoliceReport_type policeReport_type = db.PoliceReport_type.Find(id);
            db.PoliceReport_type.Remove(policeReport_type);
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
