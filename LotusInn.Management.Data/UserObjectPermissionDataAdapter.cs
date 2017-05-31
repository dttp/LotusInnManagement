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
        private const string SP_User_OBJECT_PERMISSION_INSERT = "UserObjectPermissionInsert";
        private const string SP_User_OBJECT_PERMISSION_UPDATE = "UserObjectPermissionUpdate";
        private const string SP_User_OBJECT_PERMISSION_DELETE = "UserObjectPermissionDelete";
        private const string SP_User_OBJECT_PERMISSION_GETBYID = "UserObjectPermissionGetById";
        private const string SP_USER_OBJECT_PERMISSION_GETBY_USER_ID = "UserObjectPermissionGetByUserId";
        public UserObjectPermission Insert(UserObjectPermission @object)
        {
            @object.Id = IdHelper.Generate();
            var param = new[]
            {
                new SqlParameter("@id", @object.Id),
                new SqlParameter("@userId", @object.User.Id),
                new SqlParameter("@object", @object.Object),
                new SqlParameter("@permission", (int) @object.Permission),
            };
            SqlHelper.ExecuteNonQuery(SP_User_OBJECT_PERMISSION_INSERT, param);    
            return @object;
        }

        public void Update(UserObjectPermission @object)
        {
            var param = new[]
            {
                new SqlParameter("@id", @object.Id),
                new SqlParameter("@userId", @object.User.Id),
                new SqlParameter("@object", @object.Object),
                new SqlParameter("@permission", (int) @object.Permission),
            };
            SqlHelper.ExecuteNonQuery(SP_User_OBJECT_PERMISSION_UPDATE, param);
        }

        public void Delete(string id)
        {
            var param = new[]
            {
                new SqlParameter("@id", id),
            };
            SqlHelper.ExecuteNonQuery(SP_User_OBJECT_PERMISSION_DELETE, param);

        }

        public UserObjectPermission GetById(string id)
        {
            var param = new[]
            {
                new SqlParameter("@id", id),
            };
            return SqlHelper.ExecuteReader(SP_User_OBJECT_PERMISSION_GETBYID, param, Read).FirstOrDefault();
        }

        public List<UserObjectPermission> GetByUserId(string userId)
        {
            var param = new[]
            {
                new SqlParameter("@userId", userId),
            };
            return SqlHelper.ExecuteReader(SP_USER_OBJECT_PERMISSION_GETBY_USER_ID, param, Read);
        } 

        private List<UserObjectPermission> Read(IDataReader reader)
        {
            var list = new List<UserObjectPermission>();
            var userDA = new UserDataAdapter();
            while (reader.Read())
            {
                var item = new UserObjectPermission
                {
                    Id = reader["Id"].ToString(),
                    User = new User
                    {
                        Id = reader["UserId"].ToString()
                    },
                    Object = reader["Object"].ToString(),
                    Permission = (PermissionEnum) Convert.ToInt32(reader["Permission"])
                };
                item.User = userDA.GetById(item.User.Id);
                list.Add(item);
            }
            return list;
        } 
    }
}
