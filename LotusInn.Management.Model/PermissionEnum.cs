using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusInn.Management.Model
{
    [Flags]
    public enum PermissionEnum
    {
        None = 0,
        Read = 1,
        Create = 2,
        Edit = 4,
        Delete = 8
    }

    public enum PermissionObject
    {
        House,
        User,
        Role,
        Warehouse,
        MoneySource,
        Order,
    }
}
