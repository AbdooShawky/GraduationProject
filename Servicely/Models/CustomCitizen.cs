using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicely.Models
{
    public class CustomCitizen
    {
        public int citizen_id { get; set; }
        public string citizen_national_id { get; set; }
        public string citizen_first_name { get; set; }
        public string citizen_second_name { get; set; }
        public string citizen_third_name { get; set; }
        public string citizen_fourth_name { get; set; }
        public string citizen_job_title { get; set; }
        public string citizen_gender { get; set; }
        public Nullable<int> citizen_father_id { get; set; }
        public Nullable<int> citizen_mother_id { get; set; }
        public Nullable<bool> citizen_isDeleted { get; set; }
        public string citizen_relegion { get; set; }
        public Nullable<System.DateTime> citizen_birthDate { get; set; }
        public string citizen_birthPlace { get; set; }
        public string citizen_first_name_arabic { get; set; }
        public string citizen_second_name_arabic { get; set; }
        public string citizen_third_name_arabic { get; set; }
        public string citizen_fourth_name_arabic { get; set; }
        public string citizen_birthPlace_arabic { get; set; }

    }
}