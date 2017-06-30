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
    public class UserObjectPermissionDataAdapter: IObjectDataAdapter<UserObjectPermission>
    {
        private const string SP_USER_OBJECT_PERMISSION_INSERT = "UserObjectPermissionInsert";
        private const string SP_USER_OBJECT_PERMISSION_UPDATE = "UserObjectPermissionUpdate";
        private const string SP_USER_OBJECT_PERMISSION_DELETE = "UserObjectPermissionDelete";
        private const string SP_USER_OBJECT_PERMISSION_GETBYID = "UserObjectPermissionGetById";
        private const string SP_USER_OBJECT_PERMISSION_GETBY_USER_ID = "UserObjectPermissionGetByUserId";
        private const string SP_USER_OBJECT_PERMISSION_GETCUSTOMOBJECTPERMISSION = "UserObjectPermissionGetCustomObjectPermission";
        private const string SP_USER_OBJECT_PERMISSION_GETBYOBJECTID = "UserObjectPermissionGetByObjectId";
        public UserObjectPermission Insert(UserObjectPermission @object)
        {
            @object.Id = IdHelper.Generate();
            var param = new[]
            {
                new SqlParameter("@id", @object.Id),
                new SqlParameter("@userId", @object.User.Id),
                new SqlParameter("@objectType", @object.ObjectType),
                new SqlParameter("@objectId", @object.ObjectId),
                new SqlParameter("@permission", (int) @object.Permission),
            };
            SqlHelper.ExecuteNonQuery(SP_USER_OBJECT_PERMISSION_INSERT, param);    
            return @object;
        }

        public void Update(UserObjectPermission @object)
        {
            var param = new[]
            {
                new SqlParameter("@id", @object.Id),
                new SqlParameter("@userId", @object.User.Id),
                new SqlParameter("@objectType", @object.ObjectType),
                new SqlParameter("@objectId", @object.ObjectId),
                new SqlParameter("@permission", (int) @object.Permission),
            };
            SqlHelper.ExecuteNonQuery(SP_USER_OBJECT_PERMISSION_UPDATE, param);
        }

        public void Delete(string id)
        {
            var param = new[]
            {
                new SqlParameter("@id", id),
            };
            SqlHelper.ExecuteNonQuery(SP_USER_OBJECT_PERMISSION_DELETE, param);

        }

        public UserObjectPermission GetById(string id)
        {
            var param = new[]
            {
                new SqlParameter("@id", id),
            };
            return SqlHelper.ExecuteReader(SP_USER_OBJECT_PERMISSION_GETBYID, param, Read).FirstOrDefault();
        }

        public List<UserObjectPermission> GetByUserId(string userId)
        {
            var param = new[]
            {
                new SqlParameter("@userId", userId),
            };
            return SqlHelper.ExecuteReader(SP_USER_OBJECT_PERMISSION_GETBY_USER_ID, param, Read);
        }

        public List<UserObjectPermission> GetCustomObjectPermissions(string userId, string objectType)
        {
            var param = new[]
               {
                new SqlParameter("@userId", userId),
                new SqlParameter("@objectType", objectType),
            };
            return SqlHelper.ExecuteReader(SP_USER_OBJECT_PERMISSION_GETCUSTOMOBJECTPERMISSION, param, Read);
        }

        public List<UserObjectPermission> GetByObjectId(string objectId)
        {
            var param = new[]
               {
                new SqlParameter("@objectId", objectId),
            };
            return SqlHelper.ExecuteReader(SP_USER_OBJECT_PERMISSION_GETBYOBJECTID, param, Read);
        }

        private List<UserObjectPermission> Read(IDataReader reader)
        {
            var list = new List<UserObjectPermission>();
            var userDa = new UserDataAdapter();
            while (reader.Read())
            {
                var item = new UserObjectPermission
                {
                    Id = reader["Id"].ToString(),
                    User = new User
                    {
                        Id = reader["UserId"].ToString()
                    },
                    ObjectType = reader["ObjectType"].ToString(),
                    ObjectId = reader["ObjectId"]?.ToString(),
                    Permission = (PermissionEnum) Convert.ToInt32(reader["Permission"])
                };
                item.User = userDa.GetById(item.User.Id);
                list.Add(item);
            }
            return list;
        } 
    }
}
