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
    public class PhotosController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: Photos
        public ActionResult Index()
        {
            ViewBag.Photo_citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id");

            var photos = db.Photos.Include(p => p.Citizen);
            return View(photos.ToList());
        }
        public JsonResult GetinfoByCitizenId(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var data = db.Photos.Where(a => a.Photo_citizen_id == id).Select(a => new {a.Photo_isCurrent ,a.Photo_id,a.Photo_Url ,a.Citizen.citizen_first_name,a.Citizen.citizen_first_name_arabic , a.Citizen.citizen_second_name,a.Citizen.citizen_second_name_arabic,a.Citizen.citizen_third_name,a.Citizen.citizen_third_name_arabic,a.Citizen.citizen_fourth_name, a.Citizen.citizen_fourth_name_arabic });
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UserIndex()
        {
            int cid = 0;
            if (Session["citizenID"] != null)
                cid = (int)Session["citizenID"];

            

            var photos = db.Photos.Where(a=> a.Photo_citizen_id == cid).FirstOrDefault();
            return View(photos);
        }

        // GET: Photos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // GET: Photos/Create
        public ActionResult Create()
        {
            ViewBag.Photo_citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id");
            return View();
        }

        // POST: Photos/Create
      
        [HttpPost]
     
        public ActionResult Create( UploadPhotos upload)
        {
            var data = db.Citizens.Find(upload.Photo_citizen_id);
            if (ModelState.IsValid)
            {

                string filename =  data.citizen_national_id+ "_" + data.citizen_first_name  + Path.GetExtension(upload.f1.FileName);
                string filePath = Server.MapPath("~/CitizenPhotos/" );
                string filePathName = Path.Combine( filePath , filename);
                upload.f1.SaveAs(filePathName);

                var old = db.Photos.Where(a => a.Photo_citizen_id == upload.Photo_citizen_id).ToList();
                if(old != null)
                {
                    foreach (var item in old)
                    {
                        item.Photo_isCurrent = false;
                        db.SaveChanges();
                    }
                }
               
                Photo p = new Photo();
                p.Photo_Url = filename;
                p.Photo_citizen_id = upload.Photo_citizen_id;
                p.Photo_isCurrent = true;
                db.Photos.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Photo_citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", upload.Photo_citizen_id);
            return View(upload);
        }

        // GET: Photos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            ViewBag.Photo_citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", photo.Photo_citizen_id);
            return View(photo);
        }

        // POST: Photos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Photo_id,Photo_Url,Photo_citizen_id")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(photo).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Photo_citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", photo.Photo_citizen_id);
            return View(photo);
        }

        // GET: Photos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: Photos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Photo photo = db.Photos.Find(id);
            db.Photos.Remove(photo);
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
