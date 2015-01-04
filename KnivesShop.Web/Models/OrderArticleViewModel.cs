using KnivesShop.Models.Attributes;
using KnivesShop.Web.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnivesShop.Web.Models
{
    public class OrderArticleViewModel
    {
        [HiddenInput(DisplayValue = false)]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(Errors))]
        [Display(Name = "ArticlesNames", ResourceType = typeof(DisplayNames))]
        public string ArticlesNames { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ArticleIds { get; set; }

        [Display(Name = "ClientName", Prompt = "EnterName", ResourceType = typeof(DisplayNames))]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(Errors))]
        public string ClientName { get; set; }

        [Display(Name = "ClientEmail", Prompt = "EnterEmail", ResourceType = typeof(DisplayNames))]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(Errors))]
        [RegularExpression(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                ErrorMessageResourceName = "NotValidMail", ErrorMessageResourceType = typeof(Errors))]
        public string ClientEmail { get; set; }

        [Display(Name = "ClientPhone",Prompt = "EnterPhone", ResourceType = typeof(DisplayNames))]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(Errors))]
        [RegularExpression(@"[0-9]{5,15}", ErrorMessageResourceName = "NotValidPhone", ErrorMessageResourceType = typeof(Errors))]
        //ToDo validation
        public string ClientPhone { get; set; }

        [Display(Name = "ClientAddress", Prompt = "EnterAdress", ResourceType = typeof(DisplayNames))]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(Errors))]
        [DataType(DataType.MultilineText)]
        public string ClientAddress { get; set; }

        [Display(Name = "AdditionalInfo", Prompt = "EnterAdditionalInfo", ResourceType = typeof(DisplayNames))]
        [DataType(DataType.MultilineText)]
        public string AdditionalInfo { get; set; }
    }
}