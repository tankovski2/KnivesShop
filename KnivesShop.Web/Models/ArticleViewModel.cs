using KnivesShop.Models;
using KnivesShop.Models.Enums;
using KnivesShop.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Web;

namespace KnivesShop.Web.Models
{
    public class ArticleViewModel
    {
        public int Id { get; set; }

        public string Image { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public static Expression<Func<Article, ArticleViewModel>> FromArticle(string culture)
        {
                return article => new ArticleViewModel
                {
                    Id = article.Id,
                    Image = "~/"+PathHelper.ArticlesImagesRelativeUrl + article.Image,
                    Name = culture == Culture.bg.ToString() ? article.NameBg : article.NameEn,
                    Description = culture == Culture.bg.ToString() ? article.DescriptionBg : article.DescriptionEn,
                    Price = article.Price
                };
            
        }
    }
}