using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace MVCProject.Services
{
    public class MailService
    {
        private string gmail_account = "";
        private string gmail_password = "";
        private string gmail_mail = "";

        //#寄會員驗證信
        //產生驗證碼方法
        public string GetValidatoCode()
        {
            string[] Code ={ "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K"
        , "L", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y"
            , "Z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b"
                , "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n"
                    , "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

            string ValidateCode = string.Empty;
            //宣告產生變數的物件
            Random rd = new Random();
            //使用迴圈產生驗證碼
            for(int i=0; i<10;i++)
            {
                ValidateCode += Code[rd.Next(Code.Count())];
            }
            return ValidateCode;

        }

        //將使用者資料填入驗證信範本中
        public string GetRegisterMailBody(string TempString,string UserName,string ValidateUrl)
        {
            //將使用者資料填入
            TempString = TempString.Replace("{ { UserName } }", UserName);
            TempString = TempString.Replace("{ { ValidateUrl } }", ValidateUrl);
            return TempString;
        }
        //寄驗證信的方法
        public void SendRegisterMail(string MailBody,string ToEmail)
        {
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(gmail_account, gmail_password);
            SmtpServer.EnableSsl = true;
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(gmail_mail);
            mail.To.Add(ToEmail);
            mail.Subject = "會員註冊確認信";
            mail.Body = MailBody;
            mail.IsBodyHtml = true;
            SmtpServer.Send(mail);
        }
    }
}