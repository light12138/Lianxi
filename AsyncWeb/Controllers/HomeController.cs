using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AsyncWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Task<string> t = DoSomething();
          
          


            return View();
        }

        public async Task<string> DoSomething()
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            return "ok";
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