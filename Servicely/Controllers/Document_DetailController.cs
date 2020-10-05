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
    public class Document_DetailController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: Document_Detail
        public ActionResult Index()
        {
            var document_Detail = db.Document_Detail.Include(d => d.Document).Include(d => d.Citizen);
            return View(document_Detail.ToList());
        }



        // GET: Document_Detail/Create
        public ActionResult Create()
        {
            ViewBag.documentDetail_document_id = new SelectList(db.Documents, "document_id", "document_data");
            ViewBag.documentDetail_citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id");
            return View();
        }


        [HttpPost]

        public ActionResult Create(Document_Detail document_Detail)
        {

            db.Document_Detail.Add(document_Detail);
            db.SaveChanges();
            return RedirectToAction("Index");

        }


        public ActionResult Edit(int id)
        {

            Document_Detail document_Detail = db.Document_Detail.Find(id);

            ViewBag.documentDetail_document_id = new SelectList(db.Documents, "document_id", "document_data", document_Detail.documentDetail_document_id);
            ViewBag.documentDetail_citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", document_Detail.documentDetail_citizen_id);
            return View(document_Detail);
        }


        [HttpPost]

        public ActionResult Edit(Document_Detail document_Detail)
        {

            db.Entry(document_Detail).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");


        }


        public ActionResult Delete(int id)
        {

            Document_Detail document_Detail = db.Document_Detail.Find(id);

            return View(document_Detail);
        }

        // POST: Document_Detail/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            Document_Detail document_Detail = db.Document_Detail.Find(id);
            db.Document_Detail.Remove(document_Detail);
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
