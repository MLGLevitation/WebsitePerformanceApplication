using BLL.Utility;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PageService
    {
        private readonly Func<Page> operation;
        readonly Page page;
        readonly string url;
        readonly PageLoadTimer pageLoadTimer;
        public PageService(string urlAddress)
        {
            operation = new Func<Page>(CreatePage);
            url = urlAddress;
            pageLoadTimer = new PageLoadTimer(urlAddress);
            page = new Page();
        }
        public Page CreatePage()
        {
            int time = 0;
            Task task = pageLoadTimer.GetResponseTimeAsync().
                ContinueWith(taskWithResponse =>
                {
                    time = taskWithResponse.Result;
                });

            task.Wait();
            page.URL = url;
            page.ResponseTime = time;
            return page;
        }

        public async Task<Page> CreatePageAsync()
        {
            return await Task<Page>.Factory.StartNew(CreatePage);
        }

        public IAsyncResult BeginCreatePage(AsyncCallback callback)
        {
            return operation.BeginInvoke(callback, null);
        }

        public Page EndCreatePage(IAsyncResult asyncResult)
        {
            return operation.EndInvoke(asyncResult);
        }
    }
}
