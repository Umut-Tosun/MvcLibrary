//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcLibrary.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tbl_Punishment
    {
        public int ID { get; set; }
        public Nullable<int> Member_Id { get; set; }
        public Nullable<int> Transaction_Id { get; set; }
        public Nullable<System.DateTime> Beginning { get; set; }
        public Nullable<System.DateTime> Finish { get; set; }
        public Nullable<decimal> PunishmentMoney { get; set; }
    }
}