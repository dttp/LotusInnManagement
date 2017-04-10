using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LotusInn.Management.Model;

namespace LotusInn.Management.Services
{
    public class DashboardService
    {
        public static Dashboard GetDashboard(string houseId, DateTime startDate, DateTime endDate)
        {
            var dashboard = new Dashboard
            {
                Houses = HouseService.GetHouses(houseId).Select(h => new HouseOverviewModel(h)).ToList()
            };

            foreach (var h in dashboard.Houses)
            {
                h.Rooms = RoomService.GetRoomsWithStatuses(h.Id);
                var notAvailableRoomCount = h.Rooms.Count(r => r.Status != "Available");
                if (h.Rooms.Count > 0)
                    h.RoomCoverRating = notAvailableRoomCount*100/h.Rooms.Count;
                h.TotalGuest = CustomerService.CountByHouse(h.Id);
                h.CustomersCheckoutList = CustomerService.GetCustomerCheckoutByDate(h.Id, DateTime.Now);

                h.RoomsState = new List<RoomStateRange>();
                var orders = OrderService.GetOrdersInRange(h.Id, startDate, endDate);

                var days = endDate.Subtract(startDate).Days;

                foreach (var room in h.Rooms)
                {
                    var item = new RoomStateRange()
                    {
                        StartDate = startDate,
                        EndDate = endDate,
                        RoomId = room.Id,
                        RoomNumber = room.RoomNumber,
                        States = new List<RoomState>(),
                        PercentItem = 100.0f / days
                    };
                    for (var i = 0; i < days; i++)
                    {
                        item.States.Add(new RoomState
                        {
                            Availability = "Available",
                            Date = startDate.AddDays(i),
                            RoomId = item.RoomId
                        });
                    }
                    h.RoomsState.Add(item);
                }

                foreach (var order in orders)
                {
                    var dstart = DateTime.Compare(order.CheckinDate, startDate) < 0 ? startDate : order.CheckinDate;
                    var dend = DateTime.Compare(order.CheckOutDate, endDate) < 0 ? order.CheckOutDate : endDate;
                    days = dend.Subtract(dstart).Days;
                    foreach (var room in order.Rooms)
                    {
                        var item = h.RoomsState.Find(r => r.RoomId.Equals(room.Id));
                        for (var i = 0; i < days; i++)
                        {
                            var date = dstart.AddDays(i);
                            var roomStateItem = item.States.Find(s => s.Date.Subtract(date).Days == 0);
                            roomStateItem.Availability = order.Status == "Active" ? "Busy" : "Reserved";
                            roomStateItem.OrderId = order.Id;
                        }
                    }
                }
            }

            return dashboard;
        }
    }
}
