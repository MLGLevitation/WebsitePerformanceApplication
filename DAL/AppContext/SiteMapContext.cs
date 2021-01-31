using System.Data.Entity;
using DAL.Entities;

namespace DAL.AppContext
{
    public class SiteMapContext : DbContext
    {
        public SiteMapContext(string conectionString)
            : base(conectionString)
        {
        }

        public DbSet<Page> Pages { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Host> Hosts { get; set; }
    }
}
