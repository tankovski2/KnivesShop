using KnivesShop.Models;
using KnivesShop.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace KnivesShop.Web.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public static Expression<Func<Category,CategoryViewModel>> FromCategory(string culture)
        {
                return category => new CategoryViewModel
                {
                    Id = category.Id,
                    Name = culture == Culture.bg.ToString() ? category.NameBg : category.NameEn
                };
           
        }
    }
}