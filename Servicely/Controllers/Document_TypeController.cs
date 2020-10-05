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
    public class Document_TypeController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: Document_Type
        public ActionResult Index()
        {
           


            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    return View(db.Document_Type.Where(a => a.document_type_isDeleted != true).ToList());
                }
            }

            return View(db.Document_Type.Where(a => a.document_type_isDeleted != true).ToList());
        }



        // GET: Document_Type/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Document_Type/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]

        public ActionResult Create(Document_Type document_Type)
        {
            var data = db.Document_Type.Where(a => a.document_type_name == document_Type.document_type_name && a.document_type_isDeleted!=true).SingleOrDefault();
            if (data != null)
            {
                ViewBag.errMsg = Languages.Language.document_type_already_exist;
                return View(document_Type);
            }

            db.Document_Type.Add(document_Type);
            db.SaveChanges();
            return RedirectToAction("Index");



        }

        // GET: Document_Type/Edit/5
        public ActionResult Edit(int id)
        {

            Document_Type document_Type = db.Document_Type.Find(id);

            return View(document_Type);
        }

        // POST: Document_Type/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]

        public ActionResult Edit(Document_Type d)
        {
            var data = db.Document_Type.Where(a => a.document_type_name == d.document_type_name && a.document_type_isDeleted != true &&a.document_type_id != d.document_type_id).SingleOrDefault();
            if (data != null)
            {
                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.errMsg = d.document_type_name+" " + Servicely.Languages.Language.document_type_already_exist;
                        return View(d);
                    }
                }
                ViewBag.errMsg = d.document_type_name+" " + Servicely.Languages.Language.document_type_already_exist;
                return View(d);
            }
            var old = db.Document_Type.Find(d.document_type_id);
            old.document_type_name = d.document_type_name;
            old.document_type_name_arabic = d.document_type_name_arabic;

            old.document_type_description = d.document_type_description;
            old.document_type_description_arabic = d.document_type_description_arabic;

            db.SaveChanges();
            return RedirectToAction("Index");


        }

        // GET: Document_Type/Delete/5
        public ActionResult Delete(int id)
        {

            Document_Type document_Type = db.Document_Type.Find(id);

            return View(document_Type);
        }

        // POST: Document_Type/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            var old = db.Document_Type.Find(id);
            old.document_type_isDeleted = true;

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
