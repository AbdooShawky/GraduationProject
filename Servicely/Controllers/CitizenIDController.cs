using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Servicely.Models;
using System.IO;

namespace Servicely.Controllers
{
    public class CitizenIDController : BaseController
    {
        DbMasterEntities1 db = new DbMasterEntities1();


        public ActionResult ChooseUserIndex()
        {
            int cid = 0;
            ViewBag.NId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
            if(Session["citizenID"] != null)
            {
                cid = (int)Session["citizenID"];
              
            }
            var data = db.Photos.Where(a => a.Photo_citizen_id == 5);
            return View(data.ToList());

        }

        

        // GET: CitizenID
        public ActionResult Index(int? id)
        {

            ViewBag.NId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");

            return View();

        }
        [HttpPost]
        public ActionResult Index(int NId)
        {
            Session["NID"] = NId;
          

            return  RedirectToAction("Index1");

        }

        public ActionResult Index1()
        {

            ViewBag.NId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");

            return View();

        }

       public JsonResult GetCitizenInfoCardBack()
        {
            int cid =0;
            db.Configuration.ProxyCreationEnabled = false;
            if (Session["NID"] != null)
                cid = (int)Session["NID"];
            var data = db.Citizens.Where(a=> a.citizen_id == cid).Join(db.LoginCitizens , a=>a.citizen_id, b=> b.Login_CitizenId, (a,b)=> new { a,b}).Select(a=> new { a.a.citizen_national_id ,a.a.citizen_relegion , a.a.citizen_gender ,a.a.citizen_job_title, a.b.Login_QRcode});

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCitizenQRCode()
        {
            int cid = 0;
            db.Configuration.ProxyCreationEnabled = false;
            if (Session["NID"] != null)
                cid = (int)Session["NID"];
            var data = db.LoginCitizens.Where(a=> a.Login_CitizenId == cid).SingleOrDefault();
            string aa = data.Login_QRcode.ToString();
            return Json(aa, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCitizenQRCode1(int cid)
        {
            
            db.Configuration.ProxyCreationEnabled = false;
            string aa = "";
            var data = db.LoginCitizens.Where(a => a.Login_CitizenId == cid).SingleOrDefault();
            if(data != null)
             aa= data.Login_QRcode.ToString();
            return Json(aa, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCitizenSocialStatus()
        {
            db.Configuration.ProxyCreationEnabled = false;
            int cid =0;
            if(Session["NID"] != null)
            {
                cid = (int)Session["NID"];
            }
            
            var data = db.Citizens.Find(cid);
           
                if(data.citizen_gender == "Male")
                {

                    var socialstatus = db.Social_status.Where(a => a.socialStatus_citizenId_Husband == data.citizen_id && a.social_status_isStill == true).FirstOrDefault();
                    if( socialstatus != null)
                    {
                        return Json(Languages.Language.Married, JsonRequestBehavior.AllowGet);
                    }

                    return Json(Languages.Language.Unmarried, JsonRequestBehavior.AllowGet); 
                }

            var socialstatusWife = db.Social_status.Where(a => a.socialStatus_citizenId_Wife == data.citizen_id && a.social_status_isStill == true).FirstOrDefault();
            if (socialstatusWife != null)
            {
                int husbandid = socialstatusWife.socialStatus_citizenId_Husband;
                TempData["husbandId"] = husbandid;
                return Json(Languages.Language.MarriedW, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var socialstatusWife1 = db.Social_status.Where(a => a.socialStatus_citizenId_Wife == data.citizen_id ).FirstOrDefault();
                if(socialstatusWife1 != null)
                {
                    return Json(Languages.Language.divorced, JsonRequestBehavior.AllowGet);

                }
            }

            return Json(Languages.Language.UnMarriedW, JsonRequestBehavior.AllowGet);

            
        }

        public JsonResult GetHusbandNameByHisWife()
        {
            db.Configuration.ProxyCreationEnabled = false;
            int hus = 0;
            if (TempData["husbandId"] != null)
            {
                hus = (int)TempData["husbandId"];
            }
            
            var data = db.Citizens.Find(hus);
            if (data != null)
            {
                if(Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        return Json(data.citizen_first_name_arabic + " " + data.citizen_second_name_arabic + " " + data.citizen_third_name_arabic + " " + data.citizen_fourth_name_arabic, JsonRequestBehavior.AllowGet);


                    }

                }
               
                return Json(data.citizen_first_name + " " + data.citizen_second_name + " " + data.citizen_third_name + " " + data.citizen_fourth_name, JsonRequestBehavior.AllowGet);

            }
            return Json("", JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetCurrentPhotoByCitizenId(int cid)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.Photos.Where(a => a.Photo_citizen_id == cid && a.Photo_isCurrent == true).SingleOrDefault().Photo_Url;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllInfoAboutCitizenByCitizenId (int Cid)
        {
            db.Configuration.ProxyCreationEnabled = false ;

            var CitizenInfo = db.Citizens.Where(a=> a.citizen_id == Cid).Join(db.AddressCitizens , a=> a.citizen_id , b=> b.CA_CitizenId , (a,b)=> new { a,b}).Join(db.Addresses , a=> a.b.CA_AddressId , b=>b.address_id , (x,y)=> new { x,y}).Where(a=> a.y.address_isCurrent == true).Select(w=> new { w.y.District.Region.region_name, w.y.District.Region.City.city_name , w.y.District.Region.City.State.state_name , w.y.address_street , w.x.a.citizen_first_name , w.x.a.citizen_national_id});
            
            if(Session["lang"] != null)
                {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    var CitizenInfo1 = db.Citizens.Where(a => a.citizen_id == Cid).Join(db.AddressCitizens, a => a.citizen_id, b => b.CA_CitizenId, (a, b) => new { a, b }).Join(db.Addresses, a => a.b.CA_AddressId, b => b.address_id, (x, y) => new { x, y }).Where(a => a.y.address_isCurrent == true).Select(w => new { w.y.District.Region.region_arabic_name, w.y.District.Region.City.city_arabic_name, w.y.District.Region.City.State.state_arabic_name, w.y.address_street_arabic, w.x.a.citizen_first_name_arabic, w.x.a.citizen_national_id });
                                return Json(CitizenInfo1, JsonRequestBehavior.AllowGet);


                }
            }

            return Json(CitizenInfo, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetFatherNameByCitizenId(int Cid)
        {
            db.Configuration.ProxyCreationEnabled = false;

            int? CitizenInfo = db.Citizens.Find(Cid).citizen_father_id;
            var fatherInfo = db.Citizens.Find(CitizenInfo);
            if (fatherInfo == null)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            string FatherName;
            if (Session["lang"] != null)
                {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {

                     FatherName = fatherInfo.citizen_first_name_arabic + " " + fatherInfo.citizen_second_name_arabic + " " + fatherInfo.citizen_third_name_arabic;

                    return Json(FatherName, JsonRequestBehavior.AllowGet);
                }
                else
                {
                     FatherName = fatherInfo.citizen_first_name + " " + fatherInfo.citizen_second_name + " " + fatherInfo.citizen_third_name;

                    return Json(FatherName, JsonRequestBehavior.AllowGet);
                }
            }
            FatherName = fatherInfo.citizen_first_name + " " + fatherInfo.citizen_second_name + " " + fatherInfo.citizen_third_name;

            return Json(FatherName, JsonRequestBehavior.AllowGet);

        }


        public ActionResult Create()
        {
           
           // ViewBag.citizen_citizen_id = new SelectList(c, "citizen_id", "citizen_national_id"+" " + "citizen_first_name"+" "+ "citizen_second_name" + " "+ "citizen_third_name"+" "+ "citizen_fourth_name");
            return View();
        }
        [HttpPost]
        public ActionResult Create(  HttpPostedFileBase file , Citizen_id citizen_Id)
        {
            citizen_Id.start_date = DateTime.Now;
            citizen_Id.end_date = DateTime.Now.AddDays(365 * 7);

            var fff = db.Citizens.Find(citizen_Id.citizen_citizen_id);

            string filename = fff.citizen_national_id + "_" + fff.citizen_first_name + Path.GetExtension(file.FileName);
            string filePath = Server.MapPath("~/CitizenPhotos/" + filename);
            string filePathName = Path.Combine(filename, filePath);
            file.SaveAs(filePathName);


            Photo photo = new Photo();
            photo.Photo_uploadDate = DateTime.Now.ToShortDateString();
            photo.Photo_isCurrent = true;
            photo.Photo_citizen_id = citizen_Id.citizen_citizen_id;
            photo.Photo_Url = filename;

            
            db.Photos.Add(photo);
            db.Citizen_id.Add(citizen_Id);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult GetCiticens()
        {
            db.Configuration.ProxyCreationEnabled = false;
            Nullable<System.DateTime> dd = DateTime.Now;
            var data = db.Citizens.Where(a => a.citizen_isDeleted != true).ToList();//Select(a=> new CustomCitizen {citizen_birthDate = a.citizen_birthDate , citizen_id = a.citizen_id , citizen_national_id = a.citizen_national_id}) ;

            List<Citizen> c = new List<Citizen>();
            foreach (var item in data)
            {
                if (dd.Value.Subtract(item.citizen_birthDate).Days >= 356 * 16)
                {
                    c.Add(item);
                }
            }
           var data1 = c.Select(a => new CustomCitizen { 
            
            citizen_id = a.citizen_id,
            citizen_national_id = a.citizen_national_id,
            citizen_first_name =a.citizen_first_name,
            citizen_second_name=a.citizen_second_name,
            citizen_third_name=a.citizen_third_name,
            citizen_fourth_name= a.citizen_fourth_name,
            citizen_first_name_arabic= a.citizen_first_name_arabic,
            citizen_second_name_arabic= a.citizen_second_name_arabic,
            citizen_third_name_arabic=a.citizen_third_name_arabic,
            citizen_fourth_name_arabic=a.citizen_fourth_name_arabic
            
            });
            return Json(data1, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetCitizenCarLicense(int Id)
        {
            var data = db.CarLicences.Where(a => a.CitizenId == Id).FirstOrDefault();

            if( data != null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);

        }





        protected override void OnException(ExceptionContext filterContext)
        {
            //your handling logic here
            filterContext.ExceptionHandled = true;
            filterContext.Result = RedirectToAction("errorpage", "home");
        }
    }






} 