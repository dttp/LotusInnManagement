using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LotusInn.Management.Core;
using LotusInn.Management.Model;

namespace LotusInn.Management.Services
{
    public class NotificationService
    {
        private const string SP_NOTIFICATION_GET_TODAY_NOTIFICATIONS = "NotificationGetToday";
        private const string SP_NOTIFICATION_DELETE_BY_ORDER_ID = "NotificationDeleteByOrderId";
        private const string SP_NOTIFICATION_INSERT = "NotificationInsert";


        public static void Insert(Notification notification)
        {
            var param = new[]
            {
                new SqlParameter("@id", notification.Id),
                new SqlParameter("@orderId", notification.OrderId),
                new SqlParameter("@date", notification.Date),
                new SqlParameter("@daysToPaymentDate", notification.DaysToPaymentDate)
            };

            SqlHelper.ExecuteNonQuery(SP_NOTIFICATION_INSERT, param);
        }

        public static void DeleteByOrderId(string orderId)
        {
            var param = new[]
            {
                new SqlParameter("@orderId", orderId),
            };
            SqlHelper.ExecuteNonQuery(SP_NOTIFICATION_DELETE_BY_ORDER_ID, param);
        }

        public static List<Notification> GetTodayNotifications()
        {
            return SqlHelper.ExecuteReader(SP_NOTIFICATION_GET_TODAY_NOTIFICATIONS, null, reader =>
            {
                var list = new List<Notification>();
                while (reader.Read())
                {
                    var item = new Notification
                    {
                        Id = reader["Id"].ToString(),
                        Date = Convert.ToDateTime(reader["Date"]),
                        OrderId = reader["OrderId"].ToString(),
                        DaysToPaymentDate = Convert.ToInt32(reader["DaysToPaymentDate"])
                    };
                    var order = OrderService.GetById(item.OrderId);
                    item.Customers = order.Customers;
                    item.HouseId = order.HouseId;
                    item.Rooms = order.Rooms;
                    list.Add(item);
                }
                return list;
            });
        }

        public static void Insert(BookingOrder order)
        {
            var list = new List<Notification>();
            var curDate = order.CheckinDate;
            while (true)
            {
                var date = curDate.AddMonths(order.PaymentCycle.Value);
                if (DateTime.Compare(date, order.CheckOutDate) > 0)
                    break;

                for (int i = 5; i > 0; i--)
                {
                    var notification = new Notification
                    {
                        Id = "n-" + IdHelper.Generate(),
                        OrderId = order.Id,
                        Customers = order.Customers,
                        Rooms = order.Rooms,
                        Date = date.Subtract(TimeSpan.FromDays(i)),
                        DaysToPaymentDate = i
                    };
                    list.Add(notification);
                }
                curDate = date;
            }
            foreach (var notification in list)
            {
                Insert(notification);
            }
        }
    }
}
