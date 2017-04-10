using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusInn.Management.Model
{
    public class Equipment
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string HouseId { get; set; }
        public string RoomId { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public string Price { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
