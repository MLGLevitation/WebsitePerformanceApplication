using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Test
    {
        public int TestId { get; set; }
        public Host Host { get; set; }
        public List<Page> Pages { get; set; }
        public DateTime TestDate { get; set; }
    }
}
