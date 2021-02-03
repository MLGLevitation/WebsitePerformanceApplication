using DAL.AppContext;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class TestRepository : IRepository<Test>
    {
        private readonly SiteMapContext db;

        public TestRepository(SiteMapContext context)
        {
            db = context;
        }
        public void Create(Test item)
        {
            db.Tests.Add(item);
        }

        public void Delete(int id)
        {
            Test test = db.Tests.Find(id);
            if (test != null)
            {
                db.Entry(test).State = EntityState.Deleted;
            }

        }

        public IEnumerable<Test> Find(Func<Test, bool> predicate)
        {
            return db.Tests.Where(predicate).ToList();
        }

        public Test Get(int id)
        {
            return db.Tests.Include(h => h.Host).Where(t => t.TestId == id).FirstOrDefault();
        }

        public IEnumerable<Test> GetAll()
        {
            return db.Tests;
        }

        public void Update(Test item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
