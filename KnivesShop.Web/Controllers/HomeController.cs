using KnivesShop.Data;
using KnivesShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace KnivesShop.Web.Controllers
{
    public class HomeController : BaseController
    {
        private string _cutlure;

        public string CurrentCulture 
        {             
            get{return this._cutlure;}
            set{this._cutlure = value;} 
        }

        public HomeController(IUowData data)
            :base(data)
        {
            _cutlure = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        }

        public ActionResult Index()
        {
            var topArticles = Data.Articles.All()
                .Where(article => article.IsSelectedForTopArticle)
                .Select(ArticleViewModel.FromArticle(CurrentCulture)).ToList();

            return View(topArticles);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}