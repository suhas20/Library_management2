using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_management2.Models
{
    public class TransactionViewModel
    {
        public string BookTitle { get; set; }
        public DateTime TansDate { get; set; }
        public string UserName { get; set; }
        public int TransID { get; set; }
    }
}