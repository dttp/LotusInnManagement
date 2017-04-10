using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using LotusInn.Management.Model;
using LotusInn.Management.Services;

namespace LotusInn.Management.Web.ApiControllers
{
    public class DashboardController : BaseApiController
    {
        [AcceptVerbs("GET")]
        public Dashboard GetDashboard(DateTime startDate, DateTime endDate)
        {
            return Execute((session) => DashboardService.GetDashboard(session.User.House?.Id, startDate, endDate));
        }

        [AcceptVerbs("GET")]
        public List<Notification> GetTodayNotifications()
        {
            return Execute(session => NotificationService.GetTodayNotifications());
        } 
    }
}