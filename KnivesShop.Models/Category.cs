using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnivesShop.Models
{
    public class Category
    {
        public Category()
        {
            this.Articles = new List<Article>();
            this.IsTopCategory = false;
        }

        public int Id { get; set; }

        public string NameEn { get; set; }

        public string NameBg { get; set; }

        public bool IsTopCategory { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
