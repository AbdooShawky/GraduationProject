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
    
    public partial class ReservationScheduleM_M
    {
        public int reservationSchedule_id { get; set; }
        public Nullable<int> reservationSchedule_reservation { get; set; }
        public Nullable<int> reservationSchedule_schedule { get; set; }
    
        public virtual HealthcareReservation HealthcareReservation { get; set; }
        public virtual ScheduleHealthCare ScheduleHealthCare { get; set; }
    }
}
