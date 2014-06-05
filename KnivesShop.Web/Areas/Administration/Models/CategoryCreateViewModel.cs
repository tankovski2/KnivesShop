﻿using KnivesShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KnivesShop.Web.Areas.Administration.Models
{
    public class CategoryCreateViewModel
    {
        [Required]
        [Display( Prompt="Enter bulgarian name")]
        public string NameBg { get; set; }

        [Required]
        [Display(Prompt = "Enter english name")]
        public string NameEn { get; set; }

        public bool IsTopCategory { get; set; }

        public Category TransformToCategory()
        {
            return new Category
            {
                NameBg = this.NameBg,
                NameEn = this.NameEn,
                IsTopCategory = this.IsTopCategory
            };
        }
    }
}