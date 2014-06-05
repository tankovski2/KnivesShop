using KnivesShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnivesShop.Web.Areas.Administration.Models
{
    public class ArticlesEditViewModel
    {
        [HiddenInput(DisplayValue = false)]
        [Required]
        public int Id { get; set; }

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

        [DataType("Image")]
        public HttpPostedFileBase Image { get; set; }

        [DataType("CurrentImage")]
        public string CurrentImage { get; set; }

        [Required]
        [Display(Prompt = "Enter price")]
        [Range(1, int.MaxValue, ErrorMessage = "The price of the product must be greater than zero")]
        public decimal Price { get; set; }

        [DataType("Category")]
        [Required]
        public int Cateory { get; set; }

        //The search criterias so we can return in tha same page 
        #region SearchProperties
        [HiddenInput(DisplayValue = false)]
        public int? CategoryId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? FromPrice { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? ToPrice { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string SearchSubstring { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool? IsTopArticle { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int PageNumber { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool IsSelectedForTopArticle { get; set; }
        #endregion

        public Article TransformToArticle()
        {
            return new Article
            {
                Id = this.Id,
                NameBg = this.NameBg,
                NameEn = this.NameEn,
                DescriptionBg = this.DescriptionBg,
                DescriptionEn = this.DescriptionEn,
                IsSelectedForTopArticle = this.IsSelectedForTopArticle,
                CateoryId = this.Cateory,
                Price = this.Price,
                Image = this.CurrentImage,
            };
        }

        public static ArticlesEditViewModel TransformToViewModel(Article article)
        {
            return new ArticlesEditViewModel
            {
                Id = article.Id,
                NameBg = article.NameBg,
                NameEn = article.NameEn,
                DescriptionBg = article.DescriptionBg,
                DescriptionEn = article.DescriptionEn,
                CurrentImage = article.Image,
                IsSelectedForTopArticle = article.IsSelectedForTopArticle,
                Price = article.Price,
                Cateory = article.CateoryId
            };
        }

        public ArticlesAdminSearchModel GenerateSearchModel()
        {

            return new ArticlesAdminSearchModel
            {
                CategoryId = this.CategoryId,
                FromPrice = this.FromPrice,
                ToPrice = this.ToPrice,
                IsTopArticle = this.IsTopArticle,
                PageNumber = this.PageNumber,
                SearchSubstring = this.SearchSubstring
            };

        }
    }
}