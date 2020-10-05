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
    public class Live_StatusController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: Live_Status
        public ActionResult Index()
        {
            ViewBag.NId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
            ViewBag.Live_Status_Type = new SelectList(db.Live_Status_Type.Where(a => a.live_status_type_isDeleted != true), "live_status_type_id", "live_status_type_name");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.Live_Status_Type = new SelectList(db.Live_Status_Type.Where(a => a.live_status_type_isDeleted != true), "live_status_type_id", "live_status_type_name_arabic");

                }
            }
            //   var live_Status = db.Live_Status.Include(l => l.Live_Status_Type).Include(l => l.Citizen).Where(a => a.live_satus_isDeleted != true);
            //  return View(live_Status.ToList());
            return View();
        }

        public JsonResult GetAllLiveSatusByCitizenId(int tid)
        {
            db.Configuration.ProxyCreationEnabled = false;
           
            var persons = (

                from ct in db.Citizens
                 from lst in db.Live_Status_Type
                 from ls in db.Live_Status.Where(
                     a => a.citizen_citizen_id == ct.citizen_id && ct.citizen_id == tid
                     && a.live_satus_type_id == lst.live_status_type_id
                     && a.live_satus_isDeleted != true
                     )
                     
                 select new
                 {
                     ct.citizen_first_name,
                     ct.citizen_second_name,
                     ct.citizen_third_name,
                     ct.citizen_fourth_name,
                     ct.citizen_birthDate,

                     ct.citizen_first_name_arabic,
                     ct.citizen_second_name_arabic,
                     ct.citizen_third_name_arabic,
                     ct.citizen_fourth_name_arabic,
                     ls.live_satus_id,
                     ct.citizen_national_id,
                     lst.live_status_type_name,
                     lst.live_status_type_name_arabic,



                 });

           
    
            return Json(persons.ToList(), JsonRequestBehavior.AllowGet);


        }
       
        // GET: Live_Status/Create
        public ActionResult Create()
        {
           
            ViewBag.live_satus_type_id = new SelectList(db.Live_Status_Type.Where(a =>a.live_status_type_isDeleted != true), "live_status_type_id", "live_status_type_name");
            ViewBag.citizen_citizen_id = new SelectList(db.Citizens.Where(a =>a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.live_satus_type_id = new SelectList(db.Live_Status_Type.Where(a => a.live_status_type_isDeleted != true), "live_status_type_id", "live_status_type_name_arabic");

                }
            }
            //ViewBag.live_satus_type_id = new SelectList(db.Live_Status_Type, "live_status_type_id", "live_status_type_name");
            // ViewBag.citizen_citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id");
            return View();
        }


        [HttpPost]

        public ActionResult Create(Live_Status live_Status)
        {

            ViewBag.live_satus_type_id = new SelectList(db.Live_Status_Type.Where(a => a.live_status_type_isDeleted != true), "live_status_type_id", "live_status_type_name");
            ViewBag.citizen_citizen_id = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
            var data = db.Live_Status.Where(a => a.citizen_citizen_id == live_Status.citizen_citizen_id && a.live_satus_isDeleted != true).SingleOrDefault();

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.live_satus_type_id = new SelectList(db.Live_Status_Type.Where(a => a.live_status_type_isDeleted != true), "live_status_type_id", "live_status_type_name_arabic");

                }
            }
            if (data != null)
            {
                ViewBag.id = data.live_satus_id;
                ViewBag.edit= "Edit Page";
                    ViewBag.errMsgStatus = Servicely.Languages.Language.already_exist_go_to_edit_page_to_make_changes;
                    return View(live_Status);
                
            }

            db.Live_Status.Add(live_Status);
            db.SaveChanges();
            return RedirectToAction("Index");



        }

  

        // GET: Live_Status/Edit/5
        public ActionResult Edit(int id)
        {

            Live_Status live_Status = db.Live_Status.Find(id);

            ViewBag.live_satus_type_id = new SelectList(db.Live_Status_Type, "live_status_type_id", "live_status_type_name", live_Status.live_satus_type_id);
            ViewBag.citizen_citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", live_Status.citizen_citizen_id);

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.live_satus_type_id = new SelectList(db.Live_Status_Type, "live_status_type_id", "live_status_type_name_arabic", live_Status.live_satus_type_id);


                }
            }

            return View(live_Status);
        }


        [HttpPost]

        public ActionResult Edit(Live_Status live_Status)
        {

            ViewBag.live_satus_type_id = new SelectList(db.Live_Status_Type.Where(a => a.live_status_type_isDeleted != true), "live_status_type_id", "live_status_type_name");
            ViewBag.citizen_citizen_id = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
            var data = db.Live_Status.Where(a => a.citizen_citizen_id == live_Status.citizen_citizen_id && a.live_satus_isDeleted != true).SingleOrDefault();


            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.live_satus_type_id = new SelectList(db.Live_Status_Type.Where(a => a.live_status_type_isDeleted != true), "live_status_type_id", "live_status_type_name_arabic");



                }
            }
            if (data != null)
            {
                if (data.live_satus_type_id == live_Status.live_satus_type_id)
                {
                    ViewBag.errMsgStatus = Languages.Language.already  + data.Live_Status_Type.live_status_type_name;
                    return View(live_Status);
                }
                else
                {
                    data.live_satus_type_id = live_Status.live_satus_type_id;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }


           
              
            var old = db.Live_Status.Find(live_Status.live_satus_id);
            old.live_satus_type_id = live_Status.live_satus_type_id;
            old.citizen_citizen_id = live_Status.citizen_citizen_id;
           // db.Entry(live_Status).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            

        }

        // GET: Live_Status/Delete/5
        public ActionResult Delete(int id)
        {

            Live_Status live_Status = db.Live_Status.Find(id);

            return View(live_Status);
        }

        // POST: Live_Status/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            var old = db.Live_Status.Find(id);
            old.live_satus_isDeleted = true;
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
