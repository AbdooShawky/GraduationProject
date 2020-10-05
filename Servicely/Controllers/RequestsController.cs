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
    public class RequestsController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: Requests
        public ActionResult Index()
        {
            ViewBag.request_citizenId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
            
            var requests = db.Requests.Include(r => r.Citizen).Include(a => a.governement_body).Include(a=> a.service1).Include(a=>a.TypeRequest1);

            return View(requests.ToList());
        }

        public ActionResult IndexToday()
        {
            ViewBag.request_citizenId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");

            
            var requests = db.Requests.Where(a=>a.Is_Deleted != true && a.date.Value.Year == DateTime.Now.Year && a.date.Value.Month == DateTime.Now.Month && a.date.Value.Day== DateTime.Now.Day).Include(r => r.Citizen).Include(a => a.governement_body).Include(a => a.service1).Include(a => a.TypeRequest1);

            return View(requests.ToList());
        }
        [HttpGet]
        public ActionResult Indexuser()
        {
            ViewBag.request_citizenId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
            int id =  (int)Session["citizenID"];
            var requests = db.Requests.Include(r => r.Citizen).Include(a => a.TypeRequest1).Where(a=> a.request_citizenId == id).Include(a=>a.governement_body).Include(a=>a.service1);
            return View(requests.ToList());
        }
        public ActionResult Index1()
        {

            ViewBag.requestType = new SelectList(db.TypeRequests, "typeReaquest_id", "typeReaquest_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.requestType = new SelectList(db.TypeRequests, "typeReaquest_id", "typeReaquest_name_arabic");
                }
            }



            return View();
        }
        public JsonResult requestType(int id)
        {
            var data = db.Requests.Where(a => a.typeRequest == id).Join(db.Citizens,a=>a.request_citizenId,b=>b.citizen_id,(a,b)=>new { a,b}).Join(db.services,a=>a.a.service,b=>b.service_id,(a,b)=>new {a, b}).Select(a=>new 
            {
                a.a.a.Citizen.citizen_national_id,
                a.a.a.Citizen.citizen_first_name,
                a.a.a.Citizen.citizen_second_name,
                a.a.a.Citizen.citizen_third_name,
                a.a.a.Citizen.citizen_fourth_name,
                a.a.a.quantity,
                a.a.a.language,
                a.a.a.address,
                a.a.a.governmentAgency,
                a.a.a.date,
                a.b.service_name,
                a.a.a.Citizen.citizen_first_name_arabic,
                a.a.a.Citizen.citizen_second_name_arabic,
                a.a.a.Citizen.citizen_third_name_arabic,
                a.a.a.Citizen.citizen_fourth_name_arabic,
                a.a.a.language_arabic,
                a.a.a.address_arabic,
                a.b.service_name_arabic

            }).ToList();
            db.Configuration.ProxyCreationEnabled = false;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Index(int RTId, int RId)
        {
            if (RTId == 5)
            {
                ViewBag.request_citizenId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");

                var requests1 = db.Requests.Include(r => r.Citizen).Include(a => a.service1).Include(a => a.TypeRequest1).Include(a => a.governement_body);

                return View(requests1.ToList());
            }
            var data = db.TypeRequests.Find(RTId);

            int NextId = data.typeReaquest_id;
            NextId ++;
            var next = db.TypeRequests.Find(NextId);

            var req = db.Requests.Find(RId);
            //--------------- LogRequest table ----------------
            LogRequest rr = new LogRequest();
            rr.log_citizenId = req.request_citizenId;
            rr.log_requestId = req.request_id;

            rr.log_request_typeId = req.typeRequest;
            rr.log_date = DateTime.Now.Date;

            
            if ( next == null)
            {
                ViewBag.err = Languages.Language.disabled;
                var requestss = db.Requests.Include(r => r.Citizen).Include(a => a.TypeRequest1).Include(a=> a.service1).Include(a => a.governement_body);
                return View(requestss.ToList());
               
            }


            
            req.typeRequest = next.typeReaquest_id;



            db.LogRequests.Add(rr);
            
            db.SaveChanges();

            ViewBag.request_citizenId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");

            var requests = db.Requests.Include(r => r.Citizen).Include(a => a.service1).Include(a => a.TypeRequest1).Include(a => a.governement_body);
           
            return View(requests.ToList());
        }
        [HttpPost]
        public ActionResult IndexToday(int RTId, int RId)
        {
            if (RTId == 5)
            {
                ViewBag.request_citizenId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");

                var requests1 = db.Requests.Where(a => a.Is_Deleted != true && a.date.Value.Year == DateTime.Now.Year && a.date.Value.Month == DateTime.Now.Month && a.date.Value.Day == DateTime.Now.Day).Include(r => r.Citizen).Include(a => a.service1).Include(a => a.TypeRequest1).Include(a => a.governement_body);

                return View(requests1.ToList());
            }
            var data = db.TypeRequests.Find(RTId);

            int NextId = data.typeReaquest_id;
            NextId++;
            var next = db.TypeRequests.Find(NextId);

            var req = db.Requests.Find(RId);
            //--------------- LogRequest table ----------------
            LogRequest rr = new LogRequest();
            rr.log_citizenId = req.request_citizenId;
            rr.log_requestId = req.request_id;

            rr.log_request_typeId = req.typeRequest;
            rr.log_date = DateTime.Now.Date;


            if (next == null)
            {
                ViewBag.err = Languages.Language.disabled;
                var requestss = db.Requests.Where(a => a.Is_Deleted != true && a.date.Value.Year == DateTime.Now.Year && a.date.Value.Month == DateTime.Now.Month && a.date.Value.Day == DateTime.Now.Day).Include(r => r.Citizen).Include(a => a.TypeRequest1).Include(a => a.service1).Include(a => a.governement_body);
                return View(requestss.ToList());

            }



            req.typeRequest = next.typeReaquest_id;



            db.LogRequests.Add(rr);

            db.SaveChanges();

            ViewBag.request_citizenId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");

            var requests = db.Requests.Where(a => a.Is_Deleted != true && a.date.Value.Year == DateTime.Now.Year && a.date.Value.Month == DateTime.Now.Month && a.date.Value.Day == DateTime.Now.Day).Include(r => r.Citizen).Include(a => a.service1).Include(a => a.TypeRequest1).Include(a => a.governement_body);

            return View(requests.ToList());
        }
        public ActionResult IndexUser()
        {
            ViewBag.request_citizenId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
            int cid = (int)Session["citizenID"];
            var requests = db.Requests.Include(r => r.Citizen).Where(a=> a.request_citizenId == cid);
            return View(requests.ToList());
        }


        public JsonResult GetRequestsByCitizenId( int cid )
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.Requests.Where(a => a.request_citizenId == cid);

            List<string> dd = new List<string>();

           
            
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        // GET: Requests/Create
        public ActionResult Create()        {

            //if (Session["citizenID"] != null)
            //{
            //    return RedirectToAction("errorPage","home");
            //}

            ViewBag.governmentAgency = new SelectList(db.governement_body, "id", "governement_name");
            ViewBag.service = new SelectList(db.services,"service_id","service_name");
            ViewBag.request_citizenId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");



            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {

                    ViewBag.governmentAgency = new SelectList(db.governement_body.Where(a => a.governement_isDeleted != true), "id", "governement_name_arabic");
                    ViewBag.service = new SelectList(db.services, "service_id", "service_name_arabic");
                    ViewBag.request_citizenId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");


                }
            }

            return View();
        }

       
        [HttpPost]
       
        public ActionResult Create(Request request)
        {
            int cid=0;
            //var data = db.Citizens.Where(a=> a.citizen_national_id == request.request_citizenId).;
            if(Session["citizenID"] != null)
            {
                cid = (int)Session["citizenID"];
            }
           

                var data = db.Requests.Where(a => a.service == request.service && a.governmentAgency == request.governmentAgency && a.typeRequest!= 5 && a.request_citizenId == cid).FirstOrDefault();

            if(data != null )
            {
                ViewBag.governmentAgency = new SelectList(db.governement_body.Where(a=> a.governement_isDeleted!= true), "id", "governement_name");
                ViewBag.service = new SelectList(db.services, "service_id", "service_name");
                ViewBag.request_citizenId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");



                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        
                        ViewBag.governmentAgency = new SelectList(db.governement_body.Where(a => a.governement_isDeleted != true), "id", "governement_name_arabic");
                        ViewBag.service = new SelectList(db.services, "service_id", "service_name_arabic");
                        ViewBag.request_citizenId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");


                    }
                }

                ViewBag.requeserr = Languages.Language.requestErr;
                return View();
               
            }
           

                request.date = DateTime.Now;
            request.TransactionDate = DateTime.Now;
                request.request_citizenId = (int)Session["citizenID"];
                request.typeRequest = 1;
                db.Requests.Add(request);
                db.SaveChanges();
                return RedirectToAction("IndexUser");

            
            //ViewBag.request_citizenId = new SelectList(db.Citizens.Where(a=> a.citizen_isDeleted!= true), "citizen_id", "citizen_national_id", request.request_citizenId);
            //return View(request);
        }

        public JsonResult GetAllServicesByGovernmentBoduId(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            
            var data =db.services.Where(a => a.goverenemnt_id == id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // GET: Requests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            ViewBag.request_citizenId = new SelectList(db.Citizens.Where(a=> a.citizen_isDeleted!= true), "citizen_id", "citizen_national_id", request.request_citizenId);
            return View(request);
        }

       
        [HttpPost]
       
        public ActionResult Edit(Request request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.request_citizenId = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", request.request_citizenId);
            return View(request);
        }

        // GET: Requests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Request request = db.Requests.Find(id);
           // var old = db.Requests.Find(id);
            
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
