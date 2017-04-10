using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LotusInn.Management.Web.Filter;

namespace LotusInn.Management.Web.Controllers
{
    public class EquipmentController : Controller
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