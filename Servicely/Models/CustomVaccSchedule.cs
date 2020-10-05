using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicely.Models
{
    public class CustomVaccSchedule
    {
        public string checkup_date { get; set; }
        public Nullable<System.TimeSpan> checkup_end { get; set; }
        public Nullable<System.TimeSpan> checkup_start { get; set; }
        public int schedule_id { get; set; }
    }
}