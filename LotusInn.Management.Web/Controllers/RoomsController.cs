using System.Web.Mvc;
using LotusInn.Management.Web.Filter;

namespace LotusInn.Management.Web.Controllers
{
    public class RoomsController : Controller
    {
        [Authorization]
        public ActionResult Index()
        {
            return View();
        }

        [Authorization]
        public ActionResult AddEdit()
        {
            return View();
        }
    }
}