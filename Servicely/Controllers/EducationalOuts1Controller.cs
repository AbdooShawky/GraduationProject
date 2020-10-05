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
    public class EducationalOuts1Controller : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: EducationalOuts1
        public ActionResult Index()
        {
            var educationalOuts = db.EducationalOuts.Where(a=> a.Is_Deleted!= true).Include(e => e.Citizen);
            return View(educationalOuts.ToList());
        }

       

        // GET: EducationalOuts1/Create
        public ActionResult Create()
        {
            ViewBag.CitizenId = new SelectList(db.Citizens.Where(a=> a.citizen_isDeleted!= true), "citizen_id", "citizen_national_id");
            return View();
        }

        // POST: EducationalOuts1/Create
      
        [HttpPost]
       
        public ActionResult Create(EducationalOut educationalOut , UploadPhotos f1)
        {
            var data = db.Citizens.Find(educationalOut.CitizenId);

            if (ModelState.IsValid)
            {
                var va = db.EducationalOuts.Where(a => a.Is_Deleted != true && a.CitizenId == educationalOut.CitizenId).SingleOrDefault();
                if(va != null)
                {
                    ViewBag.errmessage = Servicely.Languages.Language.NameAlreadyExist;
                    ViewBag.CitizenId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id", educationalOut.CitizenId);
                    return View(educationalOut);
                }

                string filename = data.citizen_national_id + "_" + data.citizen_first_name  + Path.GetExtension(f1.f1.FileName);
                string filePath = Server.MapPath("~/EducatedOut/");
                string filePathName = Path.Combine(filePath, filename);
                f1.f1.SaveAs(filePathName);
                educationalOut.Date = DateTime.Now;
                educationalOut.DocumentName = filename;
                db.EducationalOuts.Add(educationalOut);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CitizenId = new SelectList(db.Citizens.Where(a=> a.citizen_isDeleted!= true), "citizen_id", "citizen_national_id", educationalOut.CitizenId);
            return View(educationalOut);
        }

        // GET: EducationalOuts1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            EducationalOut educationalOut = db.EducationalOuts.Find(id);
            if (educationalOut == null)
            {
                return HttpNotFound();
            }
            ViewBag.CitizenId = new SelectList(db.Citizens.Where(a=> a.citizen_isDeleted!= true), "citizen_id", "citizen_national_id", educationalOut.CitizenId);
            return View(educationalOut);
        }

        // POST: EducationalOuts1/Edit/5
        
        [HttpPost]
        
        public ActionResult Edit( EducationalOut educationalOut, UploadPhotos f1)
        {
            var data = db.Citizens.Find(educationalOut.CitizenId);
            if (ModelState.IsValid)
            {
                var va = db.EducationalOuts.Where(a => a.Is_Deleted != true && a.Id != educationalOut.Id);

                foreach (var item in va)
                {
                    if(item.CitizenId == educationalOut.CitizenId)
                    {
                        ViewBag.errmessage = Servicely.Languages.Language.NameAlreadyExist;
                        ViewBag.CitizenId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id", educationalOut.CitizenId);
                        return View(educationalOut);
                    }
                }

                string filename = data.citizen_national_id + "_" + data.citizen_first_name + Path.GetExtension(f1.f1.FileName);
                string filePath = Server.MapPath("~/EducatedOut/");
                string filePathName = Path.Combine(filePath, filename);
                f1.f1.SaveAs(filePathName);
                educationalOut.Date = DateTime.Now;
                educationalOut.DocumentName = filename;

                var old = db.EducationalOuts.Find(educationalOut.Id);
                old.CitizenId = educationalOut.CitizenId;
                old.DocumentName = educationalOut.DocumentName;
               
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CitizenId = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", educationalOut.CitizenId);
            return View(educationalOut);
        }

        // GET: EducationalOuts1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EducationalOut educationalOut = db.EducationalOuts.Find(id);
            if (educationalOut == null)
            {
                return HttpNotFound();
            }
            return View(educationalOut);
        }

        // POST: EducationalOuts1/Delete/5
        [HttpPost, ActionName("Delete")]
       
        public ActionResult DeleteConfirmed(int id)
        {
            EducationalOut educationalOut = db.EducationalOuts.Find(id);
            educationalOut.Is_Deleted = true;
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
