using KnivesShop.Web.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnivesShop.Web.Models
{
    public class SearchArticlesViewModel
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

        [HiddenInput(DisplayValue = false)]
        public int PageNumber { get; set; }
    }
}