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
        private const string AppEmail = "***";
        private const string AppEmailPass = "***";
        private const string AppEmailHostName = "***";
        private const int AppEmailHostPort = 26;

        public static void MakeOrder(OrderArticleViewModel orderModel)
        {
            MailAddress fromMail = new MailAddress(AppEmail);
            MailAddress toMail = new MailAddress(orderModel.ClientEmail);
            MailMessage message = new MailMessage(fromMail, toMail);

            message.Subject = "Order from "+orderModel.ClientName;
            string orderBody = string.Format("Articles:{0}{1}{0} Client name: {2}{0}Client e-mail: {3}{0}" +
            "Client phone: {4}{0}Client address: {5}{0}Additional info: {6}{0}", Environment.NewLine, orderModel.ArticlesNames,
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