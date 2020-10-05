using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicely.Models;
namespace Servicely.Controllers
{
    public class IntermediateRegistrationController : BaseController
    {
        DbMasterEntities1 db = new DbMasterEntities1();
        // GET: IntermediateRegistration
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
        public ActionResult Create()
        {
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
            ViewBag.studentsFinishedPrimary = new SelectList(db.Students.Where(a=>a.Is_Deleted!= true && a.IsGraduatedP == true && a.IsGraduatedI!= true && a.IsGraduatedS!= true&& a.IsGraduatedU!= true).Join(db.Citizens,a=>a.CitizenId,b=>b.citizen_id,(a,b)=>new {a,b }).Select(x=> new {x.a.Id , x.b.citizen_national_id }  ),"Id", "citizen_national_id");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                }
            }
            
            return View();
        }
        [HttpPost]
        public ActionResult Create(Student s,int? studentsFinishedPrimary)
        {

            var checkPrimary = db.Students.Find(studentsFinishedPrimary);
            if(checkPrimary.IsGraduatedP == true)
            {
                checkPrimary.SchoolId = s.SchoolId;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.errorMessage = Languages.Language.IntermidateErr;
                ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
                ViewBag.studentsFinishedPrimary = new SelectList(db.Students.Where(a => a.Is_Deleted != true && a.IsGraduatedP == true).Join(db.Citizens, a => a.CitizenId, b => b.citizen_id, (a, b) => new { a, b }).Select(x => new { x.a.Id, x.b.citizen_national_id }), "Id", "citizen_national_id");

                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                    }
                }
                return View(s);
            }
        }
        public ActionResult Edit(int? Id)
        {
            var data = db.Students.Find(Id);
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");//,data.School.District.Region.City.State.state_id);
            ViewBag.studentsFinishedPrimary = new SelectList(db.Students.Where(a => a.Is_Deleted != true && a.IsGraduatedP == true).Join(db.Citizens, a => a.CitizenId, b => b.citizen_id, (a, b) => new { a, b }).Select(x => new { x.a.Id, x.b.citizen_national_id }), "Id", "citizen_national_id" , data.Id);

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                }
            }

            return View();
           
        }

        [HttpPost]
        public ActionResult Edit(Student s,int? studentsFinishedPrimary)
        {

            var old = db.Students.Find(studentsFinishedPrimary);
            old.IsGraduatedI = false;
            old.SchoolId = s.SchoolId;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? Id)
        {
            var s = db.Students.Where(a=>a.Id == Id).Join(db.Citizens,a=>a.CitizenId,b=>b.citizen_id,(a,b)=>new { b}).SingleOrDefault();


            return View(s);
        }

        [ActionName("Delete"),HttpPost]
        public ActionResult ConfirmDelete(int? Id)
        {
            Student s = db.Students.Find(Id);
            s.IsGraduatedI = false;
            db.SaveChanges();

            return RedirectToAction("Index");
        }









        //-----------ajax call------------------------

        //intermdiate schools
        public JsonResult getSchoolByDistrictId(int Id)
        {


            var data = db.Schools.Where(a=>a.DistrictId == Id && a.Is_Deleted !=true).Join(db.SchoolPhasesM_M,a=>a.Id,b=>b.SchoolId,(a,b)=> new { a,b} ).Where(a=>a.b.PhaseId == 3).Select(a=> new { a.a.SchoolName ,a.a.SchoolNameArabic, a.a.Id});

            db.Configuration.ProxyCreationEnabled = false;
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getDataOfStudent(int Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.Students.Where(a => a.Is_Deleted != true && a.IsGraduatedP == true && a.Id == Id).Join(db.Citizens, a => a.CitizenId, b => b.citizen_id, (a, b) => new { b,a }).Select(a=> new {a.b.citizen_first_name, a.b.citizen_second_name, a.b.citizen_third_name, a.b.citizen_fourth_name, a.b.citizen_national_id, a.a.Id, a.b.citizen_first_name_arabic, a.b.citizen_second_name_arabic, a.b.citizen_third_name_arabic, a.b.citizen_fourth_name_arabic }).SingleOrDefault();

            return Json(data,JsonRequestBehavior.AllowGet);
        }

        public JsonResult getIntermediateStudentsBySchoolId(int Id)
        {

            var data = db.Students.Where(a => a.Is_Deleted != true && a.IsGraduatedP == true && a.IsGraduatedS != true && a.IsGraduatedI != true&& a.SchoolId == Id).Join(db.Citizens,a=>a.CitizenId,b=>b.citizen_id,(a,b)=>new { a,b}).Select(a => new { a.b.citizen_first_name, a.b.citizen_second_name, a.b.citizen_third_name, a.b.citizen_fourth_name, a.b.citizen_national_id, a.a.Id, a.b.citizen_first_name_arabic, a.b.citizen_second_name_arabic, a.b.citizen_third_name_arabic, a.b.citizen_fourth_name_arabic });
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