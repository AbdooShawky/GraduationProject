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
    public class UniversityFacultyM_MController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: UniversityFacultyM_M
        public ActionResult Index()
        {
            ViewBag.UniversityId = new SelectList(db.Universities.Where(a => a.Is_Deleted != true), "Id", "UniversityName");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.UniversityId = new SelectList(db.Universities.Where(a => a.Is_Deleted != true), "Id", "UniversityNameArabic");


                }
            }
           
        
        var universityFacultyM_M = db.UniversityFacultyM_M.Where(a=> a.Is_Deleted!= true).Include(u => u.Faculty).Include(u => u.University);
            return View(universityFacultyM_M.ToList());
        }
        public JsonResult GetAllFacultiesByUniId(int Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.UniversityFacultyM_M.Where(a => a.Is_Deleted != true && a.UniversityId == Id).Select(a => new
            {
                a.Faculty.FacultyName,
                a.Faculty.FacultyNameArabic,
                a.Id
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // GET: UniversityFacultyM_M/Create
        public ActionResult Create()
        {
            ViewBag.FacultyId = new SelectList(db.Faculties.Where(a => a.Is_Dleted != true), "Id", "FacultyName");
            ViewBag.UniversityId = new SelectList(db.Universities.Where(a => a.Is_Deleted != true), "Id", "UniversityName");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.FacultyId = new SelectList(db.Faculties.Where(a => a.Is_Dleted != true), "Id", "FacultyNameArabic");
                    ViewBag.UniversityId = new SelectList(db.Universities.Where(a => a.Is_Deleted != true), "Id", "UniversityNameArabic");


                }
            }
                return View();
        }

        // POST: UniversityFacultyM_M/Create
       
        [HttpPost]
       
        public ActionResult Create(UniversityFacultyM_M universityFacultyM_M)
        {
            if (ModelState.IsValid)
            {
                var data = db.UniversityFacultyM_M.Where(a=> a.UniversityId == universityFacultyM_M.UniversityId && a.FacultyId == universityFacultyM_M.FacultyId && a.Is_Deleted != true).SingleOrDefault();
                if( data != null )
                {
                    ViewBag.UniversityFaculty = Languages.Language.UniversityFaculty;
                    ViewBag.FacultyId = new SelectList(db.Faculties.Where(a => a.Is_Dleted != true), "Id", "FacultyName" ,universityFacultyM_M.FacultyId);
                    ViewBag.UniversityId = new SelectList(db.Universities.Where(a => a.Is_Deleted != true), "Id", "UniversityName",universityFacultyM_M.UniversityId);
                    if (Session["lang"] != null)
                    {
                        if (Session["lang"].ToString().Equals("ar-EG"))
                        {
                            ViewBag.FacultyId = new SelectList(db.Faculties.Where(a => a.Is_Dleted != true), "Id", "FacultyNameArabic", universityFacultyM_M.FacultyId);
                            ViewBag.UniversityId = new SelectList(db.Universities.Where(a => a.Is_Deleted != true), "Id", "UniversityNameArabic",universityFacultyM_M.UniversityId);


                        }
                    }
                    return View(universityFacultyM_M);
                }
                db.UniversityFacultyM_M.Add(universityFacultyM_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UniversityFaculty = Languages.Language.UniversityFaculty;
            ViewBag.FacultyId = new SelectList(db.Faculties.Where(a => a.Is_Dleted != true), "Id", "FacultyName", universityFacultyM_M.FacultyId);
            ViewBag.UniversityId = new SelectList(db.Universities.Where(a => a.Is_Deleted != true), "Id", "UniversityName",universityFacultyM_M.UniversityId);
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.FacultyId = new SelectList(db.Faculties.Where(a => a.Is_Dleted != true), "Id", "FacultyNameArabic",universityFacultyM_M.FacultyId);
                    ViewBag.UniversityId = new SelectList(db.Universities.Where(a => a.Is_Deleted != true), "Id", "UniversityNameArabic",universityFacultyM_M.UniversityId);


                }
            }
            return View(universityFacultyM_M);
        }

        // GET: UniversityFacultyM_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            UniversityFacultyM_M universityFacultyM_M = db.UniversityFacultyM_M.Find(id);
            if (universityFacultyM_M == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            
            ViewBag.FacultyId = new SelectList(db.Faculties.Where(a => a.Is_Dleted != true), "Id", "FacultyName", universityFacultyM_M.FacultyId);
            ViewBag.UniversityId = new SelectList(db.Universities.Where(a => a.Is_Deleted != true), "Id", "UniversityName", universityFacultyM_M.UniversityId);
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.FacultyId = new SelectList(db.Faculties.Where(a => a.Is_Dleted != true), "Id", "FacultyNameArabic", universityFacultyM_M.FacultyId);
                    ViewBag.UniversityId = new SelectList(db.Universities.Where(a => a.Is_Deleted != true), "Id", "UniversityNameArabic",universityFacultyM_M.UniversityId);


                }
            }
            return View(universityFacultyM_M);
        }

        // POST: UniversityFacultyM_M/Edit/5
       
        [HttpPost]

        public ActionResult Edit( UniversityFacultyM_M universityFacultyM_M)
        {
            if (ModelState.IsValid)
            {
                var data = db.UniversityFacultyM_M.Where(a=> a.Is_Deleted != true && a.Id != universityFacultyM_M.Id);
                foreach (var item in data)
                {
                    if(item.FacultyId == universityFacultyM_M.FacultyId && item.UniversityId == universityFacultyM_M.UniversityId)
                    {
                        ViewBag.UniversityFaculty = Languages.Language.UniversityFaculty;
                        ViewBag.FacultyId = new SelectList(db.Faculties.Where(a => a.Is_Dleted != true), "Id", "FacultyName", universityFacultyM_M.FacultyId);
                        ViewBag.UniversityId = new SelectList(db.Universities.Where(a => a.Is_Deleted != true), "Id", "UniversityName", universityFacultyM_M.UniversityId);
                        if (Session["lang"] != null)
                        {
                            if (Session["lang"].ToString().Equals("ar-EG"))
                            {
                                ViewBag.FacultyId = new SelectList(db.Faculties.Where(a => a.Is_Dleted != true), "Id", "FacultyNameArabic",universityFacultyM_M.FacultyId);
                                ViewBag.UniversityId = new SelectList(db.Universities.Where(a => a.Is_Deleted != true), "Id", "UniversityNameArabic", universityFacultyM_M.UniversityId);


                            }
                        }
                        return View(universityFacultyM_M);
                    }
                }
                db.Entry(universityFacultyM_M).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UniversityFaculty = Languages.Language.UniversityFaculty;
            ViewBag.FacultyId = new SelectList(db.Faculties.Where(a => a.Is_Dleted != true), "Id", "FacultyName");
            ViewBag.UniversityId = new SelectList(db.Universities.Where(a => a.Is_Deleted != true), "Id", "UniversityName");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.FacultyId = new SelectList(db.Faculties.Where(a => a.Is_Dleted != true), "Id", "FacultyNameArabic");
                    ViewBag.UniversityId = new SelectList(db.Universities.Where(a => a.Is_Deleted != true), "Id", "UniversityNameArabic");


                }
            }
            return View(universityFacultyM_M);
        }

        // GET: UniversityFacultyM_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            UniversityFacultyM_M universityFacultyM_M = db.UniversityFacultyM_M.Find(id);
            if (universityFacultyM_M == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            return View(universityFacultyM_M);
        }

        // POST: UniversityFacultyM_M/Delete/5
        [HttpPost, ActionName("Delete")]
       
        public ActionResult DeleteConfirmed(int id)
        {
            UniversityFacultyM_M universityFacultyM_M = db.UniversityFacultyM_M.Find(id);
            universityFacultyM_M.Is_Deleted = true;
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
