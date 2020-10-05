using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Servicely.Models;

namespace Servicely.Controllers
{
    public class vio
    {
       
        public int Id { get; set; }
        public string Date { get; set; }
        public string CarName { get; set; }
        public string CarNameArabic { get; set; }
        public string citizen_national_id { get; set; }
        public string citizen_first_name { get; set; }
        public string citizen_second_name { get; set; }
        public string citizen_third_name { get; set; }
        public string citizen_fourth_name { get; set; }
        public string citizen_first_name_arabic { get; set; }
        public string citizen_second_name_arabic { get; set; }
        public string citizen_third_name_arabic { get; set; }
        public string citizen_fourth_name_arabic { get; set; }
        public string ViolationName { get; set; }
        public string ViolationNameArabic { get; set; }
        public decimal? ViolationPrice { get; set; }


    }
    public class Violation_CarLicenceM_MController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: Violation_CarLicenceM_M
        public ActionResult Index()
        {
            ViewBag.Car = new SelectList(db.CarLicences.Where(a => a.Is_Deleted != true), "Id", "CarCode");
            var violation_CarLicenceM_M = db.Violation_CarLicenceM_M.Include(v => v.CarLicence).Include(v => v.Violation);
            return View(violation_CarLicenceM_M.Where(a=>a.Is_Deleted!= true).ToList());
        }

        public ActionResult IndexToday()
        {
            ViewBag.Car = new SelectList(db.CarLicences.Where(a => a.Is_Deleted != true), "Id", "CarCode");
            var violation_CarLicenceM_M = db.Violation_CarLicenceM_M.Include(v => v.CarLicence).Include(v => v.Violation);
            return View(violation_CarLicenceM_M.Where(a => a.Is_Deleted != true&&a.Date.Value.Year==DateTime.Now.Year&& a.Date.Value.Month == DateTime.Now.Month&& a.Date.Value.Day == DateTime.Now.Day).ToList());
        }
        //----------- ajax Call -------------
        public JsonResult GetAllViolationByCarLicenceId1()
        {

            db.Configuration.ProxyCreationEnabled = false;

            var data = db.Violation_CarLicenceM_M.Where(a => a.Is_Deleted != true && a.Date.Value.Year == DateTime.Now.Year && a.Date.Value.Month == DateTime.Now.Month && a.Date.Value.Day == DateTime.Now.Day
).Select(a => new vio
            {
                Id = a.Id,
                Date = a.Date.ToString(),
                CarName = a.CarLicence.CarName,
                CarNameArabic = a.CarLicence.CarNameArabic,
                citizen_national_id = a.CarLicence.Citizen.citizen_national_id,
                citizen_first_name = a.CarLicence.Citizen.citizen_first_name,
                citizen_second_name = a.CarLicence.Citizen.citizen_second_name,
                citizen_third_name = a.CarLicence.Citizen.citizen_third_name,
                citizen_fourth_name = a.CarLicence.Citizen.citizen_fourth_name,
                citizen_first_name_arabic = a.CarLicence.Citizen.citizen_first_name_arabic,
                citizen_second_name_arabic = a.CarLicence.Citizen.citizen_second_name_arabic,
                citizen_third_name_arabic = a.CarLicence.Citizen.citizen_third_name_arabic,
                citizen_fourth_name_arabic = a.CarLicence.Citizen.citizen_fourth_name_arabic,

                ViolationName = a.Violation.ViolationName,
                ViolationNameArabic = a.Violation.ViolationNameArabic,
                ViolationPrice = a.Violation.ViolationPrice


            }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetAllViolationByCarLicenceId(int Id)
        {

            db.Configuration.ProxyCreationEnabled = false;

            var data = db.Violation_CarLicenceM_M.Where(a => a.Is_Deleted != true && a.CarLicenceId == Id).Select(a => new vio{ 
            Id=a.Id,
           Date= a.Date.ToString(),
           CarName= a.CarLicence.CarName ,
           CarNameArabic= a.CarLicence.CarNameArabic,
           citizen_national_id= a.CarLicence.Citizen.citizen_national_id,
           citizen_first_name= a.CarLicence.Citizen.citizen_first_name,
           citizen_second_name= a.CarLicence.Citizen.citizen_second_name,
           citizen_third_name= a.CarLicence.Citizen.citizen_third_name,
           citizen_fourth_name= a.CarLicence.Citizen.citizen_fourth_name,
              citizen_first_name_arabic=  a.CarLicence.Citizen.citizen_first_name_arabic,
               citizen_second_name_arabic= a.CarLicence.Citizen.citizen_second_name_arabic,
               citizen_third_name_arabic= a.CarLicence.Citizen.citizen_third_name_arabic,
               citizen_fourth_name_arabic= a.CarLicence.Citizen.citizen_fourth_name_arabic,

               ViolationName= a.Violation.ViolationName,
               ViolationNameArabic= a.Violation.ViolationNameArabic,
               ViolationPrice= a.Violation.ViolationPrice


            }).ToList();

            return Json(data , JsonRequestBehavior.AllowGet);

        }



        // GET: Violation_CarLicenceM_M/Create
        public ActionResult Create()
        {
            ViewBag.CarLicenceId = new SelectList(db.CarLicences.Where(a=>a.Is_Deleted!=true), "Id", "CarCode");
            ViewBag.ViolationId = new SelectList(db.Violations.Where(a => a.Is_Deleted != true), "Id", "ViolationName");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.CarLicenceId = new SelectList(db.CarLicences.Where(a => a.Is_Deleted != true), "Id", "CarCode");
                    ViewBag.ViolationId = new SelectList(db.Violations.Where(a => a.Is_Deleted != true), "Id", "ViolationNameArabic");

                }
            }

            return View();
        }

       
        [HttpPost]
      
        public ActionResult Create(Violation_CarLicenceM_M violation_CarLicenceM_M)
        {
            if (ModelState.IsValid)
            {

                decimal? price = db.Violations.Find(violation_CarLicenceM_M.ViolationId).ViolationPrice;

                var car = db.CarLicences.Find(violation_CarLicenceM_M.CarLicenceId).CitizenId;

               

                if (price != null)
                {
                    var citizenBalance = db.CitizenBalances.Where(a => a.CitizenBalance_citizen_id == car).SingleOrDefault();
                    if (citizenBalance != null)
                    {
                        if (citizenBalance.CitizenBalance_balance > price)
                        {
                            citizenBalance.CitizenBalance_balance = citizenBalance.CitizenBalance_balance - price;
                            violation_CarLicenceM_M.Is_Paid = true;


                             var father = db.Citizens.Find(car);
                             var email = db.Contacts.Where(a => a.contact_citizen_id == father.citizen_id).Join(db.Contact_Type, a => a.contact_contactType_id, b => b.contact_type_id, (a, b) => new { a, b }).Where(b => b.b.contact_type_name == "Email").Select(a => a.a).FirstOrDefault();
                            if (email != null)
                            {
                                string Title = "Dear " + father.citizen_first_name + " " + father.citizen_second_name + " " + father.citizen_third_name + " " + father.citizen_fourth_name + " \n  " +
                                  "You have been deducted from your account "+price+" EG fine ";
                                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                                smtp.EnableSsl = true;
                                smtp.UseDefaultCredentials = false;
                                smtp.Credentials = new NetworkCredential("CSCE4502@gmail.com", "csce4502f16");
                                //smtp.Send("CSCE4502@gmail.com", email.contact_data, "Vaccination", Title);
                                ViewBag.vcc = Servicely.Languages.Language.successfully_reserved_checkemail;
                               
                            }


                        }
                        else
                        {
                            violation_CarLicenceM_M.Is_Paid = false;
                            var father = db.Citizens.Find(car);
                            var email = db.Contacts.Where(a => a.contact_citizen_id == father.citizen_id).Join(db.Contact_Type, a => a.contact_contactType_id, b => b.contact_type_id, (a, b) => new { a, b }).Where(b => b.b.contact_type_name == "Email").Select(a => a.a).FirstOrDefault();
                            if (email != null)
                            {
                                string Title = "Dear " + father.citizen_first_name + " " + father.citizen_second_name + " " + father.citizen_third_name + " " + father.citizen_fourth_name + " \n  " +
                                  "";
                                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                                smtp.EnableSsl = true;
                                smtp.UseDefaultCredentials = false;
                                smtp.Credentials = new NetworkCredential("CSCE4502@gmail.com", "csce4502f16");
                                //smtp.Send("CSCE4502@gmail.com", email.contact_data, "Vaccination", Title);
                                ViewBag.vcc = Servicely.Languages.Language.successfully_reserved_checkemail;
                            }
                        }

                    }
                }
                violation_CarLicenceM_M.Date = DateTime.Now;
                
                db.Violation_CarLicenceM_M.Add(violation_CarLicenceM_M);
                db.SaveChanges();


                return RedirectToAction("Index");
            }

            ViewBag.CarLicenceId = new SelectList(db.CarLicences.Where(a => a.Is_Deleted != true), "Id", "CarCode");
            ViewBag.ViolationId = new SelectList(db.Violations.Where(a => a.Is_Deleted != true), "Id", "ViolationName");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.CarLicenceId = new SelectList(db.CarLicences.Where(a => a.Is_Deleted != true), "Id", "CarCode");
                    ViewBag.ViolationId = new SelectList(db.Violations.Where(a => a.Is_Deleted != true), "Id", "ViolationNameArabic");

                }
            }
            return View(violation_CarLicenceM_M);
        }

        // GET: Violation_CarLicenceM_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            Violation_CarLicenceM_M violation_CarLicenceM_M = db.Violation_CarLicenceM_M.Find(id);
            if (violation_CarLicenceM_M == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            ViewBag.CarLicenceId = new SelectList(db.CarLicences.Where(a=>a.Is_Deleted!=true), "Id", "CarCode", violation_CarLicenceM_M.CarLicenceId);
            ViewBag.ViolationId = new SelectList(db.Violations.Where(a=>a.Is_Deleted!=true), "Id", "ViolationName", violation_CarLicenceM_M.ViolationId);

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.CarLicenceId = new SelectList(db.CarLicences.Where(a => a.Is_Deleted != true), "Id", "CarCode", violation_CarLicenceM_M.CarLicenceId);
                    ViewBag.ViolationId = new SelectList(db.Violations.Where(a => a.Is_Deleted != true), "Id", "ViolationNameArabic", violation_CarLicenceM_M.ViolationId);

            
                }
            }
            return View(violation_CarLicenceM_M);
        }

        // POST: Violation_CarLicenceM_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
      
        public ActionResult Edit(Violation_CarLicenceM_M violation_CarLicenceM_M)
        {
            if (ModelState.IsValid)
            {
                var old = db.Violation_CarLicenceM_M.Find(violation_CarLicenceM_M.Id);
                old.ViolationId = violation_CarLicenceM_M.ViolationId;
                old.CarLicenceId = violation_CarLicenceM_M.CarLicenceId;
                old.Date = violation_CarLicenceM_M.Date;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarLicenceId = new SelectList(db.CarLicences.Where(a=>a.Is_Deleted !=true), "Id", "CarCode", violation_CarLicenceM_M.CarLicenceId);
            ViewBag.ViolationId = new SelectList(db.Violations.Where(a=>a.Is_Deleted!=true), "Id", "ViolationName", violation_CarLicenceM_M.ViolationId);
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.CarLicenceId = new SelectList(db.CarLicences.Where(a => a.Is_Deleted != true), "Id", "CarCode", violation_CarLicenceM_M.CarLicenceId);
                    ViewBag.ViolationId = new SelectList(db.Violations.Where(a => a.Is_Deleted != true), "Id", "ViolationNameArabic", violation_CarLicenceM_M.ViolationId);


                }
            }
            return View(violation_CarLicenceM_M);
        }

        // GET: Violation_CarLicenceM_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            Violation_CarLicenceM_M violation_CarLicenceM_M = db.Violation_CarLicenceM_M.Find(id);
            if (violation_CarLicenceM_M == null)
            {
                return RedirectToAction("errorpage", "home");
            }
            return View(violation_CarLicenceM_M);
        }

        // POST: Violation_CarLicenceM_M/Delete/5
        [HttpPost, ActionName("Delete")]
    
        public ActionResult DeleteConfirmed(int id)
        {
            Violation_CarLicenceM_M violation_CarLicenceM_M = db.Violation_CarLicenceM_M.Find(id);
            violation_CarLicenceM_M.Is_Deleted = true;
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
