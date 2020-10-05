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
    public class CarLicencesController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: CarLicences
        public ActionResult Index()
        {
            ViewBag.citizen = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");

            var carLicences = db.CarLicences.Where(a=>a.Is_Deleted!=true).Include(c => c.Citizen);
            return View(carLicences.ToList());
        }


    

        // GET: CarLicences/Create
        public ActionResult Create()
        {
            ViewBag.CitizenId = new SelectList(db.Citizens.Where(a=>a.citizen_isDeleted!=true), "citizen_id", "citizen_national_id");

            ViewBag.CarId = new SelectList(db.Cars.Where(a => a.Is_Deleted != true), "Id", "CarName");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {

                    ViewBag.CarId = new SelectList(db.Cars.Where(a => a.Is_Deleted != true), "Id", "CarNameArabic");

                }
            }

                return View();
        }

        // POST: CarLicences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        public ActionResult Create( CarLicence carLicence,string checkEndDate,string arkam,string hroof)
        {
            if (ModelState.IsValid)
            {

                var data = db.CarLicences.Where(a => a.CarCode == arkam + hroof && a.Is_Deleted != true).SingleOrDefault();

                 if(data == null)
                {
                    carLicence.StartDate = DateTime.Now;


                    string codeee = arkam + hroof;
                    carLicence.CarCode = codeee;
                    
                    if(checkEndDate == "1")
                    {
                        carLicence.EndDate = DateTime.Now.AddDays(3 * 365);
                    }
                    else
                    {

                        carLicence.EndDate = DateTime.Now.AddDays(365);

                    }


                    db.CarLicences.Add(carLicence);
                db.SaveChanges();
                return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.CitizenId = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", carLicence.CitizenId);
                    ViewBag.errorMessage = Servicely.Languages.Language.thisCodeIsAlreadyTaken;
                    ViewBag.CarId = new SelectList(db.Cars.Where(a => a.Is_Deleted != true), "Id", "CarName", carLicence.CarId);

                    if (Session["lang"] != null)
                    {
                        if (Session["lang"].ToString().Equals("ar-EG"))
                        {

                            ViewBag.CarId = new SelectList(db.Cars.Where(a => a.Is_Deleted != true), "Id", "CarNameArabic", carLicence.CarId);

                        }
                    }

                    return View(carLicence);
                }
            }

            ViewBag.CitizenId = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", carLicence.CitizenId);
            return View(carLicence);
        }

        // GET: CarLicences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorPage", "home");
            }
            CarLicence carLicence = db.CarLicences.Find(id);
            if (carLicence == null)
            {
                return RedirectToAction("errorPage", "home");
            }
            ViewBag.CitizenId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id", carLicence.CitizenId);

            ViewBag.CarId = new SelectList(db.Cars.Where(a => a.Is_Deleted != true), "Id", "CarName", carLicence.CarId);

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {

                    ViewBag.CarId = new SelectList(db.Cars.Where(a => a.Is_Deleted != true), "Id", "CarNameArabic" , carLicence.CarId);

                }
            }
            return View(carLicence);
        }

        // POST: CarLicences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(CarLicence carLicence , string arkam, string hroof)
        {
            if (ModelState.IsValid)
            {
                var data = db.CarLicences.Where(a => a.Id != carLicence.Id && a.Is_Deleted != true);
                foreach (var item in data)
                {
                    if(item.CarCode == carLicence.CarCode)
                    {
                        ViewBag.CitizenId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id", carLicence.CitizenId);
                        ViewBag.errorMessage = Servicely.Languages.Language.thisCodeIsAlreadyTaken;


                        ViewBag.CitizenId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");

                        ViewBag.CarId = new SelectList(db.Cars.Where(a => a.Is_Deleted != true), "Id", "CarName");

                        if (Session["lang"] != null)
                        {
                            if (Session["lang"].ToString().Equals("ar-EG"))
                            {

                                ViewBag.CarId = new SelectList(db.Cars.Where(a => a.Is_Deleted != true), "Id", "CarNameArabic");

                            }
                        }



                        return View(carLicence);
                    }
                }
                string cod = arkam + hroof;
                var old = db.CarLicences.Find(carLicence.Id);
                old.CarModel = carLicence.CarModel;
                old.CitizenId = carLicence.CitizenId;
                old.CarNameArabic = carLicence.CarNameArabic;
                old.CarName = carLicence.CarName;
                old.CarCode = cod;
       
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CitizenId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id", carLicence.CitizenId);
            return View(carLicence);
        }

        // GET: CarLicences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorPage", "home");
            }
            CarLicence carLicence = db.CarLicences.Find(id);
            if (carLicence == null)
            {
                return RedirectToAction("errorPage", "home");
            }
            return View(carLicence);
        }

        // POST: CarLicences/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            CarLicence carLicence = db.CarLicences.Find(id);
            carLicence.Is_Deleted = true;
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

        public JsonResult getLicenceByCitizenId(int Id)
        {
            var data = db.CarLicences.Where(a => a.CitizenId == Id && a.Is_Deleted != true);
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {

                    return Json(data.Select(a=>new  CustomLicence { Id = a.Id , CarCode = a.CarCode, CarModel=a.CarModel, CarNameArabic=a.CarNameArabic,StartDate =a.StartDate.ToString(),EndDate = a.EndDate.ToString() }), JsonRequestBehavior.AllowGet);

                }
                
            }
          
            db.Configuration.ProxyCreationEnabled = false;

            return Json(data.Select(a => new CustomLicence { Id = a.Id , CarCode = a.CarCode, CarModel = a.CarModel, CarName = a.CarName, StartDate = a.StartDate.ToString(), EndDate = a.EndDate.ToString() }), JsonRequestBehavior.AllowGet);

        }

        protected override void OnException(ExceptionContext filterContext)
        {
            //your handling logic here
            filterContext.ExceptionHandled = true;
            filterContext.Result = RedirectToAction("errorpage", "home");
        }
    }
}
