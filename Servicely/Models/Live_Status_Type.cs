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
    
    public partial class Live_Status_Type
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Live_Status_Type()
        {
            this.Live_Status = new HashSet<Live_Status>();
        }
    
        public int live_status_type_id { get; set; }
        public string live_status_type_name { get; set; }
        public Nullable<bool> live_status_type_isDeleted { get; set; }
        public string live_status_type_name_arabic { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Live_Status> Live_Status { get; set; }
    }
}
