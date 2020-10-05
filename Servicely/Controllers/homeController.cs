using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

using System.Web;
using System.Web.Mvc;
using Servicely.Models;

namespace Servicely.Controllers
{
    public class homeController : BaseController
    {
        DbMasterEntities1 db = new DbMasterEntities1();
        // GET: home
        public ActionResult Index()
        {

          
            return View();
        }
        public ActionResult IndexE()
        {


            return View();
        }
        public ActionResult IndexR()
        {


            return View();
        }
        public ActionResult errorPage()
        {
            return View();
        }

        public ActionResult Indexuser()
        {


            return View();
        }
        public ActionResult Index1()
        {

           
            return View();
        }
        public ActionResult IndexH()
        {


            return View();
        }
        public ActionResult IndexI()
        {


            return View();
        }
        public ActionResult Index2()
        {
           // sendmails();
            
            return View();
        }

        public ActionResult Index4()
        {
            

            return View();
        }
       

        public ActionResult Index5()
        {


            return View();


        }

        public void sendmails()
        {



            var data = db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true);

            var citizens = db.Citizens.Where(a => a.citizen_isDeleted != true);

            foreach (var item in data)
            {

                foreach (var itemm in citizens)
                {

                    TimeSpan t = DateTime.Now.Subtract(Convert.ToDateTime(itemm.citizen_birthDate));
                    if (item.vaccination_type_period <= t.Days)
                    {

                        var father = db.Citizens.Find(itemm.citizen_father_id);
                        if( father == null)
                        {
                            continue;
                        }

                        var email = db.Contacts.Where(a => a.contact_citizen_id == father.citizen_id).Join(db.Contact_Type, a => a.contact_contactType_id, b => b.contact_type_id, (a, b) => new { a, b }).Where(b => b.b.contact_type_name == "Email").Select(a => a.a).FirstOrDefault();
                        if (email != null)
                        {
                            var address = db.Addresses.Where(a => a.address_isCurrent == true).Join(db.AddressCitizens, a => a.address_id, b => b.CA_AddressId, (a, b) => b).Where(a => a.CA_CitizenId == itemm.citizen_id).Select(a => a.Address.address_district_id).SingleOrDefault();
                            var hospital = db.HealthCares.Where(a => a.hospital_district_id == address).FirstOrDefault();
                            if ( hospital == null)
                            {
                                continue;
                            }
                            string Title = "Dear " + father.citizen_first_name + " " + father.citizen_second_name + " " + father.citizen_third_name + " " + father.citizen_fourth_name + " \n  " +
                                "You have go and vaccinate your chilled " + itemm.citizen_first_name + "( " + itemm.citizen_national_id + " ) in his/her current location which is " + hospital.hospital_name + " \n\n" +
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