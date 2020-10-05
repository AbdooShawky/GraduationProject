using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Servicely.Models;

namespace Servicely.Controllers
{
    public class DocumentController : BaseController
    {
        // GET: Document
        DbMasterEntities1 db = new DbMasterEntities1();
        // GET: document
        public ActionResult Index()
        {
            ViewBag.NId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");

            return View(db.Documents.ToList());
        }



        //---------------------- Ajax Call -----------------------

        public JsonResult GetAllDocumentsByCitizenId(int tid)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var persons = (

                 from ct in db.Citizens
                 from DType in db.Document_Type
                 from doc in db.Documents.Where(
                     a => a.document_citizen_id == ct.citizen_id && ct.citizen_id == tid
                     && a.document_documentType_id == DType.document_type_id
                     && a.document_isDeleted != true
                     )

                 select new
                 {
                     ct.citizen_first_name,
                     ct.citizen_second_name,
                     ct.citizen_third_name,
                     ct.citizen_fourth_name,
                     doc.document_id,
                     ct.citizen_national_id,
                     DType.document_type_name,
                     doc.document_name,
                     doc.document_extensions,
                     doc.document_data,

                      ct.citizen_first_name_arabic,
                     ct.citizen_second_name_arabic,
                     ct.citizen_third_name_arabic,
                     ct.citizen_fourth_name_arabic,
                     DType.document_type_name_arabic
                    



                 });




            return Json(persons.ToList(), JsonRequestBehavior.AllowGet);



        }
        public JsonResult GetAllDocument()

        {
            db.Configuration.ProxyCreationEnabled = false;

            var persons = (

                 from ct in db.Citizens
                 from DType in db.Document_Type
                 from doc in db.Documents.Where(
                     a => a.document_citizen_id == ct.citizen_id
                     && a.document_documentType_id == DType.document_type_id
                     && a.document_isDeleted != true)

                 select new
                 {
                     doc.document_id,
                     ct.citizen_national_id,
                     DType.document_type_name,
                     DType.document_type_name_arabic,
                     doc.document_name,
                     doc.document_extensions,
                     doc.document_data



                 });

            //var ff = (
            //    from contac in db.Contact join citiz in db.Citizen
            //    on contac.contact_citizen_id equals citiz.citizen_id 
            //    join conType in db.Contact_Type on contac.contact_contactType_id equals conType.contact_type_id

            //    select new
            //    {
            //        citiz.citizen_national_id , conType.contact_type_name , contac.contact_data
            //    }

            //    );


            // db.Contact.Where(a => a.contact_contactType_id == tid)


            return Json(persons.ToList(), JsonRequestBehavior.AllowGet);



        }


        public ActionResult create()
        {

            ViewBag.document_citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id");
            ViewBag.document_documentType_id = new SelectList(db.Document_Type, "document_type_id", "document_type_name");
          
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.document_citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id");
                    ViewBag.document_documentType_id = new SelectList(db.Document_Type, "document_type_id", "document_type_name_arabic");

                }
            }
            return View();
        }


        [HttpPost]
        public ActionResult create( Upload f1 )
        {
            Document d = new Document();
            Session["documentTypeName"] = f1.document_documentType_id;
            if(ModelState.IsValid)
            {
                var fff = db.Document_Type.Find(f1.document_documentType_id);
                d.document_documentType_id = f1.document_documentType_id;
                string filename = Session["Nai"].ToString() + "_" + fff.document_type_name + "_" + f1.file.FileName + "_" + Guid.NewGuid() + Path.GetExtension(f1.file.FileName);
                string filePath = Server.MapPath("~/DocumentDataURL/" + filename);
                string filePathName = Path.Combine(filename, filePath);
                f1.file.SaveAs(filePathName);

                //==================================================
                int cid = Convert.ToInt32(Session["CID"]);
                d.document_citizen_id = cid;

                //==========================
                d.document_data = filePathName;
                Session["docData"] = filePathName;
                d.document_name = filename;
                d.document_date_upload = DateTime.Now.ToString();
                Session["uploadDate"] = d.document_date_upload;
                Session["filename"] = filename;
                //===============================
                //  int l = filename.Length;
                // string length = filename.Substring(l - 3, 3);
                d.document_extensions = Path.GetExtension(f1.file.FileName);
                Session["extension"] = Path.GetExtension(f1.file.FileName);
                //=========================================
               // d.document_id = 0;
               // db.Documents.Add(d);
                
               // db.SaveChanges();
                ViewBag.x  = Languages.Language.Document_successfully ;
                ViewBag.document_citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id");
                ViewBag.document_documentType_id = new SelectList(db.Document_Type, "document_type_id", "document_type_name");
                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.document_citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id");
                        ViewBag.document_documentType_id = new SelectList(db.Document_Type, "document_type_id", "document_type_name_arabic");

                    }
                }
                return RedirectToAction("Index" ,"summary");

            }
            ViewBag.document_citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id");
            ViewBag.document_documentType_id = new SelectList(db.Document_Type, "document_type_id", "document_type_name");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.document_citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id");
                    ViewBag.document_documentType_id = new SelectList(db.Document_Type, "document_type_id", "document_type_name_arabic");

                }
            }
            return View(f1);

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {

            Document document = db.Documents.Find(id);

            ViewBag.document_documentType_id = new SelectList(db.Document_Type, "document_type_id", "document_type_name", document.document_documentType_id);
            ViewBag.document_citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", document.document_citizen_id);
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {

                    ViewBag.document_documentType_id = new SelectList(db.Document_Type, "document_type_id", "document_type_name_arabic", document.document_documentType_id);
                    ViewBag.document_citizen_id = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", document.document_citizen_id);

                }
            }
            return View(document);
        }
        [HttpPost]
        public ActionResult Edit(Document document)
        {
            var old = db.Documents.Find(document.document_id);
            old.document_name = document.document_name;
            old.document_extensions = document.document_extensions;
            old.document_data = document.document_data;
            old.document_documentType_id = document.document_documentType_id;


            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {

            Document document = db.Documents.Find(id);

            return View(document);
        }

        // POST: Document_Type/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            var old = db.Documents.Find(id);
            old.document_isDeleted = true;

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
