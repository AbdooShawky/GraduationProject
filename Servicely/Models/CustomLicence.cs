using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicely.Models
{
    public class CustomLicence
    {
        public int Id { get; set; }
        public Nullable<int> CitizenId { get; set; }
        public string CarName { get; set; }
        public string CarCode { get; set; }
        public string CarNameArabic { get; set; }
        public Nullable<int> CarModel { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public Nullable<bool> Is_Deleted { get; set; }
    }
}