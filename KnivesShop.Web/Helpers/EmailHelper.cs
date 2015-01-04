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

        private const string MERCHANT_MAIL_KEY = "MerchantMail";
        private const string MERCHANT_RECEIVING_MAIL_KEY = "MerchantReceivingMail";
		
        private const string CLIENT_MAIL_TEMPLATE_VIEW = "ClientMail";
        private const string MERCHANT_MAIL_TEMPLATE_VIEW = "MerchantMail";

        #endregion

        public static void MakeOrder(OrderArticleViewModel orderModel, IEnumerable<ArticleMailViewModel> orderedItems)
        {
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