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
    public class aaa
    {
        public string state_name { get; set; }
        public string state_arabic_name { get; set; }

        public string FacultyNameArabic { get; set; }
        public string FacultyName { get; set; }
        public int? Id { get; set; }
        public  string GradeName { get; set; }
        public  string GradeNameArabic { get; set; }
        public  string Date { get; set; }
        public  string citizen_first_name { get; set; }
        public  string citizen_second_name { get; set; }
        public  string citizen_third_name { get; set; }
        public  string citizen_fourth_name { get; set; }
        public  string citizen_first_name_arabic { get; set; }
        public  string citizen_birthDate { get; set; }
        public  string citizen_second_name_arabic { get; set; }
        public  string citizen_third_name_arabic { get; set; }
        public  string citizen_fourth_name_arabic { get; set; }
        public  string SpecializationName { get; set; }
        public  string SpecializationNameArabic { get; set; }
        public  string UniversityName { get; set; }
        public  string UniversityNameArabic { get; set; }
        public  string UniversityLogo { get; set; }
        public  string citizen_national_id { get; set; }
        public  string SchoolName { get; set; }
        public  string SchoolNameArabic { get; set; }
        public  string SchoolSection { get; set; }
        public  string DirectorateName { get; set; }
        public  string DirectorateNameArabic { get; set; }
        
    }                
                     
    public class CertificateStudentM_MController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: CertificateStudentM_M
        public ActionResult Index()
        {
            ViewBag.GradeId = new SelectList(db.Grades.Where(a => a.Is_Deleted != true), "Id", "GradeName");
            ViewBag.zero = "0";
            ViewBag.UType = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeName");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.UType = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeNameArabic");
                    ViewBag.GradeId = new SelectList(db.Grades.Where(a => a.Is_Deleted != true), "Id", "GradeNameArabic");


                }
            }

            var certificateStudentM_M = db.CertificateStudentM_M.Include(c => c.Certificate).Include(c => c.Grade).Include(c => c.Student);
            return View(certificateStudentM_M.ToList());
        }

        public JsonResult GetAllSpecByFacultyId(int Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.SpecializationOfFaculties.Where(a => a.Is_Deleted != true && a.FacultyId == Id).Select(a => new
            {
                a.SpecializationName,
                a.SpecializationNameArabic,
                a.Id
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllStudentInfoByFacutlyIdandGradeId(int Id , int GId )
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data1 = db.FacultyCertificateM_M.Where(a => a.Is_deleted != true && a.FacultyId == Id).Select(a => new { a.Certificate.CirtificateTypeName, a.Certificate.CirtificateTypeNameArabic, a.CertificateId }).FirstOrDefault();

            var data = db.CertificateStudentM_M.Where(a => ( a.Is_Deleted != true && a.CertificateId == data1.CertificateId && a.GradeId == GId) ).Select(a => new aaa
            {
                Id = a.Id,
                citizen_first_name = a.Student.Citizen.citizen_first_name,
                citizen_first_name_arabic = a.Student.Citizen.citizen_first_name_arabic,
                citizen_second_name = a.Student.Citizen.citizen_second_name,
                citizen_second_name_arabic = a.Student.Citizen.citizen_second_name_arabic,
                citizen_third_name = a.Student.Citizen.citizen_third_name,
                citizen_third_name_arabic = a.Student.Citizen.citizen_third_name_arabic,
                citizen_fourth_name = a.Student.Citizen.citizen_fourth_name,
                citizen_fourth_name_arabic = a.Student.Citizen.citizen_fourth_name_arabic,
                citizen_national_id = a.Student.Citizen.citizen_national_id,
                SpecializationName = a.Student.SpecializationOfFaculty.SpecializationName,
                SpecializationNameArabic = a.Student.SpecializationOfFaculty.SpecializationNameArabic,
                Date = a.Date.ToString()

            }).ToList() ;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllStudentInfoByFacutlyIdandGradeIdd(int Id, int GId, int SId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data1 = db.FacultyCertificateM_M.Where(a => a.Is_deleted != true && a.FacultyId == Id).Select(a => new { a.Certificate.CirtificateTypeName, a.Certificate.CirtificateTypeNameArabic, a.CertificateId }).FirstOrDefault();

            var data = db.CertificateStudentM_M.Where(a => (a.Is_Deleted != true && a.CertificateId == data1.CertificateId && a.GradeId == GId && a.Student.SpecializationFacultyId == SId)).Select(a => new aaa
            {
                Id = a.Id,
                citizen_first_name = a.Student.Citizen.citizen_first_name,
                citizen_first_name_arabic = a.Student.Citizen.citizen_first_name_arabic,
                citizen_second_name = a.Student.Citizen.citizen_second_name,
                citizen_second_name_arabic = a.Student.Citizen.citizen_second_name_arabic,
                citizen_third_name = a.Student.Citizen.citizen_third_name,
                citizen_third_name_arabic = a.Student.Citizen.citizen_third_name_arabic,
                citizen_fourth_name = a.Student.Citizen.citizen_fourth_name,
                citizen_fourth_name_arabic = a.Student.Citizen.citizen_fourth_name_arabic,
                citizen_national_id = a.Student.Citizen.citizen_national_id,
                SpecializationName = a.Student.SpecializationOfFaculty.SpecializationName,
                SpecializationNameArabic = a.Student.SpecializationOfFaculty.SpecializationNameArabic,
                Date = a.Date.ToString()

            });
                return Json(data, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetAllStudentInfoByFacutlyIdandGradeIddd(int Id, int GId,   DateTime dd)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data1 = db.FacultyCertificateM_M.Where(a => a.Is_deleted != true && a.FacultyId == Id).Select(a => new { a.Certificate.CirtificateTypeName, a.Certificate.CirtificateTypeNameArabic, a.CertificateId }).FirstOrDefault();

            var data = db.CertificateStudentM_M.Where(a => ( a.Is_Deleted != true && a.CertificateId == data1.CertificateId && a.GradeId == GId && a.Date == dd)).Select(a => new aaa
            {
                Id = a.Id,
                citizen_first_name = a.Student.Citizen.citizen_first_name,
                citizen_first_name_arabic = a.Student.Citizen.citizen_first_name_arabic,
                citizen_second_name = a.Student.Citizen.citizen_second_name,
                citizen_second_name_arabic = a.Student.Citizen.citizen_second_name_arabic,
                citizen_third_name = a.Student.Citizen.citizen_third_name,
                citizen_third_name_arabic = a.Student.Citizen.citizen_third_name_arabic,
                citizen_fourth_name = a.Student.Citizen.citizen_fourth_name,
                citizen_fourth_name_arabic = a.Student.Citizen.citizen_fourth_name_arabic,
                citizen_national_id = a.Student.Citizen.citizen_national_id,
                SpecializationName = a.Student.SpecializationOfFaculty.SpecializationName,
                SpecializationNameArabic = a.Student.SpecializationOfFaculty.SpecializationNameArabic,
                Date = a.Date.ToString()

            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CertificateStudent ()
        {
            ViewBag.StudentId = new SelectList(db.Students.Where(a => a.Is_Deleted != true && a.IsGraduatedU == true && a.IsGraduatedP == true&& a.IsGraduatedS == true && a.IsGraduatedI== true).Select(a => new { a.Id, a.Citizen.citizen_national_id }), "Id", "citizen_national_id");

            ViewBag.UType = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeName");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.UType = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeNameArabic");


                }
            }
            return View();
        }

        // ----------------- School Certificate --------------------
        // ----------------- School Certificate --------------------
        // ----------------- School Certificate --------------------



        public JsonResult GetCitizenQRCode1(int cid)
        {

            db.Configuration.ProxyCreationEnabled = false;
            var dd = db.Students.Find(cid);
            string aa = "";
            var data = db.LoginCitizens.Where(a => a.Login_CitizenId == dd.CitizenId).SingleOrDefault();
            if (data != null)
                aa = data.Login_QRcode.ToString();
            return Json(aa, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SchoolCertificate ()
        {
            ViewBag.Student = new SelectList(db.PhasesOfSchooles.Where(a => a.Is_Deleted != true), "Id", "PhaseName");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.Student = new SelectList(db.PhasesOfSchooles.Where(a => a.Is_Deleted != true), "Id", "PhaseNameArabic");


                }

            }
            return View();
        }


        public JsonResult GetAllStudentByPhase(int Id)
        {
            if( Id == 1)
            {
                var data = db.Students.Where(a =>   a.Is_Deleted != true && a.IsGraduatedI == true && a.IsGraduatedP == true && a.IsGraduatedS == true).Select(a => new
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
                    a.Citizen.citizen_fourth_name_arabic,
                   
                    
                   
                }).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else if (Id == 2)
            {
                var data = db.Students.Where(a => a.Is_Deleted != true && a.IsGraduatedI == true && a.IsGraduatedP == true ).Select(a => new
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
                    a.Citizen.citizen_fourth_name_arabic,



                }).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
               
                    var data = db.Students.Where(a => a.Is_Deleted != true  && a.IsGraduatedP == true ).Select(a => new
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
                        a.Citizen.citizen_fourth_name_arabic,



                    }).ToList();
                    return Json(data, JsonRequestBehavior.AllowGet);
                
            }

        }

       
        public JsonResult GetAllInfoOfStudentByPaseAndSTudentId(int Id , int ph)
        {
            if (ph == 1)
            {
                var data = db.Students.Where(a => a.Id == Id &&a.Is_Deleted != true && a.IsGraduatedI == true && a.IsGraduatedP == true && a.IsGraduatedS == true ).Select(a => new CustomCitizen
                {
                    
                   citizen_first_name= a.Citizen.citizen_first_name,
                   citizen_second_name= a.Citizen.citizen_second_name,
                   citizen_third_name= a.Citizen.citizen_third_name,
                   citizen_fourth_name = a.Citizen.citizen_fourth_name,
                    citizen_national_id= a.Citizen.citizen_national_id,
                   citizen_first_name_arabic= a.Citizen.citizen_first_name_arabic,
                   citizen_second_name_arabic= a.Citizen.citizen_second_name_arabic,
                   citizen_third_name_arabic= a.Citizen.citizen_third_name_arabic,
                   citizen_fourth_name_arabic= a.Citizen.citizen_fourth_name_arabic,
                    



                }).ToList().SingleOrDefault();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else if (ph == 2)
            {
                var data = db.Students.Where(a => a.Id == Id && a.Is_Deleted != true && a.IsGraduatedI == true && a.IsGraduatedP == true ).Select(a => new CustomCitizen
                {
                    citizen_first_name = a.Citizen.citizen_first_name,
                    citizen_second_name = a.Citizen.citizen_second_name,
                    citizen_third_name = a.Citizen.citizen_third_name,
                    citizen_fourth_name = a.Citizen.citizen_fourth_name,
                    citizen_national_id = a.Citizen.citizen_national_id,
                    citizen_first_name_arabic = a.Citizen.citizen_first_name_arabic,
                    citizen_second_name_arabic = a.Citizen.citizen_second_name_arabic,
                    citizen_third_name_arabic = a.Citizen.citizen_third_name_arabic,
                    citizen_fourth_name_arabic = a.Citizen.citizen_fourth_name_arabic,



                }).ToList().SingleOrDefault();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {

                var data = db.Students.Where(a => a.Id == Id && a.Is_Deleted != true  && a.IsGraduatedP == true ).Select(a => new CustomCitizen
                {
                    citizen_first_name = a.Citizen.citizen_first_name,
                    citizen_second_name = a.Citizen.citizen_second_name,
                    citizen_third_name = a.Citizen.citizen_third_name,
                    citizen_fourth_name = a.Citizen.citizen_fourth_name,
                    citizen_national_id = a.Citizen.citizen_national_id,
                    citizen_first_name_arabic = a.Citizen.citizen_first_name_arabic,
                    citizen_second_name_arabic = a.Citizen.citizen_second_name_arabic,
                    citizen_third_name_arabic = a.Citizen.citizen_third_name_arabic,
                    citizen_fourth_name_arabic = a.Citizen.citizen_fourth_name_arabic,



                }).ToList().SingleOrDefault();
                return Json(data, JsonRequestBehavior.AllowGet);

            }


            
        }
        public JsonResult GetGradesOfStudentByIdAndPhase(int Id, int ph)
        {
            db.Configuration.LazyLoadingEnabled = false;
            if ( ph == 1|| ph == 3 || ph == 2 )
            {
                var data = db.CertificateStudentM_M.Where(a => a.Is_Deleted != true && a.CertificateId == ph && a.StudentId == Id).Select(a => new
                {
                    a.Student.SchoolSection,
                    a.Grade.GradeName,
                    a.Grade.GradeNameArabic,
                    a.Student.School.SchoolName,
                    a.Student.School.SchoolNameArabic,
                    a.Student.School.EducationDirectorate.DirectorateName,
                    a.Student.School.EducationDirectorate.DirectorateNameArabic,
                    a.Student.School.EducationDirectorate.State.state_arabic_name,
                    a.Student.School.EducationDirectorate.State.state_name

                }).SingleOrDefault();
                return Json(data, JsonRequestBehavior.AllowGet);
            }

            return Json("", JsonRequestBehavior.AllowGet);

        }
        // ----------------- End School Certificate --------------------
        // ----------------- End School Certificate --------------------
        // ----------------- End School Certificate --------------------
        // GET: CertificateStudentM_M/Create
        public ActionResult Create()
        {
           
            ViewBag.UType = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeName");
            
            ViewBag.CertificateId = new SelectList(db.Certificates.Where(a => a.Is_Deleted != true), "Id", "CirtificateTypeName");
            ViewBag.GradeId = new SelectList(db.Grades.Where(a => a.Is_Deleted != true), "Id", "GradeName");
            ViewBag.GraduationProjectGradeId = new SelectList(db.Grades.Where(a => a.Is_Deleted != true), "Id", "GradeName");
            ViewBag.StudentId = new SelectList(db.Students.Where(a=> a.Is_Deleted!= true && a.IsGraduatedS == true && a.IsGraduatedU!= true).Select(a=> new { a.Id , a.Citizen.citizen_national_id}), "Id", "citizen_national_id");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.UType = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeNameArabic");

                    ViewBag.CertificateId = new SelectList(db.Certificates.Where(a => a.Is_Deleted != true), "Id", "CirtificateTypeNameArabic");
                    ViewBag.GradeId = new SelectList(db.Grades.Where(a => a.Is_Deleted != true), "Id", "GradeNameArabic");
                    ViewBag.GraduationProjectGradeId = new SelectList(db.Grades.Where(a => a.Is_Deleted != true), "Id", "GradeNameArabic");
                    ViewBag.StudentId = new SelectList(db.Students.Where(a => a.Is_Deleted != true && a.IsGraduatedS == true && a.IsGraduatedU != true).Select(a => new { a.Id, a.Citizen.citizen_national_id }), "Id", "citizen_national_id");

                }
            }
                return View();
        }

        // POST: CertificateStudentM_M/Create
      
        [HttpPost]
       
        public ActionResult Create( CertificateStudentM_M certificateStudentM_M ,int facultyId ,List<int>StudentId)
        {
            if (ModelState.IsValid)
            {


                foreach (var item in StudentId)
                {
                    


                    certificateStudentM_M.StudentId = item;
                    db.CertificateStudentM_M.Add(certificateStudentM_M);
                    var old = db.Students.Find(certificateStudentM_M.StudentId);
                    old.IsGraduatedU = true;
                    db.SaveChanges();

                }
                
               
                return RedirectToAction("Index");
            }

            ViewBag.CertificateId = new SelectList(db.Certificates, "Id", "CirtificateTypeName", certificateStudentM_M.CertificateId);
            ViewBag.GradeId = new SelectList(db.Grades, "Id", "GradeName", certificateStudentM_M.GradeId);
            ViewBag.GraduationProjectGradeId = new SelectList(db.Grades, "Id", "GradeName", certificateStudentM_M.GradeId);
            ViewBag.StudentId = new SelectList(db.Students.Where(a => a.Is_Deleted != true && a.IsGraduatedS == true && a.IsGraduatedU != true).Select(a => new { a.Id, a.Citizen.citizen_national_id }), "Id", "citizen_national_id");

            return View(certificateStudentM_M);
        }

        // GET: CertificateStudentM_M/Edit/5
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
            ViewBag.GraduationProjectGradeId = new SelectList(db.Grades, "Id", "GradeName", certificateStudentM_M.GradeId);
            ViewBag.StudentId = new SelectList(db.Students.Where(a => a.Is_Deleted != true && a.IsGraduatedS == true && a.IsGraduatedU != true).Select(a => new { a.Id, a.Citizen.citizen_national_id }), "Id", "citizen_national_id");


            return View(certificateStudentM_M);
        }

        // POST: CertificateStudentM_M/Edit/5
        
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

        // GET: CertificateStudentM_M/Delete/5
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

        // POST: CertificateStudentM_M/Delete/5
        [HttpPost, ActionName("Delete")]
       
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

        //---------------- Ajax Call -------------------

        public JsonResult GetAllCertificateByFacultyId(int Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
           
            var data = db.FacultyCertificateM_M.Where(a=> a.Is_deleted!= true&& a.FacultyId == Id ).Select(a=> new { a.Certificate.CirtificateTypeName , a.Certificate.CirtificateTypeNameArabic , a.CertificateId}).FirstOrDefault();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllStudentByFacultyId(int Id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var data = db.Students.Where(a => a.Is_Deleted != true && a.FacultyId == Id).Select(a=> new { 
            
            a.Citizen.citizen_first_name   ,
            a.Citizen.citizen_second_name    ,
            a.Citizen.citizen_third_name    ,
            a.Citizen.citizen_fourth_name   ,
            a.Citizen.citizen_first_name_arabic,
            a.Citizen.citizen_second_name_arabic,
            a.Citizen.citizen_third_name_arabic,
            a.Citizen.citizen_fourth_name_arabic,
            a.Id,
            a.Citizen.citizen_national_id
            
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetAllStudentInfoById(int Id ,int FId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data1 = db.FacultyCertificateM_M.Where(a => a.Is_deleted != true && a.FacultyId == FId).Select(a => new { a.Certificate.CirtificateTypeName, a.Certificate.CirtificateTypeNameArabic, a.CertificateId }).FirstOrDefault();

            var data = db.CertificateStudentM_M.Where(a => a.Is_Deleted != true && a.Student.IsGraduatedS == true && a.StudentId == Id && a.CertificateId == data1.CertificateId).Select(a => new aaa {

                GradeName = a.Grade.GradeName,
                GradeNameArabic = a.Grade.GradeNameArabic,
                Date = a.Date.ToString(),
                citizen_first_name = a.Student.Citizen.citizen_first_name,
                citizen_second_name = a.Student.Citizen.citizen_second_name,
                citizen_third_name = a.Student.Citizen.citizen_third_name,
                citizen_fourth_name = a.Student.Citizen.citizen_fourth_name,
                citizen_first_name_arabic = a.Student.Citizen.citizen_first_name_arabic,
                citizen_birthDate = a.Student.Citizen.citizen_birthDate.ToString(),
                citizen_second_name_arabic = a.Student.Citizen.citizen_second_name_arabic,
                citizen_third_name_arabic = a.Student.Citizen.citizen_third_name_arabic,
                citizen_fourth_name_arabic = a.Student.Citizen.citizen_fourth_name_arabic,
                SpecializationName = a.Student.SpecializationOfFaculty.SpecializationName,
                SpecializationNameArabic = a.Student.SpecializationOfFaculty.SpecializationNameArabic,
                UniversityName = a.Student.University.UniversityName,
                UniversityNameArabic = a.Student.University.UniversityNameArabic,
                UniversityLogo = a.Student.University.UniversityLogo



            }).SingleOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPhotoByStudentId(int Id)
        {
            var citizen = db.Students.Find(Id).CitizenId;
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.Photos.Where(a => a.Photo_isDeleted != true && a.Photo_citizen_id == citizen && a.Photo_isCurrent == true).SingleOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAddressByStudentId(int Id)
        {
            var citizen = db.Students.Find(Id).CitizenId;
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.AddressCitizens.Where(a => a.CA_CitizenId == citizen).FirstOrDefault().CA_AddressId;//Address.District.Region.City.State.state_name;
            var state = db.Addresses.Where(a=> a.address_id ==data).Select(a=> a.District.Region.City.State.state_name).SingleOrDefault();
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    var data1 = db.AddressCitizens.Where(a => a.CA_CitizenId == citizen).FirstOrDefault().Address.District.Region.City.State.state_arabic_name;
                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(state, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllDataByStudentId(int Id)
        {
            var citizenId = db.Students.Find(Id).CitizenId;
            var faculty = db.Students.Find(Id).FacultyId;
            var cirtificate = db.FacultyCertificateM_M.Where(a=>a.FacultyId == faculty).SingleOrDefault().CertificateId;
            var NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate = db.CertificateStudentM_M.Where(a =>  a.StudentId == Id && a.CertificateId ==cirtificate).Select(a => new { a.Student.Citizen.citizen_first_name, a.Student.Citizen.citizen_second_name, a.Student.Citizen.citizen_third_name, a.Student.Citizen.citizen_fourth_name, a.Student.Citizen.citizen_first_name_arabic, a.Student.Citizen.citizen_second_name_arabic, a.Student.Citizen.citizen_third_name_arabic, a.Student.Citizen.citizen_fourth_name_arabic, a.Student.Citizen.citizen_birthDate,a.Student.Faculty.FacultyName,a.Student.Faculty.FacultyNameArabic,a.Student.University.UniversityName,a.Student.University.UniversityNameArabic,a.Grade.GradeName,a.Grade.GradeNameArabic,a.Student.SpecializationOfFaculty.SpecializationName,a.Student.SpecializationOfFaculty.SpecializationNameArabic,a.Date ,a.Student.University.UniversityLogo }).SingleOrDefault();
            var address = db.AddressCitizens.Where(a => a.CA_CitizenId == citizenId).Select(a => new { a.Address.District.Region.City.State.state_name, a.Address.District.Region.City.State.state_arabic_name }).FirstOrDefault();
            aaa aa = new aaa();
            aa.state_name = address.state_name;
            aa.state_arabic_name = address.state_arabic_name;
            aa.citizen_birthDate = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.citizen_birthDate.ToShortDateString();
            aa.SpecializationName = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.SpecializationName;
            aa.SpecializationNameArabic = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.SpecializationNameArabic;
            aa.UniversityName = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.UniversityName;
            aa.UniversityNameArabic = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.UniversityNameArabic;
            aa.citizen_first_name = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.citizen_first_name;
            aa.citizen_first_name_arabic = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.citizen_first_name_arabic;
            aa.citizen_second_name = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.citizen_second_name;
            aa.citizen_second_name_arabic = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.citizen_second_name_arabic;
            aa.citizen_third_name = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.citizen_third_name;
            aa.citizen_third_name_arabic = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.citizen_third_name_arabic;
            aa.citizen_fourth_name = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.citizen_fourth_name;
            aa.citizen_fourth_name_arabic = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.citizen_fourth_name_arabic;
            aa.FacultyName = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.FacultyName;
            aa.FacultyNameArabic = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.FacultyNameArabic;
            aa.Date = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.Date.Value.ToShortDateString();
            aa.GradeName = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.GradeName;
            aa.GradeNameArabic = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.GradeNameArabic;
            aa.UniversityLogo = NameAndBirthDateAndFacultyNameAndUniverNameAndGradeAndSpecilAndDate.UniversityLogo;
                
            return Json(aa, JsonRequestBehavior.AllowGet);
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            //your handling logic here
            filterContext.ExceptionHandled = true;
            filterContext.Result = RedirectToAction("errorpage", "home");
        }
    }
}
