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
    public class RespondsController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: Responds
        public ActionResult Index()
        {
            var responds = db.Responds.Where(a=> a.Is_Deleted!= true && a.Date.Value.Year == DateTime.Now.Year && a.Date.Value.Month == DateTime.Now.Month && a.Date.Value.Day == DateTime.Now.Day).Include(r => r.Complain);
            return View(responds.ToList());
        }

        // GET: Responds
        public ActionResult IndexUser()
        {
            if (Session["citizenID"] != null)
            {
                int c = (int)Session["citizenID"];
                var responds = db.Responds.Where(a => a.Is_Deleted != true && a.Complain.CitizenId ==c ).Include(r => r.Complain);
                return View(responds.ToList());
            }

            return RedirectToAction("errorPage", "home");
        }

        // GET: Responds/Create
        public ActionResult Create( int Id)
        {
            Session["ComplainsId"] = Id;
           // ViewBag.ComplainsId = new SelectList(db.Complains, "Id", "CitizenNationalId");
            return View();
        }

       
        [HttpPost]
        
        public ActionResult Create( Respond respond)
        {
            if (ModelState.IsValid)
            {
                if(Session["ComplainsId"] != null)
                {
                 
                    respond.Date = DateTime.Now;
                    respond.ComplainsId = (int) Session["ComplainsId"];
                    db.Responds.Add(respond);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("errorPage", "home");
            }

           // ViewBag.ComplainsId = new SelectList(db.Complains, "Id", "CitizenNationalId", respond.ComplainsId);
            return View(respond);
        }

        // GET: Responds/Edit/5
        public ActionResult Edit(int? id)
        {
           if (id == null)
            {
                return RedirectToAction("errorPage", "home");
            }
            Respond respond = db.Responds.Find(id);
            if (respond == null)
            {
                return RedirectToAction("errorPage", "home");
            }
            //ViewBag.ComplainsId = new SelectList(db.Complains, "Id", "CitizenNationalId", respond.ComplainsId);
            return View(respond);
        }


        [HttpPost]
     
        public ActionResult Edit(Respond respond)
        {
            if (ModelState.IsValid)
            {
                var old = db.Responds.Find(respond.Id);
                old.ComplainsId = respond.ComplainsId;
                old.Date = DateTime.Now;
                old.RespondText = respond.RespondText;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(respond);
        }

        // GET: Responds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorPage", "home");
            }
            Respond respond = db.Responds.Find(id);
            if (respond == null)
            {
                return RedirectToAction("errorPage", "home");
            }
            return View(respond);
        }

        // POST: Responds/Delete/5
        [HttpPost, ActionName("Delete")]
      
        public ActionResult DeleteConfirmed(int id)
        {
            Respond respond = db.Responds.Find(id);
            respond.Is_Deleted = true;
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
