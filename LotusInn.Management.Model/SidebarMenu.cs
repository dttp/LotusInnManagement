using System.Collections.Generic;

namespace LotusInn.Management.Model
{
    public class SidebarMenu
    {
        public List<MenuItem> Items { get; set; }
    }

    public class MenuItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PermissionObject { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public bool Selected { get; set; }
        public List<MenuItem> Items { get; set; }
    }

}
