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
    public class SchoolCertificateM_MController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: SchoolCertificateM_M
        public ActionResult Index()
        {
            var schoolCertificateM_M = db.SchoolCertificateM_M.Include(s => s.Certificate).Include(s => s.School).Where(a=>a.Is_Deleted !=true);
            return View(schoolCertificateM_M.ToList());
        }

      

        // GET: SchoolCertificateM_M/Create
        public ActionResult Create()
        {
            ViewBag.CertificateId = new SelectList(db.Certificates.Where(a=>a.Is_Deleted !=true), "Id", "CirtificateTypeName");
            ViewBag.SchoolId = new SelectList(db.Schools.Where(a=>a.Is_Deleted !=true), "Id", "SchoolName");


            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.CertificateId = new SelectList(db.Certificates.Where(a => a.Is_Deleted != true), "Id", "CirtificateTypeNameArabic");
                    ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.Is_Deleted != true), "Id", "SchoolNameArabic");

                }
            }


                return View();
        }

        // POST: SchoolCertificateM_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
     
        public ActionResult Create(SchoolCertificateM_M schoolCertificateM_M)
        {
            if (ModelState.IsValid)
            {
                var data = db.SchoolCertificateM_M.Where(a=>a.Is_Deleted != true && a.SchoolId == schoolCertificateM_M.SchoolId && a.CertificateId == schoolCertificateM_M.CertificateId).SingleOrDefault();
                if(data != null)
                {
                    ViewBag.errorMessage = "this certificate in already exsits in this school";
                    ViewBag.CertificateId = new SelectList(db.Certificates.Where(a => a.Is_Deleted != true), "Id", "CirtificateTypeName");
                    ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.Is_Deleted != true), "Id", "SchoolName");


                    if (Session["lang"] != null)
                    {
                        if (Session["lang"].ToString().Equals("ar-EG"))
                        {
                            ViewBag.CertificateId = new SelectList(db.Certificates.Where(a => a.Is_Deleted != true), "Id", "CirtificateTypeNameArabic");
                            ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.Is_Deleted != true), "Id", "SchoolNameArabic");

                        }
                    }
                    return View(schoolCertificateM_M);
                }

                db.SchoolCertificateM_M.Add(schoolCertificateM_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.errorMessage = "this certificate in already exsits in this school";
            ViewBag.CertificateId = new SelectList(db.Certificates.Where(a => a.Is_Deleted != true), "Id", "CirtificateTypeName");
            ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.Is_Deleted != true), "Id", "SchoolName");


            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.CertificateId = new SelectList(db.Certificates.Where(a => a.Is_Deleted != true), "Id", "CirtificateTypeNameArabic");
                    ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.Is_Deleted != true), "Id", "SchoolNameArabic");

                }
            }
            return View(schoolCertificateM_M);
        }

        // GET: SchoolCertificateM_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            SchoolCertificateM_M schoolCertificateM_M = db.SchoolCertificateM_M.Find(id);
            if (schoolCertificateM_M == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            ViewBag.CertificateId = new SelectList(db.Certificates.Where(a => a.Is_Deleted != true), "Id", "CirtificateTypeName");
            ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.Is_Deleted != true), "Id", "SchoolName");


            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.CertificateId = new SelectList(db.Certificates.Where(a => a.Is_Deleted != true), "Id", "CirtificateTypeNameArabic");
                    ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.Is_Deleted != true), "Id", "SchoolNameArabic");

                }
            }
            return View(schoolCertificateM_M);
        }

        // POST: SchoolCertificateM_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
     
        public ActionResult Edit(SchoolCertificateM_M schoolCertificateM_M)
        {
            if (ModelState.IsValid)
            {
                var data = db.SchoolCertificateM_M.Where(a=>a.Is_Deleted != true && a.Id != schoolCertificateM_M.Id);
                foreach (var item in data)
                {
                    if(item.SchoolId == schoolCertificateM_M.SchoolId && item.CertificateId == schoolCertificateM_M.CertificateId)
                    {
                        ViewBag.errorMessage = "this certificate in already exsits in this school";
                        ViewBag.CertificateId = new SelectList(db.Certificates.Where(a => a.Is_Deleted != true), "Id", "CirtificateTypeName");
                        ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.Is_Deleted != true), "Id", "SchoolName");


                        if (Session["lang"] != null)
                        {
                            if (Session["lang"].ToString().Equals("ar-EG"))
                            {
                                ViewBag.CertificateId = new SelectList(db.Certificates.Where(a => a.Is_Deleted != true), "Id", "CirtificateTypeNameArabic");
                                ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.Is_Deleted != true), "Id", "SchoolNameArabic");

                            }
                        }
                        return View(schoolCertificateM_M);
                    }
                }
                var old = db.SchoolCertificateM_M.Find(schoolCertificateM_M.Id);
                old.SchoolId = schoolCertificateM_M.SchoolId;
                old.CertificateId = schoolCertificateM_M.CertificateId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.errorMessage = "this certificate in already exsits in this school";
            ViewBag.CertificateId = new SelectList(db.Certificates.Where(a => a.Is_Deleted != true), "Id", "CirtificateTypeName");
            ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.Is_Deleted != true), "Id", "SchoolName");


            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.CertificateId = new SelectList(db.Certificates.Where(a => a.Is_Deleted != true), "Id", "CirtificateTypeNameArabic");
                    ViewBag.SchoolId = new SelectList(db.Schools.Where(a => a.Is_Deleted != true), "Id", "SchoolNameArabic");

                }
            }
            return View(schoolCertificateM_M);
        }

        // GET: SchoolCertificateM_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorpage","home");
            }
            SchoolCertificateM_M schoolCertificateM_M = db.SchoolCertificateM_M.Find(id);
            if (schoolCertificateM_M == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            return View(schoolCertificateM_M);
        }

        // POST: SchoolCertificateM_M/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            SchoolCertificateM_M schoolCertificateM_M = db.SchoolCertificateM_M.Find(id);
            schoolCertificateM_M.Is_Deleted = true;
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
