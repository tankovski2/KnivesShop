using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnivesShop.Models
{
    public class Article
    {
        public Article()
        {
            this.IsSelectedForTopArticle = false;
        }

        public int Id { get; set; }

        public string NameBg { get; set; }

        public string NameEn { get; set; }

        public string DescriptionBg { get; set; }

        public string DescriptionEn { get; set; }

        public string Image { get; set; }

        public decimal Price { get; set; }

        public bool IsSelectedForTopArticle { get; set; }
       
        //[ForeignKey("CateoryId")]
        public virtual Category Category { get; set; }

        public int CateoryId { get; set; }
    }
}
