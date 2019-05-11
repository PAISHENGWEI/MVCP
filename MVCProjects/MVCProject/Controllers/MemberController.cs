using MVCProject.Services;
using MVCProject.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Controllers
{
    public class MemberController : Controller
    {
        private MemberService memberService = new MemberService();
        private MailService mailService = new MailService();



        // GET: Member
        public ActionResult Register()
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Guestbook");
            }
            else
            {
                return View();
            }
            
        }
        [HttpPost]
        public ActionResult Register(MemberRegisterView RegisterMember)
        {
            //判斷頁面資料是否都經過驗證
            if (ModelState.IsValid)
            {
                //將頁面資料中的密碼填入
                RegisterMember.newMemder.Password = RegisterMember.Password;
                //取得信箱驗證碼
                string AuthCode = mailService.GetValidatoCode();
                //將信箱驗證碼填入
                RegisterMember.newMemder.AuthCode = AuthCode;
                //呼叫Service註冊新會員
                memberService.Register(RegisterMember.newMemder);
                //取得寫好的驗證信範本內容
                string Tempmail = System.IO.File.ReadAllText(Server.MapPath("~/View/Shared/RegisterEmailTemplate.html"));
                //宣告Email驗證用的Url
                UriBuilder ValidateUrl = new UriBuilder(Request.Url)
                {
                    Path = Url.Action("EmailValidate", "Member"
                        , new
                        {
                            UserName = RegisterMember.newMemder.Acount, AuthCode = AuthCode
                        })
                };
                //藉由 Service將使用者資料填入驗證信範本中
                string MailBody = mailService.GetRegisterMailBody(Tempmail, RegisterMember.newMemder.Name, ValidateUrl.ToString().Replace("%3F", "?"));
                //呼叫service氣出驗證信
                mailService.SendRegisterMail(MailBody, RegisterMember.newMemder.Email);
                //用Tempdata儲存註冊訊息
                TempData["RegisterState"] = "註冊成功，請去收信以驗證Email";
                //重新導向頁面
                return RedirectToAction("RegisterResult");
            }
            //未經驗證清空密碼相關欄位
            RegisterMember.Password = null;
            RegisterMember.PasswordCheck = null;
            //將資料回傳至view中
            return View(RegisterMember);
        }
    }
}