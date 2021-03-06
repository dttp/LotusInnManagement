﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusInn.Management.Model
{
    public class UserObjectPermission
    {
        public string Id { get; set; }
        public User User { get; set; }
        public string ObjectType { get; set; }
        public string ObjectId { get; set; }
        public PermissionEnum Permission { get; set; }
    }
}
