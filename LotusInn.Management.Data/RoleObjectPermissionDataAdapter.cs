using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LotusInn.Management.Core;
using LotusInn.Management.Model;

namespace LotusInn.Management.Data
{
    public class RoleObjectPermissionDataAdapter: IObjectDataAdapter<RoleObjectPermission>
    {
        private const string SP_ROLE_OBJECT_PERMISSION_INSERT = "RoleObjectPermissionInsert";
        private const string SP_ROLE_OBJECT_PERMISSION_UPDATE = "RoleObjectPermissionUpdate";
        private const string SP_ROLE_OBJECT_PERMISSION_DELETE = "RoleObjectPermissionDelete";
        private const string SP_ROLE_OBJECT_PERMISSION_GETBYID = "RoleObjectPermissionGetById";
        private const string SP_ROLE_OBJECT_PERMISSION_GETBYROLEID = "RoleObjectPermissionGetByRoleId";
        public RoleObjectPermission Insert(RoleObjectPermission @object)
        {
            @object.Id = IdHelper.Generate();
            var param = new[]
            {
                new SqlParameter("@id", @object.Id),
                new SqlParameter("@roleId", @object.Role.Id),
                new SqlParameter("@object", @object.Object),
                new SqlParameter("@permission", (int) @object.Permission),
            };
            SqlHelper.ExecuteNonQuery(SP_ROLE_OBJECT_PERMISSION_INSERT, param);    
            return @object;
        }

        public void Update(RoleObjectPermission @object)
        {
            var param = new[]
            {
                new SqlParameter("@id", @object.Id),
                new SqlParameter("@roleId", @object.Role.Id),
                new SqlParameter("@object", @object.Object),
                new SqlParameter("@permission", (int) @object.Permission),
            };
            SqlHelper.ExecuteNonQuery(SP_ROLE_OBJECT_PERMISSION_UPDATE, param);
        }

        public void Delete(string id)
        {
            var param = new[]
            {
                new SqlParameter("@id", id),
            };
            SqlHelper.ExecuteNonQuery(SP_ROLE_OBJECT_PERMISSION_DELETE, param);

        }

        public RoleObjectPermission GetById(string id)
        {
            var param = new[]
            {
                new SqlParameter("@id", id),
            };
            return SqlHelper.ExecuteReader(SP_ROLE_OBJECT_PERMISSION_GETBYID, param, Read).FirstOrDefault();
        }

        public List<RoleObjectPermission> GetByRoleId(string roleId)
        {
            var param = new[]
            {
                new SqlParameter("@roleId", roleId),
            };
            return SqlHelper.ExecuteReader(SP_ROLE_OBJECT_PERMISSION_GETBYROLEID, param, Read);
        } 

        private List<RoleObjectPermission> Read(IDataReader reader)
        {
            var list = new List<RoleObjectPermission>();
            var roleDA = new RoleDataAdapter();
            while (reader.Read())
            {
                var item = new RoleObjectPermission
                {
                    Id = reader["Id"].ToString(),
                    Role = new Role
                    {
                        Id = reader["RoleId"].ToString()
                    },
                    Object = reader["Object"].ToString(),
                    Permission = (PermissionEnum) Convert.ToInt32(reader["Permission"])
                };
                item.Role = roleDA.GetById(item.Role.Id);
                list.Add(item);
            }
            return list;
        } 
    }
}
