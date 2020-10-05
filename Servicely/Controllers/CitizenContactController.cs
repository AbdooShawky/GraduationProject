using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicely.Models;
using System.Data.Entity;

namespace Servicely.Controllers
{
    public class CitizenContactController : BaseController
    {
        DbMasterEntities1 db = new DbMasterEntities1();
        // GET: CitizenContact
        public ActionResult Index()
        {   
            try
            {
                ViewBag.NId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
                ViewBag.contact_contactType_id = new SelectList(db.Contact_Type.Where(a => a.contact_type_isDeleted != true), "contact_type_id", "contact_type_name");
                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.contact_contactType_id = new SelectList(db.Contact_Type.Where(a => a.contact_type_isDeleted != true), "contact_type_id", "contact_type_name_arabic");

                    }
                }
                return View();
            }

            catch (Exception e)
            {
                return RedirectToAction("errorpage", "home");
            }


        }

        public JsonResult GetCitizenIdToJqueryCode()
        {
            int cid = 0;
            if (Session["citizenID"] != null)
                cid = (int)Session["citizenID"];

            return Json(1, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserIndex()
        {
            try
            {

                ViewBag.NId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
                ViewBag.contact_contactType_id = new SelectList(db.Contact_Type.Where(a => a.contact_type_isDeleted != true), "contact_type_id", "contact_type_name");

                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.contact_contactType_id = new SelectList(db.Contact_Type.Where(a => a.contact_type_isDeleted != true), "contact_type_id", "contact_type_name_arabic");
                    }
                }
                return View();



            }
            catch (Exception e)
            {
                return RedirectToAction("errorpage", "home");
            }


            //jgfjgdg
        }

        //------------ ajax call --------------------
        public JsonResult GetAllContactByCitizenId(int tid)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var persons = (

                 from ct in db.Citizens
                 from contactType in db.Contact_Type
                 from cont in db.Contacts.Where(
                     a => a.contact_citizen_id == ct.citizen_id && ct.citizen_id == tid
                     && a.contact_contactType_id == contactType.contact_type_id
                     && a.contact_isDeleted != true
                     )

                 select new
                 {
                     ct.citizen_first_name,
                     ct.citizen_second_name,
                     ct.citizen_third_name,
                     ct.citizen_fourth_name,
                     ct.citizen_first_name_arabic,
                     ct.citizen_second_name_arabic,
                     ct.citizen_third_name_arabic,
                     ct.citizen_fourth_name_arabic,



                     cont.contact_id,
                     ct.citizen_national_id,
                     contactType.contact_type_name,
                     contactType.contact_type_name_arabic,

                     cont.contact_data



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

        public JsonResult GetAllContactByContactTypeId(int tid)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var persons = (

                 from ct in db.Citizens
                 from contactType in db.Contact_Type
                 from cont in db.Contacts.Where(
                     a => a.contact_citizen_id == ct.citizen_id && contactType.contact_type_id == tid
                     && a.contact_contactType_id == contactType.contact_type_id
                     && a.contact_isDeleted != true
                     )

                 select new
                 {
                     ct.citizen_first_name,
                     ct.citizen_second_name,
                     ct.citizen_third_name,
                     ct.citizen_fourth_name,
                     ct.citizen_first_name_arabic,
                     ct.citizen_second_name_arabic,
                     ct.citizen_third_name_arabic,
                     ct.citizen_fourth_name_arabic,



                     cont.contact_id,
                     ct.citizen_national_id,
                     contactType.contact_type_name,
                     contactType.contact_type_name_arabic,

                     cont.contact_data



                 });

        
            return Json(persons.ToList(), JsonRequestBehavior.AllowGet);



        }
        public JsonResult GetAllContact()
        {
            db.Configuration.ProxyCreationEnabled = false;

            var persons = (

                 from ct in db.Citizens
                 from contactType in db.Contact_Type
                 from cont in db.Contacts.Where(
                     a => a.contact_citizen_id == ct.citizen_id && a.contact_contactType_id == contactType.contact_type_id
                     && a.contact_isDeleted != true
                     )

                 select new
                 {
                     cont.contact_id,
                     ct.citizen_national_id,
                     contactType.contact_type_name,
                     cont.contact_data



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
            try {

                ViewBag.e = "^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$";
                ViewBag.contact_citizen_id = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
                ViewBag.contact_contactType_id = new SelectList(db.Contact_Type.Where(a => a.contact_type_isDeleted != true), "contact_type_id", "contact_type_name");

                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.contact_contactType_id = new SelectList(db.Contact_Type.Where(a => a.contact_type_isDeleted != true), "contact_type_id", "contact_type_name_arabic");

                    }
                }


                return View();


            } 
            catch (Exception e)
            {
                return RedirectToAction("errorpage", "home");
            }
         
        }


        [HttpPost]
        public ActionResult create(Contact s)
        {
            try { 
            var data = db.Contacts.Where(a => a.contact_data == s.contact_data && a.contact_isDeleted !=true && a.contact_citizen_id==s.contact_citizen_id).SingleOrDefault();
            if (data != null)
            {
                ViewBag.contact_citizen_id = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
                ViewBag.contact_contactType_id = new SelectList(db.Contact_Type.Where(a => a.contact_type_isDeleted != true), "contact_type_id", "contact_type_name");

                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.contact_contactType_id = new SelectList(db.Contact_Type.Where(a => a.contact_type_isDeleted != true), "contact_type_id", "contact_type_name_arabic");

                    }
                }
                ViewBag.ww = Servicely.Languages.Language.contactALREADY;
                return View();
            }

            db.Contacts.Add(s);
            db.SaveChanges();


            return RedirectToAction("Index");
        } 
            catch (Exception e)
            {
                return RedirectToAction("errorpage", "home");
             }

}



        public ActionResult createUser()
        {
            try { 
            ViewBag.e = "^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$";
            ViewBag.contact_citizen_id = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
            ViewBag.contact_contactType_id = new SelectList(db.Contact_Type.Where(a => a.contact_type_isDeleted != true), "contact_type_id", "contact_type_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.contact_contactType_id = new SelectList(db.Contact_Type.Where(a => a.contact_type_isDeleted != true), "contact_type_id", "contact_type_name_arabic");

                }
            }


            return View();

        } 
            catch (Exception e)
            {
                return RedirectToAction("errorpage", "home");
    }
}


        [HttpPost]
        public ActionResult createUser(Contact s)
        {
            try { 

            int cid = 1;
            if (Session["citizenID"] != null)
                cid = (int)Session["citizenID"];


            s.contact_citizen_id = cid;
            var data = db.Contacts.Where(a => a.contact_data == s.contact_data && a.contact_isDeleted != true && a.contact_citizen_id ==s.contact_citizen_id).SingleOrDefault();
            if (data != null)
            {
                ViewBag.contact_citizen_id = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
                ViewBag.contact_contactType_id = new SelectList(db.Contact_Type.Where(a => a.contact_type_isDeleted != true), "contact_type_id", "contact_type_name");

                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.contact_contactType_id = new SelectList(db.Contact_Type.Where(a => a.contact_type_isDeleted != true), "contact_type_id", "contact_type_name_arabic");

                    }
                }
                ViewBag.ww = Servicely.Languages.Language.contactALREADY;
                return View();
            }

            db.Contacts.Add(s);
            db.SaveChanges();


            return RedirectToAction("UserIndex");

        } 
            catch (Exception e)
            {
                return RedirectToAction("errorpage", "home");
    }

}

        public ActionResult Edit(int id)
        {
            try { 
            Contact contact = db.Contacts.Find(id);

            ViewBag.contact_contactType_id = new SelectList(db.Contact_Type, "contact_type_id", "contact_type_name", contact.contact_contactType_id);
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.contact_contactType_id = new SelectList(db.Contact_Type, "contact_type_id", "contact_type_name_arabic", contact.contact_contactType_id);

                }
            }


            return View(contact);


        } 
            catch (Exception e)
            {
                return RedirectToAction("errorpage", "home");
              }
}

        [HttpPost]

        public ActionResult Edit(Contact c)
        {
            try
            {
                var data = db.Contacts.Where(a => a.contact_data == c.contact_data && a.contact_isDeleted != true && a.contact_citizen_id == c.contact_citizen_id && a.contact_id != c.contact_id).SingleOrDefault();
                if (data != null)
                {
                    ViewBag.contact_citizen_id = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
                    ViewBag.contact_contactType_id = new SelectList(db.Contact_Type.Where(a => a.contact_type_isDeleted != true), "contact_type_id", "contact_type_name");

                    if (Session["lang"] != null)
                    {
                        if (Session["lang"].ToString().Equals("ar-EG"))
                        {
                            ViewBag.contact_contactType_id = new SelectList(db.Contact_Type.Where(a => a.contact_type_isDeleted != true), "contact_type_id", "contact_type_name_arabic");

                        }
                    }
                    ViewBag.ww = Servicely.Languages.Language.contactALREADY;
                    return View();
                }
                if (ModelState.IsValid)
                {
                    var old = db.Contacts.Find(c.contact_id);
                    old.contact_data = c.contact_data;
                    old.contact_citizen_id = c.contact_citizen_id;
                    old.contact_contactType_id = c.contact_contactType_id;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.contact_contactType_id = new SelectList(db.Contact_Type, "contact_type_id", "contact_type_name", c.contact_contactType_id);

                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.contact_contactType_id = new SelectList(db.Contact_Type, "contact_type_id", "contact_type_name_arabic", c.contact_contactType_id);

                    }
                }
                return View(c);

            }
            catch (Exception e)
            {
                return RedirectToAction("errorpage", "home");
            }
        }



        public ActionResult EditUser(int id)
        {
            try { 
            Contact contact = db.Contacts.Find(id);

            ViewBag.contact_contactType_id = new SelectList(db.Contact_Type, "contact_type_id", "contact_type_name", contact.contact_contactType_id);
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.contact_contactType_id = new SelectList(db.Contact_Type, "contact_type_id", "contact_type_name_arabic", contact.contact_contactType_id);

                }
            }


            return View(contact);
            }
            catch (Exception e)
            {
                return RedirectToAction("errorpage", "home");
            }
        }

        [HttpPost]

        public ActionResult EditUser(Contact c)
        {
            try { 
           /* int cid = 1;
            if (Session["citizenID"] != null)
                cid = (int)Session["citizenID"];
          */
        ///    c.contact_citizen_id = cid;

            var data = db.Contacts.Where(a => a.contact_data == c.contact_data && a.contact_isDeleted != true && a.contact_citizen_id == c.contact_citizen_id && a.contact_id != c.contact_id).SingleOrDefault();
            if (data != null)
            {
                ViewBag.contact_citizen_id = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true), "citizen_id", "citizen_national_id");
                ViewBag.contact_contactType_id = new SelectList(db.Contact_Type.Where(a => a.contact_type_isDeleted != true), "contact_type_id", "contact_type_name");

                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.contact_contactType_id = new SelectList(db.Contact_Type.Where(a => a.contact_type_isDeleted != true), "contact_type_id", "contact_type_name_arabic");

                    }
                }
                ViewBag.ww = Servicely.Languages.Language.contactALREADY;
                return View();
            }
            if (ModelState.IsValid)
            {
                var old = db.Contacts.Find(c.contact_id);
                old.contact_data = c.contact_data;
                old.contact_contactType_id = c.contact_contactType_id;
                db.SaveChanges();
                return RedirectToAction("UserIndex");
            }

            ViewBag.contact_contactType_id = new SelectList(db.Contact_Type, "contact_type_id", "contact_type_name", c.contact_contactType_id);

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.contact_contactType_id = new SelectList(db.Contact_Type, "contact_type_id", "contact_type_name_arabic", c.contact_contactType_id);

                }
            }
            return View(c);

        }
            catch (Exception e)
            {
                return RedirectToAction("errorpage", "home");
    }
}

        public ActionResult Delete(int id)
        {
            try { 

            Contact contact = db.Contacts.Find(id);

            return View(contact);

        }
            catch (System.ArgumentException)
            {
                return RedirectToAction("errorpage", "home");
          }
}

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            try{ 
            var old = db.Contacts.Find(id);
            old.contact_isDeleted = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
            catch (System.ArgumentException)
            {
                return RedirectToAction("errorpage", "home");
    }
}


        protected override void OnException(ExceptionContext filterContext)
        {
            //your handling logic here
            filterContext.ExceptionHandled = true;
            filterContext.Result = RedirectToAction("errorpage", "home");
        }
    }
}