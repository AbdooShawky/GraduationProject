using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicely.Models;

namespace Servicely.Controllers
{
    public class UniversityStudentsRegisterationController : BaseController
    {
        DbMasterEntities1 db = new DbMasterEntities1();
        // GET: UniversityStudentsRegisteration
        public ActionResult Index()
        {
            ViewBag.UType = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeName");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.UType = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeNameArabic");
                }
            }
            
            var data = db.Students.Where(a => a.Is_Deleted != true).ToList();
            return View(data);
        }

        public ActionResult Create()
        {
            ViewBag.UType = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeName");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.UType = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeNameArabic");
                }
            }
            ViewBag.Id = new SelectList(db.Students.Where(a => a.Is_Deleted != true && a.IsGraduatedS == true && a.IsGraduatedU != true ).Select(a => new { a.Id, a.Citizen.citizen_national_id }), "Id", "citizen_national_id");

           var dd = db.Students.Where(a => a.Is_Deleted != true && a.IsGraduatedS == true && a.IsGraduatedU != true).Select(a => new { a.Id, a.Citizen.citizen_national_id }).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student student , int? FacultyId )
        {
            var FacultyGrade = db.Coordinations.Where(a => a.Is_Deleted != true && a.FacultyId == FacultyId && a.Year == DateTime.Now.Year).SingleOrDefault();
            var studentGradeFromSecondary = db.CertificateStudentM_M.Where(a=> a.Is_Deleted!= true && a.StudentId == student.Id ).FirstOrDefault().GradeId;
            var StudentGarde = db.Grades.Find(studentGradeFromSecondary).GradePrecentage;

            if(StudentGarde < FacultyGrade.Grade)
            {
                ViewBag.errMessage =Languages.Language.GradeValidationUniversity;

                ViewBag.UType = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeName");
                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.UType = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeNameArabic");
                    }
                }
                ViewBag.Id = new SelectList(db.Students.Where(a => a.Is_Deleted != true && a.IsGraduatedS == true).Select(a => new { a.Id, a.Citizen.citizen_national_id }), "Id", "citizen_national_id");

                return View(student);
            }
            var schoolSection = db.Faculties.Find(FacultyGrade.FacultyId).SchoolSection;
            var studendSchoolSection = db.Students.Find(student.Id).SchoolSection;

            if(schoolSection != studendSchoolSection)
            {
                ViewBag.errMessage = Languages.Language.SchoolSectionValidation;

                ViewBag.UType = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeName");
                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.UType = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeNameArabic");
                    }
                }
                ViewBag.Id = new SelectList(db.Students.Where(a => a.Is_Deleted != true && a.IsGraduatedS == true).Select(a => new { a.Id, a.Citizen.citizen_national_id }), "Id", "citizen_national_id");

                return View(student);
            }

          
            var old = db.Students.Find(student.Id);
            old.UniversityId = student.UniversityId;
            old.FacultyId = FacultyId;
            db.SaveChanges();
      

            return RedirectToAction("Index");
        }

        public ActionResult Edit (int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorPage", "home");
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return RedirectToAction("errorPage", "home");
            }

            ViewBag.UType = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeName");//,student.University.UniversityType.Id);
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.UType = new SelectList(db.UniversityTypes.Where(a => a.Is_Deleted != true), "Id", "UniversityTypeNameArabic");
                }
            }
            ViewBag.Id = new SelectList(db.Students.Where(a => a.Is_Deleted != true && a.IsGraduatedS == true && a.IsGraduatedU != true).Select(a => new { a.Id, a.Citizen.citizen_national_id }), "Id", "citizen_national_id", student.Id);



            return View(student);
        }

        //----------------- Ajax Call ----------------------

        public JsonResult GetAllStudentByFacultyId(int Id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var data = db.Students.Where(a => a.Is_Deleted != true && a.FacultyId == Id).Select(a => new
            {
                a.Citizen.citizen_first_name_arabic,
                a.Citizen.citizen_second_name_arabic,
                a.Citizen.citizen_third_name_arabic,
                a.Citizen.citizen_fourth_name_arabic,
                a.Citizen.citizen_first_name,
                a.Citizen.citizen_second_name,
                a.Citizen.citizen_third_name,
                a.Citizen.citizen_fourth_name,
                a.Citizen.citizen_national_id,
                a.Id,
                a.IsGraduatedU
                

            }) ;

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            //your handling logic here
            filterContext.ExceptionHandled = true;
            filterContext.Result = RedirectToAction("errorpage", "home");
        }
    }
}