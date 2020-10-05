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
    public class RealStateRegistryInterestPropertyTaxesController : Controller
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: RealStateRegistryInterestPropertyTaxes
        public ActionResult Index()
        {
            return View(db.RealStateRegistryInterestPropertyTaxes.ToList());
        }

        // GET: RealStateRegistryInterestPropertyTaxes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealStateRegistryInterestPropertyTax realStateRegistryInterestPropertyTax = db.RealStateRegistryInterestPropertyTaxes.Find(id);
            if (realStateRegistryInterestPropertyTax == null)
            {
                return HttpNotFound();
            }
            return View(realStateRegistryInterestPropertyTax);
        }

        // GET: RealStateRegistryInterestPropertyTaxes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RealStateRegistryInterestPropertyTaxes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "realStateRegistryInterestPropertyTaxes_id,realStateRegistryInterestPropertyTaxes_number,realStateRegistryInterestPropertyTaxes_body")] RealStateRegistryInterestPropertyTax realStateRegistryInterestPropertyTax)
        {
            if (ModelState.IsValid)
            {
                db.RealStateRegistryInterestPropertyTaxes.Add(realStateRegistryInterestPropertyTax);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(realStateRegistryInterestPropertyTax);
        }

        // GET: RealStateRegistryInterestPropertyTaxes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealStateRegistryInterestPropertyTax realStateRegistryInterestPropertyTax = db.RealStateRegistryInterestPropertyTaxes.Find(id);
            if (realStateRegistryInterestPropertyTax == null)
            {
                return HttpNotFound();
            }
            return View(realStateRegistryInterestPropertyTax);
        }

        // POST: RealStateRegistryInterestPropertyTaxes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "realStateRegistryInterestPropertyTaxes_id,realStateRegistryInterestPropertyTaxes_number,realStateRegistryInterestPropertyTaxes_body")] RealStateRegistryInterestPropertyTax realStateRegistryInterestPropertyTax)
        {
            if (ModelState.IsValid)
            {
                db.Entry(realStateRegistryInterestPropertyTax).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(realStateRegistryInterestPropertyTax);
        }

        // GET: RealStateRegistryInterestPropertyTaxes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealStateRegistryInterestPropertyTax realStateRegistryInterestPropertyTax = db.RealStateRegistryInterestPropertyTaxes.Find(id);
            if (realStateRegistryInterestPropertyTax == null)
            {
                return HttpNotFound();
            }
            return View(realStateRegistryInterestPropertyTax);
        }

        // POST: RealStateRegistryInterestPropertyTaxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RealStateRegistryInterestPropertyTax realStateRegistryInterestPropertyTax = db.RealStateRegistryInterestPropertyTaxes.Find(id);
            db.RealStateRegistryInterestPropertyTaxes.Remove(realStateRegistryInterestPropertyTax);
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
