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
    public class UniversityTypesController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: UniversityTypes
        public ActionResult Index()
        {
            return View(db.UniversityTypes.Where(a=> a.Is_Deleted!= true).ToList());
        }

       
      

        // GET: UniversityTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UniversityTypes/Create
    
        [HttpPost]
      
        public ActionResult Create( UniversityType universityType)
        {
            if (ModelState.IsValid)
            {

                var data = db.UniversityTypes.Where(a => a.Is_Deleted != true && a.UniversityTypeName == universityType.UniversityTypeName).SingleOrDefault();

                if(data != null)
                {
                    ViewBag.school = Languages.Language.SchoolErr;
                    return View(universityType);
                }
                db.UniversityTypes.Add(universityType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(universityType);
        }

        // GET: UniversityTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            UniversityType universityType = db.UniversityTypes.Find(id);
            if (universityType == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            return View(universityType);
        }

        // POST: UniversityTypes/Edit/5
       
        [HttpPost]
    
        public ActionResult Edit( UniversityType universityType)
        {
            if (ModelState.IsValid)
            {
                var data = db.UniversityTypes.Where(a => a.Id != universityType.Id && a.Is_Deleted != true);
                foreach (var item in data)
                {
                    if (item.UniversityTypeName == universityType.UniversityTypeName)
                    {
                        ViewBag.school = Languages.Language.SchoolErr;
                        return View(universityType);
                    }
                }

                var old = db.UniversityTypes.Find(universityType.Id);
                old.UniversityTypeName = universityType.UniversityTypeName;
                old.UniversityTypeNameArabic = universityType.UniversityTypeNameArabic;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(universityType);
        }

        // GET: UniversityTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            UniversityType universityType = db.UniversityTypes.Find(id);
            if (universityType == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            return View(universityType);
        }

        // POST: UniversityTypes/Delete/5
        [HttpPost, ActionName("Delete")]
 
        public ActionResult DeleteConfirmed(int id)
        {
            UniversityType universityType = db.UniversityTypes.Find(id);
            universityType.Is_Deleted = true;
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
