using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicely.Models;

namespace Servicely.Controllers
{
    public class summaryController : BaseController
    {
        DbMasterEntities1 db = new DbMasterEntities1();
        // GET: Reports
        public ActionResult Index()
        {

            var x = Session["kjkj"];

            ViewBag.citizen = x;
            //ViewBag.address = db.Addresses.Find(Session["address"]);


            var record = db.Document_Type.Find(Session["documentTypeName"]);
         //   ViewBag.xx = record.document_type_name; //Document type name


          //  ViewBag.cc = Session["extension"].ToString(); //document extension

            //string[] filename = Session["filename"].ToString().Split('_');
            //ViewBag.zz = filename[2];    //document name

            return View();



        }
        [HttpPost, ActionName("Index")]
        public ActionResult ConfirmIndex()
        {
            string www = Session["Nai"].ToString();
            var validate = db.Citizens.Where(a => a.citizen_national_id == www).SingleOrDefault();
            if (validate != null)
            {
                Session["success"] = Languages.Language.CitizenAlreadExist;
                return RedirectToAction("Index", "home");
            }



            Citizen c = new Citizen();
            //-----------------citizen infooo----------------------------
            if(Session["second"]!= null)
            {
                c.citizen_second_name = Session["second"].ToString();
            }
            //third
            if (Session["third"] != null)
            {
                c.citizen_third_name = Session["third"].ToString();
            }
            //fourth
            if (Session["fourth"] != null)
            {
                c.citizen_fourth_name = Session["fourth"].ToString();
            }

            if (Session["secondArabic"] != null)
            {
                c.citizen_second_name_arabic = Session["secondArabic"].ToString();
            }


            if (Session["thirdArabic"] != null)
            {
                c.citizen_third_name_arabic = Session["thirdArabic"].ToString();
            }

            if (Session["fourthArabic"] != null)
            {
                c.citizen_fourth_name_arabic = Session["fourthArabic"].ToString();
            }


            if (Session["FirstName"] != null)
            {
                c.citizen_first_name = Session["FirstName"].ToString();
            }

            if (Session["FirstNameArabic"] != null)
            {
                c.citizen_first_name_arabic = Session["FirstNameArabic"].ToString();
            }

            if (Session["FId"] != null)
            {
                c.citizen_father_id = Convert.ToInt32(Session["FId"]);
            }

            if (Session["MId"] != null)
            {
                c.citizen_mother_id = Convert.ToInt32(Session["MId"]);
            }

         
          

            c.citizen_birthDate =  Convert.ToDateTime( Session["BirthDate"]);
            
    
            c.citizen_relegion = Session["relegion"].ToString();
            if (Session["gender"] != null)

            {

                if (Session["gender"].ToString().Equals("Male")|| Session["gender"].ToString().Equals("ذكر"))

                {


                    Session["gender"] = "Male";
                }
                else if (Session["gender"].ToString().Equals("Female") || Session["gender"].ToString().Equals("انثى"))

                {


                    Session["gender"] = "Female";
                }


            }


            if (Session["relegion"] != null)
            {
                if (Session["relegion"].ToString() == "Muslim"|| Session["relegion"].ToString() == "مسلم")
                {
                    Session["relegion"] = "Muslim";
                                     
                }
                else if (Session["relegion"].ToString() == "Cristian"|| Session["relegion"].ToString() == "مسيحي")
                {
                    Session["relegion"] = "Cristian";
                                       
                }
                else if (Session["relegion"].ToString() == "Jewish"|| Session["relegion"].ToString() == "يهودي")
                {
                    Session["relegion"] = "Jewish";
                                   
               }
              

            }

            c.citizen_relegion = Session["relegion"].ToString();
            c.citizen_gender = Session["gender"].ToString();
            c.citizen_birthPlace = Session["BirthPlace"].ToString();
            c.citizen_birthPlace_arabic = Session["BirthPlaceArabic"].ToString();
            c.citizen_national_id = Session["Nai"].ToString();
            
            db.Citizens.Add(c);
            db.SaveChanges();
            //----------------------Addresses------------------------------
            string aa = Session["asas"].ToString();
            Address ad = new Address();
            if (aa == "WithF")
            {
                ad.address_district_id = Convert.ToInt32(Session["AddressDistrictIdFF"]);
                ad.address_isBirthPlace = Convert.ToBoolean(Session["AddressIsBirthPlaceFF"]);
                ad.address_isCurrent = Convert.ToBoolean(Session["AddressIsCurrentFF"]);
                ad.address_street = Session["AddresStreetFF"].ToString();
                ad.address_street_arabic = Session["AddresStreetFFArabic"].ToString();
                db.Addresses.Add(ad);
                db.SaveChanges();

            }
            if (aa == "WithM")
            {
                ad.address_district_id = Convert.ToInt32(Session["AddressDistrictIdMM"]);
                ad.address_isBirthPlace = Convert.ToBoolean(Session["AddressIsBirthPlaceMM"]);
                ad.address_isCurrent = Convert.ToBoolean(Session["AddressIsCurrentMM"]);
                ad.address_street_arabic = Session["AddresStreetMMArabic"].ToString();
                db.Addresses.Add(ad);
                db.SaveChanges();
            }
            if(aa == "other")
            {
                ad.address_street = Session["AddresStreetOO"].ToString();
                ad.address_street_arabic = Session["AddresStreetOOArabic"].ToString() ;
                ad.address_isCurrent = Convert.ToBoolean(Session["IsCurrenOO"]) ;
                ad.address_district_id = Convert.ToInt32( Session["disId"]) ;
                db.Addresses.Add(ad);
                db.SaveChanges();
            }
            //-------------------Address citizen m To m ----------------------
            AddressCitizen cc = new AddressCitizen();
            cc.CA_AddressId = ad.address_id;
            cc.CA_CitizenId = c.citizen_id;
            db.AddressCitizens.Add(cc);
            db.SaveChanges();


            //----------------------------Documents--------------------------
            Document doc = new Document();
            doc.document_name = Session["filename"].ToString();
            doc.document_extensions = Session["extension"].ToString();
            doc.document_documentType_id = Convert.ToInt32(Session["documentTypeName"]);
            doc.document_data = Session["docData"].ToString();
            doc.document_citizen_id = c.citizen_id;
            doc.document_date_upload = Session["uploadDate"].ToString();
            db.Documents.Add(doc);
            db.SaveChanges();

            //---------------------- Login Citizen table ---------------------
            LoginCitizen log = new LoginCitizen();
            // qr code consist of datetime now , datetime seconds , datetime milliseconds , first name and national id
            log.Login_CitizenNId = c.citizen_national_id;
            string pass = Encrypt.enc(c.citizen_national_id);
            log.Login_Password = pass;
            log.Login_PinNumber = "1234";
            log.Login_CitizenId = c.citizen_id;
            string qr = Encrypt.enc(DateTime.Now.ToString() + "_" + DateTime.Now.Second + "_" + DateTime.Now.Millisecond + "_" + c.citizen_first_name + "_" + c.citizen_national_id);
            log.Login_QRcode = qr ;
            db.LoginCitizens.Add(log);
            db.SaveChanges();

            //--------------------------------------- Live Status -------------------------
            Live_Status live = new Live_Status();
            live.live_satus_type_id = 1;
            live.citizen_citizen_id = c.citizen_id;
            db.Live_Status.Add(live);
            db.SaveChanges();

         
            Session["success"] = Languages.Language.CitizenAddedSuccessfully ;
            return RedirectToAction("Index", "home");
        }
        public JsonResult getAddressByCitizenId()
        {
            string aa = "";
            if (Session["asas"]!= null)
            {
                aa = Session["asas"].ToString();
            }
            
            if (aa == "WithF")
            {
                int d = Convert.ToInt32(Session["FatherDistrictId"]);
                int father = Convert.ToInt32(Session["FId"]);
                var data1 = (
                   from ad in db.AddressCitizens
                   from dis in db.Districts
                   from re in db.Regions
                   from ct in db.Cities
                   from st in db.States
                   from cit in db.Citizens
                   from addd in db.Addresses.Where(a => a.address_id == ad.CA_AddressId
                   && father == ad.CA_CitizenId && a.address_isCurrent == true && father == cit.citizen_id
                   && dis.district_id == d && a.address_district_id == d && dis.district_region_id == re.region_id && re.region_city_id == ct.city_id
                   && ct.city_state_id == st.state_id

                   )
                   select new
                   {
                       st.state_name,
                       st.state_arabic_name,
                       ct.city_name,
                       ct.city_arabic_name,
                       re.region_name,
                       re.region_arabic_name,
                       dis.district_name,
                       dis.district_arabic_name,
                       addd.address_street_arabic,
                       addd.address_street,

                   }

                   );
                db.Configuration.ProxyCreationEnabled = false;
                db.Configuration.LazyLoadingEnabled = false;

                return Json(data1, JsonRequestBehavior.AllowGet);

            }
            else if (aa == "WithM")
            {
                int d = Convert.ToInt32(Session["motherdistrictid"]);
                int Mother = Convert.ToInt32(Session["MId"]);
                var data1 = (
                   from ad in db.AddressCitizens
                   from dis in db.Districts
                   from re in db.Regions
                   from ct in db.Cities
                   from st in db.States
                   from cit in db.Citizens
                   from addd in db.Addresses.Where(a => a.address_id == ad.CA_AddressId
                   && Mother == ad.CA_CitizenId && a.address_isCurrent == true && Mother == cit.citizen_id
                   && dis.district_id == d && a.address_district_id == d && dis.district_region_id == re.region_id && re.region_city_id == ct.city_id
                   && ct.city_state_id == st.state_id

                   )
                   select new
                   {
                       st.state_name,
                       st.state_arabic_name,
                       ct.city_name,
                       ct.city_arabic_name,
                       re.region_name,
                       re.region_arabic_name,
                       dis.district_name,
                       dis.district_arabic_name,
                       addd.address_street_arabic,
                       addd.address_street,


                   }

                   );
                db.Configuration.ProxyCreationEnabled = false;
                db.Configuration.LazyLoadingEnabled = false;

                return Json(data1, JsonRequestBehavior.AllowGet);


            }
            else
            {

                int d = Convert.ToInt32(Session["disId"]);
                int Mother = Convert.ToInt32(Session["MId"]);
                var data1 = (


                   from re in db.Regions
                   from ct in db.Cities
                   from st in db.States
                   from dis in db.Districts.Where(a => a.district_id == d
                  && a.district_region_id == re.region_id && re.region_city_id == ct.city_id
                  && ct.city_state_id == st.state_id



                   )
                   select new
                   {
                       st.state_name,
                       ct.city_name,
                       re.region_name,
                       dis.district_name,

                       st.state_arabic_name,
                       ct.city_arabic_name,
                       re.region_arabic_name,
                       dis.district_arabic_name,




                   }

                   ) ;
                db.Configuration.ProxyCreationEnabled = false;
                db.Configuration.LazyLoadingEnabled = false;

                return Json(data1, JsonRequestBehavior.AllowGet);



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