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
    
    public partial class Respond
    {
        public int Id { get; set; }
        public Nullable<int> ComplainsId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string RespondText { get; set; }
        public Nullable<bool> Is_Deleted { get; set; }
    
        public virtual Complain Complain { get; set; }
    }
}
