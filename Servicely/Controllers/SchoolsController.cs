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
    public class SchoolsController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: Schools
        public ActionResult Index()
        {
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            ViewBag.Phase = new SelectList(db.PhasesOfSchooles.Where(a => a.Is_Deleted != true), "Id", "PhaseName");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                    ViewBag.Phase = new SelectList(db.PhasesOfSchooles.Where(a => a.Is_Deleted != true), "Id", "PhaseNameArabic");

                }
            }

            var schools = db.Schools.Include(s => s.KindOfSchool).Where(a=>a.Is_Deleted !=true);
            return View(schools.ToList());
        }

        public JsonResult GetAllSchoolsBySchooTypeId( int dis)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.Schools.Where(a => a.Is_Deleted != true  && a.DistrictId == dis).Select(a => new { a.SchoolName, a.Id, a.SchoolNameArabic ,}).ToList();

           

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // GET: Schools/Create
        public ActionResult Create()
        {
            ViewBag.KindOfSchoolId = new SelectList(db.KindOfSchools.Where(a=>a.Is_Deleted!= true), "Id", "KindOfSchollName");
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.KindOfSchoolId = new SelectList(db.KindOfSchools.Where(a => a.Is_Deleted != true), "Id", "KindOfSchollNameArabic");

                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                }
            }
            return View();
        }

        // POST: Schools/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        public ActionResult Create(School school)
        {
            if (ModelState.IsValid)
            {
                var data = db.Schools.Where(a=>a.Is_Deleted !=true && a.SchoolName == school.SchoolName).SingleOrDefault();

                  if(data != null)
                {
                    ViewBag.schoolerror = Languages.Language.NameAlreadyExist;
                    ViewBag.KindOfSchoolId = new SelectList(db.KindOfSchools.Where(a => a.Is_Deleted != true), "Id", "KindOfSchollName");
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

                    if (Session["lang"] != null)
                    {
                        if (Session["lang"].ToString().Equals("ar-EG"))
                        {
                            ViewBag.KindOfSchoolId = new SelectList(db.KindOfSchools.Where(a => a.Is_Deleted != true), "Id", "KindOfSchollNameArabic");

                            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                        }
                    }
                    return View(school);
                }

                db.Schools.Add(school);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.schoolerror =Languages.Language.NameAlreadyExist;
            ViewBag.KindOfSchoolId = new SelectList(db.KindOfSchools.Where(a => a.Is_Deleted != true), "Id", "KindOfSchollName");
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.KindOfSchoolId = new SelectList(db.KindOfSchools.Where(a => a.Is_Deleted != true), "Id", "KindOfSchollNameArabic");

                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                }
            }
            return View(school);
        }

        // GET: Schools/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            School school = db.Schools.Find(id);
            if (school == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            ViewBag.KindOfSchoolId = new SelectList(db.KindOfSchools.Where(a => a.Is_Deleted != true), "Id", "KindOfSchollName",school.KindOfSchoolId);
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");//,school.District.Region.City.city_state_id);

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.KindOfSchoolId = new SelectList(db.KindOfSchools.Where(a => a.Is_Deleted != true), "Id", "KindOfSchollNameArabic", school.KindOfSchoolId);

                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");//, school.District.Region.City.State.state_id);

                }
            }
            return View(school);
        }

        // POST: Schools/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
     
        public ActionResult Edit(School school)
        {
            if (ModelState.IsValid)
            {
                var data = db.Schools.Where(a=>a.Is_Deleted!= true && a.Id != school.Id ) ;
                foreach (var item in data)
                {
                    if(item.SchoolName == school.SchoolName)
                    {
                        ViewBag.schoolerror = Languages.Language.NameAlreadyExist;
                        ViewBag.KindOfSchoolId = new SelectList(db.KindOfSchools.Where(a => a.Is_Deleted != true), "Id", "KindOfSchollName");
                        ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

                        if (Session["lang"] != null)
                        {
                            if (Session["lang"].ToString().Equals("ar-EG"))
                            {
                                ViewBag.KindOfSchoolId = new SelectList(db.KindOfSchools.Where(a => a.Is_Deleted != true), "Id", "KindOfSchollNameArabic");

                                ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                            }
                        }

                        return View(school);
                    }
                }

                var old = db.Schools.Find(school.Id);
                old.SchoolName = school.SchoolName;
                old.SchoolNameArabic = school.SchoolNameArabic;
                old.KindOfSchoolId = school.KindOfSchoolId;
                old.DistrictId = school.DistrictId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.schoolerror = Languages.Language.NameAlreadyExist;
            ViewBag.KindOfSchoolId = new SelectList(db.KindOfSchools.Where(a => a.Is_Deleted != true), "Id", "KindOfSchollName");
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.KindOfSchoolId = new SelectList(db.KindOfSchools.Where(a => a.Is_Deleted != true), "Id", "KindOfSchollNameArabic");

                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                }
            }

            return View(school);
        }

        // GET: Schools/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorpage", "home");

            }
            School school = db.Schools.Find(id);
            if (school == null)
            {
                return RedirectToAction("errorpage", "home");

            }
            return View(school);
        }

        // POST: Schools/Delete/5
        [HttpPost, ActionName("Delete")]
   
        public ActionResult DeleteConfirmed(int id)
        {
            School school = db.Schools.Find(id);
            school.Is_Deleted = true;
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
