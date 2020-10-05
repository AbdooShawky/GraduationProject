using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Servicely.Models;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace Servicely.Controllers
{
    public class RealStateRegistryInterestReportsController : Controller
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: RealStateRegistryInterestReports
        public ActionResult Index()
        {
            ViewBag.citi = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");


            var realStateRegistryInterestReports = db.RealStateRegistryInterestReports.Include(r => r.AreasAuthorityRequest).Include(r => r.Citizen).Include(r => r.Citizen1).Include(r => r.District).Include(r => r.RealStateRegistryInterestBranch).Include(r => r.RealStateRegistryInterestPopularContract).Include(r => r.RealStateRegistryInterestPropertyTax).Include(r => r.RealStateRegistryInterestReportsSubject);
            return View(realStateRegistryInterestReports.ToList());
        }

        // GET: RealStateRegistryInterestReports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealStateRegistryInterestReport realStateRegistryInterestReport = db.RealStateRegistryInterestReports.Find(id);
            if (realStateRegistryInterestReport == null)
            {
                return HttpNotFound();
            }
            return View(realStateRegistryInterestReport);
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
                from RSRIR in db.RealStateRegistryInterestReports
                from RSRIS in db.RealStateRegistryInterestReportsSubjects
                from area in db.AreasAuthorityRequests
                from RSRIB in db.RealStateRegistryInterestBranches


                //from RSRID in db.RealStateRegistryInterestDepartments
                from RSRIT in db.RealStateRegistryInterestPropertyTaxes
                from RSRIP in db.RealStateRegistryInterestPopularContracts


                from add in db.Addresses.Where(a => a.address_id == adc.CA_AddressId
                && a.address_isCurrent == true && ct.citizen_id == citi && adc.CA_CitizenId == citi
                && dis.district_region_id == re.region_id && re.region_city_id == ci.city_id
                && ci.city_state_id == stt.state_id && a.address_district_id == dis.district_id
                &&RSRIR.realStateRegistryInterest_reports_vendor_id==ct.citizen_id
               && RSRIR.realStateRegistryInterest_reports_vendor_id==citi
               && RSRIR.realStateRegistryInterest_reports_subject == RSRIS.realStateRegistryInterestReports_subject_id&& 
               // && RSRIR.realStateRegistryInterest_reports_Places_id == dis.district_id&&

              RSRIR.realStateRegistryInterest_reports_popularContract_id== RSRIP.realStateRegistryInterest_popularContract_id
              &&RSRIR.realStateRegistryInterest_reports_propertyTaxes_id == RSRIT.realStateRegistryInterestPropertyTaxes_id

              && RSRIR.realStateRegistryInterest_reports_request == area.AreasAuthorityRequest_id

              && RSRIR.realStateRegistryInterestBranch_id == RSRIB.realStateRegistryInterest_branch_id


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


                    realStateRegistryInterestReports_subject_name = RSRIS.realStateRegistryInterestReports_subject_name,
                    realStateRegistryInterest_reports_Date= RSRIR.realStateRegistryInterest_reports_Date,
                    realStateRegistryInterest_reports_price= RSRIR.realStateRegistryInterest_reports_price,
                    realStateRegistryInterest_reports_number= RSRIR.realStateRegistryInterest_reports_number,

                    realStateRegistryInterest_popularContract_name= RSRIP.realStateRegistryInterest_popularContract_name,
                    realStateRegistryInterestPropertyTaxes_number= RSRIT.realStateRegistryInterestPropertyTaxes_number,
                    AreasAuthorityRequest_number=area.AreasAuthorityRequest_number,
                    realStateRegistryInterest_branch_name=RSRIB.realStateRegistryInterest_branch_name,
                    citizen_job_title=ct.citizen_job_title,







                }
                );;














            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;



            return Json(addresCiti, JsonRequestBehavior.AllowGet);
        }




        public JsonResult RSRIData(int citi)
        {




            var data = db.Citizens.Find(citi);






            var RSRICiti = (
                from ct in db.Citizens
                from RSRI in db.RealStateRegistryInterests
                from area in db.AreasAuthorityRequests
                from RSRIB in db.RealStateRegistryInterestBranches
                from RSRIT in db.RealStateRegistryInterestPropertyTaxes
                from RSRIP in db.RealStateRegistryInterestPopularContracts
                from RSRIS in db.RealStateRegistryInterestReportsSubjects
                from RSRIR in db.RealStateRegistryInterestReports
                from RSRID in db.RealStateRegistryInterestDepartments
                from RSRIspecific in db.RSRISpecificRegistries
                from RSRIpersonal in db.RSRIPersonalRegistries
                from dis in db.Districts.Where(a => a.district_id == RSRI.realStateRegistryInterest_address &&
                RSRI.realStateRegistryInterest_Departments_id == RSRID.realStateRegistryInterestDepartments_id &&
                RSRIB.realStateRegistryInterest_branch_realstate_id == RSRI.realStateRegistryInterest_id &&
                RSRIB.realStateRegistryInterest_branch_district_id == a.district_id &&
                RSRIB.realStateRegistryInterest_branch_technical_member_id == ct.citizen_id &&
                area.AreasAuthorityRequests_District_id == a.district_id &&
                RSRIR.realStateRegistryInterest_reports_subject == RSRIS.realStateRegistryInterestReports_subject_id &&
                RSRIR.realStateRegistryInterest_reports_vendor_id == ct.citizen_id &&
                ct.citizen_id==citi&&
                RSRIR.realStateRegistryInterest_reports_vendor_id == citi &&
                RSRIR.realStateRegistryInterest_reports_buyer_id == ct.citizen_id &&
                RSRIR.realStateRegistryInterest_reports_Places_id == a.district_id &&
                RSRIR.realStateRegistryInterest_reports_popularContract_id == RSRIP.realStateRegistryInterest_popularContract_id &&
                RSRIR.realStateRegistryInterest_reports_propertyTaxes_id == RSRIT.realStateRegistryInterestPropertyTaxes_id &&
                RSRIR.realStateRegistryInterest_reports_request == area.AreasAuthorityRequest_id &&
                RSRIR.realStateRegistryInterestBranch_id == RSRIB.realStateRegistryInterest_branch_id





                )
                select new
                {

                    realStateRegistryInterestReports_subject_name = RSRIS.realStateRegistryInterestReports_subject_name


                }
                ) ;














            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;



            return Json(RSRICiti, JsonRequestBehavior.AllowGet);
        }








        // GET: RealStateRegistryInterestReports/Create
        public ActionResult Create()
        {
            ViewBag.realStateRegistryInterest_reports_request = new SelectList(db.AreasAuthorityRequests, "AreasAuthorityRequest_id", "AreasAuthorityRequest_number");
            ViewBag.realStateRegistryInterest_reports_vendor_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id");
            ViewBag.realStateRegistryInterest_reports_buyer_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id");
            ViewBag.realStateRegistryInterest_reports_Places_id = new SelectList(db.Districts, "district_id", "district_name");
            ViewBag.realStateRegistryInterestBranch_id = new SelectList(db.RealStateRegistryInterestBranches, "realStateRegistryInterest_branch_id", "realStateRegistryInterest_branch_name");
            ViewBag.realStateRegistryInterest_reports_popularContract_id = new SelectList(db.RealStateRegistryInterestPopularContracts, "realStateRegistryInterest_popularContract_id", "realStateRegistryInterest_popularContract_name");
            ViewBag.realStateRegistryInterest_reports_propertyTaxes_id = new SelectList(db.RealStateRegistryInterestPropertyTaxes, "realStateRegistryInterestPropertyTaxes_id", "realStateRegistryInterestPropertyTaxes_number");
            ViewBag.realStateRegistryInterest_reports_subject = new SelectList(db.RealStateRegistryInterestReportsSubjects, "realStateRegistryInterestReports_subject_id", "realStateRegistryInterestReports_subject_name");
            return View();
        }

        // POST: RealStateRegistryInterestReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "realStateRegistryInterest_reports_id,realStateRegistryInterest_reports_subject,realStateRegistryInterest_reports_vendor_id,realStateRegistryInterest_reports_buyer_id,realStateRegistryInterest_reports_Places_id,realStateRegistryInterest_reports_popularContract_id,realStateRegistryInterest_reports_propertyTaxes_id,realStateRegistryInterest_reports_price,realStateRegistryInterest_reports_request,realStateRegistryInterest_reports_number,realStateRegistryInterest_reports_Date,realStateRegistryInterestBranch_id")] RealStateRegistryInterestReport realStateRegistryInterestReport)
        {
            if (ModelState.IsValid)
            {
                db.RealStateRegistryInterestReports.Add(realStateRegistryInterestReport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.realStateRegistryInterest_reports_request = new SelectList(db.AreasAuthorityRequests, "AreasAuthorityRequest_id", "AreasAuthorityRequest_number", realStateRegistryInterestReport.realStateRegistryInterest_reports_request);
            ViewBag.realStateRegistryInterest_reports_vendor_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", realStateRegistryInterestReport.realStateRegistryInterest_reports_vendor_id);
            ViewBag.realStateRegistryInterest_reports_buyer_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", realStateRegistryInterestReport.realStateRegistryInterest_reports_buyer_id);
            ViewBag.realStateRegistryInterest_reports_Places_id = new SelectList(db.Districts, "district_id", "district_name", realStateRegistryInterestReport.realStateRegistryInterest_reports_Places_id);
            ViewBag.realStateRegistryInterestBranch_id = new SelectList(db.RealStateRegistryInterestBranches, "realStateRegistryInterest_branch_id", "realStateRegistryInterest_branch_name", realStateRegistryInterestReport.realStateRegistryInterestBranch_id);
            ViewBag.realStateRegistryInterest_reports_popularContract_id = new SelectList(db.RealStateRegistryInterestPopularContracts, "realStateRegistryInterest_popularContract_id", "realStateRegistryInterest_popularContract_name", realStateRegistryInterestReport.realStateRegistryInterest_reports_popularContract_id);
            ViewBag.realStateRegistryInterest_reports_propertyTaxes_id = new SelectList(db.RealStateRegistryInterestPropertyTaxes, "realStateRegistryInterestPropertyTaxes_id", "realStateRegistryInterestPropertyTaxes_number", realStateRegistryInterestReport.realStateRegistryInterest_reports_propertyTaxes_id);
            ViewBag.realStateRegistryInterest_reports_subject = new SelectList(db.RealStateRegistryInterestReportsSubjects, "realStateRegistryInterestReports_subject_id", "realStateRegistryInterestReports_subject_name", realStateRegistryInterestReport.realStateRegistryInterest_reports_subject);
            return View(realStateRegistryInterestReport);
        }

        // GET: RealStateRegistryInterestReports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealStateRegistryInterestReport realStateRegistryInterestReport = db.RealStateRegistryInterestReports.Find(id);
            if (realStateRegistryInterestReport == null)
            {
                return HttpNotFound();
            }
            ViewBag.realStateRegistryInterest_reports_request = new SelectList(db.AreasAuthorityRequests, "AreasAuthorityRequest_id", "AreasAuthorityRequest_number", realStateRegistryInterestReport.realStateRegistryInterest_reports_request);
            ViewBag.realStateRegistryInterest_reports_vendor_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", realStateRegistryInterestReport.realStateRegistryInterest_reports_vendor_id);
            ViewBag.realStateRegistryInterest_reports_buyer_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", realStateRegistryInterestReport.realStateRegistryInterest_reports_buyer_id);
            ViewBag.realStateRegistryInterest_reports_Places_id = new SelectList(db.Districts, "district_id", "district_name", realStateRegistryInterestReport.realStateRegistryInterest_reports_Places_id);
            ViewBag.realStateRegistryInterestBranch_id = new SelectList(db.RealStateRegistryInterestBranches, "realStateRegistryInterest_branch_id", "realStateRegistryInterest_branch_name", realStateRegistryInterestReport.realStateRegistryInterestBranch_id);
            ViewBag.realStateRegistryInterest_reports_popularContract_id = new SelectList(db.RealStateRegistryInterestPopularContracts, "realStateRegistryInterest_popularContract_id", "realStateRegistryInterest_popularContract_name", realStateRegistryInterestReport.realStateRegistryInterest_reports_popularContract_id);
            ViewBag.realStateRegistryInterest_reports_propertyTaxes_id = new SelectList(db.RealStateRegistryInterestPropertyTaxes, "realStateRegistryInterestPropertyTaxes_id", "realStateRegistryInterestPropertyTaxes_number", realStateRegistryInterestReport.realStateRegistryInterest_reports_propertyTaxes_id);
            ViewBag.realStateRegistryInterest_reports_subject = new SelectList(db.RealStateRegistryInterestReportsSubjects, "realStateRegistryInterestReports_subject_id", "realStateRegistryInterestReports_subject_name", realStateRegistryInterestReport.realStateRegistryInterest_reports_subject);
            return View(realStateRegistryInterestReport);
        }

        // POST: RealStateRegistryInterestReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "realStateRegistryInterest_reports_id,realStateRegistryInterest_reports_subject,realStateRegistryInterest_reports_vendor_id,realStateRegistryInterest_reports_buyer_id,realStateRegistryInterest_reports_Places_id,realStateRegistryInterest_reports_popularContract_id,realStateRegistryInterest_reports_propertyTaxes_id,realStateRegistryInterest_reports_price,realStateRegistryInterest_reports_request,realStateRegistryInterest_reports_number,realStateRegistryInterest_reports_Date,realStateRegistryInterestBranch_id")] RealStateRegistryInterestReport realStateRegistryInterestReport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(realStateRegistryInterestReport).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.realStateRegistryInterest_reports_request = new SelectList(db.AreasAuthorityRequests, "AreasAuthorityRequest_id", "AreasAuthorityRequest_number", realStateRegistryInterestReport.realStateRegistryInterest_reports_request);
            ViewBag.realStateRegistryInterest_reports_vendor_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", realStateRegistryInterestReport.realStateRegistryInterest_reports_vendor_id);
            ViewBag.realStateRegistryInterest_reports_buyer_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", realStateRegistryInterestReport.realStateRegistryInterest_reports_buyer_id);
            ViewBag.realStateRegistryInterest_reports_Places_id = new SelectList(db.Districts, "district_id", "district_name", realStateRegistryInterestReport.realStateRegistryInterest_reports_Places_id);
            ViewBag.realStateRegistryInterestBranch_id = new SelectList(db.RealStateRegistryInterestBranches, "realStateRegistryInterest_branch_id", "realStateRegistryInterest_branch_name", realStateRegistryInterestReport.realStateRegistryInterestBranch_id);
            ViewBag.realStateRegistryInterest_reports_popularContract_id = new SelectList(db.RealStateRegistryInterestPopularContracts, "realStateRegistryInterest_popularContract_id", "realStateRegistryInterest_popularContract_name", realStateRegistryInterestReport.realStateRegistryInterest_reports_popularContract_id);
            ViewBag.realStateRegistryInterest_reports_propertyTaxes_id = new SelectList(db.RealStateRegistryInterestPropertyTaxes, "realStateRegistryInterestPropertyTaxes_id", "realStateRegistryInterestPropertyTaxes_number", realStateRegistryInterestReport.realStateRegistryInterest_reports_propertyTaxes_id);
            ViewBag.realStateRegistryInterest_reports_subject = new SelectList(db.RealStateRegistryInterestReportsSubjects, "realStateRegistryInterestReports_subject_id", "realStateRegistryInterestReports_subject_name", realStateRegistryInterestReport.realStateRegistryInterest_reports_subject);
            return View(realStateRegistryInterestReport);
        }

        // GET: RealStateRegistryInterestReports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealStateRegistryInterestReport realStateRegistryInterestReport = db.RealStateRegistryInterestReports.Find(id);
            if (realStateRegistryInterestReport == null)
            {
                return HttpNotFound();
            }
            return View(realStateRegistryInterestReport);
        }

        // POST: RealStateRegistryInterestReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RealStateRegistryInterestReport realStateRegistryInterestReport = db.RealStateRegistryInterestReports.Find(id);
            db.RealStateRegistryInterestReports.Remove(realStateRegistryInterestReport);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Export()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report/CrystalReportBirth.rpt")));
            rd.SetDataSource(db.States.Select(a => new
            {
                StateName = a.state_name
            }

            ).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream st = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            st.Seek(0, SeekOrigin.Begin);
            return File(st, "application/pdf", "A7laState.pdf");
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
