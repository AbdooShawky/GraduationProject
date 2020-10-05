using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicely.Models
{
    public class Date
    {
        public string checkup_date { get; set; }
        public string checkup_end { get; set; }
        public string checkup_start { get; set; }
        public int schedule_id { get; set; }
    }
}