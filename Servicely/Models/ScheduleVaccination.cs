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
    
    public partial class ScheduleVaccination
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ScheduleVaccination()
        {
            this.ScheduleVaccM_M = new HashSet<ScheduleVaccM_M>();
            this.ScheduleVaccReservationM_M = new HashSet<ScheduleVaccReservationM_M>();
        }
    
        public int schedule_id { get; set; }
        public Nullable<System.DateTime> checkup_date { get; set; }
        public Nullable<System.TimeSpan> checkup_start { get; set; }
        public Nullable<System.TimeSpan> checkup_end { get; set; }
        public Nullable<int> checkup_number { get; set; }
        public Nullable<bool> schedule_isDeleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScheduleVaccM_M> ScheduleVaccM_M { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScheduleVaccReservationM_M> ScheduleVaccReservationM_M { get; set; }
    }
}
