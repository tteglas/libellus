using Libellus.BusinessCore.Processors.Interface;
using System.Web.Mvc;

namespace Libellus.Controllers
{
    public class HomeController : Controller
    {
        private IFacultyProcessor _processor;
        public HomeController(IFacultyProcessor processor)
        {
            _processor = processor;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}