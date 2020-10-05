using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicely.Models;


namespace Servicely.Controllers
{
    public class EmployeeController : Controller
    {
        DbMasterEntities1 db = new DbMasterEntities1();
        // GET: Employee
        public ActionResult EmployeeReport()
        {


            ViewBag.document_documentType_id = new SelectList(db.Document_Type, "document_type_id", "document_type_name");

     
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");
                    ViewBag.document_documentType_id = new SelectList(db.Document_Type, "document_type_id", "document_type_name_arabic");

                }
            }


            ViewBag.citizen_father_id = new SelectList(db.Citizens.Where(a => a.citizen_gender == "Male" && a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
            ViewBag.citizen_mother_id = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true && a.citizen_gender == "Female"), "citizen_id", "citizen_national_id");

            return View();
        }
        public ActionResult index()
        {
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