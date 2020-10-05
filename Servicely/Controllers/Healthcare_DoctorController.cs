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
    public class Healthcare_DoctorController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: Healthcare_Doctor
        public ActionResult Index()
        {
            var healthcare_Doctor = db.HealthCareDoctorM_M; //db.Healthcare_Doctor.Include(h => h.Citizen).Include(h => h.DoctorType);
            return View(healthcare_Doctor.Include(a=> a.Healthcare_Doctor).Include(a=> a.Healthcare_Doctor.Citizen).Include(a=> a.HealthCare).Where(a=> a.Healthcare_Doctor.doctor_isDeleted!= true).ToList());

        }
        public ActionResult IndexDoctor()
        {
            var healthcare_Doctor = db.Healthcare_Doctor.Where(a=> a.doctor_isDeleted!= true).Include(a=> a.Citizen).Include(a=> a.HealthCareSpecialization); //db.Healthcare_Doctor.Include(h => h.Citizen).Include(h => h.DoctorType);
            return View(healthcare_Doctor.ToList());

        }
        public JsonResult GetHospitalsByDistrictId(int DID) {
            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;
            var data = db.HealthCares.Where(a => a.hospital_district_id == DID).Select(a=>new { a.hospital_id,a.hospital_name}) ;
       
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    var dt = db.HealthCares.Where(a => a.hospital_district_id == DID).Select(a => new { a.hospital_id, a.hospital_name_arabic });
                    return Json(dt, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }



        // GET: Healthcare_Doctor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Healthcare_Doctor healthcare_Doctor = db.Healthcare_Doctor.Find(id);
            if (healthcare_Doctor == null)
            {
                return HttpNotFound();
            }
            return View(healthcare_Doctor);
        }

        // GET: Healthcare_Doctor/Create
        public ActionResult Create()
        {
            ViewBag.hospitalDoctor_doctor = new SelectList(db.Healthcare_Doctor.Join(db.Citizens.Where(a=>a.citizen_isDeleted!=true),a=>a.doctor_citizen_id,b=>b.citizen_id,(a,b)=>new { a.doctor_id,b.citizen_national_id}), "doctor_id", "citizen_national_id");
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.hospitalDoctor_doctor = new SelectList(db.Healthcare_Doctor.Join(db.Citizens.Where(a => a.citizen_isDeleted != true), a => a.doctor_citizen_id, b => b.citizen_id, (a, b) => new { a.doctor_id, b.citizen_national_id }), "doctor_id", "citizen_national_id");
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");


                }
            }

            return View();
        }

        // POST: Healthcare_Doctor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(int hospitalDoctor_doctor, int hospitalDoctor_hospital)
        {
            if (ModelState.IsValid)
            {

                var data = db.HealthCareDoctorM_M.Where(a=>a.hospitalDoctor_doctor == hospitalDoctor_doctor && a.hospitalDoctor_hospital== hospitalDoctor_hospital).SingleOrDefault();
                if (data == null)
                {
                    HealthCareDoctorM_M dd = new HealthCareDoctorM_M();
                    dd.hospitalDoctor_doctor = hospitalDoctor_doctor;
                    dd.hospitalDoctor_hospital = hospitalDoctor_hospital;
                    db.HealthCareDoctorM_M.Add(dd);
                    db.SaveChanges();




                    return RedirectToAction("Index");
                }
               

                    
            }
            ViewBag.hospitalDoctor_doctor = new SelectList(db.Healthcare_Doctor.Join(db.Citizens.Where(a => a.citizen_isDeleted != true), a => a.doctor_citizen_id, b => b.citizen_id, (a, b) => new { a.doctor_id, b.citizen_national_id }), "doctor_id", "citizen_national_id");
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.hospitalDoctor_doctor = new SelectList(db.Healthcare_Doctor.Join(db.Citizens.Where(a => a.citizen_isDeleted != true), a => a.doctor_citizen_id, b => b.citizen_id, (a, b) => new { a.doctor_id, b.citizen_national_id }), "doctor_id", "citizen_national_id");
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");


                }
            }


            ViewBag.errorMessage = Languages.Language.Doctor_hospitalMasageErr;

            return View();


        }

        // GET: Healthcare_Doctor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HealthCareDoctorM_M healthcare_Doctor = db.HealthCareDoctorM_M.Find(id);
            if (healthcare_Doctor == null)
            {
                return HttpNotFound();
            }
            //ViewBag.name = healthcare_Doctor.Healthcare_Doctor.Citizen.citizen_national_id;
            ViewBag.hospitalDoctor_doctor = new SelectList(db.Healthcare_Doctor.Join(db.Citizens.Where(a => a.citizen_isDeleted != true), a => a.doctor_citizen_id, b => b.citizen_id, (a, b) => new { a.doctor_id, b.citizen_national_id }), "doctor_id", "citizen_national_id");
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.hospitalDoctor_doctor = new SelectList(db.Healthcare_Doctor.Join(db.Citizens.Where(a => a.citizen_isDeleted != true), a => a.doctor_citizen_id, b => b.citizen_id, (a, b) => new { a.doctor_id, b.citizen_national_id }), "doctor_id", "citizen_national_id");
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");


                }
            }
            return View(healthcare_Doctor);
        }

        
        [HttpPost]
        public ActionResult Edit(HealthCareDoctorM_M mm)
        {
            if (ModelState.IsValid)
            {
                var daata = db.HealthCareDoctorM_M.Where(a=> a.hospitalDoctor_doctor != mm.hospitalDoctor_doctor && a.hospitalDoctor_hospital != mm.hospitalDoctor_hospital);
                var data = db.HealthCareDoctorM_M.Where(a=>a.hospitalDoctor_doctor == mm.hospitalDoctor_doctor && a.hospitalDoctor_hospital==mm.hospitalDoctor_hospital);
                foreach (var item in daata)
                {
                    if(item.hospitalDoctor_doctor == mm.hospitalDoctor_doctor && item.hospitalDoctor_hospital == mm.hospitalDoctor_hospital)
                    {
                        if(data == null)
                        {
                            db.Entry(mm).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }

                    }


                }


            
            }
            ViewBag.hospitalDoctor_doctor = new SelectList(db.Healthcare_Doctor.Join(db.Citizens.Where(a => a.citizen_isDeleted != true), a => a.doctor_citizen_id, b => b.citizen_id, (a, b) => new { a.doctor_id, b.citizen_national_id }), "doctor_id", "citizen_national_id");
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.hospitalDoctor_doctor = new SelectList(db.Healthcare_Doctor.Join(db.Citizens.Where(a => a.citizen_isDeleted != true), a => a.doctor_citizen_id, b => b.citizen_id, (a, b) => new { a.doctor_id, b.citizen_national_id }), "doctor_id", "citizen_national_id");
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");


                }
            }
            ViewBag.errorMessage = Languages.Language.Doctor_hospitalMasageErr;
            return View();
        }

        // GET: Healthcare_Doctor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Healthcare_Doctor healthcare_Doctor = db.Healthcare_Doctor.Find(id);
            if (healthcare_Doctor == null)
            {
                return HttpNotFound();
            }
            return View(healthcare_Doctor);
        }

        // POST: Healthcare_Doctor/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Healthcare_Doctor healthcare_Doctor = db.Healthcare_Doctor.Find(id);
            healthcare_Doctor.doctor_isDeleted = true;
            db.SaveChanges();
            return RedirectToAction("IndexDoctor");
        }

        public ActionResult DeleteMM(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Healthcare_Doctor healthcare_Doctor = db.Healthcare_Doctor.Find(id);
            if (healthcare_Doctor == null)
            {
                return HttpNotFound();
            }
            return View(healthcare_Doctor);
        }

        // POST: Healthcare_Doctor/Delete/5
        [HttpPost, ActionName("DeleteMM")]
        public ActionResult DeleteConfirmedMM(int id)
        {
            HealthCareDoctorM_M healthcare_Doctor = db.HealthCareDoctorM_M.Find(id);
            db.HealthCareDoctorM_M.Remove(healthcare_Doctor);
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
        public ActionResult MainCreateDoctor ()
        {
            ViewBag.doctor_specialization_id = new SelectList(db.HealthCareSpecializations.Where(a => a.specialization_isDeleted != true), "specialization_id", "specialization_name");
            ViewBag.doctor_citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id");
       

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.doctor_specialization_id = new SelectList(db.HealthCareSpecializations.Where(a => a.specialization_isDeleted != true), "specialization_id", "specialization_name_arabic");
                    ViewBag.doctor_citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id");
  


                }
            }



            return View();
        }
        [HttpPost]
        public ActionResult MainCreateDoctor(Healthcare_Doctor d)
        {
            var data = db.Healthcare_Doctor.Where(a=>a.doctor_citizen_id == d.doctor_citizen_id && a.doctor_isDeleted!= true).SingleOrDefault();
            if(data == null)
            {

                db.Healthcare_Doctor.Add(d);
                db.SaveChanges();
                return RedirectToAction("IndexDoctor");
            }

            ViewBag.doctor_specialization_id = new SelectList(db.HealthCareSpecializations.Where(a => a.specialization_isDeleted != true), "specialization_id", "specialization_name");
            ViewBag.doctor_citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.doctor_specialization_id = new SelectList(db.HealthCareSpecializations.Where(a => a.specialization_isDeleted != true), "specialization_id", "specialization_name_arabic");
                    ViewBag.doctor_citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id");



                }
            }
            ViewBag.errorMessage =Languages.Language.doctor_error_message;
            return View(d);


        }

        public ActionResult MainEditDoctor(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Healthcare_Doctor healthcare_Doctor = db.Healthcare_Doctor.Find(id);
            if (healthcare_Doctor == null)
            {
                return HttpNotFound();
            }

            ViewBag.doctor_specialization_id = new SelectList(db.HealthCareSpecializations.Where(a => a.specialization_isDeleted != true), "specialization_id", "specialization_name", healthcare_Doctor.HealthCareSpecialization.specialization_name);
            ViewBag.doctor_citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", healthcare_Doctor.Citizen.citizen_national_id);


            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.doctor_specialization_id = new SelectList(db.HealthCareSpecializations.Where(a => a.specialization_isDeleted != true), "specialization_id", "specialization_name_arabic" , healthcare_Doctor.HealthCareSpecialization.specialization_name_arabic);
                    ViewBag.doctor_citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", healthcare_Doctor.Citizen.citizen_national_id);



                }
            }
            return View(healthcare_Doctor);


            
        }
        [HttpPost]
        public ActionResult MainEditDoctor( Healthcare_Doctor healthcare_Doctor)
        {
            var data = db.Healthcare_Doctor.Where(a => a.doctor_isDeleted != true && a.doctor_id != healthcare_Doctor.doctor_id);
            foreach (var item in data)
            {
                if(item.doctor_specialization_id == healthcare_Doctor.doctor_specialization_id)
                {
                    ViewBag.doctor_specialization_id = new SelectList(db.HealthCareSpecializations.Where(a => a.specialization_isDeleted != true), "specialization_id", "specialization_name", healthcare_Doctor.HealthCareSpecialization.specialization_name);
                    ViewBag.doctor_citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", healthcare_Doctor.Citizen.citizen_national_id);


                    if (Session["lang"] != null)
                    {
                        if (Session["lang"].ToString().Equals("ar-EG"))
                        {
                            ViewBag.doctor_specialization_id = new SelectList(db.HealthCareSpecializations.Where(a => a.specialization_isDeleted != true), "specialization_id", "specialization_name_arabic", healthcare_Doctor.HealthCareSpecialization.specialization_name_arabic);
                            ViewBag.doctor_citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", healthcare_Doctor.Citizen.citizen_national_id);



                        }
                    }
                    ViewBag.errorMessage = Languages.Language.doctor_error_message;
                    return View(healthcare_Doctor);

                }
            }


            var old = db.Healthcare_Doctor.Find(healthcare_Doctor.doctor_id);
            old.doctor_citizen_id = healthcare_Doctor.doctor_citizen_id;
            old.doctor_specialization_id = healthcare_Doctor.doctor_specialization_id;
            db.SaveChanges();

            return RedirectToAction("IndexDoctor");
        }

        public JsonResult getCitizenName(int Id)
        {

            var data = db.Citizens.Where(a=> a.citizen_id == Id).Select(a=> new { a.citizen_first_name,a.citizen_second_name,a.citizen_third_name , a.citizen_fourth_name , a.citizen_first_name_arabic, a.citizen_second_name_arabic, a.citizen_third_name_arabic, a.citizen_fourth_name_arabic});



            db.Configuration.ProxyCreationEnabled = false;

            return Json(data,JsonRequestBehavior.AllowGet);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            //your handling logic here
            filterContext.ExceptionHandled = true;
            filterContext.Result = RedirectToAction("errorpage", "home");
        }
    }
}
