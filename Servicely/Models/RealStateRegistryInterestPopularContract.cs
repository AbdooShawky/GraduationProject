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
    
    public partial class RealStateRegistryInterestPopularContract
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RealStateRegistryInterestPopularContract()
        {
            this.RealStateRegistryInterestReports = new HashSet<RealStateRegistryInterestReport>();
        }
    
        public int realStateRegistryInterest_popularContract_id { get; set; }
        public string realStateRegistryInterest_popularContract_name { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RealStateRegistryInterestReport> RealStateRegistryInterestReports { get; set; }
    }
}
