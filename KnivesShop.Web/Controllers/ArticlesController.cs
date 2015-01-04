using KnivesShop.Data;
using KnivesShop.Models;
using KnivesShop.Models.Enums;
using KnivesShop.Web.Models;
using KnivesShop.Web.Resources;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using KnivesShop.Web.Infrastructure.Alerts;

namespace KnivesShop.Web.Controllers
{
    public class ArticlesController : BaseController
    {
        private string _cutlure;
        private int _pageSize;

        public string CurrentCulture
        {
            get { return this._cutlure; }
            set { this._cutlure = value; }
        }

        public int PageSize
        {
            get { return this._pageSize; }
            set { this._pageSize = value; }
        }

        public ArticlesController(IUowData data)
            : base(data)
        {
            _cutlure = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            PageSize = int.Parse(ConfigurationManager.AppSettings["ArticlesFromCategoryPageSize"]);
        }

        public ActionResult Detail(int? articleId)
        {
            if (articleId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var article = Data.Articles.All().Select(ArticleViewModel.FromArticle(CurrentCulture))
                .FirstOrDefault(a => a.Id == articleId);

            if (article == null)
            {
                return HttpNotFound();
            }

            return View(article);
        }

        public ActionResult ArticlesFromCategory(int? id, int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int pageNumber;
            if (page == 0)
            {
                pageNumber = 1;
            }
            else
            {
                pageNumber = page.GetValueOrDefault(1);
            }

            var articles = Data.Articles.All().Where(article => article.CateoryId == id);

            //We check if the user gave us incorrect category id
            if (articles.Count() == 0)
            {
                CheckIfCategoryExists((int)id);
            }

            var numberOfPages = Math.Ceiling((double)articles.Count() / PageSize);

            var articlesToDisplay = articles
                .OrderBy(article => article.Price)
                .Select(ArticleViewModel.FromArticle(CurrentCulture))
                .Skip((pageNumber - 1) * PageSize).Take(PageSize).ToList();

            ViewBag.CategoryId = id;
            ViewBag.Pages = numberOfPages;
            ViewBag.PageNumber = pageNumber;

            return View(articlesToDisplay);
        }

        public ActionResult SearchArticles()
        {
            return View();
        }

        public ActionResult SearchResults(SearchArticlesViewModel searchModel)
        {
            if (searchModel.FromPrice != null && searchModel.ToPrice != null)
            {
                if (searchModel.FromPrice >= searchModel.ToPrice)
                {
                    ModelState.AddModelError("ToPrice", Errors.GreaterThan);

                    return View("SearchArticles", searchModel);
                }
            }

            var articles = SearchByCriterias(searchModel);

            if (searchModel.PageNumber == 0)
            {
                searchModel.PageNumber = 1;
            }

            var numberOfPages = Math.Ceiling((double)articles.Count() / PageSize);

            var articlesToDisplay = articles
                .OrderBy(article => article.Price)
                .Select(ArticleViewModel.FromArticle(CurrentCulture))
                .Skip((searchModel.PageNumber - 1) * PageSize).Take(PageSize).ToList();

            if (articlesToDisplay.Count == 0)
            {
                return RedirectToAction("SearchArticles")
                    .WithInfo(Labels.NoArticlesFound);
            }

            ViewBag.FromPrice = searchModel.FromPrice;
            ViewBag.ToPrice = searchModel.ToPrice;
            ViewBag.CategoryId = searchModel.CategoryId;
            ViewBag.SearchSubstirng = searchModel.SearchSubstring;
            ViewBag.Pages = numberOfPages;
            ViewBag.PageNumber = searchModel.PageNumber;

            return View(articlesToDisplay);
        }

        /// <summary>
        /// Get all articles matching the criterieas in the search model
        /// </summary>
        /// <param name="searchModel">The object contains the criterias for searching</param>
        /// <returns>IQueryable<Article></returns>
        private IQueryable<Article> SearchByCriterias(SearchArticlesViewModel searchModel)
        {
            var articles = Data.Articles.All();

            if (searchModel.CategoryId != null)
            {
                articles = articles.Where(article => article.CateoryId == searchModel.CategoryId);

                //We check if the user gave us incorrect category id
                if (articles.Count() == 0)
                {
                    CheckIfCategoryExists((int)searchModel.CategoryId);
                }
            }
            if (articles.Count() != 0 && searchModel.FromPrice != null)
            {
                articles = articles.Where(article => article.Price >= searchModel.FromPrice);
            }
            if (articles.Count() != 0 && searchModel.ToPrice != null)
            {
                articles = articles.Where(article => article.Price <= searchModel.ToPrice);
            }
            if (articles.Count() != 0 && !string.IsNullOrWhiteSpace(searchModel.SearchSubstring))
            {
                articles = articles
                    .Where(article => CurrentCulture == Culture.bg.ToString() ?
                        article.NameBg.Contains(searchModel.SearchSubstring) :
                        article.NameEn.Contains(searchModel.SearchSubstring));
            }

            return articles;
        }

        /// <summary>
        /// Check if there is category with this id, and throw exception if not
        /// </summary>
        /// <param name="categoryId">The id of the category we are looking for</param>
        private void CheckIfCategoryExists(int categoryId)
        {
            var category = Data.Categories.GetById(categoryId);
            if (category == null)
            {
                throw new HttpException(404, Exceptions.NoCategoryId);
            }
        }
    }
}