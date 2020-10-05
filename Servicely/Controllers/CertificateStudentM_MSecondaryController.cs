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
    public class Certificatee
    {
        public int Id { get; set; }
        public string CirtificateTypeNameArabic { get; set; }
        public string CirtificateTypeName { get; set; }
        
    }
    public class CertificateStudentM_MSecondaryController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: CertificateStudentM_MSecondary
        public ActionResult Index()
        {
            var certificateStudentM_M = db.CertificateStudentM_M.Include(c => c.Certificate).Include(c => c.Grade).Include(c => c.Student);
            return View(certificateStudentM_M.ToList());
        }

       

        // GET: CertificateStudentM_MSecondary/Create
        public ActionResult Create()
        {
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
            ViewBag.GradeId = new SelectList(db.Grades.Where(a=> a.Is_Deleted!= true), "Id", "GradeName");

            ViewBag.Phase = new SelectList(db.PhasesOfSchooles.Where(a => a.Is_Deleted != true), "Id", "PhaseName");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                    ViewBag.Phase = new SelectList(db.PhasesOfSchooles.Where(a => a.Is_Deleted != true), "Id", "PhaseNameArabic");
                    ViewBag.GradeId = new SelectList(db.Grades.Where(a => a.Is_Deleted != true), "Id", "GradeNameArabic");

                }
            }
            //ViewBag.CertificateId = new SelectList(db.Certificates, "Id", "CirtificateTypeName");
            //ViewBag.StudentId = new SelectList(db.Students, "Id", "SchoolSection");
            return View();
        }

        // POST: CertificateStudentM_MSecondary/Create
        
        [HttpPost]
       
        public ActionResult Create( CertificateStudentM_M certificateStudentM_M , List<int> StudentId , int Phase)
        {
            if (ModelState.IsValid)
            {
                var data = db.CertificateStudentM_M.Where(a => a.StudentId == certificateStudentM_M.StudentId && a.CertificateId == certificateStudentM_M.CertificateId).SingleOrDefault();
               if(data != null)
                {
                    ViewBag.CertificateId = new SelectList(db.Certificates, "Id", "CirtificateTypeName", certificateStudentM_M.CertificateId);
                    ViewBag.GradeId = new SelectList(db.Grades.Where(a => a.Is_Deleted != true), "Id", "GradeName", certificateStudentM_M.GradeId);
                    ViewBag.StudentId = new SelectList(db.Students.Where(a => a.Is_Deleted != true), "Id", "SchoolSection", certificateStudentM_M.StudentId);
                  
                    if (Session["lang"] != null)
                   {
                        if (Session["lang"].ToString().Equals("ar-EG"))
                        {
                            ViewBag.CertificateId = new SelectList(db.Certificates, "Id", "CirtificateTypeNameArabic", certificateStudentM_M.CertificateId);
                            ViewBag.GradeId = new SelectList(db.Grades.Where(a => a.Is_Deleted != true), "Id", "GradeNameArabic", certificateStudentM_M.GradeId);
                            ViewBag.StudentId = new SelectList(db.Students.Where(a => a.Is_Deleted != true), "Id", "SchoolSection", certificateStudentM_M.StudentId);
                            

                        }

                    }
       

              

                }
                if( Phase == 1)
                {
                    foreach (var item in StudentId)
                    {
                        certificateStudentM_M.StudentId = item;
                        certificateStudentM_M.Date = DateTime.Now;
                        db.CertificateStudentM_M.Add(certificateStudentM_M);
                        var old = db.Students.Find(certificateStudentM_M.StudentId);
                        old.IsGraduatedS = true;
                        db.SaveChanges();
                   
                    }
                }
                else if (Phase == 2)
                {
                    foreach (var item in StudentId)
                    {
                        certificateStudentM_M.StudentId = item;
                        certificateStudentM_M.Date = DateTime.Now;
                        db.CertificateStudentM_M.Add(certificateStudentM_M);
                        var old = db.Students.Find(certificateStudentM_M.StudentId);
                        old.IsGraduatedI = true;
                        db.SaveChanges();
                       
                    }
                }
                else
                {
                    foreach (var item in StudentId)
                    {
                        certificateStudentM_M.StudentId = item;
                        certificateStudentM_M.Date = DateTime.Now;
                        db.CertificateStudentM_M.Add(certificateStudentM_M);
                        var old = db.Students.Find(certificateStudentM_M.StudentId);
                        old.IsGraduatedP = true;
                        db.SaveChanges();
                        
                    }
                }




                return RedirectToAction("SchoolCertificate", "CertificateStudentM_M");
            }

            ViewBag.CertificateId = new SelectList(db.Certificates, "Id", "CirtificateTypeName", certificateStudentM_M.CertificateId);
            ViewBag.GradeId = new SelectList(db.Grades.Where(a=> a.Is_Deleted!= true), "Id", "GradeName", certificateStudentM_M.GradeId);
            ViewBag.StudentId = new SelectList(db.Students.Where(a => a.Is_Deleted != true), "Id", "SchoolSection", certificateStudentM_M.StudentId);
            return View(certificateStudentM_M);
        }

        // GET: CertificateStudentM_MSecondary/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CertificateStudentM_M certificateStudentM_M = db.CertificateStudentM_M.Find(id);
            if (certificateStudentM_M == null)
            {
                return HttpNotFound();
            }
            ViewBag.CertificateId = new SelectList(db.Certificates, "Id", "CirtificateTypeName", certificateStudentM_M.CertificateId);
            ViewBag.GradeId = new SelectList(db.Grades, "Id", "GradeName", certificateStudentM_M.GradeId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "SchoolSection", certificateStudentM_M.StudentId);
            return View(certificateStudentM_M);
        }

        // POST: CertificateStudentM_MSecondary/Edit/5
        [HttpPost]
        public ActionResult Edit( CertificateStudentM_M certificateStudentM_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(certificateStudentM_M).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CertificateId = new SelectList(db.Certificates, "Id", "CirtificateTypeName", certificateStudentM_M.CertificateId);
            ViewBag.GradeId = new SelectList(db.Grades, "Id", "GradeName", certificateStudentM_M.GradeId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "SchoolSection", certificateStudentM_M.StudentId);
            return View(certificateStudentM_M);
        }

        // GET: CertificateStudentM_MSecondary/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CertificateStudentM_M certificateStudentM_M = db.CertificateStudentM_M.Find(id);
            if (certificateStudentM_M == null)
            {
                return HttpNotFound();
            }
            return View(certificateStudentM_M);
        }

        // POST: CertificateStudentM_MSecondary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CertificateStudentM_M certificateStudentM_M = db.CertificateStudentM_M.Find(id);
            db.CertificateStudentM_M.Remove(certificateStudentM_M);
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

        //------------------------Ajax Call --------------------------
        //------------------------Ajax Call --------------------------
        //------------------------Ajax Call --------------------------

        public JsonResult GetCertificateByPhaseId(int ph)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if ( ph == 1)
            {
                var data = db.Certificates.Where(a => a.Is_Deleted != true && a.Id == 1).Select(a => new Certificatee { Id = a.Id, CirtificateTypeName = a.CirtificateTypeName , CirtificateTypeNameArabic= a.CirtificateTypeNameArabic });
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else if (ph == 2)
            {
                var data = db.Certificates.Where(a => a.Is_Deleted != true && a.Id == 2).Select(a => new Certificatee { Id = a.Id, CirtificateTypeName = a.CirtificateTypeName, CirtificateTypeNameArabic = a.CirtificateTypeNameArabic });
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = db.Certificates.Where(a => a.Is_Deleted != true && a.Id == 3).Select(a => new Certificatee { Id = a.Id, CirtificateTypeName = a.CirtificateTypeName, CirtificateTypeNameArabic = a.CirtificateTypeNameArabic });
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            

        }
        public JsonResult GetAllStudentBySchoolId(int school , int Phase)
        {
            db.Configuration.ProxyCreationEnabled = false;

            if( Phase == 1)
            {

                var data = db.Students.Where(a => a.SchoolId == school && a.Is_Deleted != true && a.IsGraduatedI == true && a.IsGraduatedP == true&& a.IsGraduatedS != true && a.IsGraduatedU!= true).Select(a => new
                {
                    a.Id,
                    a.Citizen.citizen_first_name,
                    a.Citizen.citizen_second_name,
                    a.Citizen.citizen_third_name,
                    a.Citizen.citizen_fourth_name,
                    a.Citizen.citizen_national_id,
                    a.Citizen.citizen_first_name_arabic,
                    a.Citizen.citizen_second_name_arabic,
                    a.Citizen.citizen_third_name_arabic,
                    a.Citizen.citizen_fourth_name_arabic
                }).ToList();

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else if (Phase == 2)
            {

                var data = db.Students.Where(a => a.SchoolId == school && a.Is_Deleted != true && a.IsGraduatedI != true && a.IsGraduatedP == true && a.IsGraduatedS != true && a.IsGraduatedU != true).Select(a => new
                {
                    a.Id,
                    a.Citizen.citizen_first_name,
                    a.Citizen.citizen_second_name,
                    a.Citizen.citizen_third_name,
                    a.Citizen.citizen_fourth_name,
                    a.Citizen.citizen_national_id,
                    a.Citizen.citizen_first_name_arabic,
                    a.Citizen.citizen_second_name_arabic,
                    a.Citizen.citizen_third_name_arabic,
                    a.Citizen.citizen_fourth_name_arabic
                });

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                

                    var data = db.Students.Where(a => a.SchoolId == school && a.Is_Deleted != true && a.IsGraduatedI != true && a.IsGraduatedP != true && a.IsGraduatedS != true && a.IsGraduatedU != true).Select(a => new
                    {
                        a.Id,
                        a.Citizen.citizen_first_name,
                        a.Citizen.citizen_second_name,
                        a.Citizen.citizen_third_name,
                        a.Citizen.citizen_fourth_name,
                        a.Citizen.citizen_national_id,
                        a.Citizen.citizen_first_name_arabic,
                        a.Citizen.citizen_second_name_arabic,
                        a.Citizen.citizen_third_name_arabic,
                        a.Citizen.citizen_fourth_name_arabic
                    });

                    return Json(data, JsonRequestBehavior.AllowGet);
                
            }

        }
        protected override void OnException(ExceptionContext filterContext)
        {
            //your handling logic here
            filterContext.ExceptionHandled = true;
            filterContext.Result = RedirectToAction("errorpage", "home");
        }
    }
}
