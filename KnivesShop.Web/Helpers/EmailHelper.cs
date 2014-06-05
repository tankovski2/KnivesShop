using KnivesShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace KnivesShop.Web.Helpers
{
    public static class EmailHelper
    {
        private const string AppEmail = "stankovski@yabolka.com";
        private const string AppEmailPass = "system1986";
        private const string AppEmailHostName = "mail.yabolka.com";
        private const int AppEmailHostPort = 26;

        public static void MakeOrder(OrderArticleViewModel orderModel)
        {
            MailAddress fromMail = new MailAddress(AppEmail);
            MailAddress toMail = new MailAddress("ov4arq@abv.bg");
            MailMessage message = new MailMessage(fromMail, toMail);

            message.Subject = "Order for "+orderModel.ArticlesNames;
            string orderBody = string.Format("Article name: {1}{0}Amount of articles: {2}{0}Client name: {3}{0}Client e-mail: {4}{0}" +
            "Client phone: {5}{0}Client address: {6}{0}Additional info: {7}{0}", Environment.NewLine, orderModel.ArticlesNames, orderModel.AmountOfArticles,
            orderModel.ClientName, orderModel.ClientEmail, orderModel.ClientPhone, orderModel.ClientAddress, orderModel.AdditionalInfo);
            message.Body = orderBody;
            message.IsBodyHtml = false;

            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.BodyEncoding = System.Text.Encoding.UTF8;

            SmtpClient smtp = new SmtpClient(AppEmailHostName, AppEmailHostPort);
            smtp.Credentials = new NetworkCredential(AppEmail, AppEmailPass);

            smtp.Send(message);
        }
    }
}