using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using Servicely.Models;
namespace Servicely.Controllers
{
    public class ATMBirthCertificateController : Controller
    {
        DbMasterEntities1 db = new DbMasterEntities1();
        // GET: BirthCertificate
        public ActionResult Index()
        {
            ViewBag.citi = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
            return View();
        }


        public JsonResult citizenData(int citi)
        {




            var data = db.Citizens.Find(citi);




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
                select new CustomCitizenDataBirthDocument
                {
                    citizen_national_id = ct.citizen_national_id,
                    citizen_first_name = ct.citizen_first_name,
                    citizen_second_name = ct.citizen_second_name,
                    citizen_third_name = ct.citizen_third_name,
                    citizen_fourth_name = ct.citizen_fourth_name,
                    citizen_first_name_arabic = ct.citizen_first_name_arabic,
                    citizen_second_name_arabic = ct.citizen_second_name_arabic,
                    citizen_third_name_arabic = ct.citizen_third_name_arabic,
                    citizen_fourth_name_arabic = ct.citizen_fourth_name_arabic,
                    citizen_gender = ct.citizen_gender,
                    citizen_birthDate = ct.citizen_birthDate.ToString(),
                    citizen_relegion = ct.citizen_relegion,
                    state_arabic_name = stt.state_arabic_name,
                    city_arabic_name = ci.city_arabic_name,
                    state_name = stt.state_name,
                    city_name = ci.city_name
                }
                );














            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;



            return Json(addresCiti, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetFatherInformation(int citi)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var data = db.Citizens.Find(citi);




            var father = db.Citizens.Where(a => a.citizen_id == data.citizen_father_id).Select(a => new {

                a.citizen_first_name,
                a.citizen_fourth_name,
                a.citizen_second_name,
                a.citizen_third_name,
                a.citizen_relegion,
                a.citizen_first_name_arabic,
                a.citizen_fourth_name_arabic,
                a.citizen_second_name_arabic,
                a.citizen_third_name_arabic

            });

            return Json(father, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMotherInformation(int citi)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var data = db.Citizens.Find(citi);




            var mother = db.Citizens.Where(a => a.citizen_id == data.citizen_mother_id).Select(a => new {

                a.citizen_first_name,
                a.citizen_fourth_name,
                a.citizen_second_name,
                a.citizen_third_name,
                a.citizen_relegion,
                a.citizen_first_name_arabic,
                a.citizen_fourth_name_arabic,
                a.citizen_second_name_arabic,
                a.citizen_third_name_arabic

            });

            return Json(mother, JsonRequestBehavior.AllowGet);
        }
        [HttpPost, ActionName("Index")]
        public ActionResult Index(int citi)
        {
            var data = db.Citizens.Find(citi);

            var father = db.Citizens.Find(data.citizen_father_id);
            var mother = db.Citizens.Find(data.citizen_mother_id);
            ViewBag.name = data.citizen_first_name;
            ViewBag.addresCiti = (
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


            //ReportDocument rd = new ReportDocument();
            //rd.Load(Path.Combine(Server.MapPath("~/Report/CitizenCertificate.rpt")));
            //rd.SetDataSource(db.Citizens.Where(a=>a.citizen_id== citi).Select(a => new
            //{
            //    CitizenName = a.citizen_first_name + " "+a.citizen_second_name + " " + a.citizen_third_name+ " " + a.citizen_fourth_name,
            //    CitizenFatherName = father.citizen_first_name+ " " + father.citizen_second_name + " " + father.citizen_third_name+ " " + father.citizen_fourth_name,
            //    CitizenMotherName = mother.citizen_first_name + " " + mother.citizen_second_name + " " + mother.citizen_third_name + " " + mother.citizen_fourth_name,
            //    CitizenBirthDate = a.citizen_birthDate,
            //    CitizenState = addresCiti.state_name,
            //    CitizenCity = addresCiti.city_name,
            //    CitizenRelegion = a.citizen_relegion,
            //    CitizenBirthBlace = a.citizen_national_id,
            //    CitizenGender = a.citizen_gender,
            //    CitizenFatherRelegion = father.citizen_relegion,
            //    CitizenMotherRelegion = mother.citizen_relegion
            //}

            //).ToList());
            //Response.Buffer = false;
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Stream st = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //st.Seek(0, SeekOrigin.Begin);
            //ViewBag.citi = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
            ViewBag.citi = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");

            return View();//File(st, "application/pdf", data.citizen_national_id+".pdf");



        }

    


        public JsonResult GetCitizenInfoByCitizenId(int cid)
        {
            db.Configuration.ProxyCreationEnabled = false;



            return Json(db.Citizens.Where(a => a.citizen_id == cid), JsonRequestBehavior.AllowGet);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            //your handling logic here
            filterContext.ExceptionHandled = true;
            filterContext.Result = RedirectToAction("errorpage", "home");
        }
    }
}