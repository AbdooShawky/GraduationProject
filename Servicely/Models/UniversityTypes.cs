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
    
    public partial class UniversityTypes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UniversityTypes()
        {
            this.Universities = new HashSet<Universities>();
        }
    
        public int Id { get; set; }
        public string UniversityTypeName { get; set; }
        public Nullable<bool> Is_Deleted { get; set; }
        public string UniversityTypeNameArabic { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Universities> Universities { get; set; }
    }
}
