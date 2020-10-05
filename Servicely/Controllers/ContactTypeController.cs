using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicely.Models;
using System.Data.Entity;

namespace Servicely.Controllers
{
    public class ContactTypeController : BaseController
    {
        DbMasterEntities1 db = new DbMasterEntities1();
        // GET: ContactType
        public ActionResult Index()
        {
            return View(db.Contact_Type.Where(a => a.contact_type_isDeleted != true).ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Contact_Type c)
        {
            var data = db.Contact_Type.Where(a => a.contact_type_name == c.contact_type_name && a.contact_type_isDeleted != true).SingleOrDefault();
            if (data != null)
            {
                ViewBag.errMsg = Servicely.Languages.Language.Contact_type_already_exist;
                return View(c);
            }
            db.Contact_Type.Add(c);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(db.Contact_Type.Find(id));
        }
        [HttpPost]
        public ActionResult Edit(Contact_Type c)
        {

            var data = db.Contact_Type.Where(a => a.contact_type_name == c.contact_type_name && a.contact_type_id !=c.contact_type_id && a.contact_type_isDeleted != true).SingleOrDefault();
            if (data != null)
            {
                ViewBag.errMsg = Languages.Language.Contact_type_already_exist; ;
                return View(c);
            }

            db.Entry(c).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(db.Contact_Type.Find(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            var old = db.Contact_Type.Find(id);
            old.contact_type_isDeleted = true;
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