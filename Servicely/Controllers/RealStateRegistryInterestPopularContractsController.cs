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
    public class RealStateRegistryInterestPopularContractsController : Controller
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: RealStateRegistryInterestPopularContracts
        public ActionResult Index()
        {
            return View(db.RealStateRegistryInterestPopularContracts.ToList());
        }

        // GET: RealStateRegistryInterestPopularContracts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealStateRegistryInterestPopularContract realStateRegistryInterestPopularContract = db.RealStateRegistryInterestPopularContracts.Find(id);
            if (realStateRegistryInterestPopularContract == null)
            {
                return HttpNotFound();
            }
            return View(realStateRegistryInterestPopularContract);
        }

        // GET: RealStateRegistryInterestPopularContracts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RealStateRegistryInterestPopularContracts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(RealStateRegistryInterestPopularContract realStateRegistryInterestPopularContract)
        {
            if (ModelState.IsValid)
            {
                db.RealStateRegistryInterestPopularContracts.Add(realStateRegistryInterestPopularContract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(realStateRegistryInterestPopularContract);
        }

        // GET: RealStateRegistryInterestPopularContracts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealStateRegistryInterestPopularContract realStateRegistryInterestPopularContract = db.RealStateRegistryInterestPopularContracts.Find(id);
            if (realStateRegistryInterestPopularContract == null)
            {
                return HttpNotFound();
            }
            return View(realStateRegistryInterestPopularContract);
        }

        // POST: RealStateRegistryInterestPopularContracts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit( RealStateRegistryInterestPopularContract realStateRegistryInterestPopularContract)
        {
            if (ModelState.IsValid)
            {
                db.Entry(realStateRegistryInterestPopularContract).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(realStateRegistryInterestPopularContract);
        }

        // GET: RealStateRegistryInterestPopularContracts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealStateRegistryInterestPopularContract realStateRegistryInterestPopularContract = db.RealStateRegistryInterestPopularContracts.Find(id);
            if (realStateRegistryInterestPopularContract == null)
            {
                return HttpNotFound();
            }
            return View(realStateRegistryInterestPopularContract);
        }

        // POST: RealStateRegistryInterestPopularContracts/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            RealStateRegistryInterestPopularContract realStateRegistryInterestPopularContract = db.RealStateRegistryInterestPopularContracts.Find(id);
            db.RealStateRegistryInterestPopularContracts.Remove(realStateRegistryInterestPopularContract);
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
