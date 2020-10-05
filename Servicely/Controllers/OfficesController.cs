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
    public class OfficesController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: Offices
        public ActionResult Index()
        {
            var offices = db.Offices.Include(o => o.District).Include(o => o.governement_body).Where(a=> a.office_isDeleted != true);
            return View(offices.ToList());
        }

        // GET: Offices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Office office = db.Offices.Find(id);
            if (office == null)
            {
                return HttpNotFound();
            }
            return View(office);
        }

        // GET: Offices/Create
        public ActionResult Create()
        {
            if(Session["lang"] != null)
                {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                    ViewBag.office_district_id = new SelectList(db.Districts, "district_id", "district_name_arabic");
                    ViewBag.governemet_id = new SelectList(db.governement_body, "id", "governement_name_arabic");
                    return View();

                }
                else
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

                    ViewBag.office_district_id = new SelectList(db.Districts, "district_id", "district_name");
                    ViewBag.governemet_id = new SelectList(db.governement_body, "id", "governement_name");
                    return View();
                }
            }
                else
            {
                ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

                ViewBag.office_district_id = new SelectList(db.Districts, "district_id", "district_name");
                ViewBag.governemet_id = new SelectList(db.governement_body, "id", "governement_name");
                return View();
            }

            
        }

        
        [HttpPost]
      
        public ActionResult Create(Office office)
        {
            if (ModelState.IsValid)
            {
                var data = db.Offices.Where(a => a.office_name == office.office_name && a.office_isDeleted != true).SingleOrDefault();
                if(data != null)
                {
                    if (Session["lang"] != null)
                    {
                        if (Session["lang"].ToString().Equals("ar-EG"))
                        {
                            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");
                            ViewBag.officeerr = Languages.Language.officeerr;
                            ViewBag.office_district_id = new SelectList(db.Districts, "district_id", "district_name_arabic");
                            ViewBag.governemet_id = new SelectList(db.governement_body, "id", "governement_name_arabic");
                            return View();

                        }
                        else
                        {
                            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
                            ViewBag.officeerr = Languages.Language.officeerr;
                            ViewBag.office_district_id = new SelectList(db.Districts, "district_id", "district_name");
                            ViewBag.governemet_id = new SelectList(db.governement_body, "id", "governement_name");
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
                        ViewBag.officeerr = Languages.Language.officeerr;
                        ViewBag.office_district_id = new SelectList(db.Districts, "district_id", "district_name");
                        ViewBag.governemet_id = new SelectList(db.governement_body, "id", "governement_name");
                        return View();
                    }
                    
                }

                db.Offices.Add(office);
                db.SaveChanges();
                return RedirectToAction("Index");



            }

            ViewBag.office_district_id = new SelectList(db.Districts, "district_id", "district_name", office.office_district_id);
            ViewBag.governemet_id = new SelectList(db.governement_body, "id", "governement_name", office.governemet_id);
            return View(office);
        }

        // GET: Offices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Office office = db.Offices.Find(id);
            if (office == null)
            {
                return HttpNotFound();
            }
            if(Session["lang"]!= null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                    ViewBag.office_district_id = new SelectList(db.Districts, "district_id", "district_name_arabic");
                    ViewBag.governemet_id = new SelectList(db.governement_body, "id", "governement_name_arabic");
                    return View(office);
                }
               
            }
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            ViewBag.office_district_id = new SelectList(db.Districts, "district_id", "district_name");
            ViewBag.governemet_id = new SelectList(db.governement_body, "id", "governement_name");
            return View(office);

        }

        
        [HttpPost]
      
        public ActionResult Edit(Office office)
        {
            if (ModelState.IsValid)
            {
                var data = db.Offices.Where(a => a.office_id != office.office_id && a.governemet_id != office.governemet_id);

                foreach (var item in data)
                {
                    if (item.office_name == office.office_name)
                    {
                        ViewBag.officeerr = Languages.Language.officeerr;
                        return View(office);
                    }
                }

                var old = db.Offices.Find(office.office_id);
                old.office_name = office.office_name;
                old.office_name_arabic = office.office_name_arabic;
                old.office_district_id = office.office_district_id;
                old.governemet_id = office.governemet_id;

                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            ViewBag.office_district_id = new SelectList(db.Districts, "district_id", "district_name", office.office_district_id);
            ViewBag.governemet_id = new SelectList(db.governement_body, "id", "governement_name", office.governemet_id);
            return View(office);
        }

        // GET: Offices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Office office = db.Offices.Find(id);
            if (office == null)
            {
                return HttpNotFound();
            }
            return View(office);
        }

        // POST: Offices/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            Office office = db.Offices.Find(id);
            office.office_isDeleted = true;
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
