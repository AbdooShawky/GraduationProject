using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicely.Models;
using System.Data.Entity;

namespace Servicely.Controllers
{
    public class Live_Status_TypeController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: Document_Type
        public ActionResult Index()
        {
            // View(db.Live_Status_Type.ToList());


            return View(db.Live_Status_Type.Where(a => a.live_status_type_isDeleted != true).ToList());
        }



        // GET: Document_Type/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Document_Type/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]

        public ActionResult Create(Live_Status_Type l)
        {
            var data = db.Live_Status_Type.Where(a => a.live_status_type_name == l.live_status_type_name &&a.live_status_type_isDeleted !=true).SingleOrDefault();
            if (data != null)
            {
                ViewBag.errMsgLive = @Servicely.Languages.Language.LiveStatus_type__already_exist;
                return View(l);
            }

            db.Live_Status_Type.Add(l);
            db.SaveChanges();
            return RedirectToAction("Index");



        }

        // GET: Document_Type/Edit/5
        public ActionResult Edit(int id)
        {

            Live_Status_Type l = db.Live_Status_Type.Find(id);

            return View(l);
        }

        // POST: Document_Type/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]

        public ActionResult Edit(Live_Status_Type l)
        {

            var data = db.Live_Status_Type.Where(a => a.live_status_type_name == l.live_status_type_name && a.live_status_type_isDeleted != true &&l.live_status_type_id != a.live_status_type_id).SingleOrDefault();
            if (data != null)
            {
                ViewBag.errMsgLive = @Servicely.Languages.Language.LiveStatus_type__already_exist;
                return View(l);
            }


            db.Entry(l).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");


        }

        // GET: Document_Type/Delete/5
        public ActionResult Delete(int id)
        {

            Live_Status_Type l = db.Live_Status_Type.Find(id);

            return View(l);
        }

        // POST: Document_Type/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            var old = db.Live_Status_Type.Find(id);
            old.live_status_type_isDeleted = true;

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            //your handling logic here
            filterContext.ExceptionHandled = true;
            filterContext.Result = RedirectToAction("errorpage", "home");
        }
    }
}