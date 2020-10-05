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
    public class KindOfSchoolsController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: KindOfSchools
        public ActionResult Index()
        {
            return View(db.KindOfSchools.Where(a=> a.Is_Deleted != true).ToList());
        }

      

        // GET: KindOfSchools/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KindOfSchools/Create
       
        [HttpPost]
   
        public ActionResult Create( KindOfSchool kindOfSchool)
        {
            if (ModelState.IsValid)
            {
                var data = db.KindOfSchools.Where(a=> a.Is_Deleted != true && a.KindOfSchollName == kindOfSchool.KindOfSchollName).SingleOrDefault();
                if( data != null )
                {
                    ViewBag.school = Languages.Language.SchoolErr;
                    return View(kindOfSchool);
                }
                db.KindOfSchools.Add(kindOfSchool);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kindOfSchool);
        }

        // GET: KindOfSchools/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            KindOfSchool kindOfSchool = db.KindOfSchools.Find(id);
            if (kindOfSchool == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            return View(kindOfSchool);
        }

        // POST: KindOfSchools/Edit/5
      
        [HttpPost]
      
        public ActionResult Edit( KindOfSchool kindOfSchool)
        {
            if (ModelState.IsValid)
            {
                var data = db.KindOfSchools.Where(a => a.Id != kindOfSchool.Id && a.Is_Deleted!= true);
                foreach (var item in data)
                {
                    if(item.KindOfSchollName == kindOfSchool.KindOfSchollName)
                    {
                        ViewBag.school = Languages.Language.SchoolErr;
                        return View(kindOfSchool);
                    }
                }
                var old = db.KindOfSchools.Find(kindOfSchool.Id);
                old.KindOfSchollName = kindOfSchool.KindOfSchollName;
                old.KindOfSchollNameArabic = kindOfSchool.KindOfSchollNameArabic;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kindOfSchool);
        }

        // GET: KindOfSchools/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            KindOfSchool kindOfSchool = db.KindOfSchools.Find(id);
            if (kindOfSchool == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            return View(kindOfSchool);
        }

        // POST: KindOfSchools/Delete/5
        [HttpPost, ActionName("Delete")]
     
        public ActionResult DeleteConfirmed(int id)
        {
            KindOfSchool kindOfSchool = db.KindOfSchools.Find(id);
            kindOfSchool.Is_Deleted = true;
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
