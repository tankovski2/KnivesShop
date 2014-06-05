using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KnivesShop.Models;
using KnivesShop.Data;
using KnivesShop.Web.Controllers;
using KnivesShop.Web.Areas.Administration.Models;
using KnivesShop.Web.Helpers;
using KnivesShop.Web.Resources;
using System.Configuration;
using KnivesShop.Web.Infrastructure.Alerts;

namespace KnivesShop.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class ArticlesAdminController : BaseController
    {
        private int _pageSize;
        private string _msg = "The article {0} was successfully {1}";

        public int PageSize
        {
            get { return this._pageSize; }
            set { this._pageSize = value; }
        }

        public ArticlesAdminController(IUowData data)
            : base(data)
        {
            PageSize = int.Parse(ConfigurationManager.AppSettings["ArticlesAdministrationPageSize"]);
        }

        public ActionResult SearchArticles()
        {
            return View();
        }

        public ActionResult Index(ArticlesAdminSearchModel searchModel)
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
                .Select(ArticlesListViewModel.FromArticle)
                .Skip((searchModel.PageNumber - 1) * PageSize).Take(PageSize).ToList();

            ViewBag.Pages = numberOfPages;
            AddSearchCriterias(searchModel);

            return View(articlesToDisplay);
        }

        public ActionResult Details(int? id, ArticlesAdminSearchModel searchModel)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = Data.Articles.GetById((int)id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ArticlesDetailViewModel articleModel = ArticlesDetailViewModel.TransformToViewModel(article);
            //AddSearchCriterias(searchModel);

            return View(articleModel);
        }

        public ActionResult Create(ArticlesAdminSearchModel searchModel)
        {
            return View();
        }

        // POST: /Administration/ArticlesAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticleCreateViewModel article)
        {
            if (ModelState.IsValid)
            {
                Article articleEntity = article.TransformToArticle();
                FileHelper.SaveImage(articleEntity, article.Image);
                Data.Articles.Add(articleEntity);
                Data.SaveChanges();

                return RedirectToAction("Details", new { id = articleEntity.Id })
                    .WithSuccess(string.Format(_msg, article.NameEn, "created"));
            }

            return View(article);
        }

        public ActionResult Edit(int? id, ArticlesAdminSearchModel searchModel)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = Data.Articles.GetById((int)id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ArticlesEditViewModel articleModel = ArticlesEditViewModel.TransformToViewModel(article);

            return View(articleModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArticlesEditViewModel article)
        {
            if (ModelState.IsValid)
            {
                var ss = Request.QueryString["isTopArticle"];
                Article articleEntity = article.TransformToArticle();
                if (article.Image != null && article.Image.ContentLength > 0)
                {
                    FileHelper.UpdateImage(articleEntity, article.CurrentImage, article.Image);
                }
                Data.Articles.Update(articleEntity);
                Data.SaveChanges();
                ArticlesAdminSearchModel searchModel = article.GenerateSearchModel();

                return RedirectToAction("Index", searchModel)
                    .WithSuccess(string.Format(_msg, article.NameEn, "edited"));
            }

            return View(article);
        }

        public ActionResult Delete(int? id, ArticlesAdminSearchModel searchModel)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = Data.Articles.GetById((int)id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ArticlesDetailViewModel articleModel = ArticlesDetailViewModel.TransformToViewModel(article);

            return View(articleModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, ArticlesAdminSearchModel searchModel)
        {
            Article article = Data.Articles.GetById((int)id);
            FileHelper.DeleteImage(article.Image);
            Data.Articles.Delete(article);
            Data.SaveChanges();

            return RedirectToAction("Index", searchModel)
                .WithSuccess(string.Format(_msg, article.NameEn, "deleted"));
        }

        private IQueryable<Article> SearchByCriterias(ArticlesAdminSearchModel searchModel)
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
            if (articles.Count() != 0 && searchModel.IsTopArticle != null)
            {
                articles = articles.Where(article => article.IsSelectedForTopArticle == searchModel.IsTopArticle);
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
                    .Where(article =>
                        article.NameBg.Contains(searchModel.SearchSubstring) ||
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

        /// <summary>
        /// Add the search criterias to the ViewBag
        /// </summary>
        /// <param name="searchModel">The object that contains the criterias</param>
        private void AddSearchCriterias(ArticlesAdminSearchModel searchModel)
        {
            ViewBag.FromPrice = searchModel.FromPrice;
            ViewBag.ToPrice = searchModel.ToPrice;
            ViewBag.CategoryId = searchModel.CategoryId;
            ViewBag.SearchSubstirng = searchModel.SearchSubstring;
            ViewBag.PageNumber = searchModel.PageNumber;
            ViewBag.IsTopArticle = searchModel.IsTopArticle;
        }
    }
}
