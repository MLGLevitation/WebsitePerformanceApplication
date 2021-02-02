using DAL.AppContext;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly SiteMapContext db;

        private TestRepository testRepository;
        private PageRepository pageRepository;
        private HostRepository hostRepository;

        
        public EFUnitOfWork(string connection)
        {
            db = new SiteMapContext(connection);
        }

        public IRepository<Page> Pages
        {
            get
            {
                if (pageRepository == null)
                {
                    pageRepository = new PageRepository(db);
                }
                return pageRepository;
            }
        }

        public IRepository<Test> Tests
        {
            get
            {
                if (testRepository == null)
                {
                    testRepository = new TestRepository(db);
                }
                return testRepository;
            }
        }

        public IRepository<Host> Hosts
        {
            get
            {
                if (hostRepository == null)
                {
                    hostRepository = new HostRepository(db);
                }
                return hostRepository;
            }
        }

        private bool disposed = false;
        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
