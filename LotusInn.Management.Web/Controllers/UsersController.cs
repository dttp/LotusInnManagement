using System.Web.Mvc;
using LotusInn.Management.Web.Filter;

namespace LotusInn.Management.Web.Controllers
{
    public class UsersController : Controller
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
        [Authorization]
        public ActionResult MyProfile()
        {
            return View();
        }

        [Authorization]
        public ActionResult Detail()
        {
            return View();
        }

        public ActionResult Reset()
        {
            return View();
        }
    }
}