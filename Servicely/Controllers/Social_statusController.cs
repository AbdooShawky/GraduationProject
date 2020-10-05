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
    public class Sociallll
    {

        public int social_status_id { get; set; }
        public string citizen_first_name { get; set; }
        public string citizen_second_name { get; set; }
        public string citizen_third_name { get; set; }
        public string citizen_fourth_name { get; set; }
        public string citizen_first_name_arabic { get; set; }
        public string citizen_second_name_arabic { get; set; }
        public string citizen_third_name_arabic { get; set; }
        public string citizen_fourth_name_arabic { get; set; }
        public string social_status_from { get; set; }
        public string social_status_to { get; set; }
        public string social_status_type_name { get; set; }
        public string social_status_type_name_arabic { get; set; }
    }

    public class Social_statusController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: Social_status
        public ActionResult Index()
        {
            ViewBag.NId = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true && a.citizen_gender == "Male"), "citizen_id", "citizen_national_id");
            var social_status = db.Social_status.Where(a => a.social_status_isDeleted != true).Include(s => s.Social_Status_Type).Include(s => s.Citizen).Include(s => s.Citizen1).Where(a => a.social_status_isDeleted != true);
            return View();
        }


        public JsonResult GetSocialInfoByWifeId(int WId, int HId)
        {
           // DateTime d;
            db.Configuration.ProxyCreationEnabled = false;
            var persons = (
               from ct in db.Citizens
               from socialType in db.Social_Status_Type
               from Social_status in db.Social_status.Where(
                   a => a.socialStatus_citizenId_Husband == HId && a.socialStatus_citizenId_Wife == WId
                   && a.socialStatus_TypeId == socialType.social_status_type_id
                   && a.social_status_isDeleted != true
                   && ct.citizen_id == WId
                   && ct.citizen_isDeleted != true
                   )

               select new Sociallll
               {
                  citizen_first_name= ct.citizen_first_name,
                  citizen_second_name= ct.citizen_second_name,
                  citizen_third_name= ct.citizen_third_name,
                  citizen_fourth_name= ct.citizen_fourth_name,
                  citizen_first_name_arabic= ct.citizen_first_name_arabic,
                  citizen_second_name_arabic= ct.citizen_second_name_arabic,
                  citizen_third_name_arabic =ct.citizen_third_name_arabic,
                  citizen_fourth_name_arabic= ct.citizen_fourth_name_arabic,
                  social_status_id= Social_status.social_status_id,
                  social_status_from= Social_status.social_status_from.ToString(),
                  social_status_to= Social_status.social_status_to.ToString(),
                  social_status_type_name= socialType.social_status_type_name,
                  social_status_type_name_arabic= socialType.social_status_type_name_arabic,

               });
           
            return Json(persons.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetNamesByCitizenId(int ctId)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return Json(db.Citizens.Where(a => a.citizen_id == ctId && a.citizen_isDeleted != true), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllWifeByHusId(int tid)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var persons = (

                 from ct in db.Citizens
                 from Social_status in db.Social_status.Where(
                     a => a.socialStatus_citizenId_Husband == tid
                    && a.socialStatus_citizenId_Wife == ct.citizen_id
                    && a.social_status_isDeleted != true
                     )

                 select new
                 {
                     ct.citizen_id,
                     ct.citizen_national_id,
                     ct.citizen_first_name,
                     ct.citizen_second_name,
                     ct.citizen_third_name,
                     ct.citizen_fourth_name,

                 });
            return Json(persons.ToList(), JsonRequestBehavior.AllowGet);



        }

        // GET: Social_status/Create
        public ActionResult Create()
        {
            ViewBag.zero = "0";

            ViewBag.socialStatus_TypeId = new SelectList(db.Social_Status_Type.Where(a => a.social_status_type_isDeleted != true), "social_status_type_id", "social_status_type_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.socialStatus_TypeId = new SelectList(db.Social_Status_Type.Where(a => a.social_status_type_isDeleted != true), "social_status_type_id", "social_status_type_name_arabic");


                }
                else
                {
                    ViewBag.socialStatus_TypeId = new SelectList(db.Social_Status_Type.Where(a => a.social_status_type_isDeleted != true), "social_status_type_id", "social_status_type_name");

                }

            }

            ViewBag.socialStatus_citizenId_Husband = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true && a.citizen_gender == "Male"), "citizen_id", "citizen_national_id");
            ViewBag.socialStatus_citizenId_Wife = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true && a.citizen_gender == "Female"), "citizen_id", "citizen_national_id");
            return View();



        }


        [HttpPost]

        public ActionResult Create(Social_status social_status)
        {

            var type = db.Social_Status_Type.Find(social_status.socialStatus_TypeId).social_status_type_id;
            var data = db.Citizens.Find(social_status.socialStatus_citizenId_Husband);
            if(type == 1)
            {
                if (data.citizen_relegion == "Muslim")
                {

                    // howa mtgawz 2a2l mn 4 wla la2a 3ashn howa muslim
                    var countMarriage = db.Social_status.Where(a => a.socialStatus_citizenId_Husband == social_status.socialStatus_citizenId_Husband).Count();
                    if (countMarriage < 4)
                    {

                        //homa mawgodin fl database bs mtal2iin
                        var mrriageThisWife = db.Social_status.Where(a => a.socialStatus_citizenId_Husband == social_status.socialStatus_citizenId_Husband && a.socialStatus_citizenId_Wife == social_status.socialStatus_citizenId_Wife && a.social_status_isStill == false).FirstOrDefault();

                        if (mrriageThisWife != null)
                        {
                            //hya mtgawza wla la2a
                            var wifeIsAlreadyMarried = db.Social_status.Where(a => a.socialStatus_citizenId_Wife == social_status.socialStatus_citizenId_Wife && a.social_status_isStill == true);
                            if (wifeIsAlreadyMarried != null)
                            {
                                ViewBag.socialStatus_TypeId = new SelectList(db.Social_Status_Type.Where(a => a.social_status_type_isDeleted != true), "social_status_type_id", "social_status_type_name");

                                if (Session["lang"] != null)
                                {
                                    if (Session["lang"].ToString().Equals("ar-EG"))
                                    {
                                        ViewBag.socialStatus_TypeId = new SelectList(db.Social_Status_Type.Where(a => a.social_status_type_isDeleted != true), "social_status_type_id", "social_status_type_name_arabic");


                                    }
                                    else
                                    {
                                        ViewBag.socialStatus_TypeId = new SelectList(db.Social_Status_Type.Where(a => a.social_status_type_isDeleted != true), "social_status_type_id", "social_status_type_name", social_status.socialStatus_TypeId);

                                    }

                                }

                                ViewBag.wifeIsAlreadyMarried = Servicely.Languages.Language.thisladyisalreadymarried;
                                ViewBag.socialStatus_citizenId_Husband = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true && a.citizen_gender == "Male"), "citizen_id", "citizen_national_id");
                                ViewBag.socialStatus_citizenId_Wife = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true && a.citizen_gender == "Female"), "citizen_id", "citizen_national_id");

                                return View();
                            }
                            else
                            {
                                social_status.social_status_isStill = true;
                                social_status.social_status_isDeleted = false;
                                db.Social_status.Add(social_status);
                                db.SaveChanges();
                                return RedirectToAction("Index");

                            }
                        }

                        var ladyNotMarried = db.Social_status.Where(a => a.socialStatus_citizenId_Wife == social_status.socialStatus_citizenId_Wife).Count();
                        if (ladyNotMarried == 0)
                        {
                            social_status.social_status_isStill = true;
                            social_status.social_status_isDeleted = false;
                            db.Social_status.Add(social_status);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewBag.socialStatus_TypeId = new SelectList(db.Social_Status_Type.Where(a => a.social_status_type_isDeleted != true), "social_status_type_id", "social_status_type_name");
                            if (Session["lang"] != null)
                            {
                                if (Session["lang"].ToString().Equals("ar-EG"))
                                {
                                    ViewBag.socialStatus_TypeId = new SelectList(db.Social_Status_Type.Where(a => a.social_status_type_isDeleted != true), "social_status_type_id", "social_status_type_name_arabic");


                                }
                                else
                                {
                                    ViewBag.socialStatus_TypeId = new SelectList(db.Social_Status_Type.Where(a => a.social_status_type_isDeleted != true), "social_status_type_id", "social_status_type_name", social_status.socialStatus_TypeId);

                                }

                            }
                            ViewBag.socialStatus_citizenId_Husband = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true && a.citizen_gender == "Male"), "citizen_id", "citizen_national_id");
                            ViewBag.socialStatus_citizenId_Wife = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true && a.citizen_gender == "Female"), "citizen_id", "citizen_national_id");

                            ViewBag.wifeIsAlreadyMarried = Servicely.Languages.Language.thisladyisalreadymarried;

                            return View();
                        }

                    }
                    else
                    {
                        ViewBag.wifeIsAlreadyMarried = Servicely.Languages.Language.Citizenalreadymarried4;
                        ViewBag.socialStatus_TypeId = new SelectList(db.Social_Status_Type.Where(a => a.social_status_type_isDeleted != true), "social_status_type_id", "social_status_type_name");

                        if (Session["lang"] != null)
                        {
                            if (Session["lang"].ToString().Equals("ar-EG"))
                            {
                                ViewBag.socialStatus_TypeId = new SelectList(db.Social_Status_Type.Where(a => a.social_status_type_isDeleted != true), "social_status_type_id", "social_status_type_name_arabic");


                            }
                            else
                            {
                                ViewBag.socialStatus_TypeId = new SelectList(db.Social_Status_Type.Where(a => a.social_status_type_isDeleted != true), "social_status_type_id", "social_status_type_name", social_status.socialStatus_TypeId);

                            }

                        }

                        ViewBag.socialStatus_citizenId_Husband = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true && a.citizen_gender == "Male"), "citizen_id", "citizen_national_id");
                        ViewBag.socialStatus_citizenId_Wife = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true && a.citizen_gender == "Female"), "citizen_id", "citizen_national_id");

                        return View();
                    }

                }
                else if (data.citizen_relegion == "Cristian")
                {
                    var countMarriage = db.Social_status.Where(a => a.socialStatus_citizenId_Husband == social_status.socialStatus_citizenId_Husband).Count();
                    if (countMarriage == 0)
                    {

                        //homa mawgodin fl database bs mtal2iin
                        var mrriageThisWife = db.Social_status.Where(a => a.socialStatus_citizenId_Husband == social_status.socialStatus_citizenId_Husband && a.socialStatus_citizenId_Wife == social_status.socialStatus_citizenId_Wife && a.social_status_isStill == false);

                        if (mrriageThisWife != null)
                        {
                            //hya mtgawza wla la2a
                            var wifeIsAlreadyMarried = db.Social_status.Where(a => a.socialStatus_citizenId_Wife == social_status.socialStatus_citizenId_Wife && a.social_status_isStill == true);
                            if (wifeIsAlreadyMarried != null)
                            {
                                ViewBag.wifeIsAlreadyMarried = Servicely.Languages.Language.thisladyisalreadymarried;
                                ViewBag.socialStatus_TypeId = new SelectList(db.Social_Status_Type, "social_status_type_id", "social_status_type_name");

                                if (Session["lang"] != null)
                                {
                                    if (Session["lang"].ToString().Equals("ar-EG"))
                                    {
                                        ViewBag.socialStatus_TypeId = new SelectList(db.Social_Status_Type.Where(a => a.social_status_type_isDeleted != true), "social_status_type_id", "social_status_type_name_arabic");


                                    }
                                    else
                                    {
                                        ViewBag.socialStatus_TypeId = new SelectList(db.Social_Status_Type.Where(a => a.social_status_type_isDeleted != true), "social_status_type_id", "social_status_type_name", social_status.socialStatus_TypeId);

                                    }

                                }
                                ViewBag.socialStatus_citizenId_Husband = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true && a.citizen_gender == "Male"), "citizen_id", "citizen_national_id");
                                ViewBag.socialStatus_citizenId_Wife = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true && a.citizen_gender == "Female"), "citizen_id", "citizen_national_id");

                                return View();
                            }
                            else
                            {
                                social_status.social_status_isStill = true;
                                social_status.social_status_isDeleted = false;
                                db.Social_status.Add(social_status);
                                db.SaveChanges();
                                return RedirectToAction("Index");

                            }
                        }


                        var ladyNotMarried = db.Social_status.Where(a => a.socialStatus_citizenId_Wife == social_status.socialStatus_citizenId_Wife).Count();
                        if (ladyNotMarried == 0)
                        {
                            social_status.social_status_isStill = true;
                            social_status.social_status_isDeleted = false;
                            db.Social_status.Add(social_status);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewBag.socialStatus_TypeId = new SelectList(db.Social_Status_Type.Where(a => a.social_status_type_isDeleted != true), "social_status_type_id", "social_status_type_name");
                            if (Session["lang"] != null)
                            {
                                if (Session["lang"].ToString().Equals("ar-EG"))
                                {
                                    ViewBag.socialStatus_TypeId = new SelectList(db.Social_Status_Type.Where(a => a.social_status_type_isDeleted != true), "social_status_type_id", "social_status_type_name_arabic");


                                }
                                else
                                {
                                    ViewBag.socialStatus_TypeId = new SelectList(db.Social_Status_Type.Where(a => a.social_status_type_isDeleted != true), "social_status_type_id", "social_status_type_name", social_status.socialStatus_TypeId);

                                }

                            }
                            ViewBag.socialStatus_citizenId_Husband = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true && a.citizen_gender == "Male"), "citizen_id", "citizen_national_id");
                            ViewBag.socialStatus_citizenId_Wife = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true && a.citizen_gender == "Female"), "citizen_id", "citizen_national_id");

                            ViewBag.wifeIsAlreadyMarried = Servicely.Languages.Language.thisladyisalreadymarried;

                            return View();
                        }







                    }
                }
            }
            
           if(type == 2)
            {
                // if the man not married
                var man = db.Social_status.Where(a => a.socialStatus_citizenId_Husband == social_status.socialStatus_citizenId_Husband && a.social_status_isStill == true).FirstOrDefault();
                if (man == null)
                {
                    ViewBag.wifeIsAlreadyMarried = Servicely.Languages.Language.He_Sheisnotmarried;
                    ViewBag.socialStatus_TypeId = new SelectList(db.Social_Status_Type.Where(a => a.social_status_type_isDeleted != true), "social_status_type_id", "social_status_type_name");
                    if (Session["lang"] != null)
                    {
                        if (Session["lang"].ToString().Equals("ar-EG"))
                        {
                            ViewBag.socialStatus_TypeId = new SelectList(db.Social_Status_Type.Where(a => a.social_status_type_isDeleted != true), "social_status_type_id", "social_status_type_name_arabic");


                        }
                        else
                        {
                            ViewBag.socialStatus_TypeId = new SelectList(db.Social_Status_Type.Where(a => a.social_status_type_isDeleted != true), "social_status_type_id", "social_status_type_name", social_status.socialStatus_TypeId);

                        }

                    }
                    ViewBag.socialStatus_citizenId_Husband = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true && a.citizen_gender == "Male"), "citizen_id", "citizen_national_id");
                    ViewBag.socialStatus_citizenId_Wife = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true && a.citizen_gender == "Female"), "citizen_id", "citizen_national_id");

                    return View();

                }
                else 
                {
                    // verify that both is Married
                    var IsMarried = db.Social_status.Where(a => a.socialStatus_citizenId_Husband == social_status.socialStatus_citizenId_Husband && a.socialStatus_citizenId_Wife == social_status.socialStatus_citizenId_Wife && a.social_status_isStill == true).SingleOrDefault();
                    if(IsMarried != null)
                    {
                        var old = db.Social_status.Find(IsMarried.social_status_id);
                        old.social_status_isStill = false;
                        old.socialStatus_TypeId = 2;
                        old.social_status_to = social_status.social_status_from;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.wifeIsAlreadyMarried = Servicely.Languages.Language.Botharenotmarriedtoeachother;
                        ViewBag.socialStatus_TypeId = new SelectList(db.Social_Status_Type.Where(a => a.social_status_type_isDeleted != true), "social_status_type_id", "social_status_type_name");
                        if (Session["lang"] != null)
                        {
                            if (Session["lang"].ToString().Equals("ar-EG"))
                            {
                                ViewBag.socialStatus_TypeId = new SelectList(db.Social_Status_Type.Where(a => a.social_status_type_isDeleted != true), "social_status_type_id", "social_status_type_name_arabic");


                            }
                            else
                            {
                                ViewBag.socialStatus_TypeId = new SelectList(db.Social_Status_Type.Where(a => a.social_status_type_isDeleted != true), "social_status_type_id", "social_status_type_name", social_status.socialStatus_TypeId);

                            }

                        }
                        ViewBag.socialStatus_citizenId_Husband = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true && a.citizen_gender == "Male"), "citizen_id", "citizen_national_id");
                        ViewBag.socialStatus_citizenId_Wife = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true && a.citizen_gender == "Female"), "citizen_id", "citizen_national_id");

                        return View();
                    }

                }
            }

           // social_status.social_status_isStill = true;
            social_status.social_status_isDeleted = false;
            
            db.Social_status.Add(social_status);
                db.SaveChanges();
                return RedirectToAction("Index");
            



            //var data = db.Social_status.Where(a => a.socialStatus_citizenId_Husband == social_status.socialStatus_citizenId_Husband && a.socialStatus_citizenId_Wife == social_status.socialStatus_citizenId_Wife && a.social_status_isDeleted != true && a.Social_Status_Type == social_status.Social_Status_Type && a.social_status_to == null).SingleOrDefault();

            //if (data != null)
            //{
            //    ViewBag.errMsg = "Already exist for this citizen";
            //    return View(social_status);
            //}

            //if (ModelState.IsValid)
            //{
            //    db.Social_status.Add(social_status);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");

            //}

            


        }


        public ActionResult Edit(int id)
        {

            Social_status social_status = db.Social_status.Find(id);
            var data1 = db.Citizens.Where(a => a.citizen_id == social_status.socialStatus_citizenId_Husband && a.citizen_isDeleted != true).SingleOrDefault();
            var data2 = db.Citizens.Where(a => a.citizen_id == social_status.socialStatus_citizenId_Wife && a.citizen_isDeleted != true).SingleOrDefault();

            ViewBag.socialStatus_TypeId = new SelectList(db.Social_Status_Type.Where(a => a.social_status_type_isDeleted != true), "social_status_type_id", "social_status_type_name", social_status.socialStatus_TypeId);
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.socialStatus_TypeId = new SelectList(db.Social_Status_Type.Where(a => a.social_status_type_isDeleted != true), "social_status_type_id", "social_status_type_name_arabic");


                }
                else
                {
                    ViewBag.socialStatus_TypeId = new SelectList(db.Social_Status_Type.Where(a => a.social_status_type_isDeleted != true), "social_status_type_id", "social_status_type_name", social_status.socialStatus_TypeId);

                }

            }
            ViewBag.socialStatus_citizenId_Husband = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", social_status.socialStatus_citizenId_Husband);
            ViewBag.socialStatus_citizenId_Wife = new SelectList(db.Citizens, "citizen_id", "citizen_national_id", social_status.socialStatus_citizenId_Wife);
            return View(social_status);
        }


        [HttpPost]

        public ActionResult Edit(Social_status social_status)
        {
            var old = db.Social_status.Find(social_status.social_status_id);
            if (social_status.social_status_from != old.social_status_from)
            {
                old.social_status_from = social_status.social_status_from;
            }
            if (social_status.social_status_to != old.social_status_to)
            {
                old.social_status_to = social_status.social_status_to;
            }
            old.socialStatus_TypeId = social_status.socialStatus_TypeId;
            old.social_status_isStill = social_status.social_status_isStill;
            //    db.Entry(social_status).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");


        }

        // GET: Social_status/Delete/5
        public ActionResult Delete(int id)
        {

            Social_status social_status = db.Social_status.Find(id);

            return View(social_status);
        }

        // POST: Social_status/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            var old = db.Social_status.Find(id);
            old.social_status_isDeleted = true;
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
