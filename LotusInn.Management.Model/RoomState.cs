using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusInn.Management.Model
{
    public class RoomState
    {
        public string RoomId { get; set; }
        public DateTime Date { get; set; }
        public string Availability { get; set; }
        public string OrderId { get; set; }
    }

    public class RoomStateRange
    {
        public string RoomId { get; set; }
        public string RoomNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float PercentItem { get; set; }
        public List<RoomState> States { get; set; }
    }    
}
