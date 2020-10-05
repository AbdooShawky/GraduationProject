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
    public class Student2Controller : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: Students
        public ActionResult Index()
        {
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            ViewBag.Phase = new SelectList(db.PhasesOfSchooles.Where(a => a.Is_Deleted != true), "Id", "PhaseName");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                    ViewBag.Phase = new SelectList(db.PhasesOfSchooles.Where(a => a.Is_Deleted != true), "Id", "PhaseNameArabic");

                }
            }
            var students = db.Students.Include(s => s.Citizen).Include(s => s.School).Include(s => s.SpecializationOfFaculty).Include(s => s.University);
            return View(students.ToList());
        }

        //--------------- ajax call -------------------

        public JsonResult GetAllSchoolsBySchooTypeId(int typeId, int dis)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.SchoolPhasesM_M.Where(a => a.Is_Deleted != true && a.PhaseId == typeId && a.School.DistrictId == dis).Select(a => new { a.School.SchoolName, a.School.Id }).ToList();

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    var data1 = db.SchoolPhasesM_M.Where(a => a.Is_Deleted != true && a.PhaseId == typeId && a.School.DistrictId == dis).Select(a => new { a.School.SchoolNameArabic, a.School.Id });

                    return Json(data1, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllStudentBySchoolId(int school)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.Students.Where(a => a.SchoolId == school && a.Is_Deleted != true).Select(a => new
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

        // GET: Students/Create
        public ActionResult Create()
        {
            Nullable<System.DateTime> dd = DateTime.Now;
            var data = db.Citizens.Where(a => a.citizen_isDeleted != true);//Select(a=> new CustomCitizen {citizen_birthDate = a.citizen_birthDate , citizen_id = a.citizen_id , citizen_national_id = a.citizen_national_id}) ;
            List<Citizen> c = new List<Citizen>();

            foreach (var item in data)
            {
                if (dd.Value.Subtract(item.citizen_birthDate).Days >= 365 * 5)
                {
                    c.Add(item);
                }
            }
            //&& dd.Value.Subtract(a.citizen_birthDate).Days >= 365 * 5
            ViewBag.CitizenId = new SelectList(c, "citizen_id", "citizen_national_id");
            ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.Is_Deleted != true), "Id", "SchoolName");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {

                    ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.Is_Deleted != true), "Id", "SchoolNameArabic");


                }
            }
            //ViewBag.SpecializationFacultyId = new SelectList(db.SpecializationOfFaculties, "Id", "SpecializationName");
            //ViewBag.UniversityId = new SelectList(db.Universities, "Id", "UniversityName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                var data = db.Students.Where(a => a.SchoolId == student.SchoolId && a.CitizenId == student.CitizenId && a.Is_Deleted != true).FirstOrDefault();

                var data1 = db.Students.Where(a => a.CitizenId == student.CitizenId && a.Is_Deleted != true).SingleOrDefault();
                if (data1 != null)
                {
                    ViewBag.ErrMessage = "This student already in this school ";
                    Nullable<System.DateTime> dd = DateTime.Now;
                    var data2 = db.Citizens.Where(a => a.citizen_isDeleted != true);//Select(a=> new CustomCitizen {citizen_birthDate = a.citizen_birthDate , citizen_id = a.citizen_id , citizen_national_id = a.citizen_national_id}) ;

                    List<Citizen> c = new List<Citizen>();
                    foreach (var item in data2)
                    {
                        if (dd.Value.Subtract(item.citizen_birthDate).Days >= 365 * 6)
                        {

                            c.Add(item);
                        }
                    }
                    //&& dd.Value.Subtract(a.citizen_birthDate).Days >= 365 * 5
                    ViewBag.CitizenId = new SelectList(c, "citizen_id", "citizen_national_id");
                    ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.Is_Deleted != true), "Id", "SchoolName");
                    if (Session["lang"] != null)
                    {
                        if (Session["lang"].ToString().Equals("ar-EG"))
                        {

                            ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.Is_Deleted != true), "Id", "SchoolNameArabic");


                        }
                    }
                    return View(student);
                }

                if (data != null)
                {
                    ViewBag.ErrMessage = "This student already in this school ";
                    Nullable<System.DateTime> dd = DateTime.Now;
                    ViewBag.CitizenId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true && dd.Value.Subtract(a.citizen_birthDate).Days >= 365 * 5), "citizen_id", "citizen_national_id");
                    ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.Is_Deleted != true), "Id", "SchoolName");
                    if (Session["lang"] != null)
                    {
                        if (Session["lang"].ToString().Equals("ar-EG"))
                        {

                            ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.Is_Deleted != true), "Id", "SchoolNameArabic");


                        }
                    }
                    return View(student);
                }


                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Edit/5
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
            Nullable<System.DateTime> dd = DateTime.Now;
            var data = db.Citizens.Where(a => a.citizen_isDeleted != true);//Select(a=> new CustomCitizen {citizen_birthDate = a.citizen_birthDate , citizen_id = a.citizen_id , citizen_national_id = a.citizen_national_id}) ;

            List<Citizen> c = new List<Citizen>();
            foreach (var item in data)
            {
                if (dd.Value.Subtract(item.citizen_birthDate).Days >= 365 * 5)
                {

                    c.Add(item);
                }
            }
            //&& dd.Value.Subtract(a.citizen_birthDate).Days >= 365 * 5
            ViewBag.CitizenId = new SelectList(c, "citizen_id", "citizen_national_id");
            ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.Is_Deleted != true), "Id", "SchoolName");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {

                    ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.Is_Deleted != true), "Id", "SchoolNameArabic");


                }
            }
            return View(student);
        }

        // POST: Students/Edit/5

        [HttpPost]
        public ActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                var data = db.Students.Where(a => a.Is_Deleted != true && a.Id != student.Id);
                foreach (var item in data)
                {
                    if (item.CitizenId == student.CitizenId && item.SchoolId == student.SchoolId)
                    {
                        Nullable<System.DateTime> dd = DateTime.Now;
                        var data1 = db.Citizens.Where(a => a.citizen_isDeleted != true);//Select(a=> new CustomCitizen {citizen_birthDate = a.citizen_birthDate , citizen_id = a.citizen_id , citizen_national_id = a.citizen_national_id}) ;

                        List<Citizen> c = new List<Citizen>();
                        foreach (var item1 in data1)
                        {
                            if (dd.Value.Subtract(item1.citizen_birthDate).Days >= 365 * 5)
                            {

                                c.Add(item1);
                            }
                        }
                        //&& dd.Value.Subtract(a.citizen_birthDate).Days >= 365 * 5
                        ViewBag.CitizenId = new SelectList(c, "citizen_id", "citizen_national_id");
                        ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.Is_Deleted != true), "Id", "SchoolName");
                        if (Session["lang"] != null)
                        {
                            if (Session["lang"].ToString().Equals("ar-EG"))
                            {

                                ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.Is_Deleted != true), "Id", "SchoolNameArabic");


                            }
                        }
                        return View(student);

                    }
                }

                var old = db.Students.Find(student.Id);
                old.SchoolId = student.SchoolId;
                old.CitizenId = student.CitizenId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CitizenId = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", student.CitizenId);
            ViewBag.SchoolId = new SelectList(db.Schools, "Id", "SchoolName", student.SchoolId);
            ViewBag.SpecializationFacultyId = new SelectList(db.SpecializationOfFaculties, "Id", "SpecializationName", student.SpecializationFacultyId);
            ViewBag.UniversityId = new SelectList(db.Universities, "Id", "UniversityName", student.UniversityId);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
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
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            student.Is_Deleted = true;
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
