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
    public class reservation
    {
       
        public string checkup_end { get; set; }
        public string checkup_start { get; set; }
        public string citizen_first_name1 { get; set; }
        public string citizen_second_name1 { get; set; }
        public string citizen_third_name1 { get; set; }
        public string citizen_fourth_name1 { get; set; }
        public string citizen_first_name_arabic1 { get; set; }
        public string citizen_second_name_arabic1 { get; set; }
        public string citizen_third_name_arabic1 { get; set; }
        public string citizen_fourth_name_arabic1 { get; set; }
        public string citizen_first_name { get; set; }
        public string citizen_second_name { get; set; }
        public string citizen_third_name { get; set; }
        public string citizen_fourth_name { get; set; }
        public string citizen_first_name_arabic { get; set; }
        public string citizen_second_name_arabic { get; set; }
        public string citizen_third_name_arabic { get; set; }
        public string citizen_fourth_name_arabic { get; set; }
        public string hospital_name { get; set; }
        public string hospital_name_arabic { get; set; }
        public string district_name { get; set; }
        public string district_arabic_name { get; set; }
        public string Reservation_date { get; set; }
        public string checkup_date { get; set; }
        public int healthcareReservation_id { get; set; }


    }
    public class doctorReservationController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: doctorReservation
        public ActionResult Index()
        {
            //int cid = (int) Session["citizenID"] ;
            //var healthcareReservations = db.ReservationScheduleM_M.Join(db.ScheduleHealthCares,a=>a.reservationSchedule_schedule,b=>b.schedule_id,(a,b)=>new { a,b}).Join(db.HealthcareReservations,a=>a.a.reservationSchedule_reservation,b=>b.healthcareReservation_id,(x,y)=>new {   x,y  }).Where(a=>a.y.healthcareReservation_citizen_id == cid && a.y.healthcareReservation_isDeleted !=true && a.y.healthcareReservation_id == a.x.a.reservationSchedule_reservation && a.x.a.reservationSchedule_schedule == a.x.b.schedule_id   ).Select(a=>a.x.a).Include(a=> a.HealthcareReservation).Include(a=> a.HealthcareReservation.Citizen).Include(a=> a.ScheduleHealthCare);          
            return View();
        }
        public JsonResult IndexOfUser()
        {

            int cid = (int)Session["citizenID"];
            db.Configuration.ProxyCreationEnabled = false;
            var healthcareReservations = db.ReservationScheduleM_M.Join(db.ScheduleHealthCares, a => a.reservationSchedule_schedule, b => b.schedule_id, (a, b) => new { a, b }).Join(db.HealthcareReservations, a => a.a.reservationSchedule_reservation, b => b.healthcareReservation_id, (x, y) => new { x, y }).Where(a => a.y.healthcareReservation_citizen_id == cid && a.y.healthcareReservation_isDeleted != true && a.y.healthcareReservation_id == a.x.a.reservationSchedule_reservation && a.x.a.reservationSchedule_schedule == a.x.b.schedule_id).Select(a => a.x.a).Include(a => a.HealthcareReservation).Include(a => a.HealthcareReservation.Citizen).Include(a => a.ScheduleHealthCare).Select(a=> 
            new reservation
            {
                healthcareReservation_id=a.HealthcareReservation.healthcareReservation_id,
                Reservation_date =a.HealthcareReservation.Reservation_date.ToString(),
                checkup_date=  a.ScheduleHealthCare.checkup_date.ToString(),
                citizen_first_name =a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_first_name,
                citizen_second_name =a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_second_name,
                citizen_third_name= a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_third_name,
                citizen_fourth_name= a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_fourth_name,
                citizen_first_name_arabic= a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_first_name_arabic,
                citizen_second_name_arabic = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_second_name_arabic,
                citizen_third_name_arabic = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_third_name_arabic,
                citizen_fourth_name_arabic = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_fourth_name_arabic,
                hospital_name = a.HealthcareReservation.HealthCare.hospital_name,
                hospital_name_arabic = a.HealthcareReservation.HealthCare.hospital_name_arabic,
                district_name = a.HealthcareReservation.HealthCare.District.district_name,
                district_arabic_name = a.HealthcareReservation.HealthCare.District.district_arabic_name,
                checkup_end = a.ScheduleHealthCare.checkup_end.Value.Hours.ToString() + ":" + a.ScheduleHealthCare.checkup_end.Value.Minutes.ToString(),
                checkup_start = a.ScheduleHealthCare.checkup_start.Value.Hours.ToString() + ":" + a.ScheduleHealthCare.checkup_start.Value.Minutes.ToString(),

            }

            );

            return Json(healthcareReservations, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IndexOfUserU(DateTime from, DateTime to)
        {

            int cid = (int)Session["citizenID"];
            db.Configuration.ProxyCreationEnabled = false;
            var healthcareReservations = db.ReservationScheduleM_M.Join(db.ScheduleHealthCares, a => a.reservationSchedule_schedule, b => b.schedule_id, (a, b) => new { a, b }).Join(db.HealthcareReservations, a => a.a.reservationSchedule_reservation, b => b.healthcareReservation_id, (x, y) => new { x, y }).Where(a => a.y.healthcareReservation_citizen_id == cid && a.y.healthcareReservation_isDeleted != true && a.y.healthcareReservation_id == a.x.a.reservationSchedule_reservation && a.x.a.reservationSchedule_schedule == a.x.b.schedule_id && a.y.Reservation_date >= from && a.y.Reservation_date <= to).Select(a => a.x.a).Include(a => a.HealthcareReservation).Include(a => a.HealthcareReservation.Citizen).Include(a => a.ScheduleHealthCare).Select(a =>
            new reservation
            {
                healthcareReservation_id = a.HealthcareReservation.healthcareReservation_id,
                Reservation_date = a.HealthcareReservation.Reservation_date.ToString(),
                checkup_date = a.ScheduleHealthCare.checkup_date.ToString(),
                citizen_first_name = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_first_name,
                citizen_second_name = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_second_name,
                citizen_third_name = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_third_name,
                citizen_fourth_name = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_fourth_name,
                citizen_first_name_arabic = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_first_name_arabic,
                citizen_second_name_arabic = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_second_name_arabic,
                citizen_third_name_arabic = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_third_name_arabic,
                citizen_fourth_name_arabic = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_fourth_name_arabic,
                hospital_name = a.HealthcareReservation.HealthCare.hospital_name,
                hospital_name_arabic = a.HealthcareReservation.HealthCare.hospital_name_arabic,
                district_name = a.HealthcareReservation.HealthCare.District.district_name,
                district_arabic_name = a.HealthcareReservation.HealthCare.District.district_arabic_name,
                checkup_end = a.ScheduleHealthCare.checkup_end.Value.Hours.ToString() + ":" + a.ScheduleHealthCare.checkup_end.Value.Minutes.ToString(),
                checkup_start = a.ScheduleHealthCare.checkup_start.Value.Hours.ToString() + ":" + a.ScheduleHealthCare.checkup_start.Value.Minutes.ToString(),

            }

            );

            return Json(healthcareReservations, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetIndexOfUser( int Id)
        {

            int cid = Id;
            db.Configuration.ProxyCreationEnabled = false;
            var healthcareReservations = db.ReservationScheduleM_M.Join(db.ScheduleHealthCares, a => a.reservationSchedule_schedule, b => b.schedule_id, (a, b) => new { a, b }).Join(db.HealthcareReservations, a => a.a.reservationSchedule_reservation, b => b.healthcareReservation_id, (x, y) => new { x, y }).Where(a => a.y.healthcareReservation_citizen_id == cid && a.y.healthcareReservation_isDeleted != true && a.y.healthcareReservation_id == a.x.a.reservationSchedule_reservation && a.x.a.reservationSchedule_schedule == a.x.b.schedule_id).Select(a => a.x.a).Include(a => a.HealthcareReservation).Include(a => a.HealthcareReservation.Citizen).Include(a => a.ScheduleHealthCare).Select(a =>
            new reservation
            {
                healthcareReservation_id = a.HealthcareReservation.healthcareReservation_id,
                Reservation_date = a.HealthcareReservation.Reservation_date.ToString(),
                checkup_date = a.ScheduleHealthCare.checkup_date.ToString(),
                citizen_first_name = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_first_name,
                citizen_second_name = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_second_name,
                citizen_third_name = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_third_name,
                citizen_fourth_name = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_fourth_name,
                citizen_first_name_arabic = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_first_name_arabic,
                citizen_second_name_arabic = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_second_name_arabic,
                citizen_third_name_arabic = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_third_name_arabic,
                citizen_fourth_name_arabic = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_fourth_name_arabic,
                citizen_first_name1=a.HealthcareReservation.Citizen.citizen_first_name,
                citizen_second_name1=a.HealthcareReservation.Citizen.citizen_second_name,
                citizen_third_name1= a.HealthcareReservation.Citizen.citizen_third_name,
                citizen_fourth_name1= a.HealthcareReservation.Citizen.citizen_fourth_name,
                citizen_first_name_arabic1= a.HealthcareReservation.Citizen.citizen_first_name_arabic,
                citizen_second_name_arabic1= a.HealthcareReservation.Citizen.citizen_second_name_arabic,
                citizen_third_name_arabic1 =a.HealthcareReservation.Citizen.citizen_third_name_arabic,
                citizen_fourth_name_arabic1 =a.HealthcareReservation.Citizen.citizen_fourth_name_arabic,
                hospital_name = a.HealthcareReservation.HealthCare.hospital_name,
                hospital_name_arabic = a.HealthcareReservation.HealthCare.hospital_name_arabic,
                district_name = a.HealthcareReservation.HealthCare.District.district_name,
                district_arabic_name = a.HealthcareReservation.HealthCare.District.district_arabic_name,
                checkup_end=a.ScheduleHealthCare.checkup_end.Value.Hours.ToString() +":"+ a.ScheduleHealthCare.checkup_end.Value.Minutes.ToString(),
                checkup_start= a.ScheduleHealthCare.checkup_start.Value.Hours.ToString() + ":" + a.ScheduleHealthCare.checkup_start.Value.Minutes.ToString(),

            }

            );

            return Json(healthcareReservations, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetIndexOfUser1(int Id , DateTime from , DateTime to)
        {
            
            int cid = Id;
            db.Configuration.ProxyCreationEnabled = false;
            var healthcareReservations = db.ReservationScheduleM_M.Join(db.ScheduleHealthCares, a => a.reservationSchedule_schedule, b => b.schedule_id, (a, b) => new { a, b }).Join(db.HealthcareReservations, a => a.a.reservationSchedule_reservation, b => b.healthcareReservation_id, (x, y) => new { x, y }).Where(a => a.y.healthcareReservation_citizen_id == cid && a.y.healthcareReservation_isDeleted != true && a.y.healthcareReservation_id == a.x.a.reservationSchedule_reservation && a.x.a.reservationSchedule_schedule == a.x.b.schedule_id && a.y.Reservation_date >= from && a.y.Reservation_date <= to).Select(a => a.x.a).Include(a => a.HealthcareReservation).Include(a => a.HealthcareReservation.Citizen).Include(a => a.ScheduleHealthCare).Select(a =>
            new reservation
            {
                healthcareReservation_id = a.HealthcareReservation.healthcareReservation_id,
                Reservation_date = a.HealthcareReservation.Reservation_date.ToString(),
                checkup_date = a.ScheduleHealthCare.checkup_date.ToString(),
                citizen_first_name = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_first_name,
                citizen_second_name = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_second_name,
                citizen_third_name = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_third_name,
                citizen_fourth_name = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_fourth_name,
                citizen_first_name_arabic = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_first_name_arabic,
                citizen_second_name_arabic = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_second_name_arabic,
                citizen_third_name_arabic = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_third_name_arabic,
                citizen_fourth_name_arabic = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_fourth_name_arabic,
                citizen_first_name1 = a.HealthcareReservation.Citizen.citizen_first_name,
                citizen_second_name1 = a.HealthcareReservation.Citizen.citizen_second_name,
                citizen_third_name1 = a.HealthcareReservation.Citizen.citizen_third_name,
                citizen_fourth_name1 = a.HealthcareReservation.Citizen.citizen_fourth_name,
                citizen_first_name_arabic1 = a.HealthcareReservation.Citizen.citizen_first_name_arabic,
                citizen_second_name_arabic1 = a.HealthcareReservation.Citizen.citizen_second_name_arabic,
                citizen_third_name_arabic1 = a.HealthcareReservation.Citizen.citizen_third_name_arabic,
                citizen_fourth_name_arabic1 = a.HealthcareReservation.Citizen.citizen_fourth_name_arabic,
                hospital_name = a.HealthcareReservation.HealthCare.hospital_name,
                hospital_name_arabic = a.HealthcareReservation.HealthCare.hospital_name_arabic,
                district_name = a.HealthcareReservation.HealthCare.District.district_name,
                district_arabic_name = a.HealthcareReservation.HealthCare.District.district_arabic_name,
                checkup_end = a.ScheduleHealthCare.checkup_end.Value.Hours.ToString() + ":" + a.ScheduleHealthCare.checkup_end.Value.Minutes.ToString(),
                checkup_start = a.ScheduleHealthCare.checkup_start.Value.Hours.ToString() + ":" + a.ScheduleHealthCare.checkup_start.Value.Minutes.ToString(),

            }

            );

            return Json(healthcareReservations, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetIndexOfUser2( DateTime from, DateTime to)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var healthcareReservations = db.ReservationScheduleM_M.Join(db.ScheduleHealthCares, a => a.reservationSchedule_schedule, b => b.schedule_id, (a, b) => new { a, b }).Join(db.HealthcareReservations, a => a.a.reservationSchedule_reservation, b => b.healthcareReservation_id, (x, y) => new { x, y }).Where(a => a.y.healthcareReservation_isDeleted != true && a.y.healthcareReservation_id == a.x.a.reservationSchedule_reservation && a.x.a.reservationSchedule_schedule == a.x.b.schedule_id && a.y.Reservation_date >= from && a.y.Reservation_date <= to).Select(a => a.x.a).Include(a => a.HealthcareReservation).Include(a => a.HealthcareReservation.Citizen).Include(a => a.ScheduleHealthCare).Select(a =>
            new reservation
            {
                healthcareReservation_id = a.HealthcareReservation.healthcareReservation_id,
                Reservation_date = a.HealthcareReservation.Reservation_date.ToString(),
                checkup_date = a.ScheduleHealthCare.checkup_date.ToString(),
                citizen_first_name = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_first_name,
                citizen_second_name = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_second_name,
                citizen_third_name = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_third_name,
                citizen_fourth_name = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_fourth_name,
                citizen_first_name_arabic = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_first_name_arabic,
                citizen_second_name_arabic = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_second_name_arabic,
                citizen_third_name_arabic = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_third_name_arabic,
                citizen_fourth_name_arabic = a.HealthcareReservation.Healthcare_Doctor.Citizen.citizen_fourth_name_arabic,
                citizen_first_name1 = a.HealthcareReservation.Citizen.citizen_first_name,
                citizen_second_name1 = a.HealthcareReservation.Citizen.citizen_second_name,
                citizen_third_name1 = a.HealthcareReservation.Citizen.citizen_third_name,
                citizen_fourth_name1 = a.HealthcareReservation.Citizen.citizen_fourth_name,
                citizen_first_name_arabic1 = a.HealthcareReservation.Citizen.citizen_first_name_arabic,
                citizen_second_name_arabic1 = a.HealthcareReservation.Citizen.citizen_second_name_arabic,
                citizen_third_name_arabic1 = a.HealthcareReservation.Citizen.citizen_third_name_arabic,
                citizen_fourth_name_arabic1 = a.HealthcareReservation.Citizen.citizen_fourth_name_arabic,
                hospital_name = a.HealthcareReservation.HealthCare.hospital_name,
                hospital_name_arabic = a.HealthcareReservation.HealthCare.hospital_name_arabic,
                district_name = a.HealthcareReservation.HealthCare.District.district_name,
                district_arabic_name = a.HealthcareReservation.HealthCare.District.district_arabic_name,
                checkup_end = a.ScheduleHealthCare.checkup_end.Value.Hours.ToString() + ":" + a.ScheduleHealthCare.checkup_end.Value.Minutes.ToString(),
                checkup_start = a.ScheduleHealthCare.checkup_start.Value.Hours.ToString() + ":" + a.ScheduleHealthCare.checkup_start.Value.Minutes.ToString(),

            }

            );

            return Json(healthcareReservations, JsonRequestBehavior.AllowGet);
        }
        public ActionResult IndexAdmin()
        {
            ViewBag.citizen = new SelectList(db.Citizens.Where(a => a.citizen_isDeleted != true),"citizen_id","citizen_national_id" );
            var healthcareReservations = db.ReservationScheduleM_M.Join(db.ScheduleHealthCares, a => a.reservationSchedule_schedule, b => b.schedule_id, (a, b) => new { a, b }).Join(db.HealthcareReservations, a => a.a.reservationSchedule_reservation, b => b.healthcareReservation_id, (x, y) => new { x, y }).Where(a =>  a.y.healthcareReservation_isDeleted != true && a.y.healthcareReservation_id == a.x.a.reservationSchedule_reservation && a.x.a.reservationSchedule_schedule == a.x.b.schedule_id).Select(a => a.x.a);
            return View(healthcareReservations.Include(a=> a.HealthcareReservation.Citizen.Healthcare_Doctor).ToList());
        }
       
        // GET: doctorReservation/Create
        public ActionResult Create()
        {
            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
            ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name");
            ViewBag.Spec = new SelectList(db.HealthCareSpecializations.Where(a => a.specialization_isDeleted != true), "specialization_id", "specialization_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");
                    ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name_arabic");
                    ViewBag.Spec = new SelectList(db.HealthCareSpecializations.Where(a => a.specialization_isDeleted != true), "specialization_id", "specialization_name_arabic");


                }
            }


            return View();
        }

        public JsonResult scheduleDoctor (int docId ,int healthcarId)
        {


            var data = db.HealthCareSheduleM_M.Where(a => a.DoctorId == docId && a.hospitalSchedule_hospital == healthcarId).Join(db.ScheduleHealthCares, b => b.hospitalSchedule_shedule , a => a.schedule_id, (b, a) => new Date {checkup_date = a.checkup_date.ToString(), checkup_end =a.checkup_end.ToString(),checkup_start = a.checkup_start.ToString(), schedule_id =a.schedule_id });
            List<Date> d = new List<Date>();
            string[] aa;
            string[] bb;
            foreach (var item in data)
            {
                aa = item.checkup_start.Split('.');
                bb = item.checkup_end.Split('.');
                item.checkup_start = aa[0];
                item.checkup_end = bb[0];

                aa = item.checkup_start.Split(':');
                bb = item.checkup_end.Split(':');
                item.checkup_start = aa[0] + ":" + aa[1];
                item.checkup_end = bb[0] + ":" + bb[1];
                d.Add(item);
            }
            db.Configuration.ProxyCreationEnabled = false;



            return Json (d,JsonRequestBehavior.AllowGet);
        }

        
        [HttpPost]
     
        public ActionResult Create( HealthcareReservation healthcareReservation,int schedule)
        {
            int cid = (int) Session["citizenID"] ;

            ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
            ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name");
            ViewBag.Spec = new SelectList(db.HealthCareSpecializations.Where(a => a.specialization_isDeleted != true), "specialization_id", "specialization_name");

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");
                    ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name_arabic");
                    ViewBag.Spec = new SelectList(db.HealthCareSpecializations.Where(a => a.specialization_isDeleted != true), "specialization_id", "specialization_name_arabic");


                }
            }

            var data = db.HealthcareReservations.Where(a => a.healthcareReservation_citizen_id == cid && a.healthcareReservation_doctor_id == healthcareReservation.healthcareReservation_doctor_id && a.healthcareReservation_hospital_id == healthcareReservation.healthcareReservation_hospital_id && a.Reservation_date < DateTime.Now).FirstOrDefault();
            if (data != null)
            {
                ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
                ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name");
                ViewBag.Spec = new SelectList(db.HealthCareSpecializations.Where(a => a.specialization_isDeleted != true), "specialization_id", "specialization_name");

                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");
                        ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name_arabic");
                        ViewBag.Spec = new SelectList(db.HealthCareSpecializations.Where(a => a.specialization_isDeleted != true), "specialization_id", "specialization_name_arabic");


                    }
                }

                ViewBag.drmsg = Servicely.Languages.Language.you_are_already_reserved_on_this_day;
                return View(healthcareReservation); 

            }



            int? reservationmax = db.ScheduleHealthCares.Find(schedule).checkup_number;

            int maxreserv = db.ReservationScheduleM_M.Where(a => a.reservationSchedule_schedule == schedule).Join(db.HealthcareReservations, a => a.reservationSchedule_reservation, b => b.healthcareReservation_id, (a, b) => new { b }).Where(a => a.b.healthcareReservation_hospital_id == healthcareReservation.healthcareReservation_hospital_id  && a.b.Reservation_date == healthcareReservation.Reservation_date && a.b.healthcareReservation_doctor_id == healthcareReservation.healthcareReservation_doctor_id && a.b.healthcareReservation_cancel!= true).Count();

            if (maxreserv == reservationmax)
            {
                ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_name");
                ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name");
                ViewBag.Spec = new SelectList(db.HealthCareSpecializations.Where(a => a.specialization_isDeleted != true), "specialization_id", "specialization_name");

                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString().Equals("ar-EG"))
                    {
                        ViewBag.State = new SelectList(db.States.Where(a => a.state_isDeleted != true), "state_id", "state_arabic_name");
                        ViewBag.HCtype = new SelectList(db.HealthCare_Type.Where(a => a.healthcare_isDeleted != true), "healthcare_type_id", "healthcare_type_name_arabic");
                        ViewBag.Spec = new SelectList(db.HealthCareSpecializations.Where(a => a.specialization_isDeleted != true), "specialization_id", "specialization_name_arabic");


                    }
                }

                ViewBag.drmsg = Servicely.Languages.Language.day_is_full_choose_another_date;
                return View(healthcareReservation);

            }




                healthcareReservation.healthcareReservation_citizen_id = cid;

            string cod = Guid.NewGuid().ToString().Substring(0,5)+"-"+ Guid.NewGuid().ToString().Substring(0, 5);

            healthcareReservation.healthcareReservation_Code = cod;
            healthcareReservation.Reservation_date = db.ScheduleHealthCares.Find(schedule).checkup_date;
            healthcareReservation.TransactionDate = DateTime.Now;    
            db.HealthcareReservations.Add(healthcareReservation);
                db.SaveChanges();
                ReservationScheduleM_M r = new ReservationScheduleM_M();
                r.reservationSchedule_reservation = healthcareReservation.healthcareReservation_id;
                r.reservationSchedule_schedule = schedule;
                db.ReservationScheduleM_M.Add(r);
                db.SaveChanges();

            var father = db.Citizens.Find(cid);
            var vaccSchedule = db.ScheduleHealthCares.Where(a => a.schedule_id == schedule ).FirstOrDefault();
            var email = db.Contacts.Where(a => a.contact_citizen_id == cid).Join(db.Contact_Type, a => a.contact_contactType_id, b => b.contact_type_id, (a, b) => new { a, b }).Where(b => b.b.contact_type_name == "Email").Select(a => a.a).FirstOrDefault();
            int code = maxreserv;
            var HealthCare = db.HealthCares.Find(healthcareReservation.healthcareReservation_hospital_id);
            if (email != null)
            {
                string Title = "Dear " + father.citizen_first_name + " " + father.citizen_second_name + " " + father.citizen_third_name + " " + father.citizen_fourth_name + " \n  " +
                  "You have reserved an appointment on " + vaccSchedule.checkup_date.Value.ToShortDateString() + " from : " + vaccSchedule.checkup_start + " to : " + vaccSchedule.checkup_end + " \n\n" +
                  " Health Care Name: " + HealthCare.hospital_name + " \n Health Care Address: " + HealthCare.District.Region.City.State.state_name + " " + HealthCare.District.Region.City.city_name + " " + HealthCare.District.Region.region_name + " " + HealthCare.District.district_name + " \n" + "Reservation Code: "
                   + (code += 1) + " \n yours sincerely , ";
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("CSCE4502@gmail.com", "csce4502f16");
                smtp.Send("CSCE4502@gmail.com", email.contact_data, "Vaccination", Title);
                ViewBag.vcc = Servicely.Languages.Language.successfully_reserved_checkemail;
                return View();
            }



            ViewBag.drmsg = Servicely.Languages.Language.successfully_reserved_checkemail;


            return View(healthcareReservation);


           // ViewBag.healthcareReservation_hospital_id = new SelectList(db.HealthCares, "hospital_id", "hospital_name", healthcareReservation.healthcareReservation_hospital_id);
           
        }
        private int GenerateReservationCode()
        {
            // generate 4 random numbers
            int min = 1000;
            int max = 9999;
            Random rdm = new Random();
            int random = rdm.Next(min, max);
            

            // string[] a = date.Split('-');
            //int NId =  random;
            return random;

        }
        public JsonResult GetAllHEaltcaresByDistrictAndSpecAndType(int dis , int htype , int spec)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.HealthCareSpecializationM_M.Where(a => a.HealthCare.healthcare_type_id == htype && a.HealthCare.hospital_district_id == dis&& a.hospitalSpecialization_specialization_id == spec && a.HealthCare.hospital_id == a.hospitalSpecialization_hospital_id).Select(a => new { a.HealthCare.hospital_id, a.HealthCare.hospital_name, a.HealthCare.hospital_name_arabic }).ToList(),JsonRequestBehavior.AllowGet);
        }
        // GET: doctorReservation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HealthcareReservation healthcareReservation = db.HealthcareReservations.Find(id);
            if (healthcareReservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.healthcareReservation_hospital_id = new SelectList(db.HealthCares, "hospital_id", "hospital_name", healthcareReservation.healthcareReservation_hospital_id);

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {
                    ViewBag.healthcareReservation_hospital_id = new SelectList(db.HealthCares, "hospital_id", "hospital_name_arabic", healthcareReservation.healthcareReservation_hospital_id);

                }
            }


            return View(healthcareReservation);
        }

        // POST: doctorReservation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "healthcareReservation_id,healthcareReservation_service_type_id,healthcareReservation_hospital_id,Reservation_date,healthcareReservation_isDeleted")] HealthcareReservation healthcareReservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(healthcareReservation).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.healthcareReservation_hospital_id = new SelectList(db.HealthCares, "hospital_id", "hospital_name", healthcareReservation.healthcareReservation_hospital_id);

            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString().Equals("ar-EG"))
                {

                    ViewBag.healthcareReservation_hospital_id = new SelectList(db.HealthCares, "hospital_id", "hospital_name_arabic", healthcareReservation.healthcareReservation_hospital_id);
                }
            }

            return View(healthcareReservation);
        }

        // GET: doctorReservation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HealthcareReservation healthcareReservation = db.HealthcareReservations.Where(a=> a.healthcareReservation_id == id).Include(a=> a.HealthCare).Include(a=> a.Citizen).Include(a=> a.Healthcare_Doctor.Citizen).SingleOrDefault();
            if (healthcareReservation == null)
            {
                return HttpNotFound();
            }
            return View(healthcareReservation);
        }

        [HttpPost, ActionName("Delete")]
        // POST: doctorReservation/Delete/5
       //delete of admin page
        public ActionResult DeleteConfirmed(int id)
        {
            HealthcareReservation healthcareReservation = db.HealthcareReservations.Find(id);
            healthcareReservation.healthcareReservation_isDeleted = true;
            db.SaveChanges();
            return RedirectToAction("IndexAdmin");
        }

        public JsonResult DeleteJson(int id)
        {
            HealthcareReservation healthcareReservation = db.HealthcareReservations.Find(id);
            healthcareReservation.healthcareReservation_isDeleted = true;
            db.SaveChanges();
            return Json(Servicely.Languages.Language.DeletedSuccessfully);
        }



        // GET: doctorReservation/Delete/5
        public ActionResult DeleteUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HealthcareReservation healthcareReservation = db.HealthcareReservations.Where(a => a.healthcareReservation_id == id).Include(a => a.HealthCare).Include(a => a.Citizen).Include(a => a.Healthcare_Doctor.Citizen).SingleOrDefault();

            if (healthcareReservation == null)
            {
                return HttpNotFound();
            }
            return View(healthcareReservation);
        }

        [HttpPost, ActionName("DeleteUser")]
        // POST: doctorReservation/Delete/5
        //delete of admin page
        public ActionResult DeleteConfirmedUser(int id)
        {
            HealthcareReservation healthcareReservation = db.HealthcareReservations.Find(id);
            healthcareReservation.healthcareReservation_isDeleted = true;
            db.SaveChanges();
            return RedirectToAction("IndexAdmin");
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
