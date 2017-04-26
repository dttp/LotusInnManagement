using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using LotusInn.Management.Core;
using LotusInn.Management.Model;

namespace LotusInn.Management.Data
{
    public class PermissionSetsDataAdapter : IObjectDataAdapter<PermissionSets>
    {
        private const string SP_PERMISSIONSETS_INSERT = "";
        private const string SP_PERMISSIONSETS_UPDATE = "";
        private const string SP_PERMISSIONSETS_DELETE = "";
        private const string SP_PERMISSIONSETS_GETBYID = "PermissionSetsGetById";
        private const string SP_PERMISSIONSETS_GETALL = "PermissionSetsGetAll";
        private const string SP_OBJECT_PERMISSION_INSERT = "";
        private const string SP_OBJECT_PERMISSION_UPDATE = "";
        private const string SP_OBJECT_PERMISSION_DELETE = "";
        private const string SP_OBJECT_PERMISSION_GETBYPERMISSIONSETSID = "";


        public PermissionSets Insert(PermissionSets @object)
        {
            @object.Id = IdHelper.Generate();
            var param = new[]
            {
                new SqlParameter("@id", @object.Id),
                new SqlParameter("@name", @object.Name),
            };

            SqlHelper.ExecuteNonQuery(SP_PERMISSIONSETS_INSERT, param);
            return @object;
        }

        public void Update(PermissionSets @object)
        {
            var param = new[]
            {
                new SqlParameter("@id", @object.Id),
                new SqlParameter("@name", @object.Name),
            };

            SqlHelper.ExecuteNonQuery(SP_PERMISSIONSETS_UPDATE, param);

            foreach (var objPerm in @object.ObjectPermissions)
            {
                SqlHelper.ExecuteNonQuery(SP_OBJECT_PERMISSION_UPDATE, new []
                {
                    new SqlParameter("@permissionSetsId", @objPerm.PermissionSetsId),
                    new SqlParameter("@objectName", objPerm.ObjectName),
                    new SqlParameter("@permissions", objPerm.Permissions.SerializeToJson()),   
                });
            }
        }

        public void Delete(string id)
        {
            SqlHelper.StartTransaction(ConfigManager.ConnectionString, (conn, trans) =>
            {
                SqlHelper.ExecuteCommand(conn, trans, CommandType.StoredProcedure, SP_OBJECT_PERMISSION_GETBYPERMISSIONSETSID, new []
                {
                    new SqlParameter("@permissionSetsId", id), 
                });

                SqlHelper.ExecuteCommand(conn, trans, CommandType.StoredProcedure, SP_PERMISSIONSETS_DELETE, new[]
                {
                    new SqlParameter("@id", id),
                });
            });
        }

        public PermissionSets GetById(string id)
        {
            var item = SqlHelper.ExecuteReader(SP_PERMISSIONSETS_GETBYID, new[]
            {
                new SqlParameter("@id", id),
            }, Read).FirstOrDefault();
            item.ObjectPermissions = SqlHelper.ExecuteReader(SP_OBJECT_PERMISSION_GETBYPERMISSIONSETSID, new[]
            {
                new SqlParameter("@permissionSetsId", id),
            }, ReadObjectPermissions);

            return item;
        }

        public List<PermissionSets> GetAll()
        {
            return SqlHelper.ExecuteReader(SP_PERMISSIONSETS_GETALL, null, Read);
        }

        private List<PermissionSets> Read(IDataReader reader)
        {
            var list = new List<PermissionSets>();
            while (reader.Read())
            {
                var item = new PermissionSets
                {
                    Id = reader["Id"].ToString(),
                    Name = reader["Name"].ToString(),
                    ObjectPermissions = new List<ObjectPermission>()
                };
                list.Add(item);
            }
            
            return list;
        }

        private List<ObjectPermission> ReadObjectPermissions(IDataReader reader)
        {
            var list = new List<ObjectPermission>();
            while (reader.Read())
            {
                var item = new ObjectPermission
                {
                    PermissionSetsId = reader["PermissionSetsId"].ToString(),
                    ObjectName = reader["ObjectName"].ToString(),
                    Permissions = reader["Permissions"].ToString().FromJson<Dictionary<PermissionName, bool>>()
                };
                list.Add(item);
            }
            return list;
        }
    }
}
