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
    public class CategoriesController : BaseController
    {
        private string _cutlure;

        public string CurrentCulture
        {
            get { return this._cutlure; }
            set { this._cutlure = value; }
        }

        public CategoriesController(IUowData data)
            :base(data)
        {
            _cutlure = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        }

        public ActionResult GetTopCategories()
        {
            var topCategories = Data.Categories.All().Where(category => category.IsTopCategory)
                .Select(CategoryViewModel.FromCategory(CurrentCulture)).ToList();
            
            return PartialView(topCategories);
        }
	}
}