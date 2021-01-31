using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PresentationLayer.Models
{
    public class SiteMapModel
    {
        [Required(ErrorMessage = "Enter the URL")]
        [RegularExpression(@"^((https?|ftp)\:\/\/)?([a-z0-9]{1})((\.[a-z0-9-])|([a-z0-9-]))*\.([a-z]{2,6})(\/?)$",
            ErrorMessage = "Incorrect URL")]
        public string Host { get; set; }
    }
}