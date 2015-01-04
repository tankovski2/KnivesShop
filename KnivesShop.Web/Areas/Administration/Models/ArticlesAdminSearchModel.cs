using KnivesShop.Web.Resources;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace KnivesShop.Web.Areas.Administration.Models
{
    public class ArticlesAdminSearchModel
    {
        [Display(Name = "Category", ResourceType = typeof(DisplayNames))]
        [DataType("Category")]
        public int? CategoryId { get; set; }

        [Display(Name = "FromPrice", ResourceType = typeof(DisplayNames))]
        [DataType("FromPrice")]
        public int? FromPrice { get; set; }

        [Display(Name = "ToPrice", ResourceType = typeof(DisplayNames))]
        [DataType("ToPrice")]
        public int? ToPrice { get; set; }

        [Display(Name = "ArticleName", ResourceType = typeof(DisplayNames), Prompt = "TypeName")]
        public string SearchSubstring { get; set; }


        [DataType("IsTopArticle")]
        public bool? IsTopArticle { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int PageNumber { get; set; }

    }
}