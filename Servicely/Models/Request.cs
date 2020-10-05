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
    
    public partial class Request
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Request()
        {
            this.LogRequests = new HashSet<LogRequest>();
        }
    
        public int request_id { get; set; }
        public Nullable<int> request_citizenId { get; set; }
        public string address { get; set; }
        public Nullable<int> service { get; set; }
        public string language { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<int> typeRequest { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public Nullable<int> governmentAgency { get; set; }
        public string address_arabic { get; set; }
        public string language_arabic { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public Nullable<bool> Is_Deleted { get; set; }
    
        public virtual Citizen Citizen { get; set; }
        public virtual governement_body governement_body { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LogRequest> LogRequests { get; set; }
        public virtual TypeRequest TypeRequest1 { get; set; }
        public virtual service service1 { get; set; }
    }
}