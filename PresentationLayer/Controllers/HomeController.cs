using AutoMapper;
using BLL.DTO;
using BLL.Services.Interfaces;
using BLL.Utility;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITestService testService;
        private static IEnumerable<PageViewModel> pages;
        public HomeController(ITestService service)
        {
            testService = service;
        }

        
        public async Task<ActionResult> Index(SiteMapModel siteMap)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await testService.CreateTestAsync(siteMap.Host);
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                    return View(siteMap);
                }

                return RedirectToAction("TestResult");
            }
            return View(siteMap);
        }

        public ActionResult TestResult(int? id)
        {
            try
            {
                MapperConfiguration config = new MapperConfiguration(map => map.CreateMap<PageDTO, PageViewModel>());
                var mapper = config.CreateMapper();
                pages = mapper.Map<IEnumerable<PageDTO>, IEnumerable<PageViewModel>>(testService.GetTestPages(id));

                TestDTO test = testService.GetTest(id);
                ViewBag.HostURL = test.HostURL;
                ViewBag.Date = test.TestDate;

                return View(pages);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

    }
}