//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Role_based.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Trainee
    {
        public int TraineeID { get; set; }
        public string Tname { get; set; }
        public Nullable<int> Tage { get; set; }
        public string Tcity { get; set; }
        public byte[] TImage { get; set; }
        public Nullable<int> DomainID { get; set; }
    
        public virtual Training Training { get; set; }
    }
}