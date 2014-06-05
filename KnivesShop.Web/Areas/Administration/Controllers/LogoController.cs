﻿using KnivesShop.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using KnivesShop.Web.Infrastructure.Alerts;
using System.Configuration;

namespace KnivesShop.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class LogoController : Controller
    {
        public ActionResult ChooseLogo()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public ActionResult Change(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                Configuration myConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                string name = myConfiguration.AppSettings.Settings["LogoImage"].Value;

                FileHelper.SaveLogo(file);
                FileHelper.DeleteLogo(name);

                myConfiguration.AppSettings.Settings["LogoImage"].Value = file.FileName;
                myConfiguration.Save();

                return RedirectToAction("ChooseLogo").WithSuccess("Logo successfully changed");
            }
            return RedirectToAction("ChooseLogo").WithError("No file choosen");
        }
    }
}