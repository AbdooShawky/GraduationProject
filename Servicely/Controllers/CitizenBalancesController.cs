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
    public class CitizenBalancesController : Controller
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: CitizenBalances
        public ActionResult Index()
        {
            var citizenBalances = db.CitizenBalances.Include(c => c.Citizen);
            return View(citizenBalances.ToList());
        }

        // GET: CitizenBalances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CitizenBalance citizenBalance = db.CitizenBalances.Find(id);
            if (citizenBalance == null)
            {
                return HttpNotFound();
            }
            return View(citizenBalance);
        }

        // GET: CitizenBalances/Create
        public ActionResult Create()
        {
            ViewBag.CitizenBalance_citizen_id = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
            return View();
        }

        public ActionResult Deposit()
        {
            ViewBag.CitizenBalance_citizen_id = new SelectList(db.CitizenBalances.Where(a=> a.CitizenBalance_isDeleted!= true).Select(a=> new { a.Citizen.citizen_national_id , a.CitizenBalance_id}), "CitizenBalance_id", "citizen_national_id");
            return View();
        }

        [HttpPost]
        public ActionResult Deposit( CitizenBalance c)
        {

            var old = db.CitizenBalances.Find(c.CitizenBalance_id);
            old.CitizenBalance_balance += c.CitizenBalance_balance;
            db.SaveChanges();
            
            ViewBag.CitizenBalance_citizen_id = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
            return RedirectToAction("Index");
        }
        public ActionResult Withdrow()
        {
            ViewBag.CitizenBalance_citizen_id = new SelectList(db.CitizenBalances.Where(a => a.CitizenBalance_isDeleted != true).Select(a => new { a.Citizen.citizen_national_id, a.CitizenBalance_id }), "CitizenBalance_id", "citizen_national_id");
            return View();
        }

        public JsonResult GetWithdrow(int Id , int balance)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var old = db.CitizenBalances.Where(a=> a.CitizenBalance_citizen_id ==Id).SingleOrDefault();
            old.CitizenBalance_balance -= balance;
            db.SaveChanges();
            return Json("Successful process", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]

        public ActionResult Withdrow(CitizenBalance c)
        {

            var old = db.CitizenBalances.Find(c.CitizenBalance_id);
            old.CitizenBalance_balance -= c.CitizenBalance_balance;
            db.SaveChanges();

            ViewBag.CitizenBalance_citizen_id = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
            return RedirectToAction("Index");
        }

        [HttpPost]
    
        public ActionResult Create( CitizenBalance citizenBalance)
        {
            if (ModelState.IsValid)
            {
                var data = db.CitizenBalances.Where(a=> a.CitizenBalance_citizen_id == citizenBalance.CitizenBalance_citizen_id).SingleOrDefault();
                if (data != null)
                {
                    ViewBag.errbalance = Languages.Language.citizen_balance;
                    ViewBag.CitizenBalance_citizen_id = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");

                    return View(citizenBalance);
                
                }


                db.CitizenBalances.Add(citizenBalance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CitizenBalance_citizen_id = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");

            return View(citizenBalance);
        }

        // GET: CitizenBalances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CitizenBalance citizenBalance = db.CitizenBalances.Find(id);
            if (citizenBalance == null)
            {
                return HttpNotFound();
            }
            ViewBag.CitizenBalance_citizen_id = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
            return View(citizenBalance);
        }

        [HttpPost]
    
        public ActionResult Edit( CitizenBalance citizenBalance)
        {
            if (ModelState.IsValid)
            {
                var data = db.CitizenBalances.Where(a => a.CitizenBalance_citizen_id != citizenBalance.CitizenBalance_citizen_id);
                foreach (var item in data)
                {
                    if( item.CitizenBalance_citizen_id == citizenBalance.CitizenBalance_citizen_id)
                    {

                        ViewBag.errbalance = Languages.Language.citizen_balance;
                        ViewBag.CitizenBalance_citizen_id = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");

                        return View();
                    }
                }
                db.Entry(citizenBalance).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CitizenBalance_citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id");
            return View(citizenBalance);
        }

        // GET: CitizenBalances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CitizenBalance citizenBalance = db.CitizenBalances.Find(id);
            if (citizenBalance == null)
            {
                return HttpNotFound();
            }
            return View(citizenBalance);
        }

        // POST: CitizenBalances/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            CitizenBalance citizenBalance = db.CitizenBalances.Find(id);
            citizenBalance.CitizenBalance_isDeleted = true;
            
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
