using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Servicely.Models;

namespace Servicely.Controllers
{
    public class UniversitiesController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: Universities
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

            var universities = db.Universities.Where(a=> a.Is_Deleted!= true).Include(u => u.UniversityType);
            return View(universities.ToList());
        }
        public JsonResult GetAllUniversityBydistrictId(int dis)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.Universities.Where(a => a.Is_Deleted != true && a.DistrictId == dis).Select(a => new { a.UniversityName, a.Id, a.UniversityNameArabic,a.UniversityType.UniversityTypeName,a.UniversityType.UniversityTypeNameArabic }).ToList();



            return Json(data, JsonRequestBehavior.AllowGet);
        }



        // GET: Universities/Create
        public ActionResult Create()
        {
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            ViewBag.UniversitryTypeId = new SelectList(db.UniversityTypes.Where(a=> a.Is_Deleted!= true), "Id", "UniversityTypeName");
            if(Session["lang"] != null)
                {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.UniversitryTypeId = new SelectList(db.UniversityTypes.Where(a=> a.Is_Deleted!= true), "Id", "UniversityTypeNameArabic");

                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                }
            }
                return View();
        }

        // POST: Universities/Create
      
        [HttpPost]
        
        public ActionResult Create( University university , UploadPhotos upload)
        {
            if (ModelState.IsValid)
            {
                var data = db.Universities.Where(a => a.Is_Deleted != true && a.UniversityName == university.UniversityName).SingleOrDefault();
                
                if( data != null )
                {
                    ViewBag.universtyErr = Languages.Language.universtyErr;
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

                    ViewBag.UniversitryTypeId = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeName");
                    if (Session["lang"] != null)
                    {
                        if (Session["lang"].ToString().Equals("ar-EG"))
                        {
                            ViewBag.UniversitryTypeId = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeNameArabic");

                            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                        }
                    }
                    return View(university);
                }

                string filename = university.UniversityName + Path.GetExtension(upload.f1.FileName);
                string filePath = Server.MapPath("~/UniversityLogo/");
                string filePathName = Path.Combine(filePath, filename);
                upload.f1.SaveAs(filePathName);
                university.UniversityLogo = filename;
                db.Universities.Add(university);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UniversitryTypeId = new SelectList(db.UniversityTypes, "Id", "UniversityTypeName", university.UniversitryTypeId);
            return View(university);
        }

        // GET: Universities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            University university = db.Universities.Find(id);
            if (university == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");//university.District.Region.City.State.state_id);
            ViewBag.district = new SelectList(db.Districts.Where(a => a.district_isDeleted != true), "district_id", "district_name", university.DistrictId);

            ViewBag.UniversitryTypeId = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeName", university.UniversitryTypeId);
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.UniversitryTypeId = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeNameArabic", university.UniversitryTypeId);

                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name", university.District.Region.City.State.state_id);
                    ViewBag.district = new SelectList(db.Districts.Where(a => a.district_isDeleted != true), "district_id", "district_arabic_name", university.DistrictId);


                }
            }
            return View(university);
        }

        // POST: Universities/Edit/5
      
        [HttpPost]
       
        public ActionResult Edit( University university)
        {
            if (ModelState.IsValid)
            {
                var data = db.Universities.Where(a => a.Id != university.Id && a.Is_Deleted != true);
                foreach (var item in data)
                {
                    if (item.UniversityName  == university.UniversityName)
                    {
                        ViewBag.universtyErr = Languages.Language.universtyErr;
                        ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name", university.District.Region.City.State.state_id);
                        ViewBag.district = new SelectList(db.Districts.Where(a => a.district_isDeleted != true), "district_id", "district_name", university.DistrictId);

                        ViewBag.UniversitryTypeId = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeName", university.UniversitryTypeId);
                        if (Session["lang"] != null)
                        {
                            if (Session["lang"].ToString().Equals("ar-EG"))
                            {
                                ViewBag.UniversitryTypeId = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeNameArabic", university.UniversitryTypeId);

                                ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name", university.District.Region.City.State.state_id);
                                ViewBag.district = new SelectList(db.Districts.Where(a => a.district_isDeleted != true), "district_id", "district_arabic_name", university.DistrictId);

                            }
                        }
                        return View(university);
                    }
                }
                var old = db.Universities.Find(university.Id);
                old.DistrictId = university.DistrictId;
                old.UniversityNameArabic = university.UniversityNameArabic;
                old.UniversityName = university.UniversityName;
                old.UniversitryTypeId = university.UniversitryTypeId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UniversitryTypeId = new SelectList(db.UniversityTypes, "Id", "UniversityTypeName", university.UniversitryTypeId);
            return View(university);
        }

        // GET: Universities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            University university = db.Universities.Find(id);
            if (university == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            return View(university);
        }

        // POST: Universities/Delete/5
        [HttpPost, ActionName("Delete")]
       
        public ActionResult DeleteConfirmed(int id)
        {
            University university = db.Universities.Find(id);
            university.Is_Deleted = true;
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
