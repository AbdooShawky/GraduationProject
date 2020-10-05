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
    public class RealStateRegistryInterestReportsSubjectsController : Controller
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: RealStateRegistryInterestReportsSubjects
        public ActionResult Index()
        {
            return View(db.RealStateRegistryInterestReportsSubjects.ToList());
        }

        // GET: RealStateRegistryInterestReportsSubjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealStateRegistryInterestReportsSubject realStateRegistryInterestReportsSubject = db.RealStateRegistryInterestReportsSubjects.Find(id);
            if (realStateRegistryInterestReportsSubject == null)
            {
                return HttpNotFound();
            }
            return View(realStateRegistryInterestReportsSubject);
        }

        // GET: RealStateRegistryInterestReportsSubjects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RealStateRegistryInterestReportsSubjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RealStateRegistryInterestReportsSubject realStateRegistryInterestReportsSubject)
        {
            if (ModelState.IsValid)
            {
                db.RealStateRegistryInterestReportsSubjects.Add(realStateRegistryInterestReportsSubject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(realStateRegistryInterestReportsSubject);
        }

        // GET: RealStateRegistryInterestReportsSubjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealStateRegistryInterestReportsSubject realStateRegistryInterestReportsSubject = db.RealStateRegistryInterestReportsSubjects.Find(id);
            if (realStateRegistryInterestReportsSubject == null)
            {
                return HttpNotFound();
            }
            return View(realStateRegistryInterestReportsSubject);
        }

        // POST: RealStateRegistryInterestReportsSubjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit( RealStateRegistryInterestReportsSubject realStateRegistryInterestReportsSubject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(realStateRegistryInterestReportsSubject).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(realStateRegistryInterestReportsSubject);
        }

        // GET: RealStateRegistryInterestReportsSubjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealStateRegistryInterestReportsSubject realStateRegistryInterestReportsSubject = db.RealStateRegistryInterestReportsSubjects.Find(id);
            if (realStateRegistryInterestReportsSubject == null)
            {
                return HttpNotFound();
            }
            return View(realStateRegistryInterestReportsSubject);
        }

        // POST: RealStateRegistryInterestReportsSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            RealStateRegistryInterestReportsSubject realStateRegistryInterestReportsSubject = db.RealStateRegistryInterestReportsSubjects.Find(id);
            db.RealStateRegistryInterestReportsSubjects.Remove(realStateRegistryInterestReportsSubject);
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
