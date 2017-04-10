using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusInn.Management.Model
{
    public class WarehouseActivity
    {
        public string Id { get; set; }
        public string WarehouseId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
    }
}
