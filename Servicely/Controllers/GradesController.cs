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
    public class GradesController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: Grades
        public ActionResult Index()
        {
            return View(db.Grades.Where(a=> a.Is_Deleted != true).ToList());
        }

        // GET: Grades/Details/5
       

        // GET: Grades/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Grades/Create
       
        [HttpPost]
        
        public ActionResult Create( Grade grade)
        {
            if (ModelState.IsValid)
            {
                var data = db.Grades.Where(a=> a.GradeName == grade.GradeName && a.Is_Deleted!= true).SingleOrDefault();
                if(data != null)
                {
                    ViewBag.GardeErr = Languages.Language.NameAlreadyExist;
                    return View(grade);
                }
                db.Grades.Add(grade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(grade);
        }

        // GET: Grades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            Grade grade = db.Grades.Find(id);
            if (grade == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            return View(grade);
        }

        // POST: Grades/Edit/5
       
        [HttpPost]
        
        public ActionResult Edit( Grade grade)
        {
            if (ModelState.IsValid)
            {
                var data = db.Grades.Where(a => a.Id != grade.Id && a.Is_Deleted!= true);

                foreach (var item in data)
                {
                    if(item.GradeName == grade.GradeName)
                    {
                        ViewBag.GardeErr = Languages.Language.NameAlreadyExist;
                        return View(grade);
                    }
                }

                var old = db.Grades.Find(grade.Id);
                old.GradeName = grade.GradeName;
                old.GradeNameArabic = grade.GradeNameArabic;
                old.GradePrecentage = grade.GradePrecentage;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(grade);
        }

        // GET: Grades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            Grade grade = db.Grades.Find(id);
            if (grade == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            return View(grade);
        }

        // POST: Grades/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            Grade grade = db.Grades.Find(id);
            grade.Is_Deleted = true;
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
