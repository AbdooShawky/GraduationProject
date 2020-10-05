using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using Servicely.Models;

namespace Servicely.Controllers
{
    public class AddressesController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();
       
        // GET: Addresses
        public ActionResult Index()
        {
            ViewBag.NId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");

            var address = db.Addresses.Include(a => a.District);

            return View(address.ToList());
            //jgfjgdg
        }

        public ActionResult UserIndex()
        {
            ViewBag.NId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");

            var address = db.Addresses.Include(a => a.District);
            
            
            return View(address.ToList());
            //jgfjgdg
        }


        public JsonResult GetCitizenIdToJqueryCode()
        {
            int cid =0;
            if (Session["citizenID"] != null)
             cid =(int)Session["citizenID"];

            return Json(1 , JsonRequestBehavior.AllowGet);
        }

        //----------------Ajax Call-------------------------

        public JsonResult GetAllAddressesByCitizenId(int AId)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var Address = (

                from ac in db.AddressCitizens
                from ct in db.Citizens
                from Stt in db.States
                from cit in db.Cities
                from rg in db.Regions
                from dis in db.Districts
                from ad in db.Addresses.Where(a => a.address_id == ac.CA_AddressId
                && ct.citizen_id == ac.CA_CitizenId && cit.city_state_id == Stt.state_id
                && rg.region_city_id == cit.city_id && dis.district_region_id == rg.region_id
                && a.address_district_id == dis.district_id && ct.citizen_id == AId

                )

                select new
                {

                    ct.citizen_first_name,
                    ct.citizen_second_name,
                    ct.citizen_third_name,
                    ct.citizen_fourth_name,
                    ct.citizen_first_name_arabic,
                    ct.citizen_second_name_arabic,
                    ct.citizen_third_name_arabic,
                    ct.citizen_fourth_name_arabic,
                    ad.address_street,
                    ad.address_street_arabic,
                    dis.district_arabic_name,
                    rg.region_name,
                    cit.city_name,
                    Stt.state_name,
                    rg.region_arabic_name,
                    cit.city_arabic_name,
                    Stt.state_arabic_name,
                    ad.address_isCurrent,
                    ad.address_isBirthPlace,
                    ad.address_id,

                });




