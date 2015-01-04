using KnivesShop.Models;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace KnivesShop.Web.Areas.Administration.Models
{
    public class ArticleCreateViewModel
    {
        [Required]
        [Display(Prompt = "Enter bulgarian name")]
        [StringLength(200, ErrorMessage = "NameBg cannot be longer than 200 characters.")]
        public string NameBg { get; set; }

        [Required]
        [Display(Prompt = "Enter english name")]
        [StringLength(200, ErrorMessage = "NameEn cannot be longer than 200 characters.")]
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

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }

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