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
    public class FacultiesController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: Faculties
        public ActionResult Index()
        {
            return View(db.Faculties.Where(a=> a.Is_Dleted!= true).ToList());
        }

       

        // GET: Faculties/Create
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
       
        public ActionResult Create( Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                var data = db.Faculties.Where(a => a.Is_Dleted != true && a.FacultyName == faculty.FacultyName).SingleOrDefault();
                if( data != null)
                {
                    ViewBag.FacultyErr = Languages.Language.FacultyErr;
                    return View(faculty);
                }
                db.Faculties.Add(faculty);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(faculty);
        }

        // GET: Faculties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            Faculty faculty = db.Faculties.Find(id);
            if (faculty == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            return View(faculty);
        }

        // POST: Faculties/Edit/5
      
        [HttpPost]
       
        public ActionResult Edit( Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                var data = db.Faculties.Where(a => a.Id != faculty.Id && a.Is_Dleted != true);
                foreach (var item in data)
                {
                    if (item.FacultyName == faculty.FacultyName)
                    {
                        ViewBag.FacultyErr = Languages.Language.FacultyErr;
                        return View(faculty);
                    }
                }

                var old = db.Faculties.Find(faculty.Id);
                old.FacultyName = faculty.FacultyName;
                old.FacultyNameArabic = faculty.FacultyNameArabic;
                old.Year = faculty.Year;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(faculty);
        }

        // GET: Faculties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            Faculty faculty = db.Faculties.Find(id);
            if (faculty == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            return View(faculty);
        }

        // POST: Faculties/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            Faculty faculty = db.Faculties.Find(id);
            faculty.Is_Dleted = true;
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
