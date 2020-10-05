using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicely.Models;
using System.Data.Entity;
namespace Servicely.Controllers
{
    public class UserAccountController : BaseController
    {
        DbMasterEntities1 db = new DbMasterEntities1();
        // GET: UserAccount

        public ActionResult Index()
        {
            return View(db.Users);
        }

        public ActionResult Index1()
        {
            ViewBag.user_type_id = new SelectList(db.User_Type.Where(a => a.user_type_isDeleted != true), "user_type_id", "user_type_name");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.user_type_id = new SelectList(db.User_Type.Where(a => a.user_type_isDeleted != true), "user_type_id", "user_type_name_arabic");

                }
            }

            return View(db.Users.Where(a => a.user_isDeleted != true).ToList());


        }
        public JsonResult GetAllUsersByTypeName()
        {
            db.Configuration.ProxyCreationEnabled = false;
               if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    return Json(db.Users.Where(a => a.user_isDeleted != true).Join(db.User_Type, a => a.user_type_id, b => b.user_type_id, (a, b) => new { b.user_type_name_arabic, a.user_name, a.user_id }), JsonRequestBehavior.AllowGet);

                }
            }
            return Json(db.Users.Where(a=>a.user_isDeleted!=true).Join(db.User_Type, a => a.user_type_id, b => b.user_type_id, (a, b) => new { b.user_type_name, a.user_name , a.user_id }), JsonRequestBehavior.AllowGet);
         

        }

        public JsonResult GetAllUsersByTypeNameId(int tid)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    return Json(db.Users.Where(a => a.user_isDeleted != true && a.user_type_id == tid).Select(b => new { b.User_Type.user_type_name_arabic, b.user_name, b.user_id }), JsonRequestBehavior.AllowGet);

                }
            }
            return Json(db.Users.Where(a => a.user_isDeleted != true && a.user_type_id == tid).Select(b=> new { b.User_Type.user_type_name, b.user_name ,b.user_id}), JsonRequestBehavior.AllowGet);


        }

        [HttpGet]
        public ActionResult Creat()
        {
            ViewBag.user_type_id = new SelectList(db.User_Type.Where(a => a.user_type_isDeleted != true), "user_type_id", "user_type_name");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.user_type_id = new SelectList(db.User_Type.Where(a => a.user_type_isDeleted != true), "user_type_id", "user_type_name_arabic");

                }
            }
            return View();
        }
      
        
        [HttpPost]
        public ActionResult Creat(User u)
        {

            var data = db.Users.Where(a => a.user_name == u.user_name && a.user_isDeleted != true).SingleOrDefault();
            if (data != null)
            {

                ViewBag.user_type_id = new SelectList(db.User_Type.Where(a => a.user_type_isDeleted != true), "user_type_id", "user_type_name");

                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.city_state_id = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "user_type_name_arabic");
                       
                        return View(u);
                    }
                }

                ViewBag.errMsg = Servicely.Languages.Language.UserNameAlready;
                return View(u);
            }

           
            var dt = Encrypt.enc(u.user_password);
            u.user_password = dt;
            db.Users.Add(u);
            db.SaveChanges();
           return RedirectToAction("Index1");
        }
      
        
        
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.user_type_id = new SelectList(db.User_Type.Where(a => a.user_type_isDeleted != true), "user_type_id","user_type_name");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.user_type_id = new SelectList(db.User_Type.Where(a => a.user_type_isDeleted != true), "user_type_id", "user_type_name_arabic");

                }
            }

            return View();
        }


        [HttpPost]
        public ActionResult Create(User u)
        {
           


            var data = Encrypt.enc(u.user_password);
            u.user_password = data;
            db.Users.Add(u);
            db.SaveChanges();

            return RedirectToAction("Index1");
        }





        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(db.Users.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(User u)
        {


            var data = db.Users.Where(a => a.user_name == u.user_name && a.user_isDeleted != true && a.user_id !=u.user_id).SingleOrDefault();
            if (data != null)
            {

                ViewBag.user_type_id = new SelectList(db.User_Type.Where(a => a.user_type_isDeleted != true), "user_type_id", "user_type_name");

                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.city_state_id = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "user_type_name_arabic");

                        return View(u);
                    }
                }

                ViewBag.errMsg = Servicely.Languages.Language.UserNameAlready;
                return View(u);
            }

            db.Entry(u).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index1");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(db.Users.Find(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            var old = db.Users.Find(id);
            old.user_isDeleted = true;
            db.SaveChanges();
            return RedirectToAction("Index1");
        }


        protected override void OnException(ExceptionContext filterContext)
        {
            //your handling logic here
            filterContext.ExceptionHandled = true;
            filterContext.Result = RedirectToAction("errorpage", "home");
        }
    }
}