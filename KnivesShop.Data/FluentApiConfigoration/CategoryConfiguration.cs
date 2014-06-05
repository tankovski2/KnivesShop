using KnivesShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnivesShop.Data.FluentApiConfigoration
{
    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            HasKey(category => category.Id);

            Property(category => category.NameBg).HasMaxLength(50).IsRequired();
            Property(category => category.NameEn).HasMaxLength(50).IsRequired();
        }
    }
}
