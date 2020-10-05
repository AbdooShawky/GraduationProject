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
    public class EducationDirectoratesController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: EducationDirectorates
        public ActionResult Index()
        {
            ViewBag.StateId = new SelectList(db.States, "state_id", "state_name");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.StateId = new SelectList(db.States, "state_id", "state_arabic_name");

                }
            }

            return View();
        }
        // ajax call
        public JsonResult GetAllDirectorateByStateId(int Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.EducationDirectorates.Where(a => a.Is_Deleted != true && a.StateId == Id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
       

        // GET: EducationDirectorates/Create
        public ActionResult Create()
        {
            ViewBag.StateId = new SelectList(db.States.Where(a=> a.state_isDeleted!= true), "state_id", "state_name");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.StateId = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                }
               
            }

            return View();
        }

       
        [HttpPost]
        
        public ActionResult Create( EducationDirectorate educationDirectorate)
        {
            if (ModelState.IsValid)
            {
                var data = db.EducationDirectorates.Where(a => a.Is_Deleted != true && a.DirectorateName == educationDirectorate.DirectorateName).SingleOrDefault();
          
                if( data != null)
                {
                    ViewBag.StateId = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
                    if (Session["lang"] != null)
                    {
                        if (Session["lang"].ToString().Equals("ar-EG"))
                        {
                            ViewBag.StateId = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                        }

                    }
                    return View(educationDirectorate);
                }


                db.EducationDirectorates.Add(educationDirectorate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StateId = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.StateId = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                }

            }

            return View(educationDirectorate);
        }

        // GET: EducationDirectorates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EducationDirectorate educationDirectorate = db.EducationDirectorates.Find(id);
            if (educationDirectorate == null)
            {
                return HttpNotFound();
            }
            ViewBag.StateId = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.StateId = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                }

            }
            return View(educationDirectorate);
        }

      
        [HttpPost]
    
        public ActionResult Edit( EducationDirectorate educationDirectorate)
        {
            if (ModelState.IsValid)
            {

                var data = db.EducationDirectorates.Where(a => a.Is_Deleted != true && a.Id != educationDirectorate.Id);
                foreach (var item in data)
                {
                    if( item.DirectorateName == educationDirectorate.DirectorateName)
                    {
                        ViewBag.StateId = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
                        if (Session["lang"] != null)
                        {
                            if (Session["lang"].ToString().Equals("ar-EG"))
                            {
                                ViewBag.StateId = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                            }

                        }
                        return View(educationDirectorate);
                    }
                }


                var old = db.EducationDirectorates.Find(educationDirectorate.Id);
                old.DirectorateName = educationDirectorate.DirectorateName;
                old.DirectorateNameArabic = educationDirectorate.DirectorateNameArabic;
                old.StateId = educationDirectorate.StateId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StateId = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.StateId = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                }

            }
            return View(educationDirectorate);
        }

        // GET: EducationDirectorates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EducationDirectorate educationDirectorate = db.EducationDirectorates.Find(id);
            if (educationDirectorate == null)
            {
                return HttpNotFound();
            }
            return View(educationDirectorate);
        }

        // POST: EducationDirectorates/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            EducationDirectorate educationDirectorate = db.EducationDirectorates.Find(id);
            db.EducationDirectorates.Remove(educationDirectorate);
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
