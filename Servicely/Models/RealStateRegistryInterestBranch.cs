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
    
    public partial class RealStateRegistryInterestBranch
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RealStateRegistryInterestBranch()
        {
            this.RealStateRegistryInterestReports = new HashSet<RealStateRegistryInterestReport>();
        }
    
        public int realStateRegistryInterest_branch_id { get; set; }
        public string realStateRegistryInterest_branch_name { get; set; }
        public Nullable<int> realStateRegistryInterest_branch_realstate_id { get; set; }
        public Nullable<int> realStateRegistryInterest_branch_technical_member_id { get; set; }
        public Nullable<int> realStateRegistryInterest_branch_district_id { get; set; }
        public Nullable<bool> realStateRegistryInterest_branch_isDeleted { get; set; }
    
        public virtual Citizen Citizen { get; set; }
        public virtual District District { get; set; }
        public virtual RealStateRegistryInterest RealStateRegistryInterest { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RealStateRegistryInterestReport> RealStateRegistryInterestReports { get; set; }
    }
}
