using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Utility
{
    public class PageComparer : IEqualityComparer<Page>
    {

        public bool Equals(Page x, Page y)
        {
            if (x.URL == y.URL)
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(Page obj)
        {
            string page = "Page";
            return obj.URL.GetHashCode() * page.GetHashCode();
        }
    }
}
