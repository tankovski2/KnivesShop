using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnivesShop.Web.Models
{
    public class ArticleBasketViewModel
    {
        private int _quantity;
        public ArticleBasketViewModel()
        {
            _quantity = 1;
        }

        public int Id { get; set; }

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        public string Image { get; set; }

        public string NameEn { get; set; }

        public decimal Price { get; set; }

        public string NameBg { get; set; }
    }
}
