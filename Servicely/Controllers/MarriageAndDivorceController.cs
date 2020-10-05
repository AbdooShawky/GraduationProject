using CrystalDecisions.CrystalReports.Engine;
using Servicely.Models;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Servicely.Controllers
{
    public class MarriageAndDivorceController : BaseController
    {
        DbMasterEntities1 db = new DbMasterEntities1();
        // GET: MarriageAndDivorce
        public ActionResult Index()
        {
            ViewBag.citi = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true && a.citizen_gender=="Male").Join(db.Social_status,a=>a.citizen_id,b=>b.socialStatus_citizenId_Husband , (a,b)=> new { a.citizen_id,a.citizen_national_id}), "citizen_id", "citizen_national_id");
            ViewBag.tybe = new SelectList(db.Social_Status_Type, "social_status_type_id", "social_status_type_name");
            return View();
        }

        public JsonResult MarriageHusband()
        {
            db.Configuration.ProxyCreationEnabled = false;
            int h = Convert.ToInt32(Session["hid"]);
            int w = Convert.ToInt32(Session["wid"]);

            var hus = db.Citizens.Find(h);
            var wi = db.Citizens.Find(w);

            var ww = db.Citizens.Where(a => a.citizen_id == h).Select(a=> new CustomCitizenDataBirthDocument { 
            
            
               citizen_first_name= a.citizen_first_name,
                citizen_second_name= a.citizen_second_name,
                citizen_third_name= a.citizen_third_name,
                citizen_fourth_name= a.citizen_fourth_name,
               citizen_first_name_arabic= a.citizen_first_name_arabic,
               citizen_second_name_arabic= a.citizen_second_name_arabic,
                citizen_third_name_arabic=a.citizen_third_name_arabic,
               citizen_fourth_name_arabic= a.citizen_fourth_name_arabic,
               citizen_gender= a.citizen_gender,
                citizen_birthDate= a.citizen_birthDate.ToString(),
               citizen_relegion= a.citizen_relegion

            });

            return Json(ww, JsonRequestBehavior.AllowGet);

        }
        public JsonResult MarriageWife()
        {
            db.Configuration.ProxyCreationEnabled = false;
            int h = Convert.ToInt32(Session["hid"]);
            int w = Convert.ToInt32(Session["wid"]);

            var hus = db.Citizens.Find(h);
            var wi = db.Citizens.Find(w);

            var ww = db.Citizens.Where(a => a.citizen_id == w).Select(a => new CustomCitizenDataBirthDocument{


                citizen_first_name = a.citizen_first_name,
                citizen_second_name = a.citizen_second_name,
                citizen_third_name = a.citizen_third_name,
                citizen_fourth_name = a.citizen_fourth_name,
                citizen_first_name_arabic = a.citizen_first_name_arabic,
                citizen_second_name_arabic = a.citizen_second_name_arabic,
                citizen_third_name_arabic = a.citizen_third_name_arabic,
                citizen_fourth_name_arabic = a.citizen_fourth_name_arabic,
                citizen_gender = a.citizen_gender,
                citizen_birthDate = a.citizen_birthDate.ToString(),
                citizen_relegion = a.citizen_relegion

            });

            return Json(ww, JsonRequestBehavior.AllowGet);

        }
        public JsonResult SocialStatusmarriage()
        {
            db.Configuration.ProxyCreationEnabled = false;
            int h = Convert.ToInt32(Session["hid"]);
            int w = Convert.ToInt32(Session["wid"]);

            var hus = db.Citizens.Find(h);
            var wi = db.Citizens.Find(w);
            var data = db.Social_status.Where(a => a.socialStatus_citizenId_Husband == h && a.socialStatus_citizenId_Wife == w && a.social_status_isStill == true && a.socialStatus_TypeId == 1).SingleOrDefault().social_status_from;


            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public JsonResult SocialStatusDivorce()
        {
            db.Configuration.ProxyCreationEnabled = false;
            int h = Convert.ToInt32(Session["hid"]);
            int w = Convert.ToInt32(Session["wid"]);

            var hus = db.Citizens.Find(h);
            var wi = db.Citizens.Find(w);
            var data = db.Social_status.Where(a => a.socialStatus_citizenId_Husband == h && a.socialStatus_citizenId_Wife == w && a.social_status_isStill == true && a.socialStatus_TypeId == 2).SingleOrDefault().social_status_from;


            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpPost, ActionName("Index")]
        public ActionResult ConfirmIndex(int type)
        {

            int h = Convert.ToInt32(Session["hid"]);
            int w = Convert.ToInt32(Session["wid"]);

            var hus = db.Citizens.Find(h);
            var wi = db.Citizens.Find(w);

            string hName = hus.citizen_first_name + " " + hus.citizen_second_name + " " + hus.citizen_third_name + " " + hus.citizen_fourth_name;
            string wName = wi.citizen_first_name + " " + wi.citizen_second_name + " " + wi.citizen_third_name + " " + wi.citizen_fourth_name;

            var data = db.Social_status.Where(a => a.socialStatus_citizenId_Wife == w && a.socialStatus_citizenId_Husband == h).SingleOrDefault();

           if( type == 1)
            {
                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Report/Mirrage.rpt")));
                rd.SetDataSource(db.Citizens.Where(a=>a.citizen_id == h || a.citizen_id==w).Select(a => new
                {
                    WifeName = wName,
                    HusbandName = hName,
                    MirrageDate = data.social_status_from,
                    wifeBirth = wi.citizen_birthDate,
                    HusbandBirth = hus.citizen_birthDate,
                    Hrelegion = hus.citizen_relegion,
                    Wrelegion = wi.citizen_relegion

                }

                ).ToList()) ;
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                Stream st = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                st.Seek(0, SeekOrigin.Begin);
                ViewBag.citi = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true && a.citizen_gender == "M").Join(db.Social_status, a => a.citizen_id, b => b.socialStatus_citizenId_Husband, (a, b) => new { a.citizen_id, a.citizen_national_id }), "citizen_id", "citizen_national_id");

                return File(st, "application/pdf", wi.citizen_national_id+"_"+hus.citizen_national_id + ".pdf");

            }
           else
            {

                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Report/DivorceCertificate.rpt")));
                rd.SetDataSource(db.Citizens.Where(a => a.citizen_id == h || a.citizen_id == w).Select(a => new
                {
                    WifeName = wName,
                    HusbandName = hName,
                    MirrageDate = data.social_status_from,
                    wifeBirth = wi.citizen_birthDate,
                    HusbandBirth = hus.citizen_birthDate,
                    Hrelegion = hus.citizen_relegion,
                    Wrelegion = wi.citizen_relegion

                }

                ).ToList());
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                Stream st = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                st.Seek(0, SeekOrigin.Begin);
                ViewBag.citi = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true && a.citizen_gender == "M").Join(db.Social_status, a => a.citizen_id, b => b.socialStatus_citizenId_Husband, (a, b) => new { a.citizen_id, a.citizen_national_id }), "citizen_id", "citizen_national_id");

                return File(st, "application/pdf", wi.citizen_national_id + "_" + hus.citizen_national_id + ".pdf");


            }

        }

        public JsonResult GetWifeByHusId (int hid)
        {
            Session["hid"] = hid;
            
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.Social_status.Where(a => a.socialStatus_citizenId_Husband == hid).Join(db.Citizens, a => a.socialStatus_citizenId_Wife, b => b.citizen_id, (a, b) => new { b.citizen_id, b.citizen_national_id });
            return Json(data , JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetTypeByWIdAndHId( int wid)
        {
            Session["wid"] = wid;

            db.Configuration.ProxyCreationEnabled = false;

            int id = Convert.ToInt32(Session["hid"]);
            var data = db.Social_status.Where(a => a.socialStatus_citizenId_Husband == id && a.socialStatus_citizenId_Wife == wid).Select(b=>b.socialStatus_TypeId).SingleOrDefault();
            var data1 = db.Social_Status_Type.Where(a => a.social_status_type_id == data);
            return Json(data1, JsonRequestBehavior.AllowGet);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            //your handling logic here
            filterContext.ExceptionHandled = true;
            filterContext.Result = RedirectToAction("errorpage", "home");
        }
    }
}