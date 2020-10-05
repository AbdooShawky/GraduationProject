using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using Servicely.Models;

namespace Servicely.Controllers
{
    public class DeceasedCertificateController : BaseController
    {
        DbMasterEntities1 db = new DbMasterEntities1();
        // GET: DeceasedCertificate
        public ActionResult Index()
        {
            ViewBag.citi = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true).Join(db.Deceaseds, a=> a.citizen_id , b=> b.deceased_citizenId, (a,b)=> new {a.citizen_id,a.citizen_national_id }), "citizen_id", "citizen_national_id");

            return View();
        }
        public JsonResult GetAge(int Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            DateTime data = Convert.ToDateTime( db.Deceaseds.Where(a => a.deceased_isDeleted != true && a.deceased_citizenId == Id).SingleOrDefault().deceased_deathDate);
            DateTime birthdate = db.Citizens.Find(Id).citizen_birthDate;
            TimeSpan age = data.Subtract(birthdate);
            int years = age.Days / 365;
            return Json(years + Languages.Language.Year, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDeceasedInfoById(int citi)
        {
            
            var data = db.Citizens.Find(citi);
            //  string[] ddd = data.citizen_birthDate.Split('-');
            // int age = Convert.ToInt32(ddd[0]);
            var father = db.Citizens.Find(data.citizen_father_id);
            var mother = db.Citizens.Find(data.citizen_mother_id);


            var addresCiti = (
                from ct in db.Citizens
                from adc in db.AddressCitizens
                from stt in db.States
                from ci in db.Cities
                from re in db.Regions
                from dis in db.Districts
                from dc in db.Deceaseds

                from add in db.Addresses.Where(a => a.address_id == adc.CA_AddressId
                && a.address_isCurrent == true && ct.citizen_id == citi && adc.CA_CitizenId == citi
                && dis.district_region_id == re.region_id && re.region_city_id == ci.city_id
                && ci.city_state_id == stt.state_id && a.address_district_id == dis.district_id
                && dc.deceased_citizenId == ct.citizen_id && dc.deceased_citizenId == citi
                )
                select new 
                {

                    ct.citizen_national_id,
                    ct.citizen_first_name,
                    ct.citizen_second_name,
                    ct.citizen_third_name,
                    ct.citizen_fourth_name,
                    ct.citizen_first_name_arabic,
                    ct.citizen_second_name_arabic,
                    ct.citizen_third_name_arabic,
                    ct.citizen_fourth_name_arabic,
                    ct.citizen_gender,
                    ct.citizen_birthDate,
                    ct.citizen_birthPlace,
                    stt.state_arabic_name,
                    stt.state_name,
                    dc.deceased_deathDate,
                    dc.deceased_deathPlace
                }
                );

            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;
            
            return Json(addresCiti, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSocialStatusByCitizenId(int citi)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var citizen = db.Citizens.Find(citi);
            if( citizen.citizen_gender =="Male" )
            {
                var married = db.Social_status.Where(a => a.socialStatus_citizenId_Husband == citizen.citizen_id && a.social_status_isStill == true).FirstOrDefault();
                if(married != null)
                {
                    return Json(Languages.Language.Married, JsonRequestBehavior.AllowGet);
                }
                
                
                return Json(Languages.Language.Unmarried, JsonRequestBehavior.AllowGet);




            }
            else
            {
                var married = db.Social_status.Where(a => a.socialStatus_citizenId_Wife == citizen.citizen_id && a.social_status_isStill == true).FirstOrDefault();
                if (married != null)
                {
                    return Json(Languages.Language.MarriedW, JsonRequestBehavior.AllowGet);
                }
                return Json(Servicely.Languages.Language.UnMarriedW, JsonRequestBehavior.AllowGet);
            }


            
        }

        [HttpPost, ActionName("Index")]
        public ActionResult ConfirmIndex(int citi)
        {
            var data = db.Citizens.Find(citi);
          //  string[] ddd = data.citizen_birthDate.Split('-');
           // int age = Convert.ToInt32(ddd[0]);
            var father = db.Citizens.Find(data.citizen_father_id);
            var mother = db.Citizens.Find(data.citizen_mother_id);

            var addresCiti = (
                from ct in db.Citizens
                from adc in db.AddressCitizens
                from stt in db.States
                from ci in db.Cities
                from re in db.Regions
                from dis in db.Districts
                from add in db.Addresses.Where(a => a.address_id == adc.CA_AddressId
                && a.address_isCurrent == true && ct.citizen_id == citi && adc.CA_CitizenId == citi
                && dis.district_region_id == re.region_id && re.region_city_id == ci.city_id
                && ci.city_state_id == stt.state_id && a.address_district_id == dis.district_id
                )
                select new
                {
                    stt.state_name,
                    ci.city_name
                }
                ).SingleOrDefault();

            var dec = db.Citizens.Where(a=>a.citizen_id == citi).Join(db.Deceaseds,a=>a.citizen_id , b=>b.deceased_citizenId , (a,b)=> new { b.deceased_deathPlace , b.deceased_deathDate  }).ToList().SingleOrDefault();
            string[] ddd1 = dec.deceased_deathDate.Split('-');
            int deth = Convert.ToInt32(ddd1[0]);
           // int totalage = deth - age;
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report/DeceasedCertificate.rpt")));
            rd.SetDataSource(db.Citizens.Where(a => a.citizen_id == citi).Select(a => new
            {
                CitizenName = a.citizen_first_name + " " + a.citizen_second_name + " " + a.citizen_third_name + " " + a.citizen_fourth_name,
                CitizenMotherName = mother.citizen_first_name + " " + mother.citizen_second_name + " " + mother.citizen_third_name + " " + mother.citizen_fourth_name,
                Gender = a.citizen_gender,
                Date = dec.deceased_deathDate,
                Place = dec.deceased_deathPlace,
               // AgeofDeath = totalage.ToString()
            }

            ).ToList()) ;
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream st = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            st.Seek(0, SeekOrigin.Begin);
            ViewBag.citi = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");

            return File(st, "application/pdf", data.citizen_national_id + ".pdf");



        }

        protected override void OnException(ExceptionContext filterContext)
        {
            //your handling logic here
            filterContext.ExceptionHandled = true;
            filterContext.Result = RedirectToAction("errorpage", "home");
        }

    }
}