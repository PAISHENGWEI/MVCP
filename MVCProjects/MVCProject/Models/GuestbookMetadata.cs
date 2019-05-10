using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    [MetadataType(typeof(GuestbookMetadata))]
    public partial class Guestbooks
    {
        public class GuestbookMetadata
        {
            [DisplayName("編號:")]
            public System.Guid Id { get; set; }

            [DisplayName("名字:")]
            [Required(ErrorMessage ="請輸入名字")]
            [StringLength(20,ErrorMessage ="名字不可以超過20字元")]
            public string Name { get; set; }

            [DisplayName("留言內容:")]
            [Required(ErrorMessage = "請輸入留言內容")]
            [StringLength(20, ErrorMessage = "留言內容不可以超過100字元")]
            public string Contents { get; set; }

            [DisplayName("新增時間:")]
            public System.DateTime CreateTime { get; set; }
            [DisplayName("回覆內容:")]
            public string Reply { get; set; }
            [DisplayName("回復時間:")]
            public Nullable<System.DateTime> ReplyTime { get; set; }

        }
    }


}