using Splitio.Services.Client.Interfaces;
using System.Web.Mvc;

namespace PushSupportNetFramework472.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            var sdk = HttpContext.Application["sdk"] as ISplitClient;
            ViewBag.Treatment = sdk.GetTreatment("admin", "mauro_net");

            return View();
        }
    }
}
