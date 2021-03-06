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
    
    public partial class Healthcare_Doctor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Healthcare_Doctor()
        {
            this.DoctorScheduleM_M = new HashSet<DoctorScheduleM_M>();
            this.HealthCareDoctorM_M = new HashSet<HealthCareDoctorM_M>();
            this.HealthcareReservations = new HashSet<HealthcareReservation>();
            this.HealthCareSheduleM_M = new HashSet<HealthCareSheduleM_M>();
        }
    
        public int doctor_id { get; set; }
        public Nullable<int> Doctor_type_id { get; set; }
        public Nullable<int> doctor_citizen_id { get; set; }
        public Nullable<bool> doctor_isDeleted { get; set; }
        public Nullable<int> doctor_specialization_id { get; set; }
    
        public virtual Citizen Citizen { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DoctorScheduleM_M> DoctorScheduleM_M { get; set; }
        public virtual DoctorType DoctorType { get; set; }
        public virtual HealthCareSpecialization HealthCareSpecialization { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HealthCareDoctorM_M> HealthCareDoctorM_M { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HealthcareReservation> HealthcareReservations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HealthCareSheduleM_M> HealthCareSheduleM_M { get; set; }
    }
}
