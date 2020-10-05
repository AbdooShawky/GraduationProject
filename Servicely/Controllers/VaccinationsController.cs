using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicely.Models;
using System.Net;
using System.Net.Mail;
namespace Servicely.Controllers
{
    public class VaccinationsController : BaseController
    {
        DbMasterEntities1 db = new DbMasterEntities1();
        // GET: Vaccinations
        public ActionResult Index()
        {
            //sendmails();
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
            ViewBag.VaccinationType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                   
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");
                    ViewBag.VaccinationType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name_arabic");
                     
                }
            }

            var data = (List<Citizen>)TempData["data"];
            if(data != null)
            {
                return View(data.ToList());
            }
            return View();
        }

        public ActionResult Index1()
        {
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
            ViewBag.dis = TempData["dis"];
            var data = (List<AddressCitizen>)TempData["dis"];
            ViewBag.VaccinationType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");
                    ViewBag.dis = TempData["dis"];
                    var dt = (List<AddressCitizen>)TempData["dis"];
                    ViewBag.VaccinationType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name_arabic");
                    return View(dt);
                }
            }


            return View(data);
        }
        [HttpPost]
        public ActionResult Index1(DateTime From, DateTime To, int VaccinationType, int? district)
        {

            ViewBag.VaccinationType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.VaccinationType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name_arabic");

                }
            }
            var period = db.VaccinationTypes.Find(VaccinationType);

            var dis = db.AddressCitizens.Join(db.Addresses, a => a.CA_AddressId, b => b.address_id, (a, b) => a).Join(db.Citizens, a => a.CA_CitizenId, b => b.citizen_id, (a, b) => new { a, b }).ToList().Where(x => x.b.citizen_birthDate.AddDays((double)period.vaccination_type_period) >= From.Date && x.b.citizen_birthDate.AddDays((double)period.vaccination_type_period) <= To.Date && x.a.Address.address_district_id == district && x.a.Address.address_isCurrent == true).Select(a => a.a).ToList();



            var diss = db.Citizens.ToList().
                Where(a => a.citizen_birthDate.AddDays((double)period.vaccination_type_period) >= From.Date && a.citizen_birthDate.AddDays((double)period.vaccination_type_period) <= To.Date).ToList()
                .Join(db.AddressCitizens, x => x.citizen_id, y => y.CA_CitizenId, (x, y) => new AddressCitizen { CA_AddressId = y.Address.address_id, CA_CitizenId = y.Citizen.citizen_id }).Join(db.Addresses, z => z.CA_AddressId, b => b.address_id, (z, b) => b)
                .Where(s => s.address_district_id == district && s.address_isCurrent == true).Select(a => new AddressCitizen { }).ToList();


            if (dis.Count() != 0)
            {
                ViewBag.VaccinationType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name");
                ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.VaccinationType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name_arabic");
                        ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                    }
                }
                TempData["dis"] = dis;
                ViewBag.dis = dis;
                return View(dis);
            }
            
            var data = db.Citizens.ToList().Where(a => a.citizen_birthDate.AddDays((double)period.vaccination_type_period) >= From.Date && a.citizen_birthDate.AddDays((double)period.vaccination_type_period) <= To.Date).ToList();
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                }
            }
            ViewBag.data = data;
            TempData["data"] = data;
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Index(DateTime From, DateTime To , int VaccinationType , int? district)
        {
            ViewBag.VaccinationType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.VaccinationType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name_arabic");

                }
            }
            var period = db.VaccinationTypes.Find(VaccinationType);

            var dis = db.AddressCitizens.Join(db.Addresses, a => a.CA_AddressId, b => b.address_id, (a, b) => a).Join(db.Citizens, a => a.CA_CitizenId, b => b.citizen_id, (a, b) => new { a, b }).ToList().Where(x=> x.b.citizen_birthDate.AddDays((double)period.vaccination_type_period) >= From.Date && x.b.citizen_birthDate.AddDays((double)period.vaccination_type_period) <= To.Date && x.a.Address.address_district_id== district && x.a.Address.address_isCurrent==true).Select(a =>  a.a  ).ToList();
            
            
            
            var diss = db.Citizens.ToList().
                Where(a => a.citizen_birthDate.AddDays((double)period.vaccination_type_period) >= From.Date && a.citizen_birthDate.AddDays((double)period.vaccination_type_period) <= To.Date).ToList()
                .Join(db.AddressCitizens, x => x.citizen_id, y => y.CA_CitizenId, (x, y) => new AddressCitizen { CA_AddressId =  y.Address.address_id , CA_CitizenId= y.Citizen.citizen_id  }).Join(db.Addresses, z=> z.CA_AddressId, b=>b.address_id, (z,b)=> b)
                .Where(s => s.address_district_id == district && s.address_isCurrent == true).Select(a=> new AddressCitizen {} ).ToList();
            

            if( dis.Count() != 0)
            {
                ViewBag.VaccinationType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name");
                ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.VaccinationType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name_arabic");
                        ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                    }
                }
                TempData["dis"] = dis;
                ViewBag.dis = dis;
                return RedirectToAction("Index1");
            }

            var data = db.Citizens.ToList().Where(a => a.citizen_birthDate.AddDays((double)period.vaccination_type_period) >= From.Date && a.citizen_birthDate.AddDays((double)period.vaccination_type_period) <= To.Date).ToList();
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                }
            }
            ViewBag.data = data;
            return View(data.ToList());
        }

        public JsonResult GetAllCitizensVaccinationBySpecificDate (DateTime Start , DateTime End)
        {

            
            
            List<Citizen> cc = new List<Citizen>();
            // TimeSpan Subtraction = End.Subtract(Start);
            //int dd= Subtraction.Days;
            DateTime strat = Convert.ToDateTime(Start);
            DateTime end = Convert.ToDateTime(End);
            DateTime ss = Start.Date;
            if (strat < end )
            {
                strat.ToShortDateString();
            }
            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;
            var data = db.Citizens.ToList().Where(a => a.citizen_birthDate.AddDays(15) >= Start.Date && a.citizen_birthDate.AddDays(15.0)<= End.Date).ToList();

            

            return Json( data, JsonRequestBehavior.AllowGet );
        }

          
         public void sendmails()
        {



            var data = db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true);

            var citizens = db.Citizens.Where(a=>a.citizen_isDeleted!=true);

            foreach (var item in data)
            {

                foreach (var itemm in citizens)
                {

                    TimeSpan t = DateTime.Now.Subtract( Convert.ToDateTime(itemm.citizen_birthDate));
                    if(item.vaccination_type_period <= t.Days)
                    {

                        var father = db.Citizens.Find(itemm.citizen_father_id);

                        var email = db.Contacts.Where(a => a.contact_citizen_id == father.citizen_id).Join(db.Contact_Type,a=>a.contact_contactType_id,b=>b.contact_type_id,(a,b)=> new {a, b}).Where(b=>b.b.contact_type_name == "Email").Select(a=>a.a).FirstOrDefault();
                         if (email!=null)
                        {
                            var address = db.Addresses.Where(a => a.address_isCurrent == true).Join(db.AddressCitizens, a => a.address_id, b => b.CA_AddressId, (a, b) => b).Where(a => a.CA_CitizenId == itemm.citizen_id).Select(a => a.Address.address_district_id).SingleOrDefault();
                            var hospital = db.HealthCares.Where(a => a.hospital_district_id == address).FirstOrDefault();

                            string Title = "Dear "+ father.citizen_first_name +" "+ father.citizen_second_name + " " +father.citizen_third_name + " " + father.citizen_fourth_name+" \n  " +
                                "You have go and vaccinate your chilled "+itemm.citizen_first_name + "( "+itemm.citizen_national_id+" ) in his/her current location which is " + hospital.hospital_name +" \n\n"+
                                "yours sincerely , :)";
                            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                            smtp.EnableSsl = true;
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = new NetworkCredential("CSCE4502@gmail.com", "csce4502f16");
                            smtp.Send("CSCE4502@gmail.com", email.contact_data, "Vaccination", Title);




                        }
                    }
                }

            }





         




           
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            //your handling logic here
            filterContext.ExceptionHandled = true;
            filterContext.Result = RedirectToAction("errorpage", "home");
        }


    }
}