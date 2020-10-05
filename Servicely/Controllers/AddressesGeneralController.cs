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
    public class AddressesGeneralController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: Addresses
        /// <summary>
        /// Index() no parameter
        /// </summary>
        /// <returns> all address </returns>
        public ActionResult Index()
        {
            ViewBag.NId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");

            var address = db.Addresses.Include(a => a.District);

            return View(address.ToList());
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
                    ad.address_street,
                    dis.district_name,
                    rg.region_name,
                    cit.city_name,
                    Stt.state_name,
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

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.NId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            ViewBag.address_district_id = new SelectList(db.Districts, "district_id", "district_name");
            return View();
        }


        [HttpPost]

        public ActionResult Create(Address address, string inlineDefaultRadiosExample, int NId, int? State)
        {

            var stat = db.States.Find(State);
            var ddd = db.Citizens.Find(NId);

            int? father = ddd.citizen_father_id;
            int? mother = ddd.citizen_mother_id;







            ViewBag.address_district_id = new SelectList(db.Districts, "district_id", "district_name", address.address_district_id);

            if (inlineDefaultRadiosExample == "WithF")
            {
                var data = (
                    from ad in db.AddressCitizens
                    from cit in db.Citizens
                    from addd in db.Addresses.Where(a => a.address_id == ad.CA_AddressId
                    && father == ad.CA_CitizenId && a.address_isCurrent == true && father == cit.citizen_id
                    )
                    select new
                    {
                        addd.address_district_id,
                        addd.address_id,
                        addd.address_isBirthPlace,
                        addd.address_isCurrent,
                        addd.address_isDelete,
                        addd.address_street,


                    }

                    ).SingleOrDefault();

                address.address_district_id = data.address_district_id;
                address.address_isBirthPlace = data.address_isBirthPlace;
                address.address_isCurrent = data.address_isCurrent;
                address.address_street = data.address_street;





            }

            if (inlineDefaultRadiosExample == "WithM")
            {
                var data = (
                    from ad in db.AddressCitizens
                    from cit in db.Citizens
                    from addd in db.Addresses.Where(a => a.address_id == ad.CA_AddressId
                    && mother == ad.CA_CitizenId && a.address_isCurrent == true && mother == cit.citizen_id
                    )
                    select new
                    {
                        addd.address_district_id,
                        addd.address_id,
                        addd.address_isBirthPlace,
                        addd.address_isCurrent,
                        addd.address_isDelete,
                        addd.address_street,


                    }

                    ).SingleOrDefault();

                address.address_district_id = data.address_district_id;
                address.address_isBirthPlace = data.address_isBirthPlace;
                address.address_isCurrent = data.address_isCurrent;
                address.address_street = data.address_street;





            }

            if (inlineDefaultRadiosExample == "other")
            {
                address.address_isCurrent = true;

            }

            //======================================




            var iscurrent = (
                from citi in db.Citizens
                from ad in db.AddressCitizens
                from add in db.Addresses.Where(a => a.address_id == ad.CA_AddressId
                && ad.CA_CitizenId == NId && citi.citizen_id == NId
                )
                select new
                {
                    add.address_id,
                    add.address_isCurrent

                }
                ).ToList();

            Address isCur = new Address();
            IEnumerable<Address> m_oEnum = new Address[] { };
            IEnumerable<bool> i = new bool[] { };
            bool aa = false;
            int id = 0;
            foreach (var item in iscurrent)
            {
                if (item.address_isCurrent == true)
                {
                    aa = item.address_isCurrent;
                    id = item.address_id;
                }
            }



            var d1 = db.Addresses.Where(a => a.address_id == id && a.address_isCurrent == aa).Select(a => a.address_id).SingleOrDefault();
            var old = db.Addresses.Find(id);
            if (old != null)
                old.address_isCurrent = false;

            db.Addresses.Add(address);
            db.SaveChanges();

            AddressCitizen c = new AddressCitizen();
            c.CA_AddressId = address.address_id;
            c.CA_CitizenId = NId;
            db.AddressCitizens.Add(c);

            db.SaveChanges();



            return RedirectToAction("Index");



        }

        //--------------------- function generate national id -----------------

        private string GenerateNationalId(string st, string date)
        {
            // generate 4 random numbers
            int min = 1000;
            int max = 9999;
            Random rdm = new Random();
            int random = rdm.Next(min, max);
            random.ToString();

            string[] a = date.Split('-');
            string NId = st + a[0] + a[1] + a[2] + random;
            return NId;

        }
        //-----------------------------------------------------------------------------


        // GET: Addresses/Edit/5
        public ActionResult Edit(int id)
        {
            Address address = db.Addresses.Find(id);

            ViewBag.address_district_id = new SelectList(db.Districts, "district_id", "district_name", address.address_district_id);
            return View(address);
        }


        [HttpPost]

        public ActionResult Edit(Address address)
        {

            db.Entry(address).State = System.Data.Entity.EntityState.Modified;
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
