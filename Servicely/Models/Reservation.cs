//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Servicely.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reservation
    {
        public int reservation_id { get; set; }
        public Nullable<System.DateTime> reservation_date { get; set; }
        public int reservation_office_id { get; set; }
        public Nullable<int> reservation_citizen_id { get; set; }
        public Nullable<bool> reservation_isDeleted { get; set; }
        public Nullable<int> service_id { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
    
        public virtual Citizen Citizen { get; set; }
        public virtual Office Office { get; set; }
        public virtual service service { get; set; }
    }
}
