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
    public class PhasesOfSchoolesController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: PhasesOfSchooles
        public ActionResult Index()
        {
            return View(db.PhasesOfSchooles.Where(a=> a.Is_Deleted!= true).ToList());
        }

        

        // GET: PhasesOfSchooles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhasesOfSchooles/Create
        
        [HttpPost]
  
        public ActionResult Create(PhasesOfSchoole phasesOfSchoole)
        {
            if (ModelState.IsValid)
            {
                var data = db.PhasesOfSchooles.Where(a=> a.Is_Deleted!= true && a.PhaseName == phasesOfSchoole.PhaseName).SingleOrDefault();
                if( data != null)
                {
                    ViewBag.errPhase = Languages.Language.errPhase;
                    return View(phasesOfSchoole);
                }
                db.PhasesOfSchooles.Add(phasesOfSchoole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(phasesOfSchoole);
        }

        // GET: PhasesOfSchooles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            PhasesOfSchoole phasesOfSchoole = db.PhasesOfSchooles.Find(id);
            if (phasesOfSchoole == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            return View(phasesOfSchoole);
        }

       
        [HttpPost]
    
        public ActionResult Edit( PhasesOfSchoole phasesOfSchoole)
        {
            if (ModelState.IsValid)
            {

                var data = db.PhasesOfSchooles.Where(a => a.Id != phasesOfSchoole.Id && a.Is_Deleted != true);
                foreach (var item in data)
                {
                    if (item.PhaseName == phasesOfSchoole.PhaseName)
                    {
                        ViewBag.errPhase = Languages.Language.errPhase;
                        return View(phasesOfSchoole);
                    }
                }

                var old = db.PhasesOfSchooles.Find(phasesOfSchoole.Id);
                old.PhaseName = phasesOfSchoole.PhaseName;
                old.PhaseNameArabic = phasesOfSchoole.PhaseNameArabic;
                old.Year = phasesOfSchoole.Year;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(phasesOfSchoole);
        }

        // GET: PhasesOfSchooles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            PhasesOfSchoole phasesOfSchoole = db.PhasesOfSchooles.Find(id);
            if (phasesOfSchoole == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            return View(phasesOfSchoole);
        }

        // POST: PhasesOfSchooles/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PhasesOfSchoole phasesOfSchoole = db.PhasesOfSchooles.Find(id);
            phasesOfSchoole.Is_Deleted = true;
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
