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
    
    public partial class services
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public services()
        {
            this.Request = new HashSet<Request>();
            this.Reservation = new HashSet<Reservation>();
        }
    
        public int service_id { get; set; }
        public string service_name { get; set; }
        public Nullable<int> service_time { get; set; }
        public Nullable<int> goverenemnt_id { get; set; }
        public string service_name_arabic { get; set; }
        public Nullable<decimal> service_price { get; set; }
        public Nullable<bool> service_isDeleted { get; set; }
    
        public virtual governement_body governement_body { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Request> Request { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
