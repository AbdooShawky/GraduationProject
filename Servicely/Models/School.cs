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
    
    public partial class School
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public School()
        {
            this.SchoolCertificateM_M = new HashSet<SchoolCertificateM_M>();
            this.SchoolPhasesM_M = new HashSet<SchoolPhasesM_M>();
            this.SchoolStudentM_M = new HashSet<SchoolStudentM_M>();
            this.Students = new HashSet<Student>();
        }
    
        public int Id { get; set; }
        public string SchoolName { get; set; }
        public int DistrictId { get; set; }
        public Nullable<int> KindOfSchoolId { get; set; }
        public Nullable<bool> Is_Deleted { get; set; }
        public string SchoolNameArabic { get; set; }
        public Nullable<int> DirectorateId { get; set; }
    
        public virtual District District { get; set; }
        public virtual EducationDirectorate EducationDirectorate { get; set; }
        public virtual KindOfSchool KindOfSchool { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchoolCertificateM_M> SchoolCertificateM_M { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchoolPhasesM_M> SchoolPhasesM_M { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchoolStudentM_M> SchoolStudentM_M { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Students { get; set; }
    }
}
