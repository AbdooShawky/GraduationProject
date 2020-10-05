
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicely.Models;
namespace Servicely.Controllers
{ 
    public class ReportController : BaseController
    {
        DbMasterEntities1 db = new DbMasterEntities1();

        public ActionResult Create()
        {
            ViewBag.zero = "0";

            ViewBag.document_documentType_id = new SelectList(db.Document_Type, "document_type_id", "document_type_name");


            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");
                    ViewBag.document_documentType_id = new SelectList(db.Document_Type, "document_type_id", "document_type_name_arabic");

                }
            }


            ViewBag.citizen_father_id = new SelectList(db.Citizens.Where(a => a.citizen_gender == "Male" && a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
            ViewBag.citizen_mother_id = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true && a.citizen_gender == "Female"), "citizen_id", "citizen_national_id");

            return View();
            
        
        }
        [HttpPost]
        public ActionResult Create(Citizen s, Address address, string inlineDefaultRadiosExample, int? State, Upload f1)
        {
            return View();

        }
        // GET: report
        public ActionResult Index()
        {

            ViewBag.zero = "0";

            ViewBag.document_documentType_id = new SelectList(db.Document_Type, "document_type_id", "document_type_name");


            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");
                    ViewBag.document_documentType_id = new SelectList(db.Document_Type, "document_type_id", "document_type_name_arabic");

                }
            }


            ViewBag.citizen_father_id = new SelectList(db.Citizens.Where(a => a.citizen_gender == "Male" && a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
            ViewBag.citizen_mother_id = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true && a.citizen_gender == "Female"), "citizen_id", "citizen_national_id");

            return View();

        }
        [HttpPost , ActionName("Index")]
        
        public ActionResult ConfirmIndex(Citizen s , Address address, string inlineDefaultRadiosExample, int? State, Upload f1)
        {

            var fatherName = db.Citizens.Find(s.citizen_father_id);
            s.citizen_second_name = fatherName.citizen_first_name;
            s.citizen_third_name = fatherName.citizen_second_name;
            s.citizen_fourth_name = fatherName.citizen_third_name;
            s.citizen_second_name_arabic = fatherName.citizen_first_name_arabic;
            s.citizen_third_name_arabic = fatherName.citizen_second_name_arabic;
            s.citizen_fourth_name_arabic = fatherName.citizen_third_name_arabic;
            s.TransactionDate = DateTime.Now;
            db.Citizens.Add(s);
            db.SaveChanges();


            if (ModelState.IsValid)
            {
                
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
                   
                    address.address_district_id = data.address_district_id;
                    
                    address.address_isBirthPlace = data.address_isBirthPlace;
                    address.address_isCurrent = data.address_isCurrent;
                    address.address_street = data.address_street;
                    address.address_street_arabic = data.address_street_arabic;
                   

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
                    
                    address.address_isBirthPlace = data.address_isBirthPlace;
                    address.address_isCurrent = data.address_isCurrent;
                    address.address_street = data.address_street;
                    address.address_street_arabic = data.address_street_arabic;
                   
                }

                

                db.Addresses.Add(address);
                db.SaveChanges();


                AddressCitizen c = new AddressCitizen();
                c.CA_AddressId = address.address_id;
             
                c.CA_CitizenId = s.citizen_id;
                db.AddressCitizens.Add(c);

                db.SaveChanges();
               
                Document d = new Document();
                try
                {
                    var fff = db.Document_Type.Find(f1.document_documentType_id);
                    d.document_documentType_id = f1.document_documentType_id;
                    string filename = s.citizen_national_id + "_" + fff.document_type_name + "_" + f1.file.FileName + "_" + Guid.NewGuid() + Path.GetExtension(f1.file.FileName);
                    string filePath = Server.MapPath("~/DocumentDataURL/" + filename);
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    string filePathName = Path.Combine(filename, filePath);
                    f1.file.SaveAs(filePathName);

                    //==================================================

                    d.document_citizen_id = s.citizen_id;

                    //==========================
                    d.document_data = filePathName;

                    d.document_name = filename;
                    d.document_date_upload = DateTime.Now.ToString();

                    //===============================
                    //  int l = filename.Length;
                    // string length = filename.Substring(l - 3, 3);
                    d.document_extensions = Path.GetExtension(f1.file.FileName);

                    //=========================================
                    // d.document_id = 0;
                    db.Documents.Add(d);
                }
                catch (Exception e)
                {
                    var fff = db.Document_Type.Find(f1.document_documentType_id);
                    d.document_documentType_id = f1.document_documentType_id;
                    string filename = s.citizen_national_id + "_" + fff.document_type_name + "_" + f1.file.FileName + "_" + Guid.NewGuid() + Path.GetExtension(f1.file.FileName);
                    string filePath = "D:\\App\\DocumentDataURL\\" + filename;
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    
                    f1.file.SaveAs(filePath);

                    //==================================================

                    d.document_citizen_id = s.citizen_id;

                    //==========================
                    d.document_data = filePath;

                    d.document_name = filename;
                    d.document_date_upload = DateTime.Now.ToString();

                    //===============================
                    //  int l = filename.Length;
                    // string length = filename.Substring(l - 3, 3);
                    d.document_extensions = Path.GetExtension(f1.file.FileName);

                    //=========================================
                    // d.document_id = 0;
                    db.Documents.Add(d);
                }
              
                  

                db.SaveChanges();
                LoginCitizen log = new LoginCitizen();
                log.Login_CitizenId = s.citizen_id;
                log.Login_CitizenNId = s.citizen_national_id;
                var pp =Encrypt.enc(s.citizen_national_id);
                log.Login_Password = pp;
                log.Login_QRcode = pp;
                db.LoginCitizens.Add(log);
                db.SaveChanges();
                CitizenBalance citizenbalance = new CitizenBalance();
                citizenbalance.CitizenBalance_citizen_id = s.citizen_id;
                citizenbalance.CitizenBalance_balance = 0;
                db.CitizenBalances.Add(citizenbalance);
                db.SaveChanges();

                Session["success"] = Languages.Language.CitizenAddedSuccessfully;
                return RedirectToAction("Index", "home");
            }
            return View();
        }

        public JsonResult GetFatherorMotherAddress(int Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = (
                       from ad in db.AddressCitizens
                       from cit in db.Citizens
                       from addd in db.Addresses.Where(a => a.address_id == ad.CA_AddressId
                       && Id == ad.CA_CitizenId && a.address_isCurrent == true && Id == cit.citizen_id
                       )
                       select new
                       {
                           addd.District.district_name,
                           addd.District.Region.region_name,
                           addd.District.Region.City.city_name,
                           addd.District.Region.City.State.state_name,

                           addd.District.district_arabic_name,
                           addd.District.Region.region_arabic_name,
                           addd.District.Region.City.city_arabic_name,
                           addd.District.Region.City.State.state_arabic_name,

                           addd.address_district_id,
                           addd.address_id,
                           addd.address_isBirthPlace,
                           addd.address_isCurrent,
                           addd.address_isDelete,
                           addd.address_street,
                           addd.address_street_arabic

                       }

                       );
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAll(int? Stat, DateTime date, int Fid)
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
            if (date.Month >= 1 && date.Month < 10)
            {

                month = "0" + date.Month;
            }
            else
            {
                month = date.Month.ToString();
            }
            if (date.Day >= 1 && date.Day < 10)
            {

                day = "0" + date.Day;
            }
            else
            {
                day = date.Day.ToString();
            }
            string NId;
            var state = db.States.Find(Stat);
            if (state != null)
            {


                NId = state.state_code + date.Year + month + day + random;
                db.Configuration.ProxyCreationEnabled = false;
                return Json(NId, JsonRequestBehavior.AllowGet);
            }
            var fatherid1 = db.Citizens.Find(Fid);
            string st = fatherid1.citizen_national_id.Substring(0, 2);
            NId = st + date.Year + month + day + random;
            db.Configuration.ProxyCreationEnabled = false;
            return Json(NId, JsonRequestBehavior.AllowGet);

        }

        //--------------------- function generate national id -----------------
        private JsonResult Generate( int Stat , DateTime date, int Fid)
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
            if (date.Month >= 1 && date.Month < 10)
            {

                month = "0" + date.Month;
            }
            else
            {
                month = date.Month.ToString();
            }
            if (date.Day >= 1 && date.Day < 10)
            {

                day = "0" + date.Day;
            }
            else
            {
                day = date.Day.ToString();
            }
            string NId;
            var state = db.States.Find(Stat);
            if (state != null)
            {


                 NId = state.state_code + date.Year + month + day + random;
                db.Configuration.ProxyCreationEnabled = false;
                return Json(NId, JsonRequestBehavior.AllowGet);
            }
            var fatherid1 = db.Citizens.Find(Fid);
            string st =fatherid1.citizen_national_id.Substring(0, 2);
             NId = st + date.Year + month + day + random;
            db.Configuration.ProxyCreationEnabled = false;
            return Json(NId,JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetAllNameOfCitizen(string name , int Id )
        {
            db.Configuration.ProxyCreationEnabled = false;

            var data = db.Citizens.Find(Id);
            string WholeName = name + " " + data.citizen_first_name + " " + data.citizen_second_name + " " + data.citizen_third_name ;

            return Json(WholeName, JsonRequestBehavior.AllowGet);
        }


        public JsonResult getAddressByCitizenId()
        {
            
            string aa = Session["asas"].ToString();
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
                       ct.city_name,
                       re.region_name,
                       dis.district_name,

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
                       ct.city_name,
                       re.region_name,
                       dis.district_name,

                       addd.address_street,


                   }

                   );
                db.Configuration.ProxyCreationEnabled = false;
                db.Configuration.LazyLoadingEnabled = false;

                return Json(data1, JsonRequestBehavior.AllowGet);


            }

            var addressRecord = db.Addresses.Find(Session["address"]);


            var data = (


                from st in db.States
                from cit in db.Cities
                from dist in db.Districts
                from reg in db.Regions

                from ad in db.Addresses.Where(a => a.address_district_id == addressRecord.address_id &&
                dist.district_region_id == reg.region_id &&
                reg.region_city_id == cit.city_id &&
                cit.city_state_id == st.state_id &&
                dist.district_id == a.address_district_id
                )


                select new
                {

                    st.state_name,
                    cit.city_name,
                    reg.region_name,
                    dist.district_name,
                    ad.address_street

                });














            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Ay7aga()
        {
            ViewBag.zero = "0";

            ViewBag.document_documentType_id = new SelectList(db.Document_Type, "document_type_id", "document_type_name");


            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");
                    ViewBag.document_documentType_id = new SelectList(db.Document_Type, "document_type_id", "document_type_name_arabic");

                }
            }


            ViewBag.citizen_father_id = new SelectList(db.Citizens.Where(a => a.citizen_gender == "Male" && a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
            ViewBag.citizen_mother_id = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true && a.citizen_gender == "Female"), "citizen_id", "citizen_national_id");

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