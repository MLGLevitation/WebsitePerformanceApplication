using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface ITestService : IDisposable
    {
        Task CreateTestAsync(string host);
        IEnumerable<TestDTO> GetAllTests();
        TestDTO GetTest(int? id);
        IEnumerable<PageDTO> GetTestPages(int? id);
    }
}
