using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicely.Models;
using System.Data.Entity;

namespace Servicely.Controllers
{
    public class RegionController : BaseController
    {
        DbMasterEntities1 db = new DbMasterEntities1();
        // GET: Region
        public ActionResult Index()
        {

            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");
                    return View(db.Regions.Where(a => a.region_isDeleted != true).ToList());
                }
            }
            // ViewBag.City = new SelectList(db.City.Where(a => a.city_isDeleted != true), "city_id", "city_name");
            return View(db.Regions.Where(a => a.region_isDeleted != true).ToList());

        }

        //----------------------- ajax call -------------------------------

        public JsonResult GetCitiesByStateId(int stateId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Cities.Where(a => a.city_state_id == stateId && a.city_isDeleted != true), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRegionByCityId(int CityId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Regions.Where(a => a.region_city_id == CityId && a.region_isDeleted != true), JsonRequestBehavior.AllowGet);
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
        public ActionResult Create(Region r)
        {
            var data = db.Regions.Where(a => a.region_name == r.region_name && a.region_city_id == r.region_city_id && a.region_isDeleted != true).SingleOrDefault();
            ViewBag.errMsg = null;
            if (data != null)
            {
                ViewBag.errMsg = r.region_name + Servicely.Languages.Language.Region_already_exist;
                ViewBag.city_state_id = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
                return View(r);
            }

            db.Regions.Add(r);
            db.SaveChanges();
            ViewBag.city_state_id = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {

            Region r = db.Regions.Find(id);
            ViewBag.region_city_id = new SelectList(db.Cities.Where(a => a.city_isDeleted != true), "city_id", "city_name", r.region_city_id);

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.region_city_id = new SelectList(db.Cities.Where(a => a.city_isDeleted != true), "city_id", "city_arabic_name", r.region_city_id);

                }
            }
            return View(r);
        }

        public ActionResult Edit(Region r)
        {

            var data = db.Regions.Where(a => a.region_name == r.region_name && a.region_id != r.region_id && a.region_isDeleted != true && a.region_city_id == r.region_city_id).SingleOrDefault();
            ViewBag.errMsg = null;
            if (data != null)
            {
                ViewBag.errMsg = r.region_name + Servicely.Languages.Language.Region_already_exist;
                ViewBag.region_city_id = new SelectList(db.Cities.Where(a => a.city_isDeleted != true), "city_id", "city_name", r.region_city_id);
                return View(r);
            }

            var old = db.Regions.Find(r.region_id);
            old.region_name = r.region_name;
            old.region_arabic_name = r.region_arabic_name;
            old.region_city_id = r.region_city_id;
            // db.Entry(r).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");


        }

        public ActionResult Delete(int id)
        {

            Region r = db.Regions.Find(id);

            return View(r);
        }


        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            var old = db.Regions.Find(id);
            old.region_isDeleted = true;

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
