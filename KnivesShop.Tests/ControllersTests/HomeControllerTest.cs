using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KnivesShop.Web;
using KnivesShop.Web.Controllers;
using KnivesShop.Models;
using Moq;
using KnivesShop.Data;
using KnivesShop.Web.Models;
using KnivesShop.Tests.Helpers;

namespace KnivesShop.Tests.ControllersTests
{
    [TestClass]
    public class HomeControllerTest
    {

        private List<Article> _articles;

        public HomeControllerTest()
        {
            _articles = CreateModelsHelper.GetArticles().Take(3).ToList();
        }

        //--------------------Index tests---------------

        [TestMethod]
        public void Index_MustReturnOnlyTopArticles()
        {
            //Mock repository
            var mockedArticles = new Mock<IRepository<Article>>();
            mockedArticles.Setup(repo => repo.All()).Returns(_articles.AsQueryable());

            //Mock data
            var mockedUow = new Mock<IUowData>();
            mockedUow.Setup(uow => uow.Articles).Returns(() => { return mockedArticles.Object; });

            // Act
            HomeController homeController = new HomeController(mockedUow.Object);

            var results = homeController.Index() as ViewResult;
            var model = results.Model as IList<ArticleViewModel>;
            Assert.AreEqual(2, model.Count());
            Assert.AreEqual(1, model[0].Id);
            Assert.AreEqual(3, model[1].Id);
        }

        [TestMethod]
        public void Index_MustReturnEmptyListIfNoArticlesAreSelectedForTopArticles()
        {
            foreach (var article in _articles)
            {
                article.IsSelectedForTopArticle = false;
            }

            //Mock repository
            var mockedArticles = new Mock<IRepository<Article>>();
            mockedArticles.Setup(repo => repo.All()).Returns(_articles.AsQueryable());

            //Mock data
            var mockedUow = new Mock<IUowData>();
            mockedUow.Setup(uow => uow.Articles).Returns(() => { return mockedArticles.Object; });

            // Act
            HomeController homeController = new HomeController(mockedUow.Object);

            var results = homeController.Index() as ViewResult;
            var model = results.Model as IList<ArticleViewModel>;
            Assert.AreEqual(0, model.Count());
        }

        [TestMethod]
        public void About()
        {
            //// Arrange
            //HomeController controller = new HomeController();

            //// Act
            //ViewResult result = controller.About() as ViewResult;

            //// Assert
            //Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            //// Arrange
            //HomeController controller = new HomeController();

            //// Act
            //ViewResult result = controller.Contact() as ViewResult;

            //// Assert
            //Assert.IsNotNull(result);
        }
    }
}
