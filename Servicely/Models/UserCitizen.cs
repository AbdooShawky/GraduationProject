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
    
    public partial class UserCitizen
    {
        public int Id { get; set; }
        public int userCitizen_userId { get; set; }
        public int userCitezen_citizenId { get; set; }
    
        public virtual Citizen Citizen { get; set; }
        public virtual User User { get; set; }
    }
}
