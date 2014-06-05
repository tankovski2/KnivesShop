using KnivesShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace KnivesShop.Web.Areas.Administration.Models
{
    public class ArticlesListViewModel
    {
        public int Id { get; set; }

        public string NameEn { get; set; }

        public decimal Price { get; set; }

        public bool IsSelectedForTopArticle { get; set; }

        public string Category { get; set; }

        public static Expression<Func<Article,ArticlesListViewModel>> FromArticle
        {
            get
            {
                return article => new ArticlesListViewModel
                {
                    Id = article.Id,
                    NameEn = article.NameEn,
                    Price = article.Price,
                    IsSelectedForTopArticle = article.IsSelectedForTopArticle,
                    Category = article.Category.NameEn
                };
            }
        }
    }
}