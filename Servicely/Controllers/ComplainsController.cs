using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;
using Servicely.Models;
namespace Servicely.Controllers
{
  
    public class ComplainsController : BaseController
    {

        // GET: Complains
        DbMasterEntities1 db = new DbMasterEntities1();
        public ActionResult Index()
        {
            return View(db.Complains.Where(a=>a.Is_Deleted!=true && a.Date == DateTime.Now ));
        }
        public ActionResult IndexUser()
        {

            if (Session["citizenID"] != null)
            {
                int c = (int)Session["citizenID"];


                return View(db.Complains.Where(a => a.Is_Deleted != true && a.CitizenId == c&& a.Date.Value.Year == DateTime.Now.Year && a.Date.Value.Month == DateTime.Now.Month && a.Date.Value.Day == DateTime.Now.Day).ToList());
            }
            return RedirectToAction("errorPage", "home");

        }
        public JsonResult GetNationalId()
        {
            int c= 0;
            if(Session["citizenID"] != null)
            {
                c = (int)Session["citizenID"];
            }
            return Json(db.Citizens.Find(c).citizen_national_id,JsonRequestBehavior.AllowGet);
        }


        public ActionResult Create()
        {
            ViewBag.nationalId = new SelectList(db.Citizens,"Id","Citizen_national_Id");
            ViewBag.GovernementId = new SelectList(db.governement_body.Where(a => a.governement_isDeleted != true), "id", "governement_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.GovernementId = new SelectList(db.governement_body.Where(a => a.governement_isDeleted != true), "id", "governement_name_arabic");


                }
            }
                return View();
        }

        public ActionResult Create1()
        {
            ViewBag.nationalId = new SelectList(db.Citizens, "Id", "Citizen_national_Id");
            ViewBag.GovernementId = new SelectList(db.governement_body.Where(a => a.governement_isDeleted != true), "id", "governement_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.GovernementId = new SelectList(db.governement_body.Where(a => a.governement_isDeleted != true), "id", "governement_name_arabic");


                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult Create1(Complain c)
        {

            if (Session["citizenID"] != null)
            {
                c.CitizenId = (int)Session["citizenID"];
                c.Date = DateTime.Now;
                db.Complains.Add(c);
                db.SaveChanges();
                return RedirectToAction("IndexUser");

            }

            return RedirectToAction("errorPage", "home");


        }
        [HttpPost]
        public ActionResult Create(Complain c)
        {

            if(Session["citizenID"] != null)
            {
                c.CitizenId =(int) Session["citizenID"];
                c.Date = DateTime.Now;
                db.Complains.Add(c);
                db.SaveChanges();
                return RedirectToAction("IndexUser");

            }

            return RedirectToAction("errorPage", "home");


        }

        public ActionResult Edit(int Id)
        {
            ViewBag.GovernementId = new SelectList(db.governement_body.Where(a => a.governement_isDeleted != true), "id", "governement_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.GovernementId = new SelectList(db.governement_body.Where(a => a.governement_isDeleted != true), "id", "governement_name_arabic" );


                }
            }
            var data = db.Complains.Find(Id);
            if(data == null)
            {
                return RedirectToAction("errorPage", "home");
            }

            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(Complain c)
        {
            if (Session["citizenID"] != null)
            {
                var old = db.Complains.Find(c.Id);
                old.GovernementId = c.GovernementId;
                old.CitizenId = (int)Session["citizenID"];
                    old.ComplainText = c.ComplainText;
                old.CitizenNationalId = c.CitizenNationalId;
                db.SaveChanges();

                return RedirectToAction("IndexUser");
            }
            return RedirectToAction("errorPage", "home");
        }

        public ActionResult Delete(int Id)
        {
            var data = db.Complains.Find(Id);

            if(data == null)
            {
                return RedirectToAction("errorPage", "home");
            }
            return View(data);
        }

        [HttpPost,ActionName("Delete")]
        public ActionResult ConfirmDelete(int Id)
        {
            var old = db.Complains.Find(Id);
            old.Is_Deleted = true;
            db.SaveChanges();

            return RedirectToAction("IndexUser");
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            //your handling logic here
            filterContext.ExceptionHandled = true;
            filterContext.Result = RedirectToAction("errorpage", "home");
        }
    }
}