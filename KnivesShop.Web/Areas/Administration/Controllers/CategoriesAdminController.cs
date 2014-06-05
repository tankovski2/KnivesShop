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
using System.Configuration;
using KnivesShop.Web.Infrastructure.Alerts;
using KnivesShop.Web.Helpers;

namespace KnivesShop.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class CategoriesAdminController : BaseController
    {
        private int _pageSize;
        private string _successMsg = "The article {0} was successfully {1}";

        public int PageSize
        {
            get { return this._pageSize; }
            set { this._pageSize = value; }
        }

        public CategoriesAdminController(IUowData data)
            : base(data)
        {
            _pageSize = int.Parse(ConfigurationManager.AppSettings["CategoriesAdminPageSize"]);
        }

        public ActionResult Index(int? page)
        {
            int pageNumber;
            if (page == 0)
            {
                pageNumber = 1;
            }
            else
            {
                pageNumber = page.GetValueOrDefault(1);
            }

            var categories = Data.Categories.All();

            var numberOfPages = Math.Ceiling((double)categories.Count() / PageSize);

            var categoriesToDisplay = categories
                .OrderBy(category => category.Id)
                .Select(CategoryDetailViewModel.FromCategory)
                .Skip((pageNumber - 1) * PageSize).Take(PageSize).ToList();

            ViewBag.Pages = numberOfPages;
            ViewBag.PageNumber = pageNumber;

            return View(categoriesToDisplay);
        }

        public ActionResult Details(int? id, int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = Data.Categories.GetById((int)id);
            if (category == null)
            {
                return HttpNotFound();
            }
            CategoryDetailViewModel categoryModel = CategoryDetailViewModel.TransformToViewModel(category);

            return View(categoryModel);
        }

        public ActionResult Create(int? page)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryCreateViewModel category)
        {
            if (ModelState.IsValid)
            {
                Category categoryEntity = category.TransformToCategory();
                Data.Categories.Add(categoryEntity);
                Data.SaveChanges();

                return RedirectToAction("Details", new { id = categoryEntity.Id })
                    .WithSuccess(string.Format(_successMsg, category.NameEn, "created"));
            }

            return View(category);
        }

        public ActionResult Edit(int? id, int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = Data.Categories.GetById((int)id);
            if (category == null)
            {
                return HttpNotFound();
            }
            CategoryDetailViewModel categoryModel = CategoryDetailViewModel.TransformToViewModel(category);
            categoryModel.Page = page.GetValueOrDefault(1);
            return View(categoryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryDetailViewModel category)
        {
            if (ModelState.IsValid)
            {
                Category categoryEntity = category.TransformToCategory();
                Data.Categories.Update(categoryEntity);
                Data.SaveChanges();

                return RedirectToAction("Index", new { page = category.Page })
                    .WithSuccess(string.Format(_successMsg, category.NameEn, "edited"));
            }
            return View(category);
        }

        public ActionResult Delete(int? id, int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = Data.Categories.GetById((int)id);
            if (category == null)
            {
                return HttpNotFound();
            }
            CategoryDetailViewModel categoryModel = CategoryDetailViewModel.TransformToViewModel(category);
            return View(categoryModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int? page)
        {
            Category category = Data.Categories.All().Include("Articles").FirstOrDefault(cat => cat.Id == id);
            List<Article> articles = category.Articles.ToList();
            for (int i = articles.Count - 1; i >= 0; i--)
            {
                var article = articles[i];
                FileHelper.DeleteImage(article.Image);
                Data.Articles.Delete(article);
            }
            Data.Categories.Delete(category);
            Data.SaveChanges();

            return RedirectToAction("Index", new { page = page })
                .WithSuccess(string.Format(_successMsg, category.NameEn, "deleted"));
        }
    }
}
