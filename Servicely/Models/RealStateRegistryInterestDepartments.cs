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
    
    public partial class RealStateRegistryInterestDepartments
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RealStateRegistryInterestDepartments()
        {
            this.RealStateRegistryInterest = new HashSet<RealStateRegistryInterest>();
        }
    
        public int realStateRegistryInterestDepartments_id { get; set; }
        public string realStateRegistryInterestDepartments_name { get; set; }
        public Nullable<bool> realStateRegistryInterestDepartments_isDeleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RealStateRegistryInterest> RealStateRegistryInterest { get; set; }
    }
}
