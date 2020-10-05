using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using Servicely.Models;
using EntityState = System.Data.Entity.EntityState;

namespace Servicely.Controllers
{
    public class HospitalsController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();


        public ActionResult IndexHealthCareByType ()
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
        // GET: Hospitals
        public ActionResult Index()
        {
            var hospitals = db.HealthCares.Include(h => h.District).Where(a=> a.hospital_isDeleted != true);
            return View(hospitals.ToList());
        }

        // GET: Hospitals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HealthCare hospital = db.HealthCares.Find(id);
            if (hospital == null)
            {
                return HttpNotFound();
            }
            return View(hospital);
        }

        // GET: Hospitals/Create
        public ActionResult Create()
        {


            if(Session["lang"] != null)
                {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {

                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");
                    ViewBag.healthcare_type_id = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name_arabic");
                    ViewBag.vacinationhealthcare_vaccination = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name_arabic");
                    ViewBag.hospitalSpecialization_specialization_id = new SelectList(db.HealthCareSpecializations.Where(a => a.specialization_isDeleted != true), "specialization_id", "specialization_name_arabic");

                    ViewBag.hospital_district_id = new SelectList(db.Districts, "district_id", "district_name");
                    return View();
                }
                else
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
                    ViewBag.healthcare_type_id = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name");
                    ViewBag.vacinationhealthcare_vaccination = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name");
                    ViewBag.hospitalSpecialization_specialization_id = new SelectList(db.HealthCareSpecializations.Where(a => a.specialization_isDeleted != true), "specialization_id", "specialization_name");

                    ViewBag.hospital_district_id = new SelectList(db.Districts, "district_id", "district_name");
                    return View();

                }
            }
                else
            {
                ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
                ViewBag.healthcare_type_id = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name");
                ViewBag.vacinationhealthcare_vaccination = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name");
                ViewBag.hospitalSpecialization_specialization_id = new SelectList(db.HealthCareSpecializations.Where(a => a.specialization_isDeleted != true), "specialization_id", "specialization_name");

                ViewBag.hospital_district_id = new SelectList(db.Districts, "district_id", "district_name");
                return View();
            }

           
        }

      
        [HttpPost]
       
        public ActionResult Create( HealthCare hospital , List<int?> vacinationhealthcare_vaccination, List<int> hospitalSpecialization_specialization_id)
        {
            if (ModelState.IsValid)
            {

                var data = db.HealthCares.Where(a => a.hospital_name == hospital.hospital_name && a.hospital_district_id == hospital.hospital_district_id && a.hospital_isDeleted != true && a.healthcare_type_id == hospital.healthcare_type_id).SingleOrDefault();

                if(data != null )
                {


                    if (Session["lang"] != null)
                    {
                        if (Session["lang"].ToString().Equals("ar-EG"))
                        {

                            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");
                            ViewBag.healthcare_type_id = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name_arabic");
                            ViewBag.vacinationhealthcare_vaccination = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name_arabic");
                            ViewBag.hospitalSpecialization_specialization_id = new SelectList(db.HealthCareSpecializations.Where(a => a.specialization_isDeleted != true), "specialization_id", "specialization_name_arabic");
                            ViewBag.errhospital = Languages.Language.errhospital;
                            ViewBag.hospital_district_id = new SelectList(db.Districts, "district_id", "district_name");
                            return View(hospital);
                        }
                        else
                        {
                            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
                            ViewBag.healthcare_type_id = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name");
                            ViewBag.vacinationhealthcare_vaccination = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name");
                            ViewBag.hospitalSpecialization_specialization_id = new SelectList(db.HealthCareSpecializations.Where(a => a.specialization_isDeleted != true), "specialization_id", "specialization_name");
                            ViewBag.errhospital = Languages.Language.errhospital;
                            ViewBag.hospital_district_id = new SelectList(db.Districts, "district_id", "district_name");
                            return View(hospital);

                        }
                    }
                    else
                    {
                        ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
                        ViewBag.healthcare_type_id = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name");
                        ViewBag.vacinationhealthcare_vaccination = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name");
                        ViewBag.hospitalSpecialization_specialization_id = new SelectList(db.HealthCareSpecializations.Where(a => a.specialization_isDeleted != true), "specialization_id", "specialization_name");
                        ViewBag.errhospital = Languages.Language.errhospital;
                        ViewBag.hospital_district_id = new SelectList(db.Districts, "district_id", "district_name");
                        return View(hospital);
                    }


                    
                }
                db.HealthCares.Add(hospital);
                db.SaveChanges();
                if (vacinationhealthcare_vaccination != null)
                {
                    VacinationHealthCareM_M vaccinaate = new VacinationHealthCareM_M();
                    foreach (var item in vacinationhealthcare_vaccination)
                    {
                        vaccinaate.vaccinationhealthcare_hospital = hospital.hospital_id;
                        vaccinaate.vacinationhealthcare_vaccination = item;
                        db.VacinationHealthCareM_M.Add(vaccinaate);
                        db.SaveChanges();

                    }


                    
                }
                foreach (var item in hospitalSpecialization_specialization_id)
                {
                    HealthCareSpecializationM_M spec = new HealthCareSpecializationM_M();
                    spec.hospitalSpecialization_hospital_id = hospital.hospital_id;
                    spec.hospitalSpecialization_specialization_id = item;
                    db.HealthCareSpecializationM_M.Add(spec);

                    db.SaveChanges();
                }
              


                return RedirectToAction("Index");
            }
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
            ViewBag.healthcare_type_id = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name");
            ViewBag.vacinationhealthcare_vaccination = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name");
            ViewBag.hospitalSpecialization_specialization_id = new SelectList(db.HealthCareSpecializations.Where(a => a.specialization_isDeleted != true), "specialization_id", "specialization_name");

            ViewBag.hospital_district_id = new SelectList(db.Districts, "district_id", "district_name", hospital.hospital_district_id);
            return View(hospital);
        }

        // GET: Hospitals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HealthCare hospital = db.HealthCares.Find(id);
            if (hospital == null)
            {
                return HttpNotFound();
            }

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {

                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");
                    ViewBag.healthcare_type_id = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name_arabic");
                    ViewBag.vacinationhealthcare_vaccination = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name_arabic");
                    ViewBag.hospitalSpecialization_specialization_id = new SelectList(db.HealthCareSpecializations.Where(a => a.specialization_isDeleted != true), "specialization_id", "specialization_name_arabic");

                    ViewBag.hospital_district_id = new SelectList(db.Districts, "district_id", "district_name");
                    return View(hospital);
                }
                else
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
                    ViewBag.healthcare_type_id = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name");
                    ViewBag.vacinationhealthcare_vaccination = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name");
                    ViewBag.hospitalSpecialization_specialization_id = new SelectList(db.HealthCareSpecializations.Where(a => a.specialization_isDeleted != true), "specialization_id", "specialization_name");

                    ViewBag.hospital_district_id = new SelectList(db.Districts, "district_id", "district_name");
                    return View(hospital);

                }
            }
            else
            {
                ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
                ViewBag.healthcare_type_id = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name");
                ViewBag.vacinationhealthcare_vaccination = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name");
                ViewBag.hospitalSpecialization_specialization_id = new SelectList(db.HealthCareSpecializations.Where(a => a.specialization_isDeleted != true), "specialization_id", "specialization_name");

                ViewBag.hospital_district_id = new SelectList(db.Districts, "district_id", "district_name");
                return View(hospital);
            }

           
        }

        
        [HttpPost]
    
        public ActionResult Edit(HealthCare hospital)
        {
            if (ModelState.IsValid)
            {
                var data = db.HealthCares.Where(a => a.hospital_id != hospital.hospital_id && a.healthcare_type_id != hospital.healthcare_type_id && a.hospital_district_id != hospital.hospital_district_id && a.hospital_isDeleted!= true);

                foreach (var item in data)
                {
                    if( item.hospital_name == hospital.hospital_name)
                    {
                        if (Session["lang"] != null)
                        {
                            if (Session["lang"].ToString().Equals("ar-EG"))
                            {

                                ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");
                                ViewBag.healthcare_type_id = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name_arabic");
                                ViewBag.vacinationhealthcare_vaccination = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name_arabic");
                                ViewBag.hospitalSpecialization_specialization_id = new SelectList(db.HealthCareSpecializations.Where(a => a.specialization_isDeleted != true), "specialization_id", "specialization_name_arabic");
                                ViewBag.errhospital = Languages.Language.errhospital;
                                ViewBag.hospital_district_id = new SelectList(db.Districts, "district_id", "district_name");
                                return View(hospital);
                            }
                            else
                            {
                                ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
                                ViewBag.healthcare_type_id = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name");
                                ViewBag.vacinationhealthcare_vaccination = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name");
                                ViewBag.hospitalSpecialization_specialization_id = new SelectList(db.HealthCareSpecializations.Where(a => a.specialization_isDeleted != true), "specialization_id", "specialization_name");
                                ViewBag.errhospital = Languages.Language.errhospital;
                                ViewBag.hospital_district_id = new SelectList(db.Districts, "district_id", "district_name");
                                return View(hospital);

                            }
                        }
                        else
                        {
                            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
                            ViewBag.healthcare_type_id = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name");
                            ViewBag.vacinationhealthcare_vaccination = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name");
                            ViewBag.hospitalSpecialization_specialization_id = new SelectList(db.HealthCareSpecializations.Where(a => a.specialization_isDeleted != true), "specialization_id", "specialization_name");
                            ViewBag.errhospital = Languages.Language.errhospital;
                            ViewBag.hospital_district_id = new SelectList(db.Districts, "district_id", "district_name");
                            return View(hospital);
                        }
                    }
                }
                db.Entry(hospital).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.hospital_district_id = new SelectList(db.Districts, "district_id", "district_name", hospital.hospital_district_id);
            return View(hospital);
        }

        // GET: Hospitals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HealthCare hospital = db.HealthCares.Find(id);
            if (hospital == null)
            {
                return HttpNotFound();
            }
            return View(hospital);
        }

        // POST: Hospitals/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            HealthCare hospital = db.HealthCares.Find(id);
            hospital.hospital_isDeleted = true;
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
