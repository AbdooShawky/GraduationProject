using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicely.Models;
using System.Data.Entity;

namespace Servicely.Controllers
{
    public class CityController : BaseController
    {
        DbMasterEntities1 db = new DbMasterEntities1();
        // GET: City
        public ActionResult Index()
        {
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");
                    return View(db.Cities.Where(a => a.city_isDeleted != true).ToList());
                }
            }

            return View(db.Cities.Where(a => a.city_isDeleted != true).ToList());


        }
        public JsonResult Sessionvalue()
        {
            string aa = "";
            if (Session["lang"] != null)
                aa = Session["lang"].ToString();
            return Json(aa, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getCityByStateId(int Stateid)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Cities.Where(a => a.city_state_id == Stateid && a.city_isDeleted != true), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllStates()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.States, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.city_state_id = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.city_state_id = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                }
            }

            return View();
        }
        [HttpPost]
        public ActionResult Create(City c)
        {
            ViewBag.errMsg = null;
            var data = db.Cities.Where(a => a.city_name == c.city_name && a.city_isDeleted != true).SingleOrDefault();
            if (data != null)
            {

                ViewBag.city_state_id = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.city_state_id = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");
                        ViewBag.errMsg = c.city_arabic_name + Servicely.Languages.Language.already_exist_in_this_state;
                        return View(c);
                    }
                }

                ViewBag.errMsg = c.city_name + Servicely.Languages.Language.already_exist_in_this_state;
                return View(c);
            }

            db.Cities.Add(c);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {

            City s = db.Cities.Find(id);

            return View(s);
        }
        [HttpPost]
        public ActionResult Edit(City c)
        {
            var data = db.Cities.Where(a => a.city_name == c.city_name && a.city_id != c.city_id && a.city_isDeleted != true).SingleOrDefault();
            ViewBag.errMsg = null;
            if (data != null)
            {
                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.errMsg = c.city_arabic_name + Servicely.Languages.Language.City_already_exist;
                        return View(c);
                    }
                }
                ViewBag.errMsg = c.city_name + Servicely.Languages.Language.City_already_exist;
                return View(c);
            }
            var old = db.Cities.Find(c.city_id);
            old.city_name = c.city_name;
            old.city_arabic_name = c.city_arabic_name;
            
            db.SaveChanges();
            return RedirectToAction("Index");


        }

        public ActionResult Delete(int id)
        {

            City s = db.Cities.Find(id);

            return View(s);
        }


        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            var old = db.Cities.Find(id);
            old.city_isDeleted = true;

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