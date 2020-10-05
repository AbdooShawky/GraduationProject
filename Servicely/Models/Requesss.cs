using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicely.Models
{
    public class Requesss
    {

        public int request_id { get; set; }
        public Nullable<int> request_citizenId { get; set; }
        public string address { get; set; }
        public string service { get; set; }
        public string language { get; set; }
        public Nullable<int> quantity { get; set; }
        public string typeRequest { get; set; }
        public string date { get; set; }
        public string governmentAgency { get; set; }

        public virtual Citizen Citizen { get; set; }
    }
}