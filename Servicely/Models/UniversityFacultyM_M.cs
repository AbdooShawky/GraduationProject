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
    
    public partial class UniversityFacultyM_M
    {
        public int Id { get; set; }
        public Nullable<int> UniversityId { get; set; }
        public Nullable<int> FacultyId { get; set; }
        public Nullable<bool> Is_Deleted { get; set; }
    
        public virtual Faculty Faculty { get; set; }
        public virtual University University { get; set; }
    }
}
