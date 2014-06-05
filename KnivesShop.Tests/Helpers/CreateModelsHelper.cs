using KnivesShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnivesShop.Tests.Helpers
{
    public static class CreateModelsHelper
    {
        /// <summary>
        /// Creates a list of ctegories for test
        /// </summary>
        /// <returns>List fo caegories</returns>
        public static List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>
            {
                new Category
                    {
                        Id=1,
                        NameBg = "Фиксирани остриета",
                        NameEn = "Fix blade",
                    },
                    new Category
                    {
                        Id=2,
                        NameBg = "Сгъваеми",
                        NameEn = "Folding",
                    }
            };

            return categories;
        }

        /// <summary>
        /// Creates a list of articles for test
        /// </summary>
        /// <returns>List of articles</returns>
        public static List<Article> GetArticles()
        {
            List<Category> categories = GetCategories();
            List<Article> articles = new List<Article>
            {
                new Article
                {
                    Id=1,
                    NameEn = "First test knive",
                    NameBg = "Първи тестов нож",
                    DescriptionBg = "Първо тестово описание",
                    DescriptionEn = "First test description",
                    IsSelectedForTopArticle = true,
                    Price = 10.00M,
                    Category = categories[0],
                    CateoryId= categories[0].Id
                },
                new Article
                {
                    Id=2,
                    NameEn = "Second test knive",
                    NameBg = "Втори тестов нож, със странно име",
                    DescriptionBg = "Второ тестово описание",
                    DescriptionEn = "Second test description",
                    Price = 15.00M,
                    IsSelectedForTopArticle = false,
                    Category = categories[1],
                    CateoryId= categories[1].Id
                },
                new Article
                {
                    Id=3,
                    NameEn = "Third test knive",
                    NameBg = "Трети тестов нож, със странно име",
                    DescriptionBg = "Трето тестово описание",
                    DescriptionEn = "Third test description",
                    IsSelectedForTopArticle = true,
                    Price = 20.00M,
                    Category = categories[1],
                    CateoryId= categories[1].Id
                },
                new Article
                {
                    Id=4,
                    NameEn = "Fourth test knive",
                    NameBg = "Четвърти тестов нож",
                    DescriptionBg = "Четвърто тестово описание",
                    DescriptionEn = "Fourth test description",
                    IsSelectedForTopArticle = true,
                    Price = 25.00M,
                    Category = categories[0],
                    CateoryId= categories[0].Id
                }
            };

            return articles;
        }
    }
}
