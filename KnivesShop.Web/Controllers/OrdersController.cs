using KnivesShop.Data;
using KnivesShop.Web.Helpers;
using KnivesShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KnivesShop.Web.Infrastructure.Alerts;
using KnivesShop.Web.Resources;
using CaptchaMvc.HtmlHelpers;
using KnivesShop.Models;
using System.Text;
using System.Threading;
using KnivesShop.Models.Enums;

namespace KnivesShop.Web.Controllers
{
    public class OrdersController : BaseController
    {

         private string _cutlure;

        public string CurrentCulture
        {
            get { return this._cutlure; }
            set { this._cutlure = value; }
        }

        public OrdersController(IUowData data)
            : base(data)
        {
            _cutlure = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        }

        public ActionResult OrderArticle()
        {
            StringBuilder orderStrignBuilder = new StringBuilder();
            string  units = CurrentCulture == Culture.bg.ToString()?
                "броя":"units";
            foreach (var item in SessionHelper.BasketItems)
            {
                string articleName = CurrentCulture == Culture.bg.ToString()? 
                    item.NameBg: item.NameEn;
                orderStrignBuilder.AppendLine(string.Format("{0} - {1} " + units, articleName, item.Quantity));
            }

            return View(new OrderArticleViewModel { ArticlesNames = orderStrignBuilder.ToString() });
        }

        public ActionResult SendOrder(OrderArticleViewModel orderModel)
        {
            if (!this.IsCaptchaValid(""))
            {
                ModelState.AddModelError("captcha", Errors.NotValidCapctha);
            }
            if (ModelState.IsValid)
            {
                //ToDo uncomment and change e-mail settings
                //EmailHelper.MakeOrder(orderModel);
                string msg = Messages.SuccessOrder + orderModel.ArticlesNames;
                return RedirectToAction("Index", "Home")
                    .WithSuccess(msg);
            }
            return View("OrderArticle", orderModel);
        }

        public ActionResult AddToBasket(int articleId)
        {
            //If the article is already in the basket we just open it
            if (!SessionHelper.BasketItems.Any(item => item.Id == articleId))
            {
                Article article = Data.Articles.GetById(articleId);
                if (article == null)
                {
                    return HttpNotFound();
                }

                ArticleBasketViewModel basketItem = new ArticleBasketViewModel
                {
                    Id = articleId,
                    NameEn = article.NameEn,
                    NameBg = article.NameBg,
                    Price = article.Price,
                    Image = article.Image
                };

                SessionHelper.BasketItems.Add(basketItem);
            }

            return RedirectToAction("Basket");
        }

        public ActionResult UpdateBasket(BasketUpdateViewModel articles)
        {
            for (int i = 0; i < articles.Id.Count; i++)
            {
                var article = SessionHelper.BasketItems.FirstOrDefault(item => item.Id == articles.Id[i]);
                if (article == null)
                {
                    return HttpNotFound();
                }

                int quantity = articles.Quantity[i] > 1 ? articles.Quantity[i] : 1;
                article.Quantity = quantity;
            }

            return PartialView("_Basket",SessionHelper.BasketItems);
        }

        public ActionResult RemoveItemFromBasket(int itemId)
        {
            var article = SessionHelper.BasketItems.FirstOrDefault(item => item.Id == itemId);
            if (article == null)
            {
                return HttpNotFound();
            }
            SessionHelper.BasketItems.Remove(article);

            return PartialView("_Basket", SessionHelper.BasketItems);
        }

        public ActionResult Basket()
        {
            return View(SessionHelper.BasketItems);
        }
    }
}