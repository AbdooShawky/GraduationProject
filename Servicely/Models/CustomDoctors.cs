using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicely.Models
{
    public class CustomDoctors
    {
        public int doctor_id { get; set; }
        public string citizen_first_name { get; set; }
        public string citizen_fourth_name { get; set; }

        public string citizen_first_name_arabic { get; set; }
        public string citizen_fourth_name_arabic { get; set; }
    }
}