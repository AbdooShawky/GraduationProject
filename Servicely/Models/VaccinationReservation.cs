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
    
    public partial class VaccinationReservation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VaccinationReservation()
        {
            this.ScheduleVaccReservationM_M = new HashSet<ScheduleVaccReservationM_M>();
        }
    
        public int VaccReservation_id { get; set; }
        public Nullable<int> VaccReservation_HealthCare_id { get; set; }
        public Nullable<int> VaccReservation_Citizen_id { get; set; }
        public Nullable<int> VaccReservation_child_id { get; set; }
        public Nullable<System.DateTime> VaccReservation_date { get; set; }
        public Nullable<bool> VaccReservation_checked { get; set; }
        public string VaccReservation_Code { get; set; }
        public Nullable<int> VaccReservation_VaccinationType_id { get; set; }
        public Nullable<bool> VaccReservation_isDeleted { get; set; }
        public Nullable<bool> VaccReservation_cancel { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
    
        public virtual Citizen Citizen { get; set; }
        public virtual HealthCare HealthCare { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScheduleVaccReservationM_M> ScheduleVaccReservationM_M { get; set; }
        public virtual VaccinationType VaccinationType { get; set; }
        public virtual Citizen Citizen1 { get; set; }
    }
}