            return Json(Address.ToList(), JsonRequestBehavior.AllowGet);



        }

        public JsonResult GetCitiesByStateId(int stateId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Cities.Where(a => a.city_state_id == stateId && a.city_isDeleted != true), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRegionByCityId(int CityId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Regions.Where(a => a.region_city_id == CityId && a.region_isDeleted != true), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDistrictByRegionId(int rId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Districts.Where(a => a.district_region_id == rId && a.district_isDeleted != true), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getallofficesbydistrictid(int districtId)
        {



            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Offices.Where(a => a.office_id == districtId), JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult Create()
        {
            if (Session["FirstName"] == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            if(Session["lang"]!= null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                }
            }
            
            ViewBag.NId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");

            ViewBag.address_district_id = new SelectList(db.Districts, "district_id", "district_name");
            return View();
        }



        // send value of session to jquery by ajax call 
        public JsonResult Sessionvalue ()
        {
            string aa = "";
            if (Session["lang"] != null)
             aa = Session["lang"].ToString();
            return Json(aa, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]

        public ActionResult Create(Address address, string inlineDefaultRadiosExample, int? State)
        {
            string sssss = inlineDefaultRadiosExample;
          
            Session["asas"] = sssss;

            var stat = db.States.Find(State);
            string nationalId = "";
            if (stat == null)
            {

                var fatherid1 = db.Citizens.Find(Convert.ToInt32(Session["FId"]));
                string a = fatherid1.citizen_national_id.Substring(0, 2);

                DateTime birthdate1 = Convert.ToDateTime(  Session["BirthDate"]);
                nationalId = GenerateNationalId(a, birthdate1);



            }
            else
            {


                DateTime birthdate =  Convert.ToDateTime( Session["BirthDate"]);
                nationalId = GenerateNationalId(stat.state_code, birthdate);
            }
            //--------------------------
            Citizen s = new Citizen();
            s.citizen_national_id = nationalId;
            if(Session["FId"] != null)
            {
                s.citizen_father_id = Convert.ToInt32(Session["FId"]);
            }
            if (Session["MId"] != null)
            {
                s.citizen_mother_id = Convert.ToInt32(Session["MId"]);
            }
            
           
            //----------------------------------------------------------
            var fatherid = db.Citizens.Find(s.citizen_father_id);
            if(fatherid != null)
            {
                s.citizen_second_name = fatherid.citizen_first_name;  //|
                s.citizen_third_name = fatherid.citizen_second_name;    //|
                s.citizen_fourth_name = fatherid.citizen_third_name;
                s.citizen_second_name_arabic = fatherid.citizen_first_name_arabic;  //|
                s.citizen_third_name_arabic = fatherid.citizen_second_name_arabic;    //|
                s.citizen_fourth_name_arabic = fatherid.citizen_third_name_arabic;
                Session["second"] = fatherid.citizen_first_name;
                Session["third"] = fatherid.citizen_second_name;
                Session["fourth"] = fatherid.citizen_third_name;
                Session["secondArabic"] = fatherid.citizen_first_name_arabic;
                Session["thirdArabic"] = fatherid.citizen_second_name_arabic;
                Session["fourthArabic"] = fatherid.citizen_third_name_arabic;
            }

            //|
            //----------------------------------------------------------


            s.citizen_birthDate = Convert.ToDateTime( Session["BirthDate"]);
            s.citizen_first_name = Session["FirstName"].ToString();
            s.citizen_relegion = Session["relegion"].ToString();
            s.citizen_gender = Session["gender"].ToString();
            s.citizen_birthPlace = Session["BirthPlace"].ToString();
            s.citizen_id = 0;
            db.Citizens.Add(s);
            Session["kjkj"] = s;
           // db.SaveChanges();



            ViewBag.address_district_id = new SelectList(db.Districts, "district_id", "district_name", address.address_district_id);

            if (inlineDefaultRadiosExample == "WithF")
            {
                var data = (
                    from ad in db.AddressCitizens
                    from cit in db.Citizens
                    from addd in db.Addresses.Where(a => a.address_id == ad.CA_AddressId
                    && s.citizen_father_id == ad.CA_CitizenId && a.address_isCurrent == true && s.citizen_father_id == cit.citizen_id
                    )
                    select new
                    {
                        addd.address_district_id,
                        addd.address_id,
                        addd.address_isBirthPlace,
                        addd.address_isCurrent,
                        addd.address_isDelete,
                        addd.address_street,
                        addd.address_street_arabic

                    }

                    ).SingleOrDefault();
                Session["fatherAddress"] = data;
                address.address_district_id = data.address_district_id;
                Session["FatherDistrictId"] = data.address_district_id;
                address.address_isBirthPlace = data.address_isBirthPlace;
                address.address_isCurrent = data.address_isCurrent;
                address.address_street = data.address_street;
                address.address_street_arabic = data.address_street_arabic;
               // db.Addresses.Add(address);
                //db.SaveChanges();
                Session["AddressDistrictIdFF"] = address.address_district_id;
                Session["AddressIsBirthPlaceFF"] = address.address_isBirthPlace;
                Session["AddressIsCurrentFF"] = address.address_isCurrent;
                Session["AddresStreetFF"] = address.address_street;

                Session["AddresStreetFFArabic"] = address.address_street_arabic;

            }

            if (inlineDefaultRadiosExample == "WithM")
            {
                var data = (
                    from ad in db.AddressCitizens
                    from cit in db.Citizens
                    from addd in db.Addresses.Where(a => a.address_id == ad.CA_AddressId
                    && s.citizen_mother_id == ad.CA_CitizenId && a.address_isCurrent == true && s.citizen_mother_id == cit.citizen_id
                    )
                    select new
                    {
                        addd.address_district_id,
                        addd.address_id,
                        addd.address_isBirthPlace,
                        addd.address_isCurrent,
                        addd.address_isDelete,
                        addd.address_street,
                        addd.address_street_arabic

                    }

                    ).SingleOrDefault();

                address.address_district_id = data.address_district_id;
                Session["motherdistrictid"]= data.address_district_id;
                address.address_isBirthPlace = data.address_isBirthPlace;
                address.address_isCurrent = data.address_isCurrent;
                address.address_street = data.address_street;
                address.address_street_arabic = data.address_street_arabic;
                //db.Addresses.Add(address);
                // db.SaveChanges();
                Session["AddressDistrictIdMM"] = address.address_district_id;
                Session["AddressIsBirthPlaceMM"] = address.address_isBirthPlace;
                Session["AddressIsCurrentMM"] = address.address_isCurrent;
                Session["AddresStreetMM"] = address.address_street;
                Session["AddresStreetMMArabic"] = address.address_street_arabic;


            }

            if (inlineDefaultRadiosExample == "other")
            {
                address.address_isCurrent = true;
                Session["AddresStreetOO"] = address.address_street;
                Session["AddresStreetOOArabic"] = address.address_street_arabic;
                Session["IsCurrenOO"] = address.address_isCurrent;
                Session["disId"] = address.address_district_id;
               // db.Addresses.Add(address);
               // db.SaveChanges();
            }
           



            AddressCitizen c = new AddressCitizen();
            c.CA_AddressId = address.address_id;
            Session["address"] = address.address_id;
            c.CA_CitizenId = s.citizen_id;
            db.AddressCitizens.Add(c);

            //db.SaveChanges();
            Session["CID"] = s.citizen_id;
            Session["Nai"] = nationalId;

            return RedirectToAction("Create", "Document");

        }

        [HttpGet]
        public ActionResult AddNewAddressCreate()
        {
            
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                }
            }

            ViewBag.asas = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");

            ViewBag.address_district_id = new SelectList(db.Districts, "district_id", "district_name");
            return View();
        }
        [HttpPost]
        public ActionResult AddNewAddressCreate( Address address , int? asas)
        {
            
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                }
            }
            var data = (
                    from ad in db.AddressCitizens
                    from cit in db.Citizens
                    from addd in db.Addresses.Where(a => a.address_id == ad.CA_AddressId
                    &&  asas== ad.CA_CitizenId && a.address_isCurrent == true && asas == cit.citizen_id
                    )
                    select new 
                    {
                        
                         addd.address_isCurrent,
                        addd.address_district_id,
                        addd.address_street,
                         addd.address_id

                    }

                    ).SingleOrDefault();
            
          
            if( data != null)
            {

                if (data.address_district_id == address.address_district_id && data.address_street == address.address_street)
                {
                    ViewBag.asas = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
                    ViewBag.addressmsg = Languages.Language.Citizen_already_has_this_address;
                    ViewBag.address_district_id = new SelectList(db.Districts, "district_id", "district_name");
                    return View();
                }

                var old = db.Addresses.Find(data.address_id);
                old.address_isCurrent = false;


                address.address_isCurrent = true;
                db.Addresses.Add(address);
                db.SaveChanges();
                AddressCitizen aa = new AddressCitizen();
                aa.CA_AddressId = address.address_id;
                aa.CA_CitizenId = (int)asas;
                db.AddressCitizens.Add(aa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.asas = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
            ViewBag.addressmsg = Languages.Language.Citizen_already_has_this_address;
            ViewBag.address_district_id = new SelectList(db.Districts, "district_id", "district_name");
            return View();
        }





        //--------------------- function generate national id -----------------
        private string GenerateNationalId(string st, DateTime date)
        {
            // generate 4 random numbers
            int min = 1000;
            int max = 9999;
            Random rdm = new Random();
            int random = rdm.Next(min, max);
            random.ToString();
            string month = "";
            string day = "";
            // string[] a = date.Split('-');
            if ( date.Month >= 1 && date.Month< 10)
            {
               
                month = "0" + date.Month;
            }
            else
            {
                month =  date.Month.ToString();
            }
            if (date.Day >= 1 && date.Day < 10)
            {
                
                day = "0" + date.Day;
            }
            else
            {
                day = date.Day.ToString();
            }
            string NId = st + date.Year + month + day + random;
            return NId;

        }
        //-----------------------------------------------------------------------------


        // GET: Addresses/Edit/5
        public ActionResult Edit(int id)
        {
            Address address = db.Addresses.Find(id);
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                }
            }
           // ViewBag.address_district_id = new SelectList(db.Districts, "district_id", "district_name", address.address_district_id);
            return View(address);
        }


        [HttpPost]

        public ActionResult Edit(Address address , Boolean? iscurrent)
        {
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                }
            }
            if (iscurrent == true)
            {
                address.address_isCurrent = true;
                var old = db.Addresses.Find(address.address_id);
                old.address_isCurrent = address.address_isCurrent;
                old.address_district_id = address.address_district_id;
                old.address_street = address.address_street;
                old.address_street_arabic = address.address_street_arabic;

                
                
                db.SaveChanges();
                return RedirectToAction("Index");

            }
                

            if(iscurrent == null && address.address_isCurrent == true)
            {
                var data = db.Addresses.Where(a => a.address_id == address.address_id && a.address_isCurrent == true).SingleOrDefault();
                if(data != null )
                {
                    data.address_isCurrent = false;
                    db.SaveChanges();
                }

                var old = db.Addresses.Find(address.address_id);
                 old.address_isCurrent = address.address_isCurrent;
                    old.address_district_id = address.address_district_id;
                    old.address_street = address.address_street;
                    old.address_street_arabic = address.address_street_arabic;

                
            }


         





            db.SaveChanges();
            return RedirectToAction("Index");


        }


        public ActionResult Delete(int id)
        {
           
            Address address = db.Addresses.Find(id);
            return View(address);
        }


        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            //Address address = db.Address.Find(id);
            //db.Address.Remove(address);
            var old = db.Addresses.Find(id);
            if(old.address_isCurrent == true)
            {
                Session["addressmessage"] = Languages.Language.AddressDeleteMessage1;
                return RedirectToAction("Delete");
            }


            old.address_isDelete = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        protected override void OnException(ExceptionContext filterContext)
        {
            //your handling logic here
            filterContext.ExceptionHandled = true;
            filterContext.Result = RedirectToAction("errorpage", "home");
        }
    }


}
