using BLL.Services.Interfaces;
using System.Web.Mvc;

namespace PresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        ITestService testService;

        public HomeController()
        {
        }

        public HomeController(ITestService testService)
        {
            this.testService = testService;
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}