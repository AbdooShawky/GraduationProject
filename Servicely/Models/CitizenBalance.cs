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
    
    public partial class CitizenBalance
    {
        public int CitizenBalance_id { get; set; }
        public Nullable<int> CitizenBalance_citizen_id { get; set; }
        public Nullable<decimal> CitizenBalance_balance { get; set; }
        public Nullable<bool> CitizenBalance_isDeleted { get; set; }
    
        public virtual Citizen Citizen { get; set; }
    }
}
