using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Test> Tests { get; }
        IRepository<Page> Pages { get; }
        IRepository<Host> Hosts { get; }
        void Save();
    }
}
