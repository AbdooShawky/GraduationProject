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
    public class GradesCertificateM_MController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: GradesCertificateM_M
        public ActionResult Index()
        {
            var gradesCertificateM_M = db.GradesCertificateM_M.Include(g => g.Certificate).Include(g => g.Grade).Where(a => a.Is_Deleted != true);
            return View(gradesCertificateM_M.ToList());
        }

        
      

        // GET: GradesCertificateM_M/Create
        public ActionResult Create()
        {
            ViewBag.CertificateId = new SelectList(db.Certificates, "Id", "CirtificateTypeName");
            ViewBag.GradeId = new SelectList(db.Grades, "Id", "GradeName");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.CertificateId = new SelectList(db.Certificates, "Id", "CirtificateTypeNameArabic");
                    ViewBag.GradeId = new SelectList(db.Grades, "Id", "GradeNameArabic");


                }
            }


                return View();
        }

        // POST: GradesCertificateM_M/Create
        
        [HttpPost]
       
        public ActionResult Create( GradesCertificateM_M gradesCertificateM_M)
        {
            if (ModelState.IsValid)
            {
                var data = db.GradesCertificateM_M.Where(a => a.Is_Deleted != true && a.CertificateId == gradesCertificateM_M.CertificateId && a.GradeId == gradesCertificateM_M.GradeId).SingleOrDefault();
                if( data != null)
                {

                    ViewBag.GradeCertificate = Languages.Language.GradeCertificate;
                    ViewBag.CertificateId = new SelectList(db.Certificates, "Id", "CirtificateTypeName");
                    ViewBag.GradeId = new SelectList(db.Grades, "Id", "GradeName");
                    if (Session["lang"] != null)
                    {
                        if (Session["lang"].ToString().Equals("ar-EG"))
                        {
                            ViewBag.CertificateId = new SelectList(db.Certificates, "Id", "CirtificateTypeNameArabic");
                            ViewBag.GradeId = new SelectList(db.Grades, "Id", "GradeNameArabic");


                        }
                    }
                    return View(gradesCertificateM_M);
                }


                db.GradesCertificateM_M.Add(gradesCertificateM_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GradeCertificate = Languages.Language.GradeCertificate;
            ViewBag.CertificateId = new SelectList(db.Certificates, "Id", "CirtificateTypeName");
            ViewBag.GradeId = new SelectList(db.Grades, "Id", "GradeName");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.CertificateId = new SelectList(db.Certificates, "Id", "CirtificateTypeNameArabic");
                    ViewBag.GradeId = new SelectList(db.Grades, "Id", "GradeNameArabic");


                }
            }
            return View(gradesCertificateM_M);
        }

        // GET: GradesCertificateM_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            GradesCertificateM_M gradesCertificateM_M = db.GradesCertificateM_M.Find(id);
            if (gradesCertificateM_M == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            ViewBag.CertificateId = new SelectList(db.Certificates, "Id", "CirtificateTypeName", gradesCertificateM_M.CertificateId);
            ViewBag.GradeId = new SelectList(db.Grades, "Id", "GradeName", gradesCertificateM_M.GradeId);
            return View(gradesCertificateM_M);
        }

        // POST: GradesCertificateM_M/Edit/5
     
        [HttpPost]
     
        public ActionResult Edit( GradesCertificateM_M gradesCertificateM_M)
        {
            if (ModelState.IsValid)
            {
                var data = db.GradesCertificateM_M.Where(a => a.Is_Deleted!= true&&a.Id != gradesCertificateM_M.Id);
                foreach (var item in data)
                {
                    if(item.CertificateId == gradesCertificateM_M.CertificateId && item.GradeId == gradesCertificateM_M.GradeId)
                    {
                        ViewBag.GradeCertificate = Languages.Language.GradeCertificate;
                        ViewBag.CertificateId = new SelectList(db.Certificates, "Id", "CirtificateTypeName");
                        ViewBag.GradeId = new SelectList(db.Grades, "Id", "GradeName");
                        if (Session["lang"] != null)
                        {
                            if (Session["lang"].ToString().Equals("ar-EG"))
                            {
                                ViewBag.CertificateId = new SelectList(db.Certificates, "Id", "CirtificateTypeNameArabic");
                                ViewBag.GradeId = new SelectList(db.Grades, "Id", "GradeNameArabic");


                            }
                        }
                        return View(gradesCertificateM_M);

                    }
                }
                var old = db.GradesCertificateM_M.Find(gradesCertificateM_M.Id  );
                old.GradeId = gradesCertificateM_M.GradeId;
                old.CertificateId = gradesCertificateM_M.CertificateId;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GradeCertificate = Languages.Language.GradeCertificate;
            ViewBag.CertificateId = new SelectList(db.Certificates, "Id", "CirtificateTypeName");
            ViewBag.GradeId = new SelectList(db.Grades, "Id", "GradeName");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.CertificateId = new SelectList(db.Certificates, "Id", "CirtificateTypeNameArabic");
                    ViewBag.GradeId = new SelectList(db.Grades, "Id", "GradeNameArabic");


                }
            }
            return View(gradesCertificateM_M);
        }

        // GET: GradesCertificateM_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            GradesCertificateM_M gradesCertificateM_M = db.GradesCertificateM_M.Find(id);
            if (gradesCertificateM_M == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            return View(gradesCertificateM_M);
        }

        // POST: GradesCertificateM_M/Delete/5
        [HttpPost, ActionName("Delete")]
       
        public ActionResult DeleteConfirmed(int id)
        {
            GradesCertificateM_M gradesCertificateM_M = db.GradesCertificateM_M.Find(id);
            gradesCertificateM_M.Is_Deleted = true;
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
