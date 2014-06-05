using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KnivesShop.Models;
using System.Collections.Generic;
using Moq;
using KnivesShop.Data;
using KnivesShop.Web.Controllers;
using KnivesShop.Web.Models;
using System.Web.Mvc;
using System.Linq;
using KnivesShop.Tests.Helpers;
using System.Net;
using KnivesShop.Models.Enums;
using System.Web;

namespace KnivesShop.Tests.ControllersTests
{
    [TestClass]
    public class ArticlesControllerTest
    {
        private IList<Article> _articles;
        private IList<Category> _categories;

        public ArticlesControllerTest()
        {
            _articles = CreateModelsHelper.GetArticles().Take(4).ToList();
            _categories = CreateModelsHelper.GetCategories();
        }

        #region Detail Tests

        [TestMethod]
        public void Detail_MustReturnArticleIfWePassCorrectId()
        {
            //Mock repository
            var mockedArticles = new Mock<IRepository<Article>>();
            mockedArticles.Setup(repo => repo.All()).Returns(_articles.AsQueryable());

            //Mock data
            var mockedUow = new Mock<IUowData>();
            mockedUow.Setup(uow => uow.Articles).Returns(() => { return mockedArticles.Object; });

            // Act
            ArticlesController articlesController = new ArticlesController(mockedUow.Object);

            var results = articlesController.Detail(1) as ViewResult;
            var model = results.Model as ArticleViewModel;

            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Id);
        }

        [TestMethod]
        public void Detail_MustReturnBadRequesStatusCodeIfWePassNull()
        {
            //Mock repository
            var mockedArticles = new Mock<IRepository<Article>>();
            mockedArticles.Setup(repo => repo.All()).Returns(_articles.AsQueryable());

            //Mock data
            var mockedUow = new Mock<IUowData>();
            mockedUow.Setup(uow => uow.Articles).Returns(() => { return mockedArticles.Object; });

            // Act
            ArticlesController articlesController = new ArticlesController(mockedUow.Object);

            var results = articlesController.Detail(null) as HttpStatusCodeResult;
            Assert.AreEqual(results.StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void Detail_MustReturnNotFoundStatusCodeIfThereIsNoArticleWithThisId()
        {
            //Mock repository
            var mockedArticles = new Mock<IRepository<Article>>();
            mockedArticles.Setup(repo => repo.All()).Returns(_articles.AsQueryable());

            //Mock data
            var mockedUow = new Mock<IUowData>();
            mockedUow.Setup(uow => uow.Articles).Returns(() => { return mockedArticles.Object; });

            // Act
            ArticlesController articlesController = new ArticlesController(mockedUow.Object);

            var results = articlesController.Detail(222) as HttpStatusCodeResult;
            Assert.AreEqual(results.StatusCode, (int)HttpStatusCode.NotFound);
        }

        #endregion

        #region ArticlesFromCategory Tests

        [TestMethod]
        public void ArticlesFromCategory_TwoArticlesInCategory_BiggerPageSize_NoPageNumber_MustReturnTwoArticles()
        {
            //Mock repository
            var mockedArticles = new Mock<IRepository<Article>>();
            mockedArticles.Setup(repo => repo.All()).Returns(_articles.AsQueryable());

            //Mock data
            var mockedUow = new Mock<IUowData>();
            mockedUow.Setup(uow => uow.Articles).Returns(() => { return mockedArticles.Object; });

            // Act
            ArticlesController articlesController = new ArticlesController(mockedUow.Object);

            var results = articlesController.ArticlesFromCategory(2, null) as ViewResult;
            var model = results.Model as ICollection<ArticleViewModel>;

            Assert.AreEqual(2, model.Count);
        }

        [TestMethod]
        public void ArticlesFromCategory_TwoArticlesInCategory_PageSizeOne_NoPageNumber_MustReturnFirstArticle()
        {
            //Mock repository
            var mockedArticles = new Mock<IRepository<Article>>();
            mockedArticles.Setup(repo => repo.All()).Returns(_articles.AsQueryable());

            //Mock data
            var mockedUow = new Mock<IUowData>();
            mockedUow.Setup(uow => uow.Articles).Returns(() => { return mockedArticles.Object; });

            // Act
            ArticlesController articlesController = new ArticlesController(mockedUow.Object);
            articlesController.PageSize = 1;

            var results = articlesController.ArticlesFromCategory(2, null) as ViewResult;
            var model = results.Model as ICollection<ArticleViewModel>;

            var expectedArticle = _articles.FirstOrDefault(article => article.CateoryId == 2);

            Assert.AreEqual(1, model.Count);
            Assert.AreEqual(expectedArticle.Id, model.FirstOrDefault().Id);
        }

        [TestMethod]
        public void ArticlesFromCategory_TwoArticlesInCategory_PageSizeOne_PageNumberTwo_MustReturnSecondArticle()
        {
            //Mock repository
            var mockedArticles = new Mock<IRepository<Article>>();
            mockedArticles.Setup(repo => repo.All()).Returns(_articles.AsQueryable());

            //Mock data
            var mockedUow = new Mock<IUowData>();
            mockedUow.Setup(uow => uow.Articles).Returns(() => { return mockedArticles.Object; });

            // Act
            ArticlesController articlesController = new ArticlesController(mockedUow.Object);
            articlesController.PageSize = 1;

            var results = articlesController.ArticlesFromCategory(2, 2) as ViewResult;
            var model = results.Model as ICollection<ArticleViewModel>;

            var expectedArticle = _articles.Where(article => article.CateoryId == 2).ToList()[1];

            Assert.AreEqual(1, model.Count);
            Assert.AreEqual(expectedArticle.Id, model.FirstOrDefault().Id);
        }

        [TestMethod]
        public void ArticlesFromCategory_NoCategoryId_MustReturnStatusCodeBadRequest()
        {
            //Mock repository
            var mockedArticles = new Mock<IRepository<Article>>();
            mockedArticles.Setup(repo => repo.All()).Returns(_articles.AsQueryable());

            //Mock data
            var mockedUow = new Mock<IUowData>();
            mockedUow.Setup(uow => uow.Articles).Returns(() => { return mockedArticles.Object; });

            // Act
            ArticlesController articlesController = new ArticlesController(mockedUow.Object);

            var results = articlesController.ArticlesFromCategory(null, null) as HttpStatusCodeResult;

            Assert.AreEqual(results.StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void ArticlesFromCategory_NotExistingCategoryId_MustThrowHttpException()
        {
            //Mock categories repository
            int categoryId = 50;
            var mockedCategories = new Mock<IRepository<Category>>();
            mockedCategories.Setup(repo => repo.GetById(50)).Returns(_categories.FirstOrDefault(cat => cat.Id == categoryId));

            //Mock articles repository
            var mockedArticles = new Mock<IRepository<Article>>();
            mockedArticles.Setup(repo => repo.All()).Returns(_articles.AsQueryable());

            //Mock data
            var mockedUow = new Mock<IUowData>();
            mockedUow.Setup(uow => uow.Articles).Returns(() => { return mockedArticles.Object; });
            mockedUow.Setup(uow => uow.Categories).Returns(() => { return mockedCategories.Object; });

            // Act
            ArticlesController articlesController = new ArticlesController(mockedUow.Object);

            var results = articlesController.ArticlesFromCategory(categoryId, null) as ViewResult;
        }

        #endregion

        #region SearchResults Tests

        [TestMethod]
        public void SearchResults_OnlyCategoryId_MustReturnOnlyArticlesFromThisCategory()
        {
            //Mock repository
            var mockedArticles = new Mock<IRepository<Article>>();
            mockedArticles.Setup(repo => repo.All()).Returns(_articles.AsQueryable());

            //Mock data
            var mockedUow = new Mock<IUowData>();
            mockedUow.Setup(uow => uow.Articles).Returns(() => { return mockedArticles.Object; });

            // Act
            ArticlesController articlesController = new ArticlesController(mockedUow.Object);
            var searchModel = new SearchArticlesViewModel
            {
                CategoryId = 1
            };

            var results = articlesController.SearchResults(searchModel) as ViewResult;
            var model = results.Model as IList<ArticleViewModel>;

            Assert.AreEqual(2, model.Count);
            Assert.AreEqual(1, model[0].Id);
            Assert.AreEqual(4, model[1].Id);
        }

        [TestMethod]
        public void SearchResults_OnlyFromPriceFifteen_OnlyArticlesWithPriceHigherThanFifteen()
        {
            //Mock repository
            var mockedArticles = new Mock<IRepository<Article>>();
            mockedArticles.Setup(repo => repo.All()).Returns(_articles.AsQueryable());

            //Mock data
            var mockedUow = new Mock<IUowData>();
            mockedUow.Setup(uow => uow.Articles).Returns(() => { return mockedArticles.Object; });

            // Act
            ArticlesController articlesController = new ArticlesController(mockedUow.Object);
            var searchModel = new SearchArticlesViewModel
            {
                FromPrice = 15
            };

            var results = articlesController.SearchResults(searchModel) as ViewResult;
            var model = results.Model as IList<ArticleViewModel>;

            Assert.AreEqual(3, model.Count);
            foreach (var article in model)
            {
                Assert.IsTrue(article.Price >= searchModel.FromPrice);
            }

        }

        [TestMethod]
        public void SearchResults_OnlyToPRiceTwenty_MustReturnOnlyArticlesWithPriceLowerThanTwenty()
        {
            //Mock repository
            var mockedArticles = new Mock<IRepository<Article>>();
            mockedArticles.Setup(repo => repo.All()).Returns(_articles.AsQueryable());

            //Mock data
            var mockedUow = new Mock<IUowData>();
            mockedUow.Setup(uow => uow.Articles).Returns(() => { return mockedArticles.Object; });

            // Act
            ArticlesController articlesController = new ArticlesController(mockedUow.Object);
            var searchModel = new SearchArticlesViewModel
            {
                ToPrice = 20
            };

            var results = articlesController.SearchResults(searchModel) as ViewResult;
            var model = results.Model as IList<ArticleViewModel>;

            Assert.AreEqual(3, model.Count);
            foreach (var article in model)
            {
                Assert.IsTrue(article.Price <= searchModel.ToPrice);
            }

        }

        [TestMethod]
        public void SearchResults_OnlySearchSubstring_MustReturnOnlyArticlesWhoseNameForTheCurrentCultureContainsThisSubstring()
        {
            //Mock repository
            var mockedArticles = new Mock<IRepository<Article>>();
            mockedArticles.Setup(repo => repo.All()).Returns(_articles.AsQueryable());

            //Mock data
            var mockedUow = new Mock<IUowData>();
            mockedUow.Setup(uow => uow.Articles).Returns(() => { return mockedArticles.Object; });

            // Act
            ArticlesController articlesController = new ArticlesController(mockedUow.Object);
            articlesController.CurrentCulture = Culture.bg.ToString();
            var searchModel = new SearchArticlesViewModel
            {
                SearchSubstring = "странно"
            };

            var results = articlesController.SearchResults(searchModel) as ViewResult;
            var model = results.Model as IList<ArticleViewModel>;

            Assert.AreEqual(2, model.Count);
            foreach (var article in model)
            {
                Assert.IsTrue(article.Name.Contains(searchModel.SearchSubstring));
            }

        }

        [TestMethod]
        public void SearchResults_WithFullSearchModel_MustReturnOnlyArticlesThatMatchAllTheCriterias()
        {
            //Mock repository
            var mockedArticles = new Mock<IRepository<Article>>();
            mockedArticles.Setup(repo => repo.All()).Returns(_articles.AsQueryable());

            //Mock data
            var mockedUow = new Mock<IUowData>();
            mockedUow.Setup(uow => uow.Articles).Returns(() => { return mockedArticles.Object; });

            // Act
            ArticlesController articlesController = new ArticlesController(mockedUow.Object);
            articlesController.CurrentCulture = Culture.bg.ToString();
            var searchModel = new SearchArticlesViewModel
            {
                SearchSubstring = "странно",
                CategoryId = 2,
                FromPrice = 20,
                ToPrice = 30,
            };

            var results = articlesController.SearchResults(searchModel) as ViewResult;
            var model = results.Model as IList<ArticleViewModel>;

            Assert.AreEqual(1, model.Count);

            Assert.IsTrue(model[0].Name.Contains(searchModel.SearchSubstring));
            Assert.IsTrue(model[0].Price >= searchModel.FromPrice);
            Assert.IsTrue(model[0].Price <= searchModel.ToPrice);
            Assert.IsTrue(model[0].Price <= searchModel.ToPrice);
            
            //We dont have property for category id in the view model, so we check if the id of the result is
            //one of the ids of the two articles that we know are from the category we search for
            Assert.IsTrue(model[0].Id == _articles[1].Id || model[0].Id == _articles[2].Id);
        }

        [TestMethod]
        public void SearchResults_WithFromPriceBiggerThanToPrice_MustReturnTheSameSearchModel()
        {
            //Mock repository
            var mockedArticles = new Mock<IRepository<Article>>();
            mockedArticles.Setup(repo => repo.All()).Returns(_articles.AsQueryable());

            //Mock data
            var mockedUow = new Mock<IUowData>();
            mockedUow.Setup(uow => uow.Articles).Returns(() => { return mockedArticles.Object; });

            // Act
            ArticlesController articlesController = new ArticlesController(mockedUow.Object);
            var searchModel = new SearchArticlesViewModel
            {
                FromPrice = 50,
                ToPrice = 20
            };

            var results = articlesController.SearchResults(searchModel) as ViewResult;
            var model = results.Model as SearchArticlesViewModel;

            Assert.AreSame(model,searchModel);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void SearchResults_WithWrongCategoryId_MustThrowHttpException()
        {
            //Mock articles repository
            var mockedArticles = new Mock<IRepository<Article>>();
            mockedArticles.Setup(repo => repo.All()).Returns(_articles.AsQueryable());

            //Mock categories repository
            int categoryId = 50;
            var mockedCategories = new Mock<IRepository<Category>>();
            mockedCategories.Setup(repo => repo.GetById(50)).Returns(_categories.FirstOrDefault(cat => cat.Id == categoryId));

            //Mock data
            var mockedUow = new Mock<IUowData>();
            mockedUow.Setup(uow => uow.Categories).Returns(() => { return mockedCategories.Object; });
            mockedUow.Setup(uow => uow.Articles).Returns(() => { return mockedArticles.Object; });

            // Act
            ArticlesController articlesController = new ArticlesController(mockedUow.Object);
            var searchModel = new SearchArticlesViewModel
            {
                CategoryId = categoryId
            };

            var results = articlesController.SearchResults(searchModel) as ViewResult;
        }

        #endregion
    }
}
