using KnivesShop.Models;
using KnivesShop.Models.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KnivesShop.Web.Areas.Administration.Models
{
    public class ArticleCreateViewModel
    {
        [Required]
        [Display(Prompt = "Enter bulgarian name")]
        public string NameBg { get; set; }

        [Required]
        [Display(Prompt = "Enter english name")]
        public string NameEn { get; set; }

        [Required]
        [Display(Prompt = "Enter bulgarian description")]
        [DataType(DataType.MultilineText)]
        public string DescriptionBg { get; set; }

        [Required]
        [Display(Prompt = "Enter english description")]
        [DataType(DataType.MultilineText)]
        public string DescriptionEn { get; set; }

        [Required]
        [DataType("Image")]
        public HttpPostedFileBase Image { get; set; }

        [Required]
        [Display(Prompt = "Enter price")]
        [Range(1, int.MaxValue, ErrorMessage = "The price of the product must be greater than zero")]
        public decimal Price { get; set; }

        [DataType("Category")]
        [Required]
        public int CateoryId { get; set; }

        public bool IsSelectedForTopArticle { get; set; }

        public Article TransformToArticle()
        {
            return new Article
            {
                NameBg = this.NameBg,
                NameEn = this.NameEn,
                DescriptionBg = this.DescriptionBg,
                DescriptionEn = this.DescriptionEn,
                IsSelectedForTopArticle = this.IsSelectedForTopArticle,
                CateoryId = this.CateoryId,
                Price = this.Price
            };
        }
    }
}