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
    
    public partial class police_officer
    {
        public int police_officer_id { get; set; }
        public Nullable<int> police_officer_citizen_id { get; set; }
        public Nullable<int> police_officer_policeDepartmentId { get; set; }
    
        public virtual Citizen Citizen { get; set; }
        public virtual PoliceDepartment PoliceDepartment { get; set; }
    }
}
