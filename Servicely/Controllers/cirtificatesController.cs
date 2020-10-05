using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicely.Models;
namespace Servicely.Controllers
{
    public class cirtificatesController : BaseController
    {
        DbMasterEntities1 db = new DbMasterEntities1();
        // GET: cirtificates
        public ActionResult birth_certificate()
        {
            
            ViewBag.Nid = new SelectList(db.Citizens.Where(a=>a.citizen_isDeleted!=true), "citizen_id", "citizen_national_id");


            return View();
        }
        public JsonResult getCitizenRecordByCitizenID(int cid)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return Json(db.Citizens.Where(a=>a.citizen_id == cid ) , JsonRequestBehavior.AllowGet);
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            //your handling logic here
            filterContext.ExceptionHandled = true;
            filterContext.Result = RedirectToAction("errorpage", "home");
        }
    }
}