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
    public class DeceasedsController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: Deceaseds
        public ActionResult Index()
        {
            ViewBag.NId = new SelectList(db.Deceaseds.Where(a => a.deceased_isDeleted != true ).Select(a=> new { a.deceased_id , a.Citizen.citizen_national_id}), "deceased_id", "citizen_national_id");

            return View(db.Deceaseds.Where(a=> a.deceased_isDeleted!= true).Include(a=>a.Citizen).ToList());
        }

        public JsonResult GetAllDeceadedsBYId(int Id)
        {
            var data = db.Deceaseds.Where(a => a.deceased_isDeleted != true && a.deceased_id == Id).Select(a=> new {a.Citizen.citizen_first_name, a.Citizen.citizen_second_name,a.Citizen.citizen_third_name, a.Citizen.citizen_fourth_name, a.Citizen.citizen_first_name_arabic, a.Citizen.citizen_second_name_arabic,a.Citizen.citizen_third_name_arabic, a.Citizen.citizen_fourth_name_arabic , a.deceased_deathDate , a.deceased_deathPlace , a.deceased_deathPlace_arabic , a.deceased_deathCause, a.deceased_deathCause_arabic , a.deceased_id}).SingleOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDeathInfoByCitizenId(int tid)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var persons = (

                 from ct in db.Citizens
                 from cont in db.Deceaseds.Where(
                     a => a.deceased_citizenId == ct.citizen_id && ct.citizen_id == tid
                     && ct.citizen_isDeleted != true &&  a.deceased_isDeleted !=true
                     )

                 select new
                 {
                     ct.citizen_first_name,
                     ct.citizen_second_name,
                     ct.citizen_third_name,
                     ct.citizen_fourth_name,
                     ct.citizen_first_name_arabic,
                     ct.citizen_second_name_arabic,
                     ct.citizen_third_name_arabic,
                     ct.citizen_fourth_name_arabic,
                     cont.deceased_deathDate,
                     cont.deceased_deathPlace,
                     cont.deceased_deathPlace_arabic,
                     cont.deceased_deathCause,
                     cont.deceased_deathCause_arabic,
                     ct.citizen_national_id,
                     ct.citizen_gender,
                     cont.deceased_id
                    


                 });

            return Json(persons.ToList(), JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetDeathCitizen()
        {
            db.Configuration.ProxyCreationEnabled = false;

            var persons = (

                 from ct in db.Citizens
                 from lst in db.Live_Status_Type
                 from Ls  in db.Live_Status .Where(
                     a => a.live_satus_type_id == lst.live_status_type_id && a.live_satus_type_id == '2'
                     && ct.citizen_isDeleted != true && lst.live_status_type_isDeleted  !=true
                     )

                 select new
                 {
                     ct.citizen_id,
                     ct.citizen_national_id
                    
                 });

            return Json(persons.ToList(), JsonRequestBehavior.AllowGet);



        }

        // GET: Deceaseds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deceased deceased = db.Deceaseds.Find(id);
            if (deceased == null)
            {
                return HttpNotFound();
            }
            return View(deceased);
        }

        public class QueryResults
        {
            public string Value { get; set; } // change to int if Users.Id has integer value
            public string Text { get; set; }
        }

        // GET: Deceaseds/Create
        public ActionResult Create()
        {
            ViewBag.zero = "0";
            ViewBag.deceased_citizenId = new SelectList(db.Citizens.Where(a =>a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
            // ViewBag.deceased_citizenId = new SelectList(db.Citizens, "citizen_id", "citizen_national_id");
            return View();
        }

        [HttpPost]
        public ActionResult Create( Deceased deceased)
        {
            ViewBag.deceased_citizenId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
            var data = db.Deceaseds.Where(a => a.deceased_citizenId == deceased.deceased_citizenId  &&  a.deceased_isDeleted != true).SingleOrDefault();

            if (data != null)
            {
                ViewBag.errMsgDead = Languages.Language.AlreadyDied;
                return View(deceased);
            }

            if (ModelState.IsValid)
            {
                var old = db.Citizens.Find(deceased.deceased_citizenId);
                var oldL = db.Live_Status.Where(a=> a.citizen_citizen_id == old.citizen_id).SingleOrDefault();
                oldL.live_satus_type_id = 2;
                
                db.Deceaseds.Add(deceased);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(deceased);
        }

        // GET: Deceaseds/Edit/5
        public ActionResult Edit(int id)
        {
            Deceased deceased = db.Deceaseds.Find(id);
            ViewBag.deceased_citizenId = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", deceased.deceased_citizenId);
            return View(deceased);
        }

        [HttpPost]
        public ActionResult Edit(Deceased deceased)
        {
            ViewBag.deceased_citizenId = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", deceased.deceased_citizenId);
            var data = db.Deceaseds.Where(a =>a.deceased_citizenId !=deceased.deceased_citizenId &&a.deceased_id != deceased.deceased_id && a.deceased_isDeleted != true ).SingleOrDefault();

            if (data != null)
            {
                ViewBag.errMsg = Servicely.Languages.Language.Already_exist_for_this_citizen;
                return View(deceased);
            }

            var old = db.Deceaseds.Find(deceased.deceased_id);
            old.deceased_citizenId = deceased.deceased_citizenId;
            old.deceased_deathCause = deceased.deceased_deathCause;
            old.deceased_deathDate = deceased.deceased_deathDate;
            old.deceased_deathPlace = deceased.deceased_deathPlace;
            old.deceased_deathPlace_arabic = deceased.deceased_deathPlace_arabic;
            old.deceased_deathCause_arabic = deceased.deceased_deathCause_arabic;
            //  db.Entry(deceased).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult GetNamesByCitizenId(int ctId)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return Json(db.Citizens.Where(a => a.citizen_id == ctId && a.citizen_isDeleted != true), JsonRequestBehavior.AllowGet);
        }
        // GET: Deceaseds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deceased deceased = db.Deceaseds.Find(id);
            if (deceased == null)
            {
                return HttpNotFound();
            }
            return View(deceased);
        }

        // POST: Deceaseds/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var old = db.Deceaseds.Find(id);
            old.deceased_isDeleted = true;
            db.SaveChanges();
            return RedirectToAction("Index");
            //Deceased deceased = db.Deceaseds.Find(id);
           // db.Deceaseds.Remove(deceased);
            //db.SaveChanges();
           // return RedirectToAction("Index");
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
