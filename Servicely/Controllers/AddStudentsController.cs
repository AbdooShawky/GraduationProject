using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicely.Models;
namespace Servicely.Controllers
{
    public class AddStudentsController : BaseController
    {
        DbMasterEntities1 db = new DbMasterEntities1();
        // GET: AddStudents
        public ActionResult Index()
        {
            ViewBag.outt = db.EducationalOuts.Where(a => a.Is_Deleted != true).Count();
            ViewBag.inn = db.Students.Where(a => a.Is_Deleted != true).Count();

            return View();
        }
        public ActionResult Index1()
        {
            ViewBag.Uni = db.Students.Where(a => a.Is_Deleted != true && a.IsGraduatedU != true && a.IsGraduatedS == true).Count();
            ViewBag.Pri = db.Students.Where(a => a.Is_Deleted != true && a.IsGraduatedS != true && a.IsGraduatedP != true && a.IsGraduatedI != true && a.IsGraduatedU != true).Count();
            ViewBag.Int = db.Students.Where(a => a.Is_Deleted != true && a.IsGraduatedP == true && a.IsGraduatedI != true).Count();
            ViewBag.Sec = db.Students.Where(a => a.Is_Deleted != true && a.IsGraduatedI == true && a.IsGraduatedS != true).Count();

            return View();
        }

        public ActionResult Index2()
        {
            ViewBag.Uni = db.Students.Where(a => a.Is_Deleted != true && a.IsGraduatedU != true && a.IsGraduatedS == true).Count();
            ViewBag.Pri = db.Students.Where(a => a.Is_Deleted != true && a.IsGraduatedS!= true && a.IsGraduatedP!= true && a.IsGraduatedI!= true &&a.IsGraduatedU!= true ).Count();
            ViewBag.Int = db.Students.Where(a => a.Is_Deleted != true  && a.IsGraduatedP== true && a.IsGraduatedI!= true).Count();
            ViewBag.Sec = db.Students.Where(a => a.Is_Deleted != true && a.IsGraduatedI == true && a.IsGraduatedS!= true ).Count();


            return View();
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            //your handling logic here
            filterContext.ExceptionHandled = true;
            filterContext.Result = RedirectToAction("errorpage", "home");
        }

    }
}