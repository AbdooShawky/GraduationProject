using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicely.Models;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace Servicely.Controllers
{
    public class loginController : BaseController
    {

        DbMasterEntities1 db = new DbMasterEntities1();
        Encrypt e = new Encrypt();
        // GET: login
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.user_type_id = new SelectList(db.User_Type, "user_type_id", "user_type_name");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {

                    ViewBag.user_type_id = new SelectList(db.User_Type, "user_type_id", "user_type_name_arabic");

                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult Index(User b)
        {
            var dataa = Encrypt.enc(b.user_password);
            dataa.ToString();
            var data = db.Users.Where(a => a.user_name == b.user_name && a.user_password == dataa).SingleOrDefault();
            if (b.user_type_id == 2)
            {
                var data1 = db.LoginCitizens.Where(a => a.Login_CitizenNId == b.user_name && a.Login_Password == dataa).SingleOrDefault();

                if (data1 != null)
                {
                    var citizenPhoto = db.Photos.Where(a => a.Photo_citizen_id == data1.Login_CitizenId && a.Photo_isCurrent == true).SingleOrDefault();
                    if(citizenPhoto != null)
                    {
                        Session["PhotoName"] = citizenPhoto.Photo_Url.ToString();
                    }
                    
                    var data3 = db.Citizens.Where(a => a.citizen_national_id == b.user_name).Select(a => new { a.citizen_first_name, a.citizen_second_name }).SingleOrDefault();
                    Session["citizenID"] = db.Citizens.Where(a => a.citizen_national_id == b.user_name).Select(a => a.citizen_id).SingleOrDefault();
                    Session["citizenFirstName"] = data3.citizen_first_name + " " + data3.citizen_second_name;
                    Session["Layout"] = "~/Views/Shared/userlayout.cshtml" ;
                    return RedirectToAction("Indexuser", "home");
                }

            }
            User_Type u = new User_Type();
            // var dd = db.Users.Where(a=>a.user_name == b.user_name && a.user_password == dataa).Join(db.User_Type,a=>a.user_type_id,x=>x.user_type_id,(a,x)=>new {x.user_type_id }).SingleOrDefault();
            if (b.user_type_id == 1)
            {


                if (data != null)
                {
                    var citizenPhoto = db.Photos.Where(a => a.Photo_citizen_id == data.user_citizen_id && a.Photo_isCurrent == true).SingleOrDefault();
                   
                    if(citizenPhoto != null )
                    {
                        Session["PhotoName"] = citizenPhoto.Photo_Url.ToString();
                    }


                    // Session["citizenID"] = db.Citizens.Find(data.user_citizen_id).citizen_id;
                    Session["Layout"] = "~/Views/Shared/_Layout.cshtml";
                    Session["username"] = data.user_name;
                    return RedirectToAction("Index", "home");
                }

            }

            if (b.user_type_id == 3)
            {


                if (data != null)
                {
                    var citizenPhoto = db.Photos.Where(a => a.Photo_citizen_id == data.user_citizen_id && a.Photo_isCurrent == true).SingleOrDefault();

                    if (citizenPhoto != null)
                    {
                        Session["PhotoName"] = citizenPhoto.Photo_Url.ToString();
                    }


                    // Session["citizenID"] = db.Citizens.Find(data.user_citizen_id).citizen_id;
                    Session["Layout"] = "~/Views/Shared/HealthCare_LayOut.cshtml";
                    Session["username"] = data.user_name;
                    return RedirectToAction("IndexH", "home");
                }

            }
            if (b.user_type_id == 4)
            {


                if (data != null)
                {
                    var citizenPhoto = db.Photos.Where(a => a.Photo_citizen_id == data.user_citizen_id && a.Photo_isCurrent == true).SingleOrDefault();

                    if (citizenPhoto != null)
                    {
                        Session["PhotoName"] = citizenPhoto.Photo_Url.ToString();
                    }


                    // Session["citizenID"] = db.Citizens.Find(data.user_citizen_id).citizen_id;
                    Session["Layout"] = "~/Views/Shared/Interior_LayOut.cshtml";
                    Session["username"] = data.user_name;
                    return RedirectToAction("IndexI", "home");
                }

            }
            if (b.user_type_id == 5)
            {


                if (data != null)
                {
                    var citizenPhoto = db.Photos.Where(a => a.Photo_citizen_id == data.user_citizen_id && a.Photo_isCurrent == true).SingleOrDefault();

                    if (citizenPhoto != null)
                    {
                        Session["PhotoName"] = citizenPhoto.Photo_Url.ToString();
                    }


                    // Session["citizenID"] = db.Citizens.Find(data.user_citizen_id).citizen_id;
                    Session["Layout"] = "~/Views/Shared/Educational_LayOut.cshtml";
                    Session["username"] = data.user_name;
                    return RedirectToAction("IndexE", "home");
                }

            }


            ViewBag.user_type_id = new SelectList(db.User_Type, "user_type_id", "user_type_name");


            ViewBag.msg = Languages.Language.login_MSG;
            return View();
        }

        public JsonResult GetCitizenId()
        {

            db.Configuration.ProxyCreationEnabled = false;
            int Id = (int)Session["citizenID"];
            return Json(Id, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RecoverPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RecoverPassword(string email)
        {
            //string Title = "Hi there to reset your password please click on this link \n  https://192.168.1.34:88/Login/ResetPassword";
            //SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            //smtp.EnableSsl = true;
            //smtp.UseDefaultCredentials = false;
            //smtp.Credentials = new NetworkCredential("CSCE4502@gmail.com", "csce4502f16");
            //smtp.Send("CSCE4502@gmail.com", email, "Recover your Password", Title);
            Session["succmsg"] = Languages.Language.succmsg;
            return View();
        }
        public JsonResult GetMessage()
        {
            string msg = "";
            if(Session["succmsg"] != null)
            {
                msg = Session["succmsg"].ToString();
                Session["succmsg"] = null;
            }
            return Json(msg,JsonRequestBehavior.AllowGet);
        }
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword( ForgotPassword p)
        {

            return View();
        }
        public ActionResult Index1()
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