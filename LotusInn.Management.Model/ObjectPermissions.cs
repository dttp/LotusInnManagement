using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusInn.Management.Model
{
    public class ObjectPermissions
    {
        public string ObjectType { get; set; }
        public string ObjectId { get; set; }
        public List<RoleObjectPermission> RolesPermissions { get; set; }
        public List<UserObjectPermission> UsersPermissions { get; set; }
    }
}
