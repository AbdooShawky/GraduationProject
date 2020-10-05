using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicely.Models;
using System.Data.Entity;
using Servicely.Report;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
namespace Servicely.Controllers
{
    public class StateController : BaseController
    {
        DbMasterEntities1 db = new DbMasterEntities1();
        // GET: State
        public ActionResult Index()
        {

            return View(db.States.Where(a => a.state_isDeleted != true).ToList());
        }
        public ActionResult Export()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report/CrystalReportBirth.rpt")));
            rd.SetDataSource(db.States.Select(a => new
            {
                StateName = a.state_name
            }

            ).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream st = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            st.Seek(0, SeekOrigin.Begin);
            return File(st, "application/pdf", "A7laState.pdf");
        }

        //kjgjhg
        //sasd
        //// public JsonResult PersonList()
        // {
        //     db.Configuration.ProxyCreationEnabled = false;
        //     try
        //     {
        //         List<State> persons = db.State.ToList();
        //         return Json(new { Result = "OK", Records = persons });
        //     }
        //     catch (Exception ex)
        //     {
        //         return Json(new { Result = "ERROR", Message = ex.Message });
        //     }
        // }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(State s)
        {
            var data = db.States.Where(a => a.state_name == s.state_name && a.state_isDeleted != true).SingleOrDefault();
            var code = db.States.Where(a => a.state_code == s.state_code &&  a.state_isDeleted != true).SingleOrDefault();
            ViewBag.errMsg = null;
            if (data != null)
            {
                // ModelState.AddModelError("State", " State Name already exist");
                ViewBag.errMsg = s.state_name +Servicely.Languages.Language.State_already_exist;
                return View(s);
            }
            if (code != null)
            {
                ViewBag.errMsg = s.state_code + Servicely.Languages.Language.Code_already_exist;
                return View(s);
            }
            if (s.state_code.Length == 1)
            {
                string a = "0" + s.state_code.ToString();
                s.state_code = a;
            }
            Session["Create"] = Servicely.Languages.Language.SuccessFulCreate;
            db.States.Add(s);
            db.SaveChanges();
            return RedirectToAction("Index");



        }

        public JsonResult GetsuccessMessage()
        {
            string id = "";
            if (Session["Create"] != null)
            {
                id =Session["Create"].ToString();
                Session["Create"] = null;
            }
            
            return Json(id, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUpdateMessage()
        {
            string id = "";
            if (Session["Edit"] != null)
            {
                id = Session["Edit"].ToString();
                Session["Edit"] = null;
            }

            return Json(id, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDeletedMessage()
        {
            string id = "";
            if (Session["Delete"] != null)
            {
                id = Session["Delete"].ToString();
                Session["Delete"] = null;
            }

            return Json(id, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {

            State s = db.States.Find(id);

            return View(s);
        }

        public ActionResult Edit(State s)
        {
            var data = db.States.Where(a => a.state_name == s.state_name && a.state_isDeleted != true).SingleOrDefault();
            var dta = db.States.Where(a => a.state_id != s.state_id && a.state_isDeleted != true);

            if (s.state_code.Length == 1)
            {
                string a = "0" + s.state_code.ToString();
                s.state_code = a;
            }
            var code = db.States.Where(a => a.state_code == s.state_code).SingleOrDefault();


            foreach (var item in dta)
            {
                ViewBag.errMsg = null;
                if (item.state_name == s.state_name)
                {
                    ViewBag.errMsg = item.state_name + Servicely.Languages.Language.State_already_exist;
                    return View(s);
                }
                if (item.state_code == s.state_code)
                {
                    ViewBag.errMsg = item.state_code + Servicely.Languages.Language.Code_already_exist;
                    return View(s);
                }
            }


            if (data == null || data.state_name == s.state_name || code.state_code == s.state_code)
            {
                var old = db.States.Find(s.state_id);
                old.state_name = s.state_name;
                old.state_code = s.state_code;
                old.state_arabic_name = s.state_arabic_name;
                Session["Edit"] = Servicely.Languages.Language.EditedSuccessfully;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else

            {
                ViewBag.errMsg = Servicely.Languages.Language.State_already_exist;
                return View(s);
            }

        }

        public ActionResult Delete(int id)
        {

            State s = db.States.Find(id);

            return View(s);
        }

        // POST: Document_Type/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            var old = db.States.Find(id);
            old.state_isDeleted = true;
            Session["Delete"] = Servicely.Languages.Language.DeletedSuccessfully;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            //your handling logic here
            filterContext.ExceptionHandled = true;
            filterContext.Result = RedirectToAction("errorpage", "home");
        }
    }
}