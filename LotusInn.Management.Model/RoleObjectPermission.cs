﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusInn.Management.Model
{
    public class RoleObjectPermission
    {
        public string Id { get; set; }
        public Role Role { get; set; }
        public string Object { get; set; }
        public PermissionEnum Permission { get; set; }
    }
}
