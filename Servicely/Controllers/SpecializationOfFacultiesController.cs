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
    public class SpecializationOfFacultiesController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();


        public JsonResult GetAllFacultyByUniversityId(int Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.UniversityFacultyM_M.Where(a => a.Is_Deleted != true && a.UniversityId == Id).Select(a => new { a.Faculty.FacultyName, a.Faculty.FacultyNameArabic, a.Faculty.Id });
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllSpecByFacId(int Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.SpecializationOfFaculties.Where(a => a.Is_Deleted != true && a.FacultyId == Id).Select(a => new { a.SpecializationName, a.SpecializationNameArabic, a.FacultyId ,a.Id });
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        // GET: SpecializationOfFaculties
        public ActionResult Index()
        {
            ViewBag.errorMessage = Servicely.Languages.Language.Specialization_already_exist;
            //        ViewBag.FacultyId = new SelectList(db.Faculties.Where(a => a.Is_Dleted != true), "Id", "FacultyName");
            ViewBag.UniversityId = new SelectList(db.Universities.Where(a => a.Is_Deleted != true), "Id", "UniversityName");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    //               ViewBag.FacultyId = new SelectList(db.Faculties.Where(a => a.Is_Dleted != true), "Id", "FacultyNameArabic");
                    ViewBag.UniversityId = new SelectList(db.Universities.Where(a => a.Is_Deleted != true), "Id", "UniversityNameArabic");


                }
            }
          ///  var specializationOfFaculties = db.SpecializationOfFaculties.Include(s => s.Faculty).Where(a=>a.Is_Deleted!= true);
          //  return View(specializationOfFaculties.ToList());
            return View();
        }


      

        // GET: SpecializationOfFaculties/Create
        public ActionResult Create()
        {
          //  ViewBag.FacultyId = new SelectList(db.Faculties.Where(a => a.Is_Dleted != true), "Id", "FacultyName");
            ViewBag.UniversityId = new SelectList(db.Universities.Where(a => a.Is_Deleted != true), "Id", "UniversityName");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
          //          ViewBag.FacultyId = new SelectList(db.Faculties.Where(a => a.Is_Dleted != true), "Id", "FacultyNameArabic");
                    ViewBag.UniversityId = new SelectList(db.Universities.Where(a => a.Is_Deleted != true), "Id", "UniversityNameArabic");


                }
            }
            return View();
        }

        [HttpPost]
       
        public ActionResult Create(SpecializationOfFaculty specializationOfFaculty)
        {
            if (ModelState.IsValid)
            {
                var data = db.SpecializationOfFaculties.Where(a => a.SpecializationName == specializationOfFaculty.SpecializationName && a.FacultyId == specializationOfFaculty.FacultyId && a.Is_Deleted!=true).SingleOrDefault();
                if(data != null)
                {
                    ViewBag.errorMessage = Servicely.Languages.Language.Specialization_already_exist;
            //        ViewBag.FacultyId = new SelectList(db.Faculties.Where(a => a.Is_Dleted != true), "Id", "FacultyName");
                    ViewBag.UniversityId = new SelectList(db.Universities.Where(a => a.Is_Deleted != true), "Id", "UniversityName");
                    if (Session["lang"] != null)
                    {
                        if (Session["lang"].ToString().Equals("ar-EG"))
                        {
             //               ViewBag.FacultyId = new SelectList(db.Faculties.Where(a => a.Is_Dleted != true), "Id", "FacultyNameArabic");
                            ViewBag.UniversityId = new SelectList(db.Universities.Where(a => a.Is_Deleted != true), "Id", "UniversityNameArabic");


                        }
                    }
                    return View(specializationOfFaculty);
                }
                db.SpecializationOfFaculties.Add(specializationOfFaculty);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FacultyId = new SelectList(db.Faculties.Where(a=>a.Is_Dleted !=true), "Id", "FacultyName", specializationOfFaculty.FacultyId);
            ViewBag.UniversityId = new SelectList(db.Universities.Where(a => a.Is_Deleted != true), "Id", "UniversityName");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.FacultyId = new SelectList(db.Faculties.Where(a => a.Is_Dleted != true), "Id", "FacultyNameArabic");
                    ViewBag.UniversityId = new SelectList(db.Universities.Where(a => a.Is_Deleted != true), "Id", "UniversityNameArabic");


                }
            }
            return View(specializationOfFaculty);
        }

        // GET: SpecializationOfFaculties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorpage","home");
            }
            SpecializationOfFaculty specializationOfFaculty = db.SpecializationOfFaculties.Find(id);
            if (specializationOfFaculty == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            ViewBag.FacultyId = new SelectList(db.Faculties.Where(a=>a.Is_Dleted!= true), "Id", "FacultyName", specializationOfFaculty.FacultyId);
            ViewBag.UniversityId = new SelectList(db.Universities.Where(a => a.Is_Deleted != true), "Id", "UniversityName", specializationOfFaculty.Faculty.UniversityFacultyM_M);
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.FacultyId = new SelectList(db.Faculties.Where(a => a.Is_Dleted != true), "Id", "FacultyNameArabic", specializationOfFaculty.FacultyId);
                    ViewBag.UniversityId = new SelectList(db.Universities.Where(a => a.Is_Deleted != true), "Id", "UniversityNameArabic", specializationOfFaculty.Faculty.UniversityFacultyM_M);


                }
            }
            return View(specializationOfFaculty);
        }

        // POST: SpecializationOfFaculties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        public ActionResult Edit(SpecializationOfFaculty specializationOfFaculty)
        {
            if (ModelState.IsValid)
            {
                var data = db.SpecializationOfFaculties.Where(a=>a.Is_Deleted != true && a.Id != specializationOfFaculty.Id);
                foreach (var item in data)
                {
                    if(item.SpecializationName == specializationOfFaculty.SpecializationName && item.FacultyId == specializationOfFaculty.FacultyId)

                    {

                        ViewBag.errorMessage = Servicely.Languages.Language.Specialization_already_exist;
                        ViewBag.FacultyId = new SelectList(db.Faculties.Where(a => a.Is_Dleted != true), "Id", "FacultyName", specializationOfFaculty.FacultyId);
                        ViewBag.UniversityId = new SelectList(db.Universities.Where(a => a.Is_Deleted != true), "Id", "UniversityName");
                        if (Session["lang"] != null)
                        {
                            if (Session["lang"].ToString().Equals("ar-EG"))
                            {
                                ViewBag.FacultyId = new SelectList(db.Faculties.Where(a => a.Is_Dleted != true), "Id", "FacultyNameArabic", specializationOfFaculty.FacultyId);
                                ViewBag.UniversityId = new SelectList(db.Universities.Where(a => a.Is_Deleted != true), "Id", "UniversityNameArabic");


                            }
                        }
                        return View(specializationOfFaculty);


                    }
                }
                var old = db.SpecializationOfFaculties.Find(specializationOfFaculty.Id);
                old.SpecializationName = specializationOfFaculty.SpecializationName;
                old.SpecializationNameArabic = specializationOfFaculty.SpecializationNameArabic;
                old.FacultyId = specializationOfFaculty.FacultyId;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FacultyId = new SelectList(db.Faculties.Where(a=>a.Is_Dleted != true), "Id", "FacultyName", specializationOfFaculty.FacultyId);
            ViewBag.UniversityId = new SelectList(db.Universities.Where(a => a.Is_Deleted != true), "Id", "UniversityName");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.FacultyId = new SelectList(db.Faculties.Where(a => a.Is_Dleted != true), "Id", "FacultyNameArabic");
                    ViewBag.UniversityId = new SelectList(db.Universities.Where(a => a.Is_Deleted != true), "Id", "UniversityNameArabic");


                }
            }
            return View(specializationOfFaculty);
        }

        // GET: SpecializationOfFaculties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            SpecializationOfFaculty specializationOfFaculty = db.SpecializationOfFaculties.Find(id);
            if (specializationOfFaculty == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            return View(specializationOfFaculty);
        }

        // POST: SpecializationOfFaculties/Delete/5
        [HttpPost, ActionName("Delete")]
       
        public ActionResult DeleteConfirmed(int id)
        {
            SpecializationOfFaculty specializationOfFaculty = db.SpecializationOfFaculties.Find(id);
            specializationOfFaculty.Is_Deleted = true;
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
