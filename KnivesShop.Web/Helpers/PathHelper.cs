using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace KnivesShop.Web.Helpers
{
    public static class PathHelper
    {
        public static string LogoImage
        {
            get
            {
                return "/" + LogoImagesRelativeUrl + ConfigurationManager.AppSettings["LogoImage"];
            }
        }

        public static string LogoImagesAbsolutePath
        {
            get
            {
                string relativeFolderPath = ConfigurationManager.AppSettings["LogoImagesPath"];
                string absoluteFolderPath = AppDomain.CurrentDomain.BaseDirectory + relativeFolderPath;

                return absoluteFolderPath;
            }
        }

        public static string AppDataAbsolutePath
        {
            get
            {
                string relativeFolderPath = ConfigurationManager.AppSettings["DataDir"];
                string absoluteFolderPath = AppDomain.CurrentDomain.BaseDirectory + relativeFolderPath;

                return absoluteFolderPath;
            }
        }

        public static string LogoImagesRelativeUrl
        {
            get
            {
                string relativeUrl = ConfigurationManager.AppSettings["LogoImagesPath"];

                return relativeUrl;
            }
        }

        public static string LogoCssFileAbsolutePath
        {
            get
            {
                string relativeFolderPath = ConfigurationManager.AppSettings["LogoCssFilePath"];
                string absoluteFolderPath = AppDomain.CurrentDomain.BaseDirectory + relativeFolderPath;

                return absoluteFolderPath;
            }
        }

        public static string ArticlesImagesAbsolutePath
        {
            get
            {
                string relativeFolderPath = ConfigurationManager.AppSettings["ArticlesImagesPath"];
                string absoluteFolderPath = AppDomain.CurrentDomain.BaseDirectory + relativeFolderPath;

                return absoluteFolderPath;
            }
        }

        public static string ArticlesImagesRelativeUrl
        {
            get
            {
                string relativeUrl = ConfigurationManager.AppSettings["ArticlesImagesPath"];

                return relativeUrl;
            }
        }
    }
}
