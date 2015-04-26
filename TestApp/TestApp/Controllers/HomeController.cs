using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApp.Abstract;
using TestApp.Concrete;

namespace TestApp.Controllers
{
    public class HomeController : Controller
    {
        private ICleaneable cleaner;
        public HomeController(ICleaneable cleaner) 
        {
            this.cleaner = cleaner;
        }
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]//For quick allow html :))
        public ActionResult Clean(string htmlText, List<string> whiteList, List<string> blackList, bool useExtension = false)
        {
            //Check if lists from client-side is empty
            whiteList.RemoveAll(String.IsNullOrWhiteSpace);
            blackList.RemoveAll(String.IsNullOrWhiteSpace); 
            
            return Content(cleaner.CleanHtml(htmlText, whiteList, blackList));
        }

               
    }
}