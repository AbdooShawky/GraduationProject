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
    public class VaccinationsSchedulesController : BaseController
    {
        DbMasterEntities1 db = new DbMasterEntities1();
        // GET: VaccinationsSchedules
        public ActionResult Index()
        {
            var data = db.VaccinationReservations.Where(a => a.VaccReservation_isDeleted != true).Include(v => v.Citizen).Include(v => v.HealthCare).Include(v => v.VaccinationType);
                       // var vaccinationReservations = db.VaccinationReservations.Include(v => v.Citizen).Include(v => v.HealthCare).Include(v => v.VaccinationType);

            
            return View(data.ToList());
        }
        public ActionResult IndexToday()
        {
            var data = db.VaccinationReservations.Where(a => a.VaccReservation_isDeleted != true && a.VaccReservation_date.Value.Day == DateTime.Now.Day && a.VaccReservation_date.Value.Month == DateTime.Now.Month && a.VaccReservation_date.Value.Year == DateTime.Now.Year);


            return View(data.ToList());
        }

        public ActionResult IndexUser()
        {
            int cid = 0;
            if (Session["citizenID"] != null)
            cid = (int)Session["citizenID"];
        
            var da = db.VaccinationReservations.Where(a => a.VaccReservation_isDeleted != true && a.VaccReservation_Citizen_id == cid); //, a => a.citizen_id, b => b.VaccReservation_Citizen_id, (a, b) => b);
            
            return View(da.ToList());
        }


        /// <summary>
        /// Ajax Call bta3t el vaccination wellazy meno :)
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllChiledByCitizenId()
        {
            int cid = (int)Session["citizenID"];
            db.Configuration.ProxyCreationEnabled = false;

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    var da = db.Citizens.Where(a => a.citizen_father_id == cid).Select(a => new { a.citizen_id, a.citizen_national_id, a.citizen_first_name_arabic, a.citizen_second_name_arabic, a.citizen_third_name_arabic, a.citizen_fourth_name_arabic });

                    return Json(da, JsonRequestBehavior.AllowGet);
                }
            }
            var data = db.Citizens.Where(a => a.citizen_father_id == cid).Select(a=> new { a.citizen_id,a.citizen_national_id , a.citizen_first_name,a.citizen_second_name,a.citizen_third_name,a.citizen_fourth_name});

            return Json( data , JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetFirstName(int id)
        {
            
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    return Json(db.Citizens.Find(id).citizen_first_name_arabic, JsonRequestBehavior.AllowGet);

                }
            }
            return Json(db.Citizens.Find(id).citizen_first_name, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetAllVaccinationTypeByCitizenId(int cti)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var citizen = db.Citizens.Find(cti).citizen_birthDate;
            var vccType = db.VaccinationTypes.Where(a=> a.vaccination_isDeleted!= true);

            foreach (var item in vccType)
            {
                item.vaccination_type_period += 60;
            }
             List< VaccinationType> tb = new List<VaccinationType>();
            foreach (var item in vccType)
            {
                TimeSpan t = DateTime.Now.Subtract(Convert.ToDateTime(citizen));
                if( t.Days < item.vaccination_type_period  )
                {
                    tb.Add(item);
                }

            }

            var data = tb.ToList();


            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    return Json(data.Select(a => new { a.vaccination_type_name_arabic, a.vaccination_type_id }), JsonRequestBehavior.AllowGet);


                }
            }
            return Json(data.Select(a=> new { a.vaccination_type_name,a.vaccination_type_id}), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllHealtCaresByVaccinationType_HealthcareTypeAndDistrictIds(int VtId , int DId , int HCTId)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var data = db.HealthCares.Where(a=> a.healthcare_type_id == HCTId && a.hospital_district_id == DId).Join(db.VacinationHealthCareM_M,a=> a.hospital_id, b=> b.vaccinationhealthcare_hospital , (a,b)=> new { a,b}).Where(x=> x.b.vacinationhealthcare_vaccination == x.b.VaccinationType.vaccination_type_id && x.b.vacinationhealthcare_vaccination == VtId).Select(a=> new { a.a.hospital_id , a.a.hospital_name});
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    var dt  = db.HealthCares.Where(a => a.healthcare_type_id == HCTId && a.hospital_district_id == DId).Join(db.VacinationHealthCareM_M, a => a.hospital_id, b => b.vaccinationhealthcare_hospital, (a, b) => new { a, b }).Where(x => x.b.vacinationhealthcare_vaccination == x.b.VaccinationType.vaccination_type_id && x.b.vacinationhealthcare_vaccination == VtId).Select(a => new { a.a.hospital_id, a.a.hospital_name_arabic });
                    return Json(dt, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(data,JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetAllVaccinationAppointmentByHealthCareId(int hos , int vacType)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var appointment = db.ScheduleVaccM_M.Where(a => a.HealthCareId == hos && a.scheduleVacc_vaccType_id == vacType).Join(db.ScheduleVaccinations.Where(a => a.schedule_isDeleted != true && a.checkup_date > DateTime.Now), x => x.scheduleVacc_schedule_id, a => a.schedule_id, (x, a) => new Date { checkup_date = a.checkup_date.ToString(), checkup_end = a.checkup_end.ToString(), checkup_start = a.checkup_start.ToString(), schedule_id = a.schedule_id });

            // var appointment = db.ScheduleVaccM_M.Where(a => a.HealthCareId == hos && a.scheduleVacc_vaccType_id == vacType).Join(db.ScheduleVaccinations.Where(a => a.schedule_isDeleted != true), a => a.scheduleVacc_schedule_id, cb => cb.schedule_id , (cb)=> new {cb});
            //var data = (
            ////    from health in db.HealthCares
            ////    from homm in db.VacinationHealthCareM_M
            ////    from smm in db.ScheduleVaccM_M
            ////    from VaccType in db.VaccinationTypes
            ////    from sc in db.ScheduleVaccinations.Where(a => health.hospital_id == hos && health.hospital_id == homm.vaccinationhealthcare_hospital
            ////    && VaccType.vaccination_type_id == homm.vacinationhealthcare_vaccination
            ////    && a.schedule_id == smm.scheduleVacc_schedule_id && VaccType.vaccination_type_id == smm.scheduleVacc_vaccType_id && a.checkup_date.Value.Year >= DateTime.Now.Year
            ////    && a.checkup_date.Value.Month >= DateTime.Now.Month && ((a.checkup_date.Value.Day > DateTime.Now.Day && a.checkup_date.Value.Month >= DateTime.Now.Month) || (a.checkup_date.Value.Day <= DateTime.Now.Day && a.checkup_date.Value.Month > DateTime.Now.Month))

            ////    )
            ////    select new Date
            ////    {
            ////        checkup_date =sc.checkup_date.ToString(),
            ////        checkup_start = sc.checkup_start.ToString(),
            ////        checkup_end =sc.checkup_end.ToString(),
            ////        schedule_id= sc.schedule_id

            ////    }

            //    );

            List<object> mm = new List<object>();
               List< Date> d = new List<Date>();
            string[] aa;
            string[] b;
            foreach (var item in appointment)
            {
                aa = item.checkup_start.Split('.');
                b = item.checkup_end.Split('.');
                item.checkup_start = aa[0];
                item.checkup_end = b[0];

                aa = item.checkup_start.Split(':');
                b = item.checkup_end.Split(':');
                item.checkup_start = aa[0] + ":" + aa[1];
                item.checkup_end = b[0] + ":" + b[1];
                d.Add(item);
            }
            

            return Json(d, JsonRequestBehavior.AllowGet) ;
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name");
            ViewBag.VaccType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                    ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name_arabic");
                    ViewBag.VaccType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name_arabic");
                }
            }

            return View();
        }
        [HttpPost]
        
        public ActionResult Create(VaccinationReservation vaccinationReservation ,int schedule ,int VaccType )
        {
            

            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name");
            ViewBag.VaccType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                    ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name_arabic");
                    ViewBag.VaccType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name_arabic");

                }
            }
            int cid =  (int)Session["citizenID"];
            vaccinationReservation.VaccReservation_VaccinationType_id = VaccType;
            vaccinationReservation.VaccReservation_Citizen_id = cid;
            vaccinationReservation.VaccReservation_date = db.ScheduleVaccinations.Find(schedule).checkup_date;

            //---------------------------------------------------------------------
            var CitizenAge = db.Citizens.Find(vaccinationReservation.VaccReservation_child_id).citizen_birthDate;
            var AgeonVacc = db.VaccinationTypes.Find(VaccType);
            //if (AgeonVacc != null)
            //{
            //    int? vaccday = AgeonVacc.vaccination_type_period + 60;
            //    TimeSpan t = db.ScheduleVaccinations.Find(schedule).checkup_date.Value.Subtract(Convert.ToDateTime(CitizenAge));

            //    if (t.Days > vaccday || t.Days < AgeonVacc.vaccination_type_period)
            //    {
            //        ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            //        ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name");
            //        ViewBag.VaccType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name");

            //        if (Session["lang"] != null)
            //        {
            //            if (Session["lang"].ToString().Equals("ar-EG"))
            //            {
            //                 ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

            //        ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name_arabic");
            //        ViewBag.VaccType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name_arabic");

                        
            //            }
            //        }

            //        ViewBag.vcc =Servicely.Languages.Language.Vaccination_not_allowed_to_the_child;
            //        return View();
                    
            //    }
            //}

            int? reservationmax = db.ScheduleVaccinations.Find(schedule).checkup_number;

            int maxreserv = db.ScheduleVaccReservationM_M.Where(a => a.ScheduleVaccReservation_schedule_id == schedule).Join(db.VaccinationReservations, a => a.ScheduleVaccReservation_reservationVacc_id, b => b.VaccReservation_id, (a, b) => new { b }).Where(a => a.b.VaccReservation_isDeleted != true && a.b.VaccReservation_date == vaccinationReservation.VaccReservation_date && a.b.VaccReservation_HealthCare_id == vaccinationReservation.VaccReservation_HealthCare_id && a.b.VaccReservation_VaccinationType_id == VaccType &&a.b.VaccReservation_cancel != true).Count();

            if (maxreserv == reservationmax)
            {
                ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

                ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name");
                ViewBag.VaccType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name");

                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                        ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name_arabic");
                        ViewBag.VaccType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name_arabic");


                    }
                }
                ViewBag.vcc = Servicely.Languages.Language.day_is_full_choose_another_date;
                return View();
               
            }





            var data = db.VaccinationReservations.Where(a=> a.VaccReservation_Citizen_id == vaccinationReservation.VaccReservation_Citizen_id && a.VaccReservation_VaccinationType_id == VaccType && a.VaccReservation_checked==true && a.VaccReservation_date <DateTime.Now).FirstOrDefault();
            if( data != null )
            {
                ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

                ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name");
                ViewBag.VaccType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name");
                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                        ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name_arabic");
                        ViewBag.VaccType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name_arabic");


                    }
                }
                ViewBag.vcc = Servicely.Languages.Language.your_child_is_vaccinated;
                return View();

            }
            var data1 = db.VaccinationReservations.Where(a => a.VaccReservation_Citizen_id == vaccinationReservation.VaccReservation_Citizen_id && a.VaccReservation_child_id == vaccinationReservation.VaccReservation_child_id && a.VaccReservation_VaccinationType_id == vaccinationReservation.VaccReservation_VaccinationType_id && ((a.VaccReservation_date.Value.Year >= DateTime.Now.Year && a.VaccReservation_date.Value.Month > DateTime.Now.Month) || (a.VaccReservation_date.Value.Year >= DateTime.Now.Year && a.VaccReservation_date.Value.Month == DateTime.Now.Month && a.VaccReservation_date.Value.Day >= DateTime.Now.Day)) && a.VaccReservation_cancel != true && a.VaccReservation_isDeleted != true).FirstOrDefault();
            if ( data1 != null)
            {
                ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

                ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name");
                ViewBag.VaccType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name");
                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                        ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name_arabic");
                        ViewBag.VaccType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name_arabic");


                    }
                }
                ViewBag.vcc = Servicely.Languages.Language.you_are_already_reserved_on_this_day;
                return View();
            }

            string cod = Guid.NewGuid().ToString().Substring(0, 5) + "-" + Guid.NewGuid().ToString().Substring(0, 5);

            vaccinationReservation.VaccReservation_Code = cod;
            vaccinationReservation.VaccReservation_checked = false;
            vaccinationReservation.TransactionDate = DateTime.Now;
            db.VaccinationReservations.Add(vaccinationReservation);
            db.SaveChanges();
            ScheduleVaccReservationM_M mm = new ScheduleVaccReservationM_M();
            mm.ScheduleVaccReservation_reservationVacc_id = vaccinationReservation.VaccReservation_id;
            mm.ScheduleVaccReservation_schedule_id = schedule;
            db.ScheduleVaccReservationM_M.Add(mm);
            db.SaveChanges();

            var father = db.Citizens.Find(cid);
            var child = db.Citizens.Find(vaccinationReservation.VaccReservation_child_id);
            // var address = db.Addresses.Where(a => a.address_isCurrent == true).Join(db.AddressCitizens, a => a.address_id, b => b.CA_AddressId, (a, b) => b).Where(a => a.CA_CitizenId == itemm.citizen_id).Select(a => a.Address.address_district_id).SingleOrDefault();
            //var hospital = db.HealthCares.Where(a => a.hospital_district_id == address).FirstOrDefault();
            var vaccSchedule = db.ScheduleVaccM_M.Where(a=> a.scheduleVacc_vaccType_id == vaccinationReservation.VaccReservation_VaccinationType_id).Join(db.ScheduleVaccinations,a=>a.scheduleVacc_schedule_id , b=> b.schedule_id,(a,b)=> new { b}).FirstOrDefault();
            var email = db.Contacts.Where(a => a.contact_citizen_id == cid).Join(db.Contact_Type, a => a.contact_contactType_id, b => b.contact_type_id, (a, b) => new { a, b }).Where(b => b.b.contact_type_name == "Email").Select(a => a.a).FirstOrDefault();
            int code = maxreserv;
            var HealthCare = db.HealthCares.Find(vaccinationReservation.VaccReservation_HealthCare_id);
            if(email != null)
            {
                //string Title = "Dear " + father.citizen_first_name + " " + father.citizen_second_name + " " + father.citizen_third_name + " " + father.citizen_fourth_name + " \n  " +
                //  "You have reserved an appointment to your chilled " + child.citizen_first_name + "( " + child.citizen_national_id + " ) on " + vaccSchedule.b.checkup_date.Value.ToShortDateString()+" from : "+ vaccSchedule.b.checkup_start +"to : "+ vaccSchedule.b.checkup_end + " \n\n" +
                //  " Health Care Name: "+HealthCare.hospital_name+" \n Health Care Address: "+HealthCare.District.Region.City.State.state_name +" "+ HealthCare.District.Region.City.city_name + " " + HealthCare.District.Region.region_name + " " + HealthCare.District.district_name + " \n" +"Reservation Code: "
                //   +( code+=1 ) + " \n yours sincerely , ";
                //SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                //smtp.EnableSsl = true;
                //smtp.UseDefaultCredentials = false;
                //smtp.Credentials = new NetworkCredential("CSCE4502@gmail.com", "csce4502f16");
                //smtp.Send("CSCE4502@gmail.com", email.contact_data, "Vaccination", Title);
                ViewBag.vcc = Servicely.Languages.Language.successfully_reserved_checkemail;
                return View();
            }




            ViewBag.vcc = Servicely.Languages.Language.successfully_reserved;
            return View();
        }

        public ActionResult Edit (int id)
        {
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name");
            ViewBag.VaccType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                    ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name_arabic");
                    ViewBag.VaccType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name_arabic");


                }
            }
            var data = db.VaccinationReservations.Find(id);
            return View(data);

        }
        [HttpPost]
        public ActionResult Edit( VaccinationReservation vaccinationReservation , int schedule, int VaccType)
        {
            var old = db.VaccinationReservations.Find(vaccinationReservation.VaccReservation_id);
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

            ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name");
            ViewBag.VaccType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name");
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                    ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name_arabic");
                    ViewBag.VaccType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name_arabic");


                }
            }

            //---------------------------------------------------------------------
            var CitizenAge = db.Citizens.Find(vaccinationReservation.VaccReservation_Citizen_id).citizen_birthDate;
            var AgeonVacc = db.VaccinationTypes.Find(VaccType);
            if (AgeonVacc != null)
            {
                int? vaccday = AgeonVacc.vaccination_type_period + 60;
                TimeSpan t = vaccinationReservation.VaccReservation_date.Value.Subtract(Convert.ToDateTime(CitizenAge));

                if (t.Days > vaccday || t.Days < AgeonVacc.vaccination_type_period)
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

                    ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name");
                    ViewBag.VaccType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name");
                    if (Session["lang"] != null)
                    {
                        if (Session["lang"].ToString().Equals("ar-EG"))
                        {
                            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                            ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name_arabic");
                            ViewBag.VaccType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name_arabic");


                        }
                    }
                    ViewBag.vcc = Servicely.Languages.Language.Vaccination_not_allowed_to_the_child;
                    return View();

                }
            }

            int? reservationmax = db.ScheduleVaccinations.Find(schedule).checkup_number;

            int maxreserv = db.ScheduleVaccReservationM_M.Where(a => a.ScheduleVaccReservation_schedule_id == schedule).Join(db.VaccinationReservations, a => a.ScheduleVaccReservation_reservationVacc_id, b => b.VaccReservation_id, (a, b) => new { b }).Where(a => a.b.VaccReservation_date == vaccinationReservation.VaccReservation_date).Count();

            if (maxreserv == reservationmax)
            {
                ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

                ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name");
                ViewBag.VaccType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name");
                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                        ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name_arabic");
                        ViewBag.VaccType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name_arabic");


                    }
                }
                ViewBag.vcc =Servicely.Languages.Language.day_is_full_choose_another_date;
                return View();

            }





            var data = db.VaccinationReservations.Where(a => a.VaccReservation_Citizen_id == vaccinationReservation.VaccReservation_Citizen_id && a.VaccReservation_VaccinationType_id == VaccType && a.VaccReservation_checked == true && a.VaccReservation_date < DateTime.Now).FirstOrDefault();
            if (data != null)
            {
                ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

                ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name");
                ViewBag.VaccType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name");
                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                        ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name_arabic");
                        ViewBag.VaccType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name_arabic");


                    }
                }
                ViewBag.vcc = Servicely.Languages.Language.your_child_is_vaccinated;
                return View();

            }
            var data1 = db.VaccinationReservations.Where(a => a.VaccReservation_Citizen_id == vaccinationReservation.VaccReservation_Citizen_id && a.VaccReservation_VaccinationType_id == VaccType && a.VaccReservation_date >= DateTime.Now).FirstOrDefault();
            if (data1 != null)
            {
                ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");

                ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name");
                ViewBag.VaccType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name");
                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");

                        ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name_arabic");
                        ViewBag.VaccType = new SelectList(db.VaccinationTypes.Where(a => a.vaccination_isDeleted != true), "vaccination_type_id", "vaccination_type_name_arabic");


                    }
                }
                ViewBag.vcc = Servicely.Languages.Language.you_have_already_reservation;
                return View();
            }





            
            old.VaccReservation_HealthCare_id = vaccinationReservation.VaccReservation_HealthCare_id;
            old.VaccReservation_date = vaccinationReservation.VaccReservation_date;
            old.VaccReservation_Citizen_id = vaccinationReservation.VaccReservation_Citizen_id;
            old.VaccReservation_VaccinationType_id = VaccType;
            
            db.SaveChanges();


            return RedirectToAction("IndexUser");

        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = db.VaccinationReservations.Where(a => a.VaccReservation_isDeleted != true && a.VaccReservation_id == id).Include(v => v.Citizen).Include(a=> a.Citizen.Citizen1).Include(v => v.HealthCare).Include(v => v.VaccinationType).SingleOrDefault();
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        [HttpPost, ActionName("Delete")]
        // POST: doctorReservation/Delete/5
        //delete of admin page
        public ActionResult DeleteConfirmed(int? id)
        {
            var data = db.VaccinationReservations.Where(a => a.VaccReservation_isDeleted != true && a.VaccReservation_id == id && a.VaccReservation_date > DateTime.Now).SingleOrDefault();
            if (data != null)
            {
                ViewBag.err = "old reservation can not be canceled";
                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.err = "لا يمكن الغاء حجز قديم";

                    }
                }
                var data1 = db.VaccinationReservations.Where(a => a.VaccReservation_isDeleted != true && a.VaccReservation_id == id).Include(v => v.Citizen).Include(a => a.Citizen.Citizen1).Include(v => v.HealthCare).Include(v => v.VaccinationType).SingleOrDefault();

                return View(data1);
            }
            VaccinationReservation vacc = db.VaccinationReservations.Find(id);
            vacc.VaccReservation_isDeleted = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = db.VaccinationReservations.Where(a => a.VaccReservation_isDeleted != true && a.VaccReservation_id == id).Include(v => v.Citizen).Include(a => a.Citizen.Citizen1).Include(v => v.HealthCare).Include(v => v.VaccinationType).SingleOrDefault();
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        [HttpPost, ActionName("DeleteUser")]
        // POST: doctorReservation/Delete/5
        //delete of admin page
        public ActionResult DeleteConfirmedUser(int? id)
        {
            var data = db.VaccinationReservations.Where(a => a.VaccReservation_isDeleted != true && a.VaccReservation_id == id&& a.VaccReservation_date > DateTime.Now).SingleOrDefault();
            if(data == null)
            {
                ViewBag.err = "old reservation can not be canceled";
                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.err = "لا يمكن الغاء حجز قديم";

                    }
                }
                var data1 = db.VaccinationReservations.Where(a => a.VaccReservation_isDeleted != true && a.VaccReservation_id == id).Include(v => v.Citizen).Include(a => a.Citizen.Citizen1).Include(v => v.HealthCare).Include(v => v.VaccinationType).SingleOrDefault();

                return View(data1);
            }
            VaccinationReservation vacc = db.VaccinationReservations.Find(id);
            vacc.VaccReservation_isDeleted = true;
            db.SaveChanges();
            return RedirectToAction("IndexUser");
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            //your handling logic here
            filterContext.ExceptionHandled = true;
            filterContext.Result = RedirectToAction("errorpage", "home");
        }
    }
}