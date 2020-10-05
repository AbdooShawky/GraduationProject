//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using Servicely.Models;

//namespace Servicely.Controllers
//{
//    public class YearOfPhasesController : BaseController
//    {
//        private DbMasterEntities1 db = new DbMasterEntities1();

//        // GET: YearOfPhases
//        public ActionResult Index()
//        {
//            ViewBag.PhaseId = new SelectList(db.PhasesOfSchooles, "Id", "PhaseName");
//            if (Session["lang"] != null)
//            {
//                if (Session["lang"].ToString().Equals("ar-EG"))
//                {

//                    ViewBag.PhaseId = new SelectList(db.PhasesOfSchooles, "Id", "PhaseNameArabic");

//                }

//            }
//            var yearOfPhases = db.YearOfPhases.Where(a=> a.Is_Deleted!= true).Include(y => y.PhasesOfSchoole);
//            return View(yearOfPhases.ToList());
//        }

       
//        // GET: YearOfPhases/Create
//        public ActionResult Create()
//        {
//            ViewBag.PhaseId = new SelectList(db.PhasesOfSchooles, "Id", "PhaseName");
//            if (Session["lang"] != null)
//            {
//                if (Session["lang"].ToString().Equals("ar-EG"))
//                {

//                    ViewBag.PhaseId = new SelectList(db.PhasesOfSchooles, "Id", "PhaseNameArabic");

//                }

//            }
//            return View();
//        }

     
//        [HttpPost]
      
//        public ActionResult Create( YearOfPhase yearOfPhase)
//        {
//            if (ModelState.IsValid)
//            {
//                var data = db.YearOfPhases.Where(a => a.Is_Deleted != true && a.NameOfYearePhase == yearOfPhase.NameOfYearePhase).SingleOrDefault();
//                if ( data != null)
//                {
//                    ViewBag.PhaseId = new SelectList(db.PhasesOfSchooles, "Id", "PhaseName", yearOfPhase.PhaseId);
//                    if (Session["lang"] != null)
//                    {
//                        if (Session["lang"].ToString().Equals("ar-EG"))
//                        {

//                            ViewBag.PhaseId = new SelectList(db.PhasesOfSchooles, "Id", "PhaseNameArabic" , yearOfPhase.PhaseId);

//                        }
                     
//                    }
//                    ViewBag.err = Languages.Language.NameAleadyExist;
//                    return View(yearOfPhase);
//                }
//                db.YearOfPhases.Add(yearOfPhase);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            ViewBag.PhaseId = new SelectList(db.PhasesOfSchooles, "Id", "PhaseName", yearOfPhase.PhaseId);
//            return View(yearOfPhase);
//        }

//        // GET: YearOfPhases/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return RedirectToAction("errorpage", "home");
//            }
//            YearOfPhase yearOfPhase = db.YearOfPhases.Find(id);
//            if (yearOfPhase == null)
//            {
//                return RedirectToAction("errorpage", "home");
//            }
//            ViewBag.PhaseId = new SelectList(db.PhasesOfSchooles, "Id", "PhaseName", yearOfPhase.PhaseId);
//            if (Session["lang"] != null)
//            {
//                if (Session["lang"].ToString().Equals("ar-EG"))
//                {

//                    ViewBag.PhaseId = new SelectList(db.PhasesOfSchooles, "Id", "PhaseNameArabic", yearOfPhase.PhaseId);

//                }
                
//            }
//            return View(yearOfPhase);
//        }

//        // POST: YearOfPhases/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
       
//        public ActionResult Edit( YearOfPhase yearOfPhase)
//        {
//            if (ModelState.IsValid)
//            {
//                var data = db.YearOfPhases.Where(a => a.Is_Deleted != true && a.Id != yearOfPhase.Id && a.NameOfYearePhase ==yearOfPhase.NameOfYearePhase).SingleOrDefault();
//                if ( data != null)
//                {
//                    ViewBag.PhaseId = new SelectList(db.PhasesOfSchooles, "Id", "PhaseName", yearOfPhase.PhaseId);
//                    if (Session["lang"] != null)
//                    {
//                        if (Session["lang"].ToString().Equals("ar-EG"))
//                        {

//                            ViewBag.PhaseId = new SelectList(db.PhasesOfSchooles, "Id", "PhaseNameArabic", yearOfPhase.PhaseId);

//                        }
                       
//                    }
//                    ViewBag.err = Languages.Language.NameAleadyExist;
//                    return View(yearOfPhase);
//                }

//                var old = db.YearOfPhases.Find(yearOfPhase.Id);
//                old.NameOfYearePhase = yearOfPhase.NameOfYearePhase;
//                old.NameOfYearePhaseArabic = yearOfPhase.NameOfYearePhaseArabic;
//                old.PhaseId = yearOfPhase.PhaseId;
                
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.PhaseId = new SelectList(db.PhasesOfSchooles, "Id", "PhaseName", yearOfPhase.PhaseId);
//            if (Session["lang"] != null)
//            {
//                if (Session["lang"].ToString().Equals("ar-EG"))
//                {

//                    ViewBag.PhaseId = new SelectList(db.PhasesOfSchooles, "Id", "PhaseNameArabic", yearOfPhase.PhaseId);

//                }
//                ViewBag.err = Languages.Language.NameAleadyExist;
//            }
//            return View(yearOfPhase);
//        }

//        // GET: YearOfPhases/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            YearOfPhase yearOfPhase = db.YearOfPhases.Find(id);
//            if (yearOfPhase == null)
//            {
//                return HttpNotFound();
//            }
//            return View(yearOfPhase);
//        }

//        // POST: YearOfPhases/Delete/5
//        [HttpPost, ActionName("Delete")]
      
//        public ActionResult DeleteConfirmed(int id)
//        {
//            YearOfPhase yearOfPhase = db.YearOfPhases.Find(id);
//            yearOfPhase.Is_Deleted = true;
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        // ----------- ajax call ------------
//        public JsonResult GetAllYearOfPhaseBYPhaseId(int Id)
//        {
//            db.Configuration.ProxyCreationEnabled = false;
//            var data = db.YearOfPhases.Where(a => a.Is_Deleted != true && a.PhaseId == Id);
//            return Json(data, JsonRequestBehavior.AllowGet);
//        }
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
