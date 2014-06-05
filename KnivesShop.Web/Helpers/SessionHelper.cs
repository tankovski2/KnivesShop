using KnivesShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnivesShop.Web.Helpers
{
    public static class SessionHelper
    {
        public static List<ArticleBasketViewModel> BasketItems 
        {
            get
            {
                if (HttpContext.Current.Session["Basket"] == null)
                {
                    HttpContext.Current.Session["Basket"] = new List<ArticleBasketViewModel>();
                }

                return HttpContext.Current.Session["Basket"] as List<ArticleBasketViewModel>;
            }
            set 
            {
                HttpContext.Current.Session["Basket"] = value;
            }
        }
    }
}