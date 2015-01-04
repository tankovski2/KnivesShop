using KnivesShop.Web.Models;
using KnivesShop.Web.Resources;
using Postal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace KnivesShop.Web.Helpers
{
    public static class EmailHelper
    {
        #region Constants

        private const string AppEmail = "stankovski@yabolka.com";
        private const string AppEmailPass = "system1986";
        private const string AppEmailHostName = "mail.yabolka.com";
        private const int AppEmailHostPort = 26;

        private const string MERCHANT_MAIL_KEY = "MerchantMail";
        private const string MERCHANT_RECEIVING_MAIL_KEY = "MerchantReceivingMail";
        private const string CLIENT_MAIL_TEMPLATE_VIEW = "ClientMail";
        private const string MERCHANT_MAIL_TEMPLATE_VIEW = "MerchantMail";

        #endregion

        public static void MakeOrder(OrderArticleViewModel orderModel, IEnumerable<ArticleMailViewModel> orderedItems)
        {
            //MailAddress fromMail = new MailAddress(AppEmail);
            //MailAddress toMail = new MailAddress("ov4arq@abv.bg");
            //MailMessage message = new MailMessage(fromMail, toMail);

            //message.Subject = "Order from "+orderModel.ClientName;
            //string orderBody = string.Format("Article name: {1}{0}Client name: {2}{0}Client e-mail: {3}{0}" +
            //"Client phone: {4}{0}Client address: {5}{0}Additional info: {6}{0}", Environment.NewLine, orderModel.ArticlesNames,
            //orderModel.ClientName, orderModel.ClientEmail, orderModel.ClientPhone, orderModel.ClientAddress, orderModel.AdditionalInfo);
            //message.Body = orderBody;
            //message.IsBodyHtml = false;

            //message.SubjectEncoding = System.Text.Encoding.UTF8;
            //message.BodyEncoding = System.Text.Encoding.UTF8;

            //SmtpClient smtp = new SmtpClient(AppEmailHostName, AppEmailHostPort);
            //smtp.Credentials = new NetworkCredential(AppEmail, AppEmailPass);

            //smtp.Send(message);

            SendMailToMerchant(orderModel);
            SendMailToClient(orderModel, orderedItems);
        }

        private static void SendMailToClient(OrderArticleViewModel orderModel, IEnumerable<ArticleMailViewModel> orderedItems)
        {
            string merchantMail = ConfigurationManager.AppSettings[MERCHANT_MAIL_KEY];

            var email = new KnivesShopEmail
            {
                To = orderModel.ClientEmail,
                From = merchantMail,
                Subject = Messages.OrderMailSubject,
                OrderedItems = orderedItems,
                OrderModel = orderModel,
                ViewName = CLIENT_MAIL_TEMPLATE_VIEW
            };

            email.Send();
        }

        private static void SendMailToMerchant(OrderArticleViewModel orderModel)
        {
            string merchantMail = ConfigurationManager.AppSettings[MERCHANT_MAIL_KEY];
            string merchantReceivingMail = ConfigurationManager.AppSettings[MERCHANT_RECEIVING_MAIL_KEY];

            var email = new KnivesShopEmail
            {
                To = merchantReceivingMail,
                From = merchantMail,
                Subject = Messages.OrderMailSubject,
                OrderModel = orderModel,
                ViewName = MERCHANT_MAIL_TEMPLATE_VIEW
            };

            email.Send();
        }
    }
}