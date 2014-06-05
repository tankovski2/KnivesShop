using KnivesShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace KnivesShop.Web.Areas.Administration.Models
{
    public class CategoryDetailViewModel
    {
        public CategoryDetailViewModel()
        {
            this.IsTopCategory = false;
        }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        public string NameBg { get; set; }

        [Required]
        public string NameEn { get; set; }

        public bool IsTopCategory { get; set; }

        //We keep the current page so we can redirect after editing
         [HiddenInput(DisplayValue = false)]
        public int Page { get; set; }

        public Category TransformToCategory()
        {
            return new Category
            {
                Id = this.Id,
                NameBg = this.NameBg,
                NameEn = this.NameEn,
                IsTopCategory = this.IsTopCategory
            };
        }

        public static CategoryDetailViewModel TransformToViewModel(Category category)
        {
            return new CategoryDetailViewModel
            {
                Id = category.Id,
                NameBg = category.NameBg,
                NameEn = category.NameEn,
                IsTopCategory = category.IsTopCategory
            };
        }

        public static Expression<Func<Category, CategoryDetailViewModel>> FromCategory
        {
            get
            {
                return category => new CategoryDetailViewModel
                {
                    Id = category.Id,
                    NameBg = category.NameBg,
                    NameEn = category.NameEn,
                    IsTopCategory = category.IsTopCategory
                };
            }
        }
    }
}