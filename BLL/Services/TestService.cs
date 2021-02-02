using BLL.DTO;
using BLL.Services.Interfaces;
using BLL.Utility;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TestService : ITestService
    {
        IUnitOfWork Database { get; set; }
        private readonly List<Page> pages;

        public TestService(IUnitOfWork db)
        {
            Database = db;
            pages = new List<Page>();
        }

        private int LastTestIndex
        {
            get
            {
                return GetAllTests().Select(t => t.TestId).LastOrDefault();
            }
        }

        public async Task CreateTestAsync(string host)
        {
            List<string> urlAddresses = new UrlParser(host).GetURLAddresses();

            if (urlAddresses == null)
                throw new Exception("Provide a valid URL");


            List<Task> tasks = new List<Task>();

            foreach (var url in urlAddresses)
            {
                tasks.Add(CreatePageAsync(url));
            }
            await Task.WhenAll(tasks);


            Test test = new Test()
            {
                Host = Database.Hosts.GetAll()
                .Where(h => h.HostURL == host).FirstOrDefault()
                ?? new Host() { HostURL = host },
                TestDate = DateTime.Now,
                Pages = pages
            };

            Database.Tests.Create(test);
            Database.Save();

        }

        async Task CreatePageAsync(string url)
        {
            Page page = await new PageService(url).CreatePageAsync();
            pages.Add(page);
        }

        public IEnumerable<TestDTO> GetAllTests()
        {
            var testsDTO = from test in Database.Tests.GetAll().AsQueryable()
                           select new TestDTO()
                           {
                               TestId = test.TestId,
                               HostURL = test.Host.HostURL,
                               TestDate = test.TestDate
                           };

            return testsDTO;
        }

        public TestDTO GetTest(int? id)
        {
            if (id == null)
            {
                throw new Exception("No test performed yet");
            }
            id = id ?? LastTestIndex;

            Test test = Database.Tests.Get(id.Value);

            TestDTO testDTO = new TestDTO()
            {
                TestId = id.Value,
                HostURL = test.Host.HostURL,
                TestDate = test.TestDate
            };

            return testDTO;
        }

        public IEnumerable<PageDTO> GetTestPages(int? id)
        {

            id = id ?? LastTestIndex;
            Test test = Database.Tests.Get(id.Value);
            if (test == null)
                throw new Exception("Test not found");

            var pages = test.Pages.OrderByDescending(p => p.ResponseTime).AsQueryable().ToList();
            if (pages == null | pages.Count() == 0)
                throw new Exception("Pages not found");

            var allPages = Database.Pages.GetAll().OrderByDescending(p => p.ResponseTime).AsQueryable().ToList();
            var pagesDTO = from page in pages.AsQueryable()
                           join maxpage in allPages
                           .Distinct(new PageComparer())
                           on page.URL equals maxpage.URL
                           join minpage in allPages.OrderBy(p => p.ResponseTime)
                           .Distinct(new PageComparer())
                           on page.URL equals minpage.URL
                           select new PageDTO()
                           {
                               Id = page.Id,
                               URL = page.URL,
                               ResponseTime = page.ResponseTime,
                               MaxTime = maxpage.ResponseTime,
                               MinTime = minpage.ResponseTime
                           };

            return pagesDTO;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
