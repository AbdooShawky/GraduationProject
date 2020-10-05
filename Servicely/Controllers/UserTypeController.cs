using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicely.Models;
using System.Data.Entity;
namespace Servicely.Controllers

{
    public class UserTypeController : BaseController
    {
        // GET: UserType
        DbMasterEntities1 db = new DbMasterEntities1();
        public ActionResult Index()
        {

            return View(db.User_Type.Where(a => a.user_type_isDeleted != true).ToList());
        }

        public ActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Create(User_Type u)
        {
            var data = db.User_Type.Where(a => a.user_type_name == u.user_type_name && a.user_type_isDeleted !=true).SingleOrDefault();
            if (data != null)
            {
                ViewBag.errMsg = Languages.Language.This_type_already_exist;
                return View(u);
            }
            db.User_Type.Add(u);
            db.SaveChanges();

            return RedirectToAction("Index");

        }
        public ActionResult Edit(int id)
        {

            User_Type User_Type = db.User_Type.Find(id);

            return View(User_Type);
        }

       
        [HttpPost]
        public ActionResult Edit(User_Type u)
        {


            var data = db.User_Type.Where(a => a.user_type_name == u.user_type_name && a.user_type_isDeleted != true && a.user_type_id !=u.user_type_id).SingleOrDefault();
            if (data != null)
            {
                ViewBag.errMsg = Languages.Language.This_type_already_exist;
                return View(u);
            }
            db.Entry(u).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");


        }

        // GET: Document_Type/Delete/5
        public ActionResult Delete(int id)
        {

           User_Type user_Type = db.User_Type.Find(id);

            return View(user_Type);
        }

        // POST: Document_Type/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            var old = db.User_Type.Find(id);
           old.user_type_isDeleted = true;

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