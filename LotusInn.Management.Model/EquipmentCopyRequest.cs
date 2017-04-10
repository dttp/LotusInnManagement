using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusInn.Management.Model
{
    public class EquipmentCopyRequest
    {
        public Room FromRoom { get; set; }
        public List<Room> Target { get; set; } 
    }
}
