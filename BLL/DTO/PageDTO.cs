using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class PageDTO
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public int ResponseTime { get; set; }
        public int MaxTime { get; set; }
        public int MinTime { get; set; }
    }
}
