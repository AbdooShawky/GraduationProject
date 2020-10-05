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
    public class PoliceDepartmentsController : Controller
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: PoliceDepartments
        public ActionResult Index()
        {
            ViewBag.citi = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");


            var policeDepartments = db.PoliceDepartments.Include(p => p.District);
            return View(policeDepartments.ToList());
        }

        // GET: PoliceDepartments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoliceDepartment policeDepartment = db.PoliceDepartments.Find(id);
            if (policeDepartment == null)
            {
                return HttpNotFound();
            }
            return View(policeDepartment);
        }

        // GET: PoliceDepartments/Create
        public ActionResult Create()
        {
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");


            ViewBag.police_department_district_id = new SelectList(db.Districts, "district_id", "district_name");
            return View();
        }

        // POST: PoliceDepartments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        public ActionResult Create( PoliceDepartment policeDepartment)
        {
            if (ModelState.IsValid)
            {
                ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

                ViewBag.police_department_district_id = new SelectList(db.Districts, "district_id", "district_name", policeDepartment.police_department_district_id);





                db.PoliceDepartments.Add(policeDepartment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.police_department_district_id = new SelectList(db.Districts, "district_id", "district_name", policeDepartment.police_department_district_id);
            return View(policeDepartment);
        }


        public JsonResult citizenData(int citi)
        {




            var data = db.Citizens.Find(citi);






            var addresCiti = (
                from ct in db.Citizens
                from adc in db.AddressCitizens
                from stt in db.States
                from ci in db.Cities
                from re in db.Regions
                from dis in db.Districts

                from area in db.AreasAuthorityRequests
                from RSRIB in db.RealStateRegistryInterestBranches
                from RSRIT in db.RealStateRegistryInterestPropertyTaxes
                from RSRIP in db.RealStateRegistryInterestPopularContracts
                from RSRIS in db.RealStateRegistryInterestReportsSubjects
                from RSRIR in db.RealStateRegistryInterestReports
                from RSRID in db.RealStateRegistryInterestDepartments
                from RSRI in db.RealStateRegistryInterests

                from add in db.Addresses.Where(a => a.address_id == adc.CA_AddressId
                && a.address_isCurrent == true && ct.citizen_id == citi && adc.CA_CitizenId == citi
                && dis.district_region_id == re.region_id && re.region_city_id == ci.city_id
                && ci.city_state_id == stt.state_id && a.address_district_id == dis.district_id
                && RSRIR.realStateRegistryInterest_reports_subject == RSRIS.realStateRegistryInterestReports_subject_id &&
                RSRIR.realStateRegistryInterest_reports_vendor_id == ct.citizen_id &&
                RSRIR.realStateRegistryInterest_reports_popularContract_id == RSRIP.realStateRegistryInterest_popularContract_id &&
                RSRIR.realStateRegistryInterest_reports_propertyTaxes_id == RSRIT.realStateRegistryInterestPropertyTaxes_id &&
                RSRIR.realStateRegistryInterest_reports_request == area.AreasAuthorityRequest_id &&
                RSRIR.realStateRegistryInterestBranch_id == RSRIB.realStateRegistryInterest_branch_id &&
                RSRID.realStateRegistryInterestDepartments_id == RSRI.realStateRegistryInterest_id &&
                RSRIB.realStateRegistryInterest_branch_realstate_id == RSRI.realStateRegistryInterest_id &&
                RSRIR.realStateRegistryInterest_reports_vendor_id == citi
                )
                select new
                {
                    citizen_first_name = ct.citizen_first_name,
                    citizen_second_name = ct.citizen_second_name,
                    citizen_third_name = ct.citizen_third_name,
                    citizen_fourth_name = ct.citizen_fourth_name,

                    citizen_first_name_arabic = ct.citizen_first_name_arabic,
                    citizen_second_name_arabic = ct.citizen_second_name_arabic,
                    citizen_third_name_arabic = ct.citizen_third_name_arabic,
                    citizen_fourth_name_arabic = ct.citizen_fourth_name_arabic,
                    citizen_relegion = ct.citizen_relegion,
                    state_arabic_name = stt.state_arabic_name,
                    city_arabic_name = ci.city_arabic_name,
                    state_name = stt.state_name,
                    city_name = ci.city_name,

                    realStateRegistryInterest_reports_price = RSRIR.realStateRegistryInterest_reports_price



                }
                );














            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;



            return Json(addresCiti, JsonRequestBehavior.AllowGet);
        }



        // GET: PoliceDepartments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoliceDepartment policeDepartment = db.PoliceDepartments.Find(id);
            if (policeDepartment == null)
            {
                return HttpNotFound();
            }
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            ViewBag.police_department_district_id = new SelectList(db.Districts, "district_id", "district_name", policeDepartment.police_department_district_id);
            return View(policeDepartment);
        }

        // POST: PoliceDepartments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Edit( PoliceDepartment policeDepartment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(policeDepartment).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.police_department_district_id = new SelectList(db.Districts, "district_id", "district_name", policeDepartment.police_department_district_id);
            return View(policeDepartment);
        }

        // GET: PoliceDepartments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoliceDepartment policeDepartment = db.PoliceDepartments.Find(id);
            if (policeDepartment == null)
            {
                return HttpNotFound();
            }
            return View(policeDepartment);
        }

        // POST: PoliceDepartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PoliceDepartment policeDepartment = db.PoliceDepartments.Find(id);
            db.PoliceDepartments.Remove(policeDepartment);
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
