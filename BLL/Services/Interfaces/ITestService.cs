using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface ITestService : IDisposable
    {
        Task MakeTestAsync(string host);
        IEnumerable<TestDTO> GetAllTests();
        TestDTO GetTest(int? id);
        IEnumerable<PageDTO> GetTestPages(int? id);
    }
}
