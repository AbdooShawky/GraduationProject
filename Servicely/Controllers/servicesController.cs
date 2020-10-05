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
    public class servicesController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: services
        public ActionResult Index()
        {
            ViewBag.Gove = new SelectList(db.governement_body.Where(a => a.governement_isDeleted != true), "id", "governement_name");

            if (Session["lang"] != null)
                {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {

                    ViewBag.Gove = new SelectList(db.governement_body.Where(a => a.governement_isDeleted != true), "id", "governement_name_arabic");

                }
               
            }
             
            var services = db.services.Where(a=> a.service_isDeleted!= true).Include(s => s.governement_body);
            return View(services.ToList());
        }

        public JsonResult GetAllServicesByGId(int Id)
        {
            var data = db.services.Where(a => a.service_isDeleted != true && a.goverenemnt_id == Id).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // GET: services/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            service service = db.services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: services/Create
        public ActionResult Create()
        {
            ViewBag.goverenemnt_id = new SelectList(db.governement_body.Where(a => a.governement_isDeleted != true), "id", "governement_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {

                    ViewBag.goverenemnt_id = new SelectList(db.governement_body.Where(a => a.governement_isDeleted != true), "id", "governement_name_arabic");

                }
            }
            return View();
        }

       
        [HttpPost]
      
        public ActionResult Create( service service)
        {
            if (ModelState.IsValid)
            {
                var data = db.services.Where(a=> a.service_name == service.service_name).SingleOrDefault();
                if(data != null)
                {
                    ViewBag.goverenemnt_id = new SelectList(db.governement_body.Where(a => a.governement_isDeleted != true), "id", "governement_name");


                    if (Session["lang"] != null)
                    {
                        if (Session["lang"].ToString().Equals("ar-EG"))
                        {

                            ViewBag.goverenemnt_id = new SelectList(db.governement_body.Where(a => a.governement_isDeleted != true), "id", "governement_name_arabic");
                            ViewBag.sererror = Languages.Language.Service_Already_exist;
                            return View();
                        }
                    }

                    ViewBag.sererror = Languages.Language.Service_Already_exist;
                    return View();
                }
                db.services.Add(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.goverenemnt_id = new SelectList(db.governement_body.Where(a => a.governement_isDeleted != true), "id", "governement_name", service.goverenemnt_id);
            return View(service);                                                                          
        }

        // GET: services/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            service service = db.services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            ViewBag.goverenemnt_id = new SelectList(db.governement_body.Where(a => a.governement_isDeleted != true), "id", "governement_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {

                    ViewBag.goverenemnt_id = new SelectList(db.governement_body.Where(a => a.governement_isDeleted != true), "id", "governement_name_arabic");

                }
            }



            return View(service);
        }

        
        [HttpPost]
        
        public ActionResult Edit( service service)
        {
            if (ModelState.IsValid)
            {
                var data = db.services.Where(a=> a.service_id != service.service_id).ToList();
                
                foreach (var item in data)
                {
                    if(item.service_name == service.service_name)
                    {


                        ViewBag.goverenemnt_id = new SelectList(db.governement_body, "id", "governement_name");

                        if (Session["lang"] != null)
                        {
                            if (Session["lang"].ToString().Equals("ar-EG"))
                            {

                                ViewBag.goverenemnt_id = new SelectList(db.governement_body, "id", "governement_name_arabic");

                            }
                        }


                    }
                }
                var old = db.services.Find(service.service_id);
                old.service_name = service.service_name;
                old.service_name_arabic = service.service_name_arabic;
                old.service_price = service.service_price;
                old.service_time = old.service_time;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.governement_id = new SelectList(db.governement_body, "id", "governement_name", service.goverenemnt_id);
            return View(service);
        }

        // GET: services/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            service service = db.services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: services/Delete/5
        [HttpPost, ActionName("Delete")]
  
        public ActionResult DeleteConfirmed(int id)
        {
            service service = db.services.Find(id);
            service.service_isDeleted = true;
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
