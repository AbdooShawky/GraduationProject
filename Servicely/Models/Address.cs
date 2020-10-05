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
    
    public partial class Address
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Address()
        {
            this.AddressCitizens = new HashSet<AddressCitizen>();
        }
    
        public int address_id { get; set; }
        public int address_district_id { get; set; }
        public bool address_isCurrent { get; set; }
        public bool address_isBirthPlace { get; set; }
        public string address_street { get; set; }
        public Nullable<bool> address_isDelete { get; set; }
        public string address_street_arabic { get; set; }
        public Nullable<System.DateTime> transactionDate { get; set; }
    
        public virtual District District { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AddressCitizen> AddressCitizens { get; set; }
    }
}