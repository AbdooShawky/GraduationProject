﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicely.Models;

namespace Servicely.Controllers
{
    public class UniversityStudentsForeignRegesterationsController : Controller
    {
        DbMasterEntities1 db = new DbMasterEntities1();
        // GET: UniversityStudentsForeignRegesterations
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            ViewBag.CitizenId = new SelectList(db.EducationalOuts.Where(a => a.IsGraduatedS == true && a.Is_Deleted != true).Join(db.Citizens.Where(a => a.citizen_isDeleted != true), a => a.CitizenId, b => b.citizen_id, (a, b) => new { b.citizen_national_id, b.citizen_id }), "citizen_id", "citizen_national_id");
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
        [HttpPost]
        public ActionResult Create(Student s)
        {

            var data = db.Students.Where(a => a.Is_Deleted != true && a.CitizenId == s.CitizenId).SingleOrDefault();
            if (data != null)
            {

                ViewBag.ErrMessage = Languages.Language.ForeignStudent;
                ViewBag.CitizenId = new SelectList(db.EducationalOuts.Where(a => a.IsGraduatedS == true && a.Is_Deleted != true).Join(db.Citizens.Where(a => a.citizen_isDeleted != true), a => a.CitizenId, b => b.citizen_id, (a, b) => new { b.citizen_national_id, b.citizen_id }), "citizen_id", "citizen_national_id");
                ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                    }
                }
                return View(s);

            }

            s.IsGraduatedS = true;
            db.Students.Add(s);
            db.SaveChanges();

            return RedirectToAction("Index", "UniversityStudentsRegisteration");
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            //your handling logic here
            filterContext.ExceptionHandled = true;
            filterContext.Result = RedirectToAction("errorpage", "home");
        }
    }
}