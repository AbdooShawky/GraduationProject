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
    public class ScheduleVaccinationsController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: ScheduleVaccinations
        public ActionResult Index()
        {

            return View(db.ScheduleVaccM_M.Where(a=> a.ScheduleVaccination.schedule_isDeleted!= true).Include(a=> a.HealthCare).Include(a=> a.ScheduleVaccination).Include(a=> a.VaccinationType).ToList());
        }

        // GET: ScheduleVaccinations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduleVaccination scheduleVaccination = db.ScheduleVaccinations.Find(id);
            if (scheduleVaccination == null)
            {
                return HttpNotFound();
            }
            return View(scheduleVaccination);
        }

        // GET: ScheduleVaccinations/Create
        public ActionResult Create()
        {
            ViewBag.zero = "0";
            ViewBag.date = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");
            if (Session["VaccID"]!= null)
            {
               int  dd = (int)Session["VaccID"];
                ViewBag.data = db.VaccinationTypes.Find(dd).vaccination_type_name;
                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.data = db.VaccinationTypes.Find(dd).vaccination_type_name_arabic;
                    }
                }
            }


            return View();
        }

       
        [HttpPost]
        
        public ActionResult Create( ScheduleVaccination scheduleVaccination)
        {

           

            if (ModelState.IsValid)
            {

                int d = 0;
                    int h=0;
                if (Session["VaccID"] != null && Session["hh"] != null)
                {

                    d = (int)Session["VaccID"];
                    h = (int)Session["hh"];
                    // var data = db.ScheduleHealthCares.Where(a => a.checkup_date.ToString() == scheduleHealthCare.checkup_date.ToString() && scheduleHealthCare.checkup_start >= a.checkup_start && scheduleHealthCare.checkup_end <= a.checkup_end);
                    var data1 = db.ScheduleVaccM_M.Where(a => a.ScheduleVaccination.checkup_date.Value.Year == scheduleVaccination.checkup_date.Value.Year&& a.ScheduleVaccination.checkup_date.Value.Month == scheduleVaccination.checkup_date.Value.Month && a.ScheduleVaccination.checkup_date.Value.Day == scheduleVaccination.checkup_date.Value.Day  && a.HealthCareId==h && a.VaccinationType.vaccination_type_id == d && a.ScheduleVaccination.schedule_isDeleted !=true && ((scheduleVaccination.checkup_start <= a.ScheduleVaccination.checkup_start && scheduleVaccination.checkup_end >= a.ScheduleVaccination.checkup_end) || (scheduleVaccination.checkup_end >= a.ScheduleVaccination.checkup_start && scheduleVaccination.checkup_end <= a.ScheduleVaccination.checkup_end) || (scheduleVaccination.checkup_start >= a.ScheduleVaccination.checkup_start && scheduleVaccination.checkup_start <= a.ScheduleVaccination.checkup_end))).FirstOrDefault();

                    if (data1 != null)
                    {
                        ViewBag.scheduleErrVacc = Languages.Language.scheduleErrVacc;
                        return View(scheduleVaccination);
                    }



                    db.ScheduleVaccinations.Add(scheduleVaccination);
                    db.SaveChanges();

                    ScheduleVaccM_M mm = new ScheduleVaccM_M();
                    mm.scheduleVacc_schedule_id = scheduleVaccination.schedule_id;
                    mm.scheduleVacc_vaccType_id = d;
                    mm.HealthCareId = h;
                    db.ScheduleVaccM_M.Add(mm);
                    db.SaveChanges();
                }
                else
                {
                    return View();
                }
               
                return RedirectToAction("Index");
            }

            return View(scheduleVaccination);
        }
        public JsonResult GetAllVaccTypeByHospitalId(int hid )
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.VacinationHealthCareM_M.Where(a=> a.vaccinationhealthcare_hospital == hid).Join(db.VaccinationTypes.Where(a=> a.vaccination_isDeleted!=true),a=>a.vacinationhealthcare_vaccination,b=> b.vaccination_type_id,(a,b)=> new { b.vaccination_type_id , b.vaccination_type_name});
           
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    var dt = db.VacinationHealthCareM_M.Where(a => a.vaccinationhealthcare_hospital == hid).Join(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), a => a.vacinationhealthcare_vaccination, b => b.vaccination_type_id, (a, b) => new { b.vaccination_type_id, b.vaccination_type_name_arabic });
                    return Json(dt, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Step1Shcedule ()
        {
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
            ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");
                    ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name_arabic");

                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Step1Shcedule(int VaccID , int hospitalDoctor_hospital)
        {
            Session["VaccID"] = VaccID;
            Session["hh"] = hospitalDoctor_hospital;
            return RedirectToAction("Create");
        }

        // GET: ScheduleVaccinations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduleVaccination scheduleVaccination = db.ScheduleVaccinations.Find(id);
            if (scheduleVaccination == null)
            {
                return HttpNotFound();
            }
            return View(scheduleVaccination);
        }

       
        [HttpPost]
     
        public ActionResult Edit( ScheduleVaccination scheduleVaccination)
        {
            //DoctorScheduleM_M mm = new DoctorScheduleM_M();
            //var data = db.DoctorScheduleM_M.Where(a => a.doctorSchedule_schedule == scheduleHealthCare.schedule_id);
            //var data1 = db.ScheduleHealthCares.Where(a => a.schedule_id == scheduleHealthCare.schedule_id);
            var health = db.ScheduleVaccM_M.Where(a => a.scheduleVacc_schedule_id == scheduleVaccination.schedule_id).SingleOrDefault();
            var data1 = db.ScheduleVaccM_M.Where(a => a.ScheduleVaccination.checkup_date.Value.Year == scheduleVaccination.checkup_date.Value.Year && a.ScheduleVaccination.checkup_date.Value.Month == scheduleVaccination.checkup_date.Value.Month && a.ScheduleVaccination.checkup_date.Value.Day == scheduleVaccination.checkup_date.Value.Day  && a.HealthCareId == health.HealthCareId&& a.VaccinationType.vaccination_type_id == health.scheduleVacc_vaccType_id && a.ScheduleVaccination.schedule_isDeleted != true &&a.scheduleVacc_schedule_id != scheduleVaccination.schedule_id && ((scheduleVaccination.checkup_start <= a.ScheduleVaccination.checkup_start && scheduleVaccination.checkup_end >= a.ScheduleVaccination.checkup_end) || (scheduleVaccination.checkup_end >= a.ScheduleVaccination.checkup_start && scheduleVaccination.checkup_end <= a.ScheduleVaccination.checkup_end) || (scheduleVaccination.checkup_start >= a.ScheduleVaccination.checkup_start && scheduleVaccination.checkup_start <= a.ScheduleVaccination.checkup_end))).FirstOrDefault();

            if (data1 != null)
            {
                ViewBag.scheduleErrVacc = Languages.Language.scheduleErrVacc;
                return View(scheduleVaccination);
            }
            if (ModelState.IsValid)
            {




                db.Entry(scheduleVaccination).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(scheduleVaccination);
        }

        // GET: ScheduleVaccinations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var scheduleVaccination = db.ScheduleVaccM_M.Where(a=> a.scheduleVacc_schedule_id == id).Include(a => a.HealthCare).Include(a => a.ScheduleVaccination).Include(a => a.VaccinationType).SingleOrDefault();
            if (scheduleVaccination == null)
            {
                return HttpNotFound();
            }
            return View(scheduleVaccination);
        }

        // POST: ScheduleVaccinations/Delete/5
        [HttpPost, ActionName("Delete")]
   
        public ActionResult DeleteConfirmed(int id)
        {
            ScheduleVaccination scheduleVaccination = db.ScheduleVaccinations.Find(id);
            scheduleVaccination.schedule_isDeleted = true;
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
