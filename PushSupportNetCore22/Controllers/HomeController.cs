using Microsoft.AspNetCore.Mvc;
using PushSupportNetCore22.Models;
using Splitio.Services.Client.Interfaces;
using System.Diagnostics;

namespace PushSupportNetCore22.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISplitClient _sdk;

        public HomeController(MyAppData data)
        {
            _sdk = data.Sdk;
        }

        public IActionResult Index()
        {
            ViewBag.Treatment = _sdk.GetTreatment("admin", "mauro_net");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
