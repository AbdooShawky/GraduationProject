using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicely.Models;
using System.Data.Entity;

namespace Servicely.Controllers
{
    public class DistrictController : BaseController
    {
        DbMasterEntities1 db = new DbMasterEntities1();
        // GET: District
        public ActionResult Index()
        {
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
            if (Session["lang"] != null)
            {

                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

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

        public JsonResult GetDistrictByRegionId(int rId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Districts.Where(a => a.district_region_id == rId && a.district_isDeleted != true), JsonRequestBehavior.AllowGet);
        }


        public ActionResult create()
        {
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
            ViewBag.district_region_id = new SelectList(db.Regions, "region_id", "region_name");
            if (Session["lang"] != null)
            {

                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");
                    ViewBag.district_region_id = new SelectList(db.Regions, "region_id", "region_arabic_name");
                }
            }

            if (Session["lang"] != null)
            {

                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");
                    ViewBag.district_region_id = new SelectList(db.Regions, "region_id", "region_arabic_name");


                }
            }
           
            return View();

        }
        [HttpPost]
        public ActionResult create(District d)
        {
            var data = db.Districts.Where(a => a.district_name == d.district_name && a.district_region_id == d.district_region_id && a.district_isDeleted != true).SingleOrDefault();
            ViewBag.errMsg = null;
            if (data != null)
            {
                ViewBag.errMsg = d.district_name + Servicely.Languages.Language.Region_already_exist;
                ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
                if (Session["lang"] != null)
                {

                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                    }
                }
                if (Session["lang"] != null)
                {

                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");
                      


                    }
                }
                return View(d);
            }
            db.Districts.Add(d);
            db.SaveChanges();
            return RedirectToAction("Index");

        }


        public ActionResult Edit(int id)
        {

            District district = db.Districts.Find(id);
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
            ViewBag.district_region_id = new SelectList(db.Regions, "region_id", "region_name", district.district_region_id);

            if (Session["lang"] != null)
            {

                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");
                    ViewBag.district_region_id = new SelectList(db.Regions, "region_id", "region_arabic_name", district.district_region_id);

                }
            }
            return View(district);
        }

        [HttpPost]

        public ActionResult Edit(District d)
        {

                        ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            var data = db.Districts.Where(a => a.district_name == d.district_name && a.district_id != d.district_id && a.district_isDeleted != true && a.district_region_id == d.district_region_id).SingleOrDefault();
         
            
            ViewBag.errMsg = null;
            if (data != null)
            {
                ViewBag.errMsg = d.district_name + Servicely.Languages.Language.Distric_already_exist;

                ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

                ViewBag.district_region_id = new SelectList(db.Regions, "region_id", "region_name", d.district_region_id);

                if (Session["lang"] != null)
                {

                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                        ViewBag.district_region_id = new SelectList(db.Regions, "region_id", "region_arabic_name", d.district_region_id);
                    }
                }

                return View(d);
            }
            var old = db.Districts.Find(d.district_id);
            old.district_name = d.district_name;
            old.district_region_id = d.district_region_id;
            old.district_arabic_name = d.district_arabic_name;
            ///   db.Entry(d).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");


        }

        public ActionResult Delete(int id)
        {
            District district = db.Districts.Find(id);

            return View(district);
        }

        // POST: Districts/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {

            var old = db.Districts.Find(id);
            old.district_isDeleted = true;
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