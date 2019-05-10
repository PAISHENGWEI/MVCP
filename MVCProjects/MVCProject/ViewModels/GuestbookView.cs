using MVCProject.Models;
using MVCProject.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MVCProject.ViewModels
{
    public class GuestbookView
    {
        
        [DisplayName("搜尋：")]
        public string Search { get; set; }
       
        public List<Guestbooks> DataList { get; set; }

        public ForPaging Paging { get; set; }
    }
}