namespace LotusInn.Management.Model
{
    public enum PermissionObject
    {
        House,
        User,
        Role,
    }    

    public class RolePermission
    {
        public string RoleId { get; set; }
        public PermissionObject Object { get; set; }
        public string Permission { get; set; }
    }
}
