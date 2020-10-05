using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicely.Models;
using System.Data.Entity;
namespace Servicely.Controllers
{
    public class Social_Status_TypeController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: Document_Type
        public ActionResult Index()
        {
            return View(db.Social_Status_Type.Where(a=> a.social_status_type_isDeleted!=true).ToList());
        }



        // GET: Document_Type/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Document_Type/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]

        public ActionResult Create(Social_Status_Type Social_Status_Type)
        {
            var data = db.Social_Status_Type.Where(a => a.social_status_type_name == Social_Status_Type.social_status_type_name && a.social_status_type_isDeleted !=true).SingleOrDefault();
            if (data != null)
            {
                ViewBag.errMsg = Languages.Language.This_type_already_exist;
                return View(Social_Status_Type);
            }

            db.Social_Status_Type.Add(Social_Status_Type);
            db.SaveChanges();
            return RedirectToAction("Index");



        }

        // GET: Document_Type/Edit/5
        public ActionResult Edit(int id)
        {

            Social_Status_Type Social_Status_Type = db.Social_Status_Type.Find(id);

            return View(Social_Status_Type);
        }

        // POST: Document_Type/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]

        public ActionResult Edit(Social_Status_Type Social_Status_Type)
        {

            var data = db.Social_Status_Type.Where(a => a.social_status_type_name == Social_Status_Type.social_status_type_name && a.social_status_type_isDeleted != true && a.social_status_type_id != Social_Status_Type.social_status_type_id).SingleOrDefault();
            if (data != null)
            {
                ViewBag.errMsg = Languages.Language.This_type_already_exist;
                return View(Social_Status_Type);
            }

            db.Entry(Social_Status_Type).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");


        }

        // GET: Document_Type/Delete/5
        public ActionResult Delete(int id)
        {

            Social_Status_Type Social_Status_Type = db.Social_Status_Type.Find(id);

            return View(Social_Status_Type);
        }

        // POST: Document_Type/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            var old = db.Social_Status_Type.Find(id);
            old.social_status_type_isDeleted = true;

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