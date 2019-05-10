using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Models
{
    [MetadataType(typeof(MembersMetadata))]
    public partial class Members
    {
        private class MembersMetadata
        {
            [DisplayName("帳號")]
            [Required(ErrorMessage ="請輸入帳號")]
            [StringLength(30,MinimumLength =6,ErrorMessage ="帳號長度須介於6~30字元")]
            [Remote("AccountCheck","Member",ErrorMessage ="此帳號已被註冊過")]
            public string Acount { get; set; }

            public string Password { get; set; }

            [DisplayName("姓名")]
            [StringLength(20,ErrorMessage ="姓名長度最多20字元")]
            [Required(ErrorMessage = "請輸入姓名")]
            public string Name { get; set; }

            [DisplayName("Email")]
            [Required(ErrorMessage ="請輸入Email")]
            [StringLength(200,ErrorMessage ="Email長度最多200字元")]
            public string Email { get; set; }

            public string AuthCode { get; set; }

            public bool IsAdmin { get; set; }

        }
    }
   
}