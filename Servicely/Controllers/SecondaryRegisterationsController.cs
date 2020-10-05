using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicely.Models;
namespace Servicely.Controllers
{
    public class SecondaryRegisterationsController : BaseController
    {
        DbMasterEntities1 db = new DbMasterEntities1();
        // GET: SecondaryRegisterations
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
            return View();
        }

        public JsonResult getSecondaryStudentsBySchoolId(int Id)
        {

            var data = db.Students.Where(a => a.Is_Deleted != true && a.IsGraduatedI == true && a.IsGraduatedS != true &&  a.SchoolId == Id).Join(db.Citizens.Where(a=> a.citizen_isDeleted!= true), a => a.CitizenId, b => b.citizen_id, (a, b) => new { a, b }).Select(a => new { a.b.citizen_first_name, a.b.citizen_second_name, a.b.citizen_third_name, a.b.citizen_fourth_name, a.b.citizen_national_id, a.a.Id, a.b.citizen_first_name_arabic, a.b.citizen_second_name_arabic, a.b.citizen_third_name_arabic, a.b.citizen_fourth_name_arabic });
            db.Configuration.ProxyCreationEnabled = false;
            return Json(data, JsonRequestBehavior.AllowGet);
        }





        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                }
            }
            ViewBag.Id = new SelectList(db.Students.Where(a=> a.Is_Deleted!= true&& a.IsGraduatedI == true && a.IsGraduatedP== true && a.IsGraduatedS!= true&& a.IsGraduatedU!= true).Select(a=> new { a.Id,a.Citizen.citizen_national_id}),"Id", "citizen_national_id");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student student , string inlineDefaultRadiosExample)
        {
            var data = db.Students.Where(a=> a.Is_Deleted!= true && a.Id == student.Id && a.IsGraduatedI!= true).SingleOrDefault();
            if(data != null)
            {
                ViewBag.ErrMessage = Languages.Language.studentschool;
                ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                    }
                }
                ViewBag.Id = new SelectList(db.Students.Where(a => a.Is_Deleted != true && a.IsGraduatedI == true && a.IsGraduatedP == true && a.IsGraduatedS != true && a.IsGraduatedU != true).Select(a => new { a.Id, a.Citizen.citizen_national_id }), "Id", "citizen_national_id");
                return View(student);
            }
           
            

            var old = db.Students.Find(student.Id);
            old.SchoolId = student.SchoolId;
            old.SchoolSection = inlineDefaultRadiosExample;
            db.SaveChanges();


            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return RedirectToAction("errorpage", "home");
            }

            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                }
            }
            ViewBag.Id = new SelectList(db.Students.Where(a => a.Is_Deleted != true && a.IsGraduatedI == true && a.IsGraduatedP == true && a.IsGraduatedS != true && a.IsGraduatedU != true).Select(a => new { a.Id, a.Citizen.citizen_national_id }), "Id", "citizen_national_id");
        
            return View(student);
        }
        [HttpPost]
        public ActionResult Edit(Student student , string inlineDefaultRadiosExample)
        {
            var data = db.Students.Where(a => a.Is_Deleted != true && a.Id == student.Id && a.IsGraduatedI != true).SingleOrDefault();
            if (data != null)
            {
                ViewBag.ErrMessage = Languages.Language.studentschool;
                ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                    }
                }
                ViewBag.Id = new SelectList(db.Students.Where(a => a.Is_Deleted != true && a.IsGraduatedI == true && a.IsGraduatedP == true && a.IsGraduatedS != true && a.IsGraduatedU != true).Select(a => new { a.Id, a.Citizen.citizen_national_id }), "Id", "citizen_national_id");
                return View(student);
            }



            var old = db.Students.Find(student.Id);
            old.SchoolId = student.SchoolId;
            old.SchoolSection = inlineDefaultRadiosExample;
            db.SaveChanges();


            return RedirectToAction("Index");
        }

        [HttpGet]
        //public ActionResult Delete (int? id)
        //{
        //    if (id == null)
        //    {
        //        return RedirectToAction("errorpage", "home");
        //    }
        //    Student student = db.Students.Find(id);
        //    if (student == null)
        //    {
        //        return RedirectToAction("errorpage", "home");
        //    }
        //    return View(student);
        //}
        //[HttpPost, ActionName("Delete")]
        //public ActionResult ConfirmDelete(Student student)
        //{
        //    var data = db.Students.Find(student.Id);
        //    data.isgrad

        //}
        //------------ Ajax Call ---------------


        public JsonResult GetAllNameByStudentID(int Std)
        {
            int? ci = db.Students.Find(Std).CitizenId;

            db.Configuration.ProxyCreationEnabled = false;
            var data = db.Citizens.Where(a => a.citizen_isDeleted != true && a.citizen_id == ci).Select(a => new { a.citizen_first_name, a.citizen_second_name, a.citizen_third_name, a.citizen_fourth_name }).SingleOrDefault();

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    var data1 = db.Citizens.Where(a => a.citizen_isDeleted != true && a.citizen_id == ci).Select(a => new {  a.citizen_first_name_arabic, a.citizen_second_name_arabic, a.citizen_third_name_arabic, a.citizen_fourth_name_arabic }).SingleOrDefault();

                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllHighSchoolsByDistrictId(int dis)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.SchoolPhasesM_M.Where(a => a.Is_Deleted != true && a.School.DistrictId == dis && a.PhaseId == 1).Select(a=> new { a.School.Id, a.School.SchoolName , a.School.SchoolNameArabic });
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllStudentinHighPhaseBySchoolId(int Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.Students.Where(a => a.Is_Deleted != true && a.IsGraduatedP == true && a.IsGraduatedI == true && a.IsGraduatedS != true && a.IsGraduatedU != true).Select(a => new { 
            
                a.Citizen.citizen_national_id,
                a.Citizen.citizen_first_name,
                a.Citizen.citizen_second_name,
                a.Citizen.citizen_third_name,
                a.Citizen.citizen_fourth_name,
                a.Citizen.citizen_first_name_arabic,
                a.Citizen.citizen_second_name_arabic,
                a.Citizen.citizen_third_name_arabic,
                a.Citizen.citizen_fourth_name_arabic,
                a.Id

            }).ToList();
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