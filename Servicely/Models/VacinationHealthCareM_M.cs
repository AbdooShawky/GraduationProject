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
    
    public partial class VacinationHealthCareM_M
    {
        public int vaccinationhealthcare_id { get; set; }
        public Nullable<int> vaccinationhealthcare_hospital { get; set; }
        public Nullable<int> vacinationhealthcare_vaccination { get; set; }
    
        public virtual HealthCare HealthCare { get; set; }
        public virtual VaccinationType VaccinationType { get; set; }
    }
}
