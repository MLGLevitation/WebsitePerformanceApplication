using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PresentationLayer.Models
{
    public class TestViewModel
    {
        public int TestId { get; set; }
        public string HostURL { get; set; }
        public DateTime TestDate { get; set; }
    }
}