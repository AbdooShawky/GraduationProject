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
    public class FacultyCertificateM_MController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: UniversityCertificateM_M
        public ActionResult Index()
        {
            var universityCertificateM_M = db.FacultyCertificateM_M.Include(u => u.Certificate).Include(u => u.Faculty).Where(a=>a.Is_deleted!=true);
            return View(universityCertificateM_M.ToList());
        }

     

        // GET: UniversityCertificateM_M/Create
        public ActionResult Create()
        {
            ViewBag.CertificateId = new SelectList(db.Certificates.Where(a => a.Is_Deleted != true), "Id", "CirtificateTypeName");
            ViewBag.FacultyId = new SelectList(db.Faculties.Where(a => a.Is_Dleted != true), "Id", "UniversityName");
            return View();
        }

        // POST: UniversityCertificateM_M/Create
      
        [HttpPost]
        public ActionResult Create(FacultyCertificateM_M universityCertificateM_M)
        {
            if (ModelState.IsValid)
            {
                var data = db.FacultyCertificateM_M.Where(a=>a.Is_deleted!= true && a.FacultyId  == universityCertificateM_M.FacultyId && a.CertificateId == universityCertificateM_M.CertificateId).SingleOrDefault();

                if(data !=null)
                {
                    ViewBag.errorMessage = "this certificate is already exsits in this university";

                    ViewBag.CertificateId = new SelectList(db.Certificates.Where(a => a.Is_Deleted != true), "Id", "CirtificateTypeName", universityCertificateM_M.CertificateId);
                    ViewBag.FacultyId = new SelectList(db.Faculties.Where(a => a.Is_Dleted != true), "Id", "UniversityName", universityCertificateM_M.FacultyId);
                    return View(universityCertificateM_M);
                }
                db.FacultyCertificateM_M.Add(universityCertificateM_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CertificateId = new SelectList(db.Certificates.Where(a => a.Is_Deleted != true), "Id", "CirtificateTypeName", universityCertificateM_M.CertificateId);
            ViewBag.facultyId = new SelectList(db.Faculties.Where(a => a.Is_Dleted != true), "Id", "UniversityName", universityCertificateM_M.FacultyId);
            return View(universityCertificateM_M);
        }

        // GET: UniversityCertificateM_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorpage","home");
            }
            FacultyCertificateM_M universityCertificateM_M = db.FacultyCertificateM_M.Find(id);
            if (universityCertificateM_M == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            ViewBag.CertificateId = new SelectList(db.Certificates.Where(a=>a.Is_Deleted !=true), "Id", "CirtificateTypeName", universityCertificateM_M.CertificateId);
            ViewBag.FacultyId = new SelectList(db.Faculties.Where(a => a.Is_Dleted != true), "Id", "UniversityName", universityCertificateM_M.FacultyId);
            return View(universityCertificateM_M);
        }

        // POST: UniversityCertificateM_M/Edit/5
       
        [HttpPost]
       
        public ActionResult Edit(FacultyCertificateM_M universityCertificateM_M)
        {
            if (ModelState.IsValid)
            {
                var data = db.FacultyCertificateM_M.Where(a => a.Id != universityCertificateM_M.Id);
                foreach (var item in data)
                {
                    if(item.CertificateId == universityCertificateM_M.CertificateId && item.FacultyId == universityCertificateM_M.FacultyId)
                    {
                        ViewBag.errorMessage = "this certificate is already exsits in this university";
                        ViewBag.CertificateId = new SelectList(db.Certificates.Where(a=>a.Is_Deleted !=true), "Id", "CirtificateTypeName", universityCertificateM_M.CertificateId);
                        ViewBag.FacultyId = new SelectList(db.Faculties.Where(a=>a.Is_Dleted!=true), "Id", "UniversityName", universityCertificateM_M.FacultyId);
                        return View(universityCertificateM_M);

                    }
                }

                var old = db.FacultyCertificateM_M.Find(universityCertificateM_M.Id);
                old.FacultyId = universityCertificateM_M.FacultyId;
                old.CertificateId = universityCertificateM_M.CertificateId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CertificateId = new SelectList(db.Certificates.Where(a=>a.Is_Deleted != true), "Id", "CirtificateTypeName", universityCertificateM_M.CertificateId);
            ViewBag.FacultyId = new SelectList(db.Faculties.Where(a=>a.Is_Dleted !=true), "Id", "UniversityName", universityCertificateM_M.FacultyId);
            return View(universityCertificateM_M);
        }

        // GET: UniversityCertificateM_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorpage","home");
            }
            FacultyCertificateM_M universityCertificateM_M = db.FacultyCertificateM_M.Find(id);
            if (universityCertificateM_M == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            return View(universityCertificateM_M);
        }

        // POST: UniversityCertificateM_M/Delete/5
        [HttpPost, ActionName("Delete")]
    
        public ActionResult DeleteConfirmed(int id)
        {
            FacultyCertificateM_M universityCertificateM_M = db.FacultyCertificateM_M.Find(id);
            universityCertificateM_M.Is_deleted = true;
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
