using System.Web.Mvc;
using LotusInn.Management.Web.Filter;

namespace LotusInn.Management.Web.Controllers
{
    public class OrdersController : Controller
    {
        [Authorization]
        public ActionResult Index()
        {
            return View();
        }

        [Authorization]
        public ActionResult Checkin()
        {
            return View();
        }

        [Authorization]
        public ActionResult Edit()
        {
            return View("Checkin");
        }

        [Authorization]
        public ActionResult Checkout()
        {
            return View();
        }

        [Authorization]
        public ActionResult Receipt()
        {
            return View();
        }
    }
}