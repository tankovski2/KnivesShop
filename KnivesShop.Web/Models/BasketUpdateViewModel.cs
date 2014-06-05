using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnivesShop.Web.Models
{
    public class BasketUpdateViewModel
    {
        private List<int> _ids;
        private List<int> _quantities;
        public BasketUpdateViewModel()
        {
            _ids = new List<int>();
            _quantities = new List<int>();
        }

        public List<int> Id
        { get { return _ids; } set { _ids = value; } }

        public List<int> Quantity
        { get { return _quantities; } set { _quantities = value; } }
    }
}