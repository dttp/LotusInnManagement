using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusInn.Management.Model
{
    public class WarehouseItem
    {
        public string Id { get; set; }
        public string WarehouseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public string Condition { get; set; }
    }
}
