using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusInn.Management.Model
{
    public class UserPermissions
    {
        public bool InheriteFromRole { get; set; }
        public List<UserObjectPermission> Permissions { get; set; }
    }
}
