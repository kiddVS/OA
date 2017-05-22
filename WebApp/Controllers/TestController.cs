using CUP.PE.OA.BLL;
using CUP.PE.OA.IBLL;
using CUP.PE.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index(string year)
        {
            ViewBag.year = year;
            return View();
        }
        public ActionResult TestError()
        {
            int a = 0;
            int b = 1;
            int c = b / a;
            return Content(c.ToString());
        }
        public ActionResult InsertBook()
        {
            Books book = new Books();
            book.Title = "一本闲书";
            book.Author = "姜敞";
            book.PublisherId = 1;
            book.PublishDate = DateTime.Now;
            book.ISBN = "fsaf";
            book.WordsCount = 100;
            book.UnitPrice = 100;
            book.ContentDescription = "Java面向对象，.net面向对象，一本闲书，微软姜敞，深受专家和程序员喜爱";
            IBooksService bookService = new BooksService();
            bookService.AddEntity(book);
            DataIndexManager.GetInstance().InserData2Queue(10000, book.Title, book.ContentDescription, EnumBookSave.Addbook);
            return Content("OK");
        }
    }
}