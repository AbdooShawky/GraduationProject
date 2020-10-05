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
    public class SchoolPhasesM_MController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: SchoolPhasesM_M
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

            var schoolPhasesM_M = db.SchoolPhasesM_M.Where(a=> a.Is_Deleted!= true).Include(s => s.PhasesOfSchoole).Include(s => s.School);
            return View(schoolPhasesM_M.ToList());
        }

        public JsonResult GetAllSchoolsBySchooTypeId(int typeId, int dis)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.SchoolPhasesM_M.Where(a => a.Is_Deleted != true && a.PhaseId == typeId && a.School.DistrictId == dis).Select(a => new { a.School.SchoolName, a.Id , a.School.SchoolNameArabic , a.PhasesOfSchoole.PhaseName,a.PhasesOfSchoole.PhaseNameArabic }).ToList();


            return Json(data, JsonRequestBehavior.AllowGet);
        }


        // GET: SchoolPhasesM_M/Create
        public ActionResult Create()
        {
            ViewBag.PhaseId = new SelectList(db.PhasesOfSchooles.Where(a=> a.Is_Deleted!= true), "Id", "PhaseName");
            ViewBag.SchoolId = new SelectList(db.Schools.Where(a=> a.Is_Deleted!= true), "Id", "SchoolName");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.PhaseId = new SelectList(db.PhasesOfSchooles.Where(a => a.Is_Deleted != true), "Id", "PhaseNameArabic");
                    ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.Is_Deleted != true), "Id", "SchoolNameArabic");


                }
            }
     
            return View();
        }

        // POST: SchoolPhasesM_M/Create
      
        [HttpPost]
        
        public ActionResult Create( SchoolPhasesM_M schoolPhasesM_M)
        {
            if (ModelState.IsValid)
            {
                var data = db.SchoolPhasesM_M.Where(a=> a.Is_Deleted != true && a.PhaseId == schoolPhasesM_M.PhaseId && a.SchoolId == schoolPhasesM_M.SchoolId).SingleOrDefault();
                if(data != null)
                {
                    ViewBag.SchoolPhaseErr = Languages.Language.SchoolPhaseErr;
                    ViewBag.PhaseId = new SelectList(db.PhasesOfSchooles.Where(a => a.Is_Deleted != true), "Id", "PhaseName");
                    ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.Is_Deleted != true), "Id", "SchoolName");
                    if (Session["lang"] != null)
                    {
                        if (Session["lang"].ToString().Equals("ar-EG"))
                        {
                            ViewBag.PhaseId = new SelectList(db.PhasesOfSchooles.Where(a => a.Is_Deleted != true), "Id", "PhaseNameArabic");
                            ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.Is_Deleted != true), "Id", "SchoolNameArabic");


                        }
                    }
                    return View(schoolPhasesM_M);
                }
                db.SchoolPhasesM_M.Add(schoolPhasesM_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PhaseId = new SelectList(db.PhasesOfSchooles, "Id", "PhaseName", schoolPhasesM_M.PhaseId);
            ViewBag.SchoolId = new SelectList(db.Schools, "Id", "SchoolName", schoolPhasesM_M.SchoolId);
            return View(schoolPhasesM_M);
        }

        // GET: SchoolPhasesM_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            SchoolPhasesM_M schoolPhasesM_M = db.SchoolPhasesM_M.Find(id);
            if (schoolPhasesM_M == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            ViewBag.PhaseId = new SelectList(db.PhasesOfSchooles.Where(a => a.Is_Deleted != true), "Id", "PhaseName", schoolPhasesM_M.PhaseId);
            ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.Is_Deleted != true), "Id", "SchoolName", schoolPhasesM_M.SchoolId);
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.PhaseId = new SelectList(db.PhasesOfSchooles.Where(a => a.Is_Deleted != true), "Id", "PhaseNameArabic", schoolPhasesM_M.PhaseId);
                    ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.Is_Deleted != true), "Id", "SchoolNameArabic", schoolPhasesM_M.SchoolId);


                }
            }
            return View(schoolPhasesM_M);
        }

        // POST: SchoolPhasesM_M/Edit/5
    
        [HttpPost]
        
        public ActionResult Edit( SchoolPhasesM_M schoolPhasesM_M)
        {
            if (ModelState.IsValid)
            {
                var data = db.SchoolPhasesM_M.Where(a => a.Is_Deleted != true && a.Id != schoolPhasesM_M.Id);
                foreach (var item in data)
                {
                    if( item.PhaseId == schoolPhasesM_M.PhaseId && item.SchoolId == schoolPhasesM_M.SchoolId)
                    {
                        ViewBag.SchoolPhaseErr = Languages.Language.SchoolPhaseErr;
                        ViewBag.PhaseId = new SelectList(db.PhasesOfSchooles.Where(a => a.Is_Deleted != true), "Id", "PhaseName", schoolPhasesM_M.PhaseId);
                        ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.Is_Deleted != true), "Id", "SchoolName", schoolPhasesM_M.SchoolId);
                        if (Session["lang"] != null)
                        {
                            if (Session["lang"].ToString().Equals("ar-EG"))
                            {
                                ViewBag.PhaseId = new SelectList(db.PhasesOfSchooles.Where(a => a.Is_Deleted != true), "Id", "PhaseNameArabic", schoolPhasesM_M.PhaseId);
                                ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.Is_Deleted != true), "Id", "SchoolNameArabic", schoolPhasesM_M.SchoolId);


                            }
                        }
                        return View(schoolPhasesM_M);

                    }
                }
                var old = db.SchoolPhasesM_M.Find(schoolPhasesM_M.Id);
                old.SchoolId = schoolPhasesM_M.SchoolId;
                old.PhaseId = schoolPhasesM_M.PhaseId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SchoolPhaseErr = Languages.Language.SchoolPhaseErr;
            ViewBag.PhaseId = new SelectList(db.PhasesOfSchooles.Where(a => a.Is_Deleted != true), "Id", "PhaseName");
            ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.Is_Deleted != true), "Id", "SchoolName");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.PhaseId = new SelectList(db.PhasesOfSchooles.Where(a => a.Is_Deleted != true), "Id", "PhaseNameArabic");
                    ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.Is_Deleted != true), "Id", "SchoolNameArabic");


                }
            }
            return View(schoolPhasesM_M);
        }

        // GET: SchoolPhasesM_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            SchoolPhasesM_M schoolPhasesM_M = db.SchoolPhasesM_M.Find(id);
            if (schoolPhasesM_M == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            return View(schoolPhasesM_M);
        }

        // POST: SchoolPhasesM_M/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            SchoolPhasesM_M schoolPhasesM_M = db.SchoolPhasesM_M.Find(id);
            schoolPhasesM_M.Is_Deleted = true;
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
