using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProject.Services
{
    public class GuestbookDBService
    {
        MVCProjectEntities db = new MVCProjectEntities();

        public List<Guestbooks> GetDataList()
        {
           
            return db.Guestbooks.ToList();
        }

       
        //新增資料方法
        public void InsertGuestbooks(Guestbooks newData)
        {
            newData.Id = Guid.NewGuid();
           
            newData.CreateTime = DateTime.Now;
            
            db.Guestbooks.Add(newData);

            db.SaveChanges();
            //try {
            //    db.SaveChanges();
            //}
            //catch(Exception)
            //{
            //    throw;
            //}
        }



        public List<Guestbooks> GetDataList(ForPaging Paging,string Search)
        {
            //宣告要接受全部搜尋資料的物件
            IQueryable<Guestbooks> SearchData;
            //判斷搜尋是否為空或Null，用於決定要呼叫取得搜尋資料
            if (String.IsNullOrEmpty(Search))
            {
                SearchData = GetAllDataList(Paging);
            }
            else
            {
                SearchData = GetAllDataList(Paging, Search);
            }
            //先排序再根據分頁來回傳所需部分的資料陣列
            return SearchData.OrderByDescending(p=>p.Id).Skip((Paging.NowPage-1)*Paging.ItemNum).Take(Paging.ItemNum).ToList();
        }
        //無搜尋值的搜尋方法
        public IQueryable<Guestbooks>GetAllDataList(ForPaging Paging)
        {
            IQueryable<Guestbooks> Data = db.Guestbooks;
            Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Data.Count()) / Paging.ItemNum));
            Paging.SetRightPage();
            return Data;
        }
        //包含搜尋值的方法
        public IQueryable<Guestbooks>GetAllDataList(ForPaging Paging ,string Search)
        {
            IQueryable<Guestbooks> Data = db.Guestbooks.Where(p => p.Acount.Contains(Search)
                 || p.Contents.Contains(Search) || p.Reply.Contains(Search));
            Paging.SetRightPage();

            return Data;
        }
        //刪除資料
        public void DeletGuestbooks(Guid id)
        {
            Guestbooks DeletData = db.Guestbooks.Find(id);
            db.Guestbooks.Remove(DeletData);
            db.SaveChanges();
        }

        //id 取得單筆資料
        public Guestbooks GetDataById(Guid Id)
        {
            return db.Guestbooks.Find(Id);
        }
        //修改留言方法
        public void UpdateGuestbooks(Guestbooks UpdateData)
        {
            Guestbooks OldData = db.Guestbooks.Find(UpdateData.Id);
            OldData.Acount = UpdateData.Acount;
            OldData.Contents = UpdateData.Contents;
            db.SaveChanges();
        }
        //回覆留言方法
        public void ReplyGuestbooks(Guestbooks ReplyData)
        {
            Guestbooks OldData = db.Guestbooks.Find(ReplyData.Id);
            OldData.Reply = ReplyData.Reply;
            OldData.ReplyTime= DateTime.Now;
            db.SaveChanges();
        }
        //修改資料判斷的方法
        public bool CheckUpdata(Guid Id)
        {
            Guestbooks Data = db.Guestbooks.Find(Id);
            return (Data != null && Data.ReplyTime == null);
        }
    }
}
