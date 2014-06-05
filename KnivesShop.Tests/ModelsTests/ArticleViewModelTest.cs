using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using KnivesShop.Models;
using KnivesShop.Tests.Helpers;
using KnivesShop.Web.Models;
using System.Linq;
using KnivesShop.Models.Enums;

namespace KnivesShop.Tests.ModelsTests
{
    [TestClass]
    public class ArticleViewModelTest
    {
        private List<Article> _articles;

        public ArticleViewModelTest()
        {
            _articles = CreateModelsHelper.GetArticles().Take(2).ToList();
        }

        [TestMethod]
        public void FromArticle_AllReturnedArticlesMustHaveBgPropertiesIfCultureIsBg()
        {
            IList<ArticleViewModel> models = _articles.AsQueryable().Select(ArticleViewModel
                .FromArticle(Culture.bg.ToString())).ToList();

            Assert.AreEqual(models[0].Name, _articles[0].NameBg);
            Assert.AreEqual(models[0].Description, _articles[0].DescriptionBg);
            Assert.AreEqual(models[1].Name, _articles[1].NameBg);
            Assert.AreEqual(models[1].Description, _articles[1].DescriptionBg);
        }

        [TestMethod]
        public void FromArticle_MustReturnEmptyListIfNoArticlesAreSelectedForTopArticles()
        {
            IList<ArticleViewModel> models = _articles.AsQueryable().Select(ArticleViewModel
                .FromArticle(Culture.en.ToString())).ToList();

            Assert.AreEqual(models[0].Name, _articles[0].NameEn);
            Assert.AreEqual(models[0].Description, _articles[0].DescriptionEn);
            Assert.AreEqual(models[1].Name, _articles[1].NameEn);
            Assert.AreEqual(models[1].Description, _articles[1].DescriptionEn);
        }
    }
}
