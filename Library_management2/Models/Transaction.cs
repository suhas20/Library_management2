//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Library_management2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transaction
    {
        public int TransID { get; set; }
        public int BookID { get; set; }
        public string BookTitle { get; set; }
        public string TransStatus { get; set; }
        public System.DateTime TansDate { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
    
        public virtual Book Book { get; set; }
        public virtual Transaction Transactions1 { get; set; }
        public virtual Transaction Transaction1 { get; set; }
        public virtual User User { get; set; }
    }
}
