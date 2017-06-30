using LotusInn.Management.Model;
using System.Collections.Generic;
using System.Data;
using LotusInn.Management.Core;
using LotusInn.Management.Data;

namespace LotusInn.Management.Services
{
    public class RoleService        
    {        
        private RoleDataAdapter _adapter = new RoleDataAdapter();
        private RoleObjectPermissionDataAdapter _roleObjPermDataAdapter = new RoleObjectPermissionDataAdapter();
        public List<Role> GetRoles()
        {
            return _adapter.GetAll();
        }

        public Role GetById( string id)
        {
            return _adapter.GetById(id);
        }

        public Role Insert(Role role)
        {
            return _adapter.Insert(role);
        }

        public void Update(Role role)
        {
            _adapter.Update(role);
        }

        public void Delete(string id)
        {
            _adapter.Delete(id);
        }

        public List<RoleObjectPermission> GetPermissions(string roleId)
        {
            return _roleObjPermDataAdapter.GetByRoleId(roleId);
        }

        public void SetPermissions(List<RoleObjectPermission> permissionsList)
        {
            if (permissionsList.Count == 0) return;
            var roleId = permissionsList[0].Role.Id;

            var currentList = _roleObjPermDataAdapter.GetByRoleId(roleId);
            foreach (var item in permissionsList)
            {
                var curItem = currentList.Find(c => c.ObjectType.Equals(item.ObjectType));
                if (curItem != null && curItem.Permission != item.Permission)
                {
                    curItem.Permission = item.Permission;
                    _roleObjPermDataAdapter.Update(curItem);
                }
                else if (curItem == null)
                {
                    _roleObjPermDataAdapter.Insert(item);
                }
            }
        }
    }
}
