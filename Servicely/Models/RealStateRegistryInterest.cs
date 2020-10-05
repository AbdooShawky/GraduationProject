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
    
    public partial class RealStateRegistryInterest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RealStateRegistryInterest()
        {
            this.RealStateRegistryInterestBranches = new HashSet<RealStateRegistryInterestBranch>();
        }
    
        public int realStateRegistryInterest_id { get; set; }
        public string realStateRegistryInterest_name { get; set; }
        public Nullable<int> realStateRegistryInterest_address { get; set; }
        public Nullable<int> realStateRegistryInterest_Departments_id { get; set; }
        public Nullable<bool> realStateRegistryInterest_isDeleted { get; set; }
    
        public virtual District District { get; set; }
        public virtual RealStateRegistryInterestDepartment RealStateRegistryInterestDepartment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RealStateRegistryInterestBranch> RealStateRegistryInterestBranches { get; set; }
    }
}