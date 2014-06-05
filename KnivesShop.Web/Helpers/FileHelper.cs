using KnivesShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace KnivesShop.Web.Helpers
{
    public static class FileHelper
    {
        public static Article SaveImage(Article article, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0 && Regex.IsMatch(file.ContentType, "image/\\S+"))
            {
                string absoluteFolderPath = PathHelper.ArticlesImagesAbsolutePath;
                string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string fileName = timeStamp + "_" + file.FileName;

                if (!Directory.Exists(absoluteFolderPath))
                {
                    Directory.CreateDirectory(absoluteFolderPath);
                }
                string documentAbsoluteFilePath = absoluteFolderPath + fileName;

                file.SaveAs(documentAbsoluteFilePath);
                article.Image = fileName;
            }
            else
            {
                throw new ArgumentException("The file is not valid image file");
            }

            return article;
        }

        public static Article UpdateImage(Article article, string oldImageName, HttpPostedFileBase file)
        {
            article = SaveImage(article, file);
            DeleteImage(oldImageName);
            return article;
        }

        public static void DeleteImage(string oldImageName)
        {
            string absoluteFolderPath = PathHelper.ArticlesImagesAbsolutePath;
            if (Directory.Exists(absoluteFolderPath))
            {
                string imagePath = absoluteFolderPath + oldImageName;
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
                else
                {
                    throw new ArgumentException("Articles old image can not be found");
                }
            }
            else
            {
                throw new ArgumentException("The path of articles old image  is not correct");
            }
        }

        public static void DeleteLogo(string logoName)
        {
            string absoluteFilePath = PathHelper.LogoImagesAbsolutePath + logoName;
            if (File.Exists(absoluteFilePath))
            {
                File.Delete(absoluteFilePath);
            }
        }

        public static void SaveLogo(HttpPostedFileBase file)
        {
            if (Regex.IsMatch(file.ContentType, "image/\\S+"))
            {
                string absoluteFilePath = PathHelper.LogoImagesAbsolutePath + file.FileName;
                if (File.Exists(absoluteFilePath))
                {
                    File.Delete(absoluteFilePath);
                }
                else
                {
                    file.SaveAs(absoluteFilePath);
                }
            }
            else
            {
                throw new ArgumentException("The file is not valid image file");
            }
        }
    }
}