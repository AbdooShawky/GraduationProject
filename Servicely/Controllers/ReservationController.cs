using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Servicely.Models;
namespace Servicely.Controllers
{
    public class ReservationController : BaseController
    {
        DbMasterEntities1 db = new DbMasterEntities1();
        // GET: Reservation
        public ActionResult Index()
        {
            ViewBag.NId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
            var data = db.Reservations.Where(a => a.reservation_isDeleted != true).Include(a => a.Citizen).Include(a => a.service).Include(a => a.Office).ToList();
            return View(data.ToList());
        }
        public ActionResult IndexToday()
        {
            ViewBag.NId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
            var data = db.Reservations.Where(a => a.reservation_isDeleted != true && a.reservation_date.Value.Year==DateTime.Now.Year && a.reservation_date.Value.Month == DateTime.Now.Month && a.reservation_date.Value.Day == DateTime.Now.Day).Include(a => a.Citizen).Include(a => a.service).Include(a => a.Office).ToList();
            return View(data.ToList());
        }


        public ActionResult Indexuser()
        {
            int id = 0;
            ViewBag.NId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
            if(Session["citizenID"] != null) 
            id = (int)Session["citizenID"];
            return View(db.Reservations.Where(a => a.reservation_isDeleted != true).Include(a=> a.Office).Include(a => a.service).Include(a => a.Citizen).Where(a=> a.reservation_citizen_id == id));
        }


        public JsonResult GetAllDocuments()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Document_Type, JsonRequestBehavior.AllowGet);
        }




        public JsonResult GetAllStates()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.States.Where(a=> a.state_isDeleted!=true), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getallcitiesbystateid(int Stateidd)
        {

            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Cities.Where(a => a.city_state_id == Stateidd && a.city_isDeleted != true), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllRegionsByCityId(int CityId)
        {

            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Regions.Where(a => a.region_city_id == CityId && a.region_isDeleted != true), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllOfficeByDisyrictId(int DisId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Offices.Where(a=>a.office_district_id== DisId && a.office_isDeleted != true), JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetAllOfficesByDistrictIdAndGovernementId(int dis , int gov)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Offices.Where(a=>a.governemet_id == gov && a.office_district_id == dis && a.office_isDeleted != true),JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllServicesByGovernementBodyId(int goo)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return Json (db.services.Where(a=>a.goverenemnt_id == goo && a.service_isDeleted != true),JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.states = new SelectList(db.States.Where(a=>a.state_isDeleted!=true), "state_id","state_name");
            ViewBag.id = new SelectList(db.governement_body.Where(a => a.governement_isDeleted != true), "id", "governement_name");
            ViewBag.reservation_office_id = new SelectList(db.Offices.Where(a => a.office_isDeleted != true), "office_id", "office_name");
            ViewBag.service_id = new SelectList(db.services.Where(a => a.service_isDeleted != true), "service_id", "service_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.states = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");
                    ViewBag.id = new SelectList(db.governement_body.Where(a => a.governement_isDeleted != true), "id", "governement_name_arabic");
                    ViewBag.reservation_office_id = new SelectList(db.Offices.Where(a => a.office_isDeleted != true), "office_id", "office_name_arabic");
                    ViewBag.service_id = new SelectList(db.services.Where(a => a.service_isDeleted != true), "service_id", "service_name_arabic");

                }
            }


            return View();
        }

        [HttpPost]
        public ActionResult Create(Reservation u)
        {
            var d = db.services.Find(u.service_id);
            int? sumService = d.service_time;
            if(Session["CitizenId"] != null)
            {
                u.reservation_citizen_id = (int)Session["CitizenId"];
            }
           
            var data = db.Reservations.Where(a => a.reservation_date == u.reservation_date && a.reservation_office_id == u.reservation_office_id).Join(db.services, a=>a.service_id, b=> b.service_id, (a,b)=> new {b.service_time });
            foreach (var item in data)
            {
                sumService += item.service_time;
            }
            var govern = db.services.Where(a => a.service_id== u.service_id).Select(a=>a.goverenemnt_id).SingleOrDefault();
            var gov = db.governement_body.Find(d.goverenemnt_id);
            
            TimeSpan? ss = gov.end_time -  gov.start_time;
            double time = ss.Value.Hours;

            double ddd =(double) sumService / 60;
            if ( ddd > time )
            {
                ViewBag.states = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
                ViewBag.id = new SelectList(db.governement_body.Where(a => a.governement_isDeleted != true), "id", "governement_name");
                ViewBag.reservation_office_id = new SelectList(db.Offices.Where(a => a.office_isDeleted != true), "office_id", "office_name");
                ViewBag.service_id = new SelectList(db.services.Where(a => a.service_isDeleted != true), "service_name");

                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.states = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");
                        ViewBag.id = new SelectList(db.governement_body.Where(a => a.governement_isDeleted != true), "id", "governement_name_arabic");
                        ViewBag.reservation_office_id = new SelectList(db.Offices.Where(a => a.office_isDeleted != true), "office_id", "office_name_arabic");
                        ViewBag.service_id = new SelectList(db.services.Where(a => a.service_isDeleted != true), "service_id", "service_name_arabic");

                    }
                }
                ViewBag.err = Servicely.Languages.Language.day_full;

                return View();
            }

            int cID = 0;
            if(Session["citizenID"]!= null)
           cID = (int)Session["citizenID"];
            var dataCitizen = db.Reservations.Where(a => a.reservation_date == u.reservation_date &&  a.reservation_office_id == u.reservation_office_id && a.reservation_citizen_id == cID).SingleOrDefault();

           if(dataCitizen != null)
            {
                ViewBag.states = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
                ViewBag.id = new SelectList(db.governement_body.Where(a => a.governement_isDeleted != true), "id", "governement_name");
                ViewBag.reservation_office_id = new SelectList(db.Offices.Where(a => a.office_isDeleted != true), "office_id", "office_name");
                ViewBag.service_id = new SelectList(db.services.Where(a => a.service_isDeleted != true), "service_id", "service_name");


                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.states = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");
                        ViewBag.id = new SelectList(db.governement_body.Where(a => a.governement_isDeleted != true), "id", "governement_name_arabic");
                        ViewBag.reservation_office_id = new SelectList(db.Offices.Where(a => a.office_isDeleted != true), "office_id", "office_name_arabic");
                        ViewBag.service_id = new SelectList(db.services.Where(a => a.service_isDeleted != true), "service_id", "service_name_arabic");

                    }
                }
                ViewBag.err = Servicely.Languages.Language.reservation_msg;

                return View();
            }
            u.TransactionDate = DateTime.Now;
            u.reservation_citizen_id = cID;
            db.Reservations.Add(u);
            db.SaveChanges();



            return RedirectToAction("IndexUser");

        }

        protected override void OnException(ExceptionContext filterContext)
        {
            //your handling logic here
            filterContext.ExceptionHandled = true;
            filterContext.Result = RedirectToAction("errorpage", "home");
        }

    }
}