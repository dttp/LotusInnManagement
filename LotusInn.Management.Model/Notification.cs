using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusInn.Management.Model
{
    public class Notification
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string HouseId { get; set; }
        public int DaysToPaymentDate { get; set; }
        public DateTime Date { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Room> Rooms { get; set; }
    }
}
