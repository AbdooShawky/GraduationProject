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
    public class CoordinationsController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: Coordinations
        public ActionResult Index()
        {

            ViewBag.UType = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeName");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.UType = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeNameArabic");


                }
            }
            var coordinations = db.Coordinations.Where(a=> a.Is_Deleted!= true).Include(c => c.Faculty);
            return View(coordinations.ToList());
        }

      //--------- Ajax Call -------------

        public JsonResult GetAllUnversityByTypeId(int Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.Universities.Where(a=> a.UniversitryTypeId == Id && a.Is_Deleted!= true);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllFacultyByUniversityId(int Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.UniversityFacultyM_M.Where(a => a.Is_Deleted != true && a.UniversityId == Id).Select(a => new { a.Faculty.FacultyName, a.Faculty.FacultyNameArabic, a.Faculty.Id });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllCoordinationsByFacultyId(int Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.Coordinations.Where(a => a.Is_Deleted != true && a.FacultyId == Id).Select(a => new
            {
                a.Grade,
                a.Year,
                a.Id,
                a.Size
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // GET: Coordinations/Create
        public ActionResult Create()
        {
            ViewBag.UType = new SelectList(db.UniversityTypes.Where(a=> a.Is_Deleted!= true), "Id", "UniversityTypeName");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.UType = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeNameArabic");


                }
            }

                return View();
        }

        // POST: Coordinations/Create

        [HttpPost]
       
        public ActionResult Create( Coordination coordination)
        {
            if (ModelState.IsValid)
            {
                var data = db.Coordinations.Where(a => a.Is_Deleted != true && a.Year == DateTime.Now.Year && a.FacultyId == coordination.FacultyId).SingleOrDefault();
                if(data != null)
                {
                    ViewBag.ErrMessage =Languages.Language.CoordinationErr;
                    ViewBag.UType = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeName");
                    if (Session["lang"] != null)
                    {
                        if (Session["lang"].ToString().Equals("ar-EG"))
                        {
                            ViewBag.UType = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeNameArabic");


                        }
                    }
                    return View(coordination);
                }
                coordination.Year = DateTime.Now.Year;
                db.Coordinations.Add(coordination);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ErrMessage = Languages.Language.CoordinationErr;
            ViewBag.UType = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeName");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.UType = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeNameArabic");


                }
            }
            return View(coordination);
        }

        // GET: Coordinations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coordination coordination = db.Coordinations.Find(id);
            if (coordination == null)
            {
                return HttpNotFound();
            }
            ViewBag.ErrMessage = Languages.Language.CoordinationErr;
            ViewBag.UType = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeName");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.UType = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeNameArabic");


                }
            }
            return View(coordination);
        }

        // POST: Coordinations/Edit/5
       
        [HttpPost]
       
        public ActionResult Edit( Coordination coordination)
        {
            if (ModelState.IsValid)
            {
                var data = db.Coordinations.Where(a => a.Is_Deleted != true && a.Id != coordination.Id);
                foreach (var item in data)
                {
                    if(item.Year == coordination.Year && item.FacultyId == coordination.FacultyId)
                    {
                        ViewBag.ErrMessage = Languages.Language.CoordinationErr;
                        ViewBag.UType = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeName");
                        if (Session["lang"] != null)
                        {
                            if (Session["lang"].ToString().Equals("ar-EG"))
                            {
                                ViewBag.UType = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeNameArabic");


                            }
                        }
                        return View(coordination);
                    }
                }

                var old = db.Coordinations.Find(coordination.Id);
                old.Size = coordination.Size;
                old.FacultyId = coordination.FacultyId;
                old.Year = coordination.Year;
                old.Grade = coordination.Grade;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ErrMessage = Languages.Language.CoordinationErr;
            ViewBag.UType = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeName");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.UType = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeNameArabic");


                }
            }
            return View(coordination);
        }

        // GET: Coordinations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coordination coordination = db.Coordinations.Find(id);
            if (coordination == null)
            {
                return HttpNotFound();
            }
            return View(coordination);
        }

        // POST: Coordinations/Delete/5
        [HttpPost, ActionName("Delete")]
   
        public ActionResult DeleteConfirmed(int id)
        {
            Coordination coordination = db.Coordinations.Find(id);
            coordination.Is_Deleted = true;
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
