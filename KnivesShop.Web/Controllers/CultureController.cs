using KnivesShop.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnivesShop.Web.Controllers
{
    public class CultureController : Controller
    {
        public ActionResult ChangeCulture(Culture lang, string returnUrl)
        {
            if (returnUrl.StartsWith("/Administration") || returnUrl.Contains("Account/Login"))
            {
                return Redirect(returnUrl);
            }
            if (returnUrl.Length >= 3)
            {
                returnUrl = returnUrl.Substring(3);
            }
            return Redirect("/" + lang.ToString() + returnUrl);
        }
	}
}