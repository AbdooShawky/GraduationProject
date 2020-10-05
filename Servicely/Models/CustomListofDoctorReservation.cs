using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicely.Models
{
    public class CustomListofDoctorReservation
    {
        public int healthcareReservation_id { get; set; }
        public int? healthcareReservation_doctor_id { get; set; }
        public int? healthcareReservation_hospital_id { get; set; }
        public string healthcareReservation_Code { get; set; }
        public DateTime? Reservation_date { get; set; }
        public string specialization_name { get; set; }
        public string specialization_name_arabic { get;  set; }
    }
}