using System.Web.Mvc;
using LotusInn.Management.Web.Filter;

namespace LotusInn.Management.Web.Controllers
{
    public class DashboardController : Controller
    {
        [Authorization]
        public ActionResult Index()
        {
            return View();
        }
    }
}