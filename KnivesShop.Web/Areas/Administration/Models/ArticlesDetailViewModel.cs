using KnivesShop.Models;
using KnivesShop.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace KnivesShop.Web.Areas.Administration.Models
{
    public class ArticlesDetailViewModel
    {
        public int Id { get; set; }

        public string NameBg { get; set; }

        public string NameEn { get; set; }

        public string DescriptionBg { get; set; }

        public string DescriptionEn { get; set; }

        public string Image { get; set; }

        public decimal Price { get; set; }

        public bool IsSelectedForTopArticle { get; set; }

        public string Category { get; set; }


        public static ArticlesDetailViewModel TransformToViewModel(Article article)
        {

            return new ArticlesDetailViewModel
            {
                Id = article.Id,
                NameEn = article.NameEn,
                NameBg = article.NameBg,
                DescriptionBg = article.DescriptionBg,
                DescriptionEn = article.DescriptionEn,
                Image = "~/"+PathHelper.ArticlesImagesRelativeUrl + article.Image,
                Price = article.Price,
                IsSelectedForTopArticle = article.IsSelectedForTopArticle,
                Category = article.Category.NameEn
            };

        }
    }
}