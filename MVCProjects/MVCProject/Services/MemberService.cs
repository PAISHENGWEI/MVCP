using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MVCProject.Services
{
    public class MemberService
    {
        private MVCProjectEntities db = new MVCProjectEntities();
        //註冊新會員方法
        public void Register(Members newMember)
        {
            newMember.Password = HashPassword(newMember.Password);
            db.Members.Add(newMember);
            db.SaveChanges();
        }
        //Hash 密碼用的方法
        public string HashPassword(string Password)
        {
            string saltkey = "sE7T8E4Wr81sd9qwQG3Jjgio8tj3kg";
            string saltAndPassword = String.Concat(Password, saltkey);
            //定義SHA1的Hash物件
            SHA1CryptoServiceProvider sha1Hasher = new SHA1CryptoServiceProvider();
            byte[] PasswordData = Encoding.Default.GetBytes(saltAndPassword);
            byte[] HashData = sha1Hasher.ComputeHash(PasswordData);
            //string Hashresult1= Convert.ToBase64String(HashData);
            string Hashresult = "";
            for(int i=0;i< HashData.Length;i++)
            {
                Hashresult += HashData[i].ToString("x2");
            }
            return Hashresult;
        }

        //確認要註冊的帳號是否被註冊過
        public bool AccountChecck(string Account)
        {
            //藉由傳入帳號取得資料
            Members search = db.Members.Find(Account);
            //判斷是否有查詢到會員
            bool result = (search == null);
            return result;
        }
        //信箱驗證碼驗證方法
        public string EmailValidate(string UserName,string AuthCode)
        {
            //取的傳入帳號的會員資料
            Members ValidateMember = db.Members.Find(UserName);
            //宣告驗證後訊息字串
            string Validatestr = string.Empty;
            if(ValidateMember!=null)
            {
                //判斷傳入驗證碼與資料庫中是否相同
                if (ValidateMember.AuthCode == AuthCode)
                {
                    //將資料庫中的驗證碼設為空
                    ValidateMember.AuthCode = string.Empty;
                    db.SaveChanges();
                    Validatestr = "帳號信箱驗證成功，現在可以登入";
                }
                else
                {
                    Validatestr = "驗證錯誤，請重新確認或再註冊";
                }
                
            }
            else
            {
                Validatestr = "傳送資料錯誤，請重新確認再註冊";

            }
            return Validatestr;
        }
    }
}