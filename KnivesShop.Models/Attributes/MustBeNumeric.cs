using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnivesShop.Models.Attributes
{
    public class MustBeNumeric : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string valueAsString = value as string;
            int valueAsInt = 0;
            if (valueAsString == null)
            {
                return false;
            }

            if (int.TryParse(valueAsString, out valueAsInt))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

