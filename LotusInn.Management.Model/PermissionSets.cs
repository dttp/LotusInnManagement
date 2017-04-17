using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusInn.Management.Model
{
    public class PermissionSets
    {
        public string Id { get; set; }

        public string Name { get; set; }

    }

    public class ObjectPermission
    {
        public string PermissionSetId { get; set; }
        public string ObjectName { get; set; }

        public Dictionary<PermissionName, bool> Permissions { get; set; } 
    }

    public enum PermissionName
    {
        Read,
        Create,
        Edit,
        Delete
    }
}
