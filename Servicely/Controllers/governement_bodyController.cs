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
    public class governement_bodyController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: governement_body
        public ActionResult Index()
        {
            return View(db.governement_body.Where(a=> a.governement_isDeleted!=true).ToList());
        }

        // GET: governement_body/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            governement_body governement_body = db.governement_body.Find(id);
            if (governement_body == null)
            {
                return HttpNotFound();
            }
            return View(governement_body);
        }

        // GET: governement_body/Create
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
       
        public ActionResult Create( governement_body governement_body)
        {
            if (ModelState.IsValid)
            {
                var data = db.governement_body.Where(a => a.governement_name == governement_body.governement_name).SingleOrDefault();

                if(data != null)
                {
                    ViewBag.errx = Languages.Language.governmenterr;
                    return View(governement_body);
                }

                db.governement_body.Add(governement_body);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(governement_body);
        }

        // GET: governement_body/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            governement_body governement_body = db.governement_body.Find(id);
            if (governement_body == null)
            {
                return HttpNotFound();
            }
            return View(governement_body);
        }

        
        [HttpPost]
  
        public ActionResult Edit( governement_body governement_body)
        {
            if (ModelState.IsValid)
            {

                var data = db.governement_body.Where(a => a.id != governement_body.id);

                foreach (var item in data)
                {
                    if(item.governement_name == governement_body.governement_name)
                    {
                        ViewBag.errx = Languages.Language.governmenterr;
                        return View(governement_body);
                    }
                }
                var old = db.governement_body.Find(governement_body.id);
                old.governement_name = governement_body.governement_name;
                old.governement_name_arabic = governement_body.governement_name_arabic;
               
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(governement_body);
        }

        // GET: governement_body/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            governement_body governement_body = db.governement_body.Find(id);
            if (governement_body == null)
            {
                return HttpNotFound();
            }
            return View(governement_body);
        }

        // POST: governement_body/Delete/5
        [HttpPost, ActionName("Delete")]
  
        public ActionResult DeleteConfirmed(int id)
        {
            governement_body governement_body = db.governement_body.Find(id);
            governement_body.governement_isDeleted = true;
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
