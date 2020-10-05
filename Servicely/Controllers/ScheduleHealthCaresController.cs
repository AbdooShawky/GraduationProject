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
    public class ScheduleHealthCaresController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: ScheduleHealthCares
        public ActionResult Index()
        {
            return View(db.HealthCareSheduleM_M.Where(a => a.ScheduleHealthCare.schedule_isDeleted != true).Include(a => a.HealthCare).Include(a => a.Healthcare_Doctor).Include(a => a.Healthcare_Doctor.Citizen).Include(a=> a.Healthcare_Doctor.HealthCareSpecialization).Include(a=> a.ScheduleHealthCare).ToList());

           // return View(db.ScheduleHealthCares.Where(a => a.schedule_isDeleted != true).ToList());

           
        }

        // GET: ScheduleHealthCares/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduleHealthCare scheduleHealthCare = db.ScheduleHealthCares.Find(id);
            if (scheduleHealthCare == null)
            {
                return HttpNotFound();
            }
            return View(scheduleHealthCare);
        }

        // GET: ScheduleHealthCares/Create
        public ActionResult Create()
        {
          // int doctoId = (int)Session["doctorId"];
          //  int? cid = db.Healthcare_Doctor.Find(doctoId).doctor_citizen_id;
          // var data = db.Citizens.Find(cid);
          //   ViewBag.Dname = data.citizen_first_name + " " + data.citizen_second_name + " " + data.citizen_third_name + " " + data.citizen_fourth_name;
          //  ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
          //  ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a=> a.healthcare_isDeleted!= true ), "healthcare_type_id", "healthcare_type_name");
            return View();
        }

       
        [HttpPost]
        public ActionResult Create( ScheduleHealthCare scheduleHealthCare )
        {
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                }
            }

            if (ModelState.IsValid)
            {
                int hos = (int)Session["hos"];
                // start > start1 w start1< end 
                int doctoId = (int)Session["doctorId"]; 
                //   data = db.ScheduleHealthCares.Where(a=> a.checkup_date.ToString() == scheduleHealthCare.checkup_date.ToString() && scheduleHealthCare.checkup_start >= a.checkup_start && scheduleHealthCare.checkup_end <= a.checkup_end );                                                                                                                                                                                                                                                                                                                                                                                                                                                              start < db start && end >= end || end >= db start && end <= end  || start >= start && start <= endvar
                var data1 = db.HealthCareSheduleM_M.Where(a=>a.ScheduleHealthCare.checkup_date.Value.Year== scheduleHealthCare.checkup_date.Value.Year&& a.ScheduleHealthCare.checkup_date.Value.Month == scheduleHealthCare.checkup_date.Value.Month&& a.ScheduleHealthCare.checkup_date.Value.Day == scheduleHealthCare.checkup_date.Value.Day  && a.DoctorId == doctoId &&  a.ScheduleHealthCare.schedule_isDeleted != true && ((scheduleHealthCare.checkup_start<= a.ScheduleHealthCare.checkup_start&& scheduleHealthCare.checkup_end >=a.ScheduleHealthCare.checkup_end )||(scheduleHealthCare.checkup_end >= a.ScheduleHealthCare.checkup_start && scheduleHealthCare.checkup_end <= a.ScheduleHealthCare.checkup_end)||(scheduleHealthCare.checkup_start >= a.ScheduleHealthCare.checkup_start && scheduleHealthCare.checkup_start <= a.ScheduleHealthCare.checkup_end))).FirstOrDefault();
                //var data = db.ScheduleHealthCares.Where(a => a.checkup_date.ToString() == scheduleHealthCare.checkup_date.ToString() && scheduleHealthCare.checkup_start >= a.checkup_start && scheduleHealthCare.checkup_end <= a.checkup_end);
               // var data1 = db.DoctorScheduleM_M.Where(a => a.ScheduleHealthCare.checkup_date.ToString() == scheduleHealthCare.checkup_date.ToString() && scheduleHealthCare.checkup_start >= a.ScheduleHealthCare.checkup_start && scheduleHealthCare.checkup_end <= a.ScheduleHealthCare.checkup_end && a.ScheduleHealthCare.checkup_end < scheduleHealthCare.checkup_start && a.Healthcare_Doctor.doctor_id == doctoId && a.Healthcare_Doctor.HealthCareDoctorM_M.Where(x => x.hospitalDoctor_hospital == hos).FirstOrDefault().HealthCare.hospital_id == hos).SingleOrDefault();
                if ( data1 != null)
                {
                    ViewBag.doctorschedule = Languages.Language.scheduleErrDoctor;
                    return View(scheduleHealthCare);
                }




                db.ScheduleHealthCares.Add(scheduleHealthCare);
                db.SaveChanges();
               
                HealthCareSheduleM_M mm = new HealthCareSheduleM_M();
                mm.hospitalSchedule_hospital =hos ;
                mm.DoctorId = doctoId;
                mm.hospitalSchedule_shedule = scheduleHealthCare.schedule_id;

                db.HealthCareSheduleM_M.Add(mm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(scheduleHealthCare);
        }

        public ActionResult Step1_ShcheduleHealthCare ()
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
        public ActionResult Step1_ShcheduleHealthCare(int doctorSchedule_doctor , int hospitalDoctor_hospital)
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

            Session["hos"] = hospitalDoctor_hospital;
            Session["doctorId"] = doctorSchedule_doctor;
            return RedirectToAction("Create");
        }


        // GET: ScheduleHealthCares/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduleHealthCare scheduleHealthCare = db.ScheduleHealthCares.Find(id);
            if (scheduleHealthCare == null)
            {
                return HttpNotFound();
            }
            return View(scheduleHealthCare);
        }

        public JsonResult GetAllHospitalsByTypesAndDistrictId(int tid , int dis)
        {
            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;
            var data = db.HealthCares.Where(a => a.healthcare_type_id == tid && a.hospital_district_id == dis).Select(a => new { a.hospital_id, a.hospital_name }).ToList();
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    var dt = db.HealthCares.Where(a => a.healthcare_type_id == tid && a.hospital_district_id == dis).Select(a => new { a.hospital_id, a.hospital_name_arabic }).ToList();
                    return Json(dt, JsonRequestBehavior.AllowGet);
                }
            }
            return Json (data,JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllSepcByHospitalId(int hid)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.HealthCareSpecializationM_M.Where(a=> a.hospitalSpecialization_hospital_id == hid).Join(db.HealthCareSpecializations.Where(a=> a.specialization_isDeleted!= true),a=> a.hospitalSpecialization_specialization_id , b=> b.specialization_id, (a,b)=> new {b.specialization_id,b.specialization_name });
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    var dt = db.HealthCareSpecializationM_M.Where(a => a.hospitalSpecialization_hospital_id == hid).Join(db.HealthCareSpecializations.Where(a => a.specialization_isDeleted != true), a => a.hospitalSpecialization_specialization_id, b => b.specialization_id, (a, b) => new { b.specialization_id, b.specialization_name_arabic });
                    return Json(dt, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAlldoctorsBySpecId(int sid , int hoss)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = ( from mm in db.HealthCareDoctorM_M   from hos in db.HealthCares from hmm in db.HealthCareSpecializationM_M from doc in db.Healthcare_Doctor.Where(a=> a.doctor_specialization_id == sid &&hos.hospital_id == hoss && mm.hospitalDoctor_hospital == hos.hospital_id && a.doctor_id==mm.hospitalDoctor_doctor &&hmm.hospitalSpecialization_specialization_id == sid &&  hos.hospital_id == hmm.hospitalSpecialization_hospital_id) select new { doc.doctor_id , doc.Citizen.citizen_first_name , doc.Citizen.citizen_second_name , doc.Citizen.citizen_third_name , doc.Citizen.citizen_fourth_name } );

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    var dt = (from mm in db.HealthCareDoctorM_M from hos in db.HealthCares from hmm in db.HealthCareSpecializationM_M from doc in db.Healthcare_Doctor.Where(a => a.doctor_specialization_id == sid && hos.hospital_id == hoss && mm.hospitalDoctor_hospital == hos.hospital_id && a.doctor_id == mm.hospitalDoctor_doctor && hmm.hospitalSpecialization_specialization_id == sid && hos.hospital_id == hmm.hospitalSpecialization_hospital_id) select new { doc.doctor_id, doc.Citizen.citizen_first_name_arabic, doc.Citizen.citizen_second_name_arabic, doc.Citizen.citizen_third_name_arabic, doc.Citizen.citizen_fourth_name_arabic });
                    return Json(dt, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Edit( ScheduleHealthCare scheduleHealthCare)
        {
            HealthCareSheduleM_M mm = new HealthCareSheduleM_M();
           // var data = db.DoctorScheduleM_M.Where(a=> a.doctorSchedule_schedule == scheduleHealthCare.schedule_id);
           // var data1 = db.ScheduleHealthCares.Where(a=> a.schedule_id == scheduleHealthCare.schedule_id );
          //  var allschedule = db.ScheduleHealthCares.Where(a=> a.schedule_id != scheduleHealthCare.schedule_id);

            var health = db.HealthCareSheduleM_M.Where(a => a.hospitalSchedule_shedule== scheduleHealthCare.schedule_id).SingleOrDefault();
            var data1 = db.HealthCareSheduleM_M.Where(a => a.ScheduleHealthCare.checkup_date.Value.Year == scheduleHealthCare.checkup_date.Value.Year && a.ScheduleHealthCare.checkup_date.Value.Month == scheduleHealthCare.checkup_date.Value.Month && a.ScheduleHealthCare.checkup_date.Value.Day == scheduleHealthCare.checkup_date.Value.Day  && a.Healthcare_Doctor.doctor_id == health.DoctorId &&  a.ScheduleHealthCare.schedule_isDeleted != true && a.hospitalSchedule_shedule != scheduleHealthCare.schedule_id && ((scheduleHealthCare.checkup_start <= a.ScheduleHealthCare.checkup_start && scheduleHealthCare.checkup_end >= a.ScheduleHealthCare.checkup_end) || (scheduleHealthCare.checkup_end >= a.ScheduleHealthCare.checkup_start && scheduleHealthCare.checkup_end <= a.ScheduleHealthCare.checkup_end) || (scheduleHealthCare.checkup_start >= a.ScheduleHealthCare.checkup_start && scheduleHealthCare.checkup_start <= a.ScheduleHealthCare.checkup_end))).FirstOrDefault();

            if (data1 != null)
            {
                ViewBag.doctorschedule = Languages.Language.scheduleErrDoctor;
                return View(scheduleHealthCare);
            }

           
              

            if (ModelState.IsValid)
            {
                db.Entry(scheduleHealthCare).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(scheduleHealthCare);
        }

        // GET: ScheduleHealthCares/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var scheduleHealthCare = db.HealthCareSheduleM_M.Where(a => a.ScheduleHealthCare.schedule_isDeleted != true && a.hospitalSchedule_shedule == id).Include(a => a.HealthCare).Include(a => a.Healthcare_Doctor).Include(a => a.Healthcare_Doctor.Citizen).Include(a => a.Healthcare_Doctor.HealthCareSpecialization).Include(a => a.ScheduleHealthCare).ToList().SingleOrDefault();

            if (scheduleHealthCare == null)
            {
                return HttpNotFound();
            }
            return View(scheduleHealthCare);
        }

        // POST: ScheduleHealthCares/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
           
            ScheduleHealthCare scheduleHealthCare = db.ScheduleHealthCares.Find(id);
            scheduleHealthCare.schedule_isDeleted = true;
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
