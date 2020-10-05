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
    
    public partial class University
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public University()
        {
            this.Students = new HashSet<Student>();
            this.UniversityFacultyM_M = new HashSet<UniversityFacultyM_M>();
            this.UniversityStudentM_M = new HashSet<UniversityStudentM_M>();
        }
    
        public int Id { get; set; }
        public string UniversityName { get; set; }
        public Nullable<int> UniversitryTypeId { get; set; }
        public Nullable<bool> Is_Deleted { get; set; }
        public string UniversityNameArabic { get; set; }
        public Nullable<int> DistrictId { get; set; }
        public string UniversityLogo { get; set; }
    
        public virtual District District { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Students { get; set; }
        public virtual UniversityType UniversityType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UniversityFacultyM_M> UniversityFacultyM_M { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UniversityStudentM_M> UniversityStudentM_M { get; set; }
    }
}
