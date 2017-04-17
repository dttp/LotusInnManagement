using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusInn.Management.Model
{
    public class MoneySourcePermission
    {
        public User User { get; set; }
        public string MoneySourceId { get; set; }
        public Dictionary<MoneySourcePermissionName, bool> Permissions { get; set; }
    }

    public enum MoneySourcePermissionName
    {
        Read,
        Edit
    }
}
