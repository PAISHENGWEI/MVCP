using MVCProject.Models;
using MVCProject.Services;
using MVCProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Controllers
{
    public class GuestbookController : Controller
    {
        GuestbookDBService guestbookService = new GuestbookDBService();
        // GET: Guestbook
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult Index(string Search, int Page=1)
        {
            //宣告一個新頁面模型
            GuestbookView Data = new GuestbookView();
            
            Data.Search = Search;
            Data.Paging = new ForPaging(Page);
            Data.DataList = guestbookService.GetDataList(Data.Paging,Data.Search);
            
            return View(Data);
        }
        public ActionResult Create()
        {
            
            return PartialView();
        }
        
        [HttpPost] 
        //使用Bind的Include來定義只接受的欄位，用來避免傳入其他不相干值
        public ActionResult Add([Bind(Include = "Name,Contents")]Guestbooks Data)
        {
            
            guestbookService.InsertGuestbooks(Data);
            
            return RedirectToAction("Index");
        }
        public ActionResult Delete(Guid id)
        {
            guestbookService.DeletGuestbooks(id);
                return RedirectToAction("Index");
        }

        //修改留言
        public ActionResult Edit(Guid Id)
        {
            Guestbooks Data = guestbookService.GetDataById(Id);

            return View(Data);
        }
        [HttpPost]
        public ActionResult Edit(Guid Id,[Bind(Include ="Name,Contents")]Guestbooks UpdateData)
        {
            if (guestbookService.CheckUpdata(Id))
            {
                UpdateData.Id = Id;
                guestbookService.UpdateGuestbooks(UpdateData);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        //回覆留言
        public ActionResult Reply(Guid Id)
        {
            Guestbooks Data = guestbookService.GetDataById(Id);
            return View(Data);
        }
        [HttpPost]
        public ActionResult Reply(Guid Id ,[Bind(Include ="Reply,ReplyTime")]Guestbooks ReplyData)
        {
            if (guestbookService.CheckUpdata(Id))
            {
                ReplyData.Id = Id;
                guestbookService.ReplyGuestbooks(ReplyData);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

    }
}