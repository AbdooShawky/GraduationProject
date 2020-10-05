using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicely.Models;

namespace Servicely.Controllers
{
    public class CitizenController : BaseController
    {
        // GET: Citizen
        DbMasterEntities1 db = new DbMasterEntities1();
        public ActionResult Index()
        {

            return View(db.Citizens.Where(a => a.citizen_isDeleted != true).ToList());
        }

        // GET: Citizen
        public JsonResult getAllCitzenNId()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.Citizens.Where(a => a.citizen_isDeleted != true);
            data.ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNamesByCitizenId(int ctId)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return Json(db.Citizens.Where(a => a.citizen_id == ctId && a.citizen_isDeleted != true), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult create()
        {
            ViewBag.stateCode = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_code", "state_name");
            ViewBag.zero = "0";
            Session["data-live-search"] = null;
            TempData["data-live-search"] = "true";
            ViewBag.citizen_father_id = new SelectList(db.Citizens.Where(a => a.citizen_gender == "Male" && a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
            ViewBag.citizen_mother_id = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true && a.citizen_gender == "Female"), "citizen_id", "citizen_national_id");

            return View();
        }


        [HttpPost]
        public ActionResult create(Citizen s)
        {
            var data = db.Citizens.Where(a => a.citizen_national_id == s.citizen_national_id).SingleOrDefault();
            if (data != null)
            {
                ViewBag.ww = "national id exist";
                return View();
            }


             




            Session["MId"] = s.citizen_mother_id;
            Session["FId"] = s.citizen_father_id;
            Session["BirthDate"] = s.citizen_birthDate;
            Session["FirstName"] = s.citizen_first_name;
            Session["FirstNameArabic"] = s.citizen_first_name_arabic;
            Session["relegion"] = s.citizen_relegion;
            Session["gender"] = s.citizen_gender;
            Session["BirthPlace"] = s.citizen_birthPlace;
            Session["BirthPlaceArabic"] = s.citizen_birthPlace_arabic;
            // s.citizen_national_id = GenerateNationalId(stateCode, dateee);


            return RedirectToAction("Create", "Addresses");

        }


        //--------------------- function generate national id -----------------

        private string GenerateNationalId(string st, string date)
        {
            // generate 4 random numbers
            int min = 1000;
            int max = 9999;
            Random rdm = new Random();
            int random = rdm.Next(min, max);
            random.ToString();

            string[] a = date.Split('-');
            string NId = st + a[0] + a[1] + a[2] + random;
            return NId;

        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var citizen = db.Citizens.Find(id);
            ViewBag.citizen_father_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id",citizen.citizen_father_id);
            ViewBag.citizen_mother_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", citizen.citizen_mother_id);

            Citizen s = db.Citizens.Find(id);
            GetGenderById(s.citizen_id);
            TempData["gender"] = s.citizen_gender;
            Session["Session"] = s.citizen_gender;
            TempData["gender"].ToString();
            return View(s);
        }
        public JsonResult GetGenderById(int Gid)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Citizens.Find(Gid), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Edit(Citizen c)
        {
            ViewBag.citizen_father_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id");
            ViewBag.citizen_mother_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id");

            var data = db.Citizens.Find(c.citizen_father_id);
            var old = db.Citizens.Find(c.citizen_id);
            old.citizen_father_id = c.citizen_father_id;
            old.citizen_second_name = data.citizen_first_name;
            old.citizen_third_name = data.citizen_second_name;
            old.citizen_fourth_name = data.citizen_third_name;
            old.citizen_first_name = c.citizen_first_name;
            old.citizen_second_name_arabic = data.citizen_first_name_arabic;
            old.citizen_third_name_arabic = data.citizen_second_name_arabic;
            old.citizen_fourth_name_arabic = data.citizen_third_name_arabic;
            old.citizen_first_name_arabic = c.citizen_first_name_arabic;
            old.citizen_birthDate = c.citizen_birthDate;
            old.citizen_gender = c.citizen_gender;
            old.citizen_relegion = c.citizen_relegion;
            old.citizen_birthPlace = c.citizen_birthPlace;
            old.citizen_mother_id = c.citizen_mother_id;
            db.SaveChanges();
            return RedirectToAction("Index");


        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            ViewBag.citizen_father_id = new SelectList(db.Citizens.Where(a=> a.citizen_gender =="Male"), "citizen_id", "citizen_national_id");
            ViewBag.citizen_mother_id = new SelectList(db.Citizens.Where(a=> a.citizen_gender == "Female"), "citizen_id", "citizen_national_id");

            Citizen s = db.Citizens.Find(id);

            return View(s);
        }


        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            var old = db.Citizens.Find(id);
            old.citizen_isDeleted = true;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            //your handling logic here
            filterContext.ExceptionHandled = true;
            filterContext.Result = RedirectToAction("errorpage", "home");
        }



    }
}