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
    public class RealStateRegistryInterestDepartmentsController : Controller
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: RealStateRegistryInterestDepartments
        public ActionResult Index()
        {
            return View(db.RealStateRegistryInterestDepartments.ToList());
        }

        // GET: RealStateRegistryInterestDepartments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealStateRegistryInterestDepartment realStateRegistryInterestDepartment = db.RealStateRegistryInterestDepartments.Find(id);
            if (realStateRegistryInterestDepartment == null)
            {
                return HttpNotFound();
            }
            return View(realStateRegistryInterestDepartment);
        }

        // GET: RealStateRegistryInterestDepartments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RealStateRegistryInterestDepartments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create( RealStateRegistryInterestDepartment realStateRegistryInterestDepartment)
        {
            if (ModelState.IsValid)
            {
                db.RealStateRegistryInterestDepartments.Add(realStateRegistryInterestDepartment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(realStateRegistryInterestDepartment);
        }

        // GET: RealStateRegistryInterestDepartments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealStateRegistryInterestDepartment realStateRegistryInterestDepartment = db.RealStateRegistryInterestDepartments.Find(id);
            if (realStateRegistryInterestDepartment == null)
            {
                return HttpNotFound();
            }
            return View(realStateRegistryInterestDepartment);
        }

        // POST: RealStateRegistryInterestDepartments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit( RealStateRegistryInterestDepartment realStateRegistryInterestDepartment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(realStateRegistryInterestDepartment).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(realStateRegistryInterestDepartment);
        }

        // GET: RealStateRegistryInterestDepartments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealStateRegistryInterestDepartment realStateRegistryInterestDepartment = db.RealStateRegistryInterestDepartments.Find(id);
            if (realStateRegistryInterestDepartment == null)
            {
                return HttpNotFound();
            }
            return View(realStateRegistryInterestDepartment);
        }

        // POST: RealStateRegistryInterestDepartments/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)

        {

            RealStateRegistryInterestDepartment RSRIDepartment = db.RealStateRegistryInterestDepartments.Find(id);
            RSRIDepartment.realStateRegistryInterestDepartments_isDeleted = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            //your handling logic here
            filterContext.ExceptionHandled = true;
            filterContext.Result = RedirectToAction("errorpage", "home");
        }
    }
}
