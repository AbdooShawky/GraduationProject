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
    public class CertificateTypesController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: CertificateTypes
        public ActionResult Index()
        {
            return View(db.Certificates.Where(a=> a.Is_Deleted!= true).ToList());
        }

    

        // GET: CertificateTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CertificateTypes/Create
        
        [HttpPost]
   
        public ActionResult Create(Certificate  certificate)
        {
            if (ModelState.IsValid)
            {
                var data = db.Certificates.Where(a => a.CirtificateTypeName == certificate.CirtificateTypeName).SingleOrDefault();
                if(data != null)
                {
                    ViewBag.school = Languages.Language.SchoolErr;
                    return View(certificate);
                }

                db.Certificates.Add(certificate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(certificate);
        }

        // GET: CertificateTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            Certificate certificateType = db.Certificates.Find(id);
            if (certificateType == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            return View(certificateType);
        }

        // POST: CertificateTypes/Edit/5
        
        [HttpPost]
       
        public ActionResult Edit( Certificate certificateType)
        {
            if (ModelState.IsValid)
            {
                var data = db.Certificates.Where(a => a.Id != certificateType.Id && a.Is_Deleted != true);
                foreach (var item in data)
                {
                    if (item.CirtificateTypeName == certificateType.CirtificateTypeName)
                    {
                        ViewBag.school = Languages.Language.SchoolErr;
                        return View(certificateType);
                    }
                }

                var old = db.Certificates.Find(certificateType.Id);
                old.CirtificateTypeName = certificateType.CirtificateTypeName;
                old.CirtificateTypeNameArabic = certificateType.CirtificateTypeNameArabic;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(certificateType);
        }

        // GET: CertificateTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            Certificate certificateType = db.Certificates.Find(id);
            if (certificateType == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            return View(certificateType);
        }

        // POST: CertificateTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            Certificate certificateType = db.Certificates.Find(id);
            certificateType.Is_Deleted = true;
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
