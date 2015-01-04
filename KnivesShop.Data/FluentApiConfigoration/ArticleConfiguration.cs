using KnivesShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure.Annotations;

namespace KnivesShop.Data.FluentApiConfigoration
{
    public class ArticleConfiguration : EntityTypeConfiguration<Article>
    {
        public ArticleConfiguration()
        {
            HasKey(article => article.Id);

            HasRequired(article => article.Category)
                .WithMany(categegory => categegory.Articles)
                .HasForeignKey(article => article.CateoryId);

            Property(article => article.NameBg).HasMaxLength(200).IsRequired();
            Property(article => article.NameEn).HasMaxLength(200).IsRequired();
            Property(article => article.DescriptionBg).IsRequired();
            Property(article => article.DescriptionEn).IsRequired();
            Property(article => article.Image).IsRequired();
            Property(article => article.Price).IsRequired();
        }
    }
}
