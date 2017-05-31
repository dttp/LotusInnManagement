using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LotusInn.Management.Core;
using LotusInn.Management.Model;

namespace LotusInn.Management.Data
{
    public class UserDataAdapter
    {
        private const string SP_GET_USER_BY_NAME = "UserGetByName";
        private const string SP_GET_USER_BY_ID = "UserGetById";
        private const string SP_GET_ALL_USERS = "UserGetAll";
        private const string SP_USER_INSERT = "UserInsert";
        private const string SP_USER_UPDATE = "UserUpdate";
        private const string SP_USER_UPDATE_STATUS = "UserUpdateStatus";
        private const string SP_USER_DELETE = "UserDelete";
        private const string SP_USER_CHANGE_PASSWORD = "UserChangePassword";

        public User GetByName(string userName)
        {
            var param = new[]
            {
                new SqlParameter("@username", userName)
            };
            var users = SqlHelper.ExecuteReader( SP_GET_USER_BY_NAME, param, ReadUsers);
            if (users == null || users.Count != 1)
                throw new Exception($"Cannot find user '{userName}'");
            return users[0];
        }

        public User GetById(string id)
        {
            var param = new[]
            {
                new SqlParameter("@id", id)
            };
            var users = SqlHelper.ExecuteReader(SP_GET_USER_BY_ID,param, ReadUsers);
            if (users == null || users.Count != 1)
                throw new Exception($"Cannot find user with id = '{id}'");
            return users[0];
        }

        public User Insert(User user)
        {
            var id = IdHelper.Generate();
            user.Id = "u-" + id;
            user.Password = IdHelper.Generate();
            SqlHelper.ExecuteNonQuery(SP_USER_INSERT,
                new[]
                {
                   new SqlParameter("@id", user.Id),
                   new SqlParameter("@username", user.Username),
                   new SqlParameter("@password", user.Password),
                   new SqlParameter("@email", user.Email),
                   new SqlParameter("@phone", user.Phone),
                   new SqlParameter("@houseId", user.House != null ? user.House.Id : ""),
                   new SqlParameter("@roleId", user.Role.Id),
                });            

            return user;
        }

        public void Update(User user)
        {
            SqlHelper.ExecuteNonQuery(SP_USER_UPDATE,
                new[]
                {
                    new SqlParameter("@id", user.Id),
                    new SqlParameter("@username", user.Username),
                    new SqlParameter("@email", user.Email),
                    new SqlParameter("@phone", user.Phone),
                    new SqlParameter("@houseId", user.House != null ? user.House.Id : ""),
                    new SqlParameter("@roleId", user.Role.Id),
                });
        }

        public void Delete(string id)
        {
            SqlHelper.ExecuteNonQuery(SP_USER_DELETE,
                new[]
                {
                    new SqlParameter("@id", id)
                });
        }

        public void UpdateStatus(string id, string status)
        {
            SqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.StoredProcedure, SP_USER_UPDATE_STATUS,
                new[]
                {
                    new SqlParameter("@id", id),
                    new SqlParameter("@status", status)
                });
        }

        public void ChangePassword(string id, string newpass)
        {
            SqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.StoredProcedure, SP_USER_CHANGE_PASSWORD,
                new[]
                {
                    new SqlParameter("@id", id),
                    new SqlParameter("@password", newpass)
                });
        }

        public List<User> GetAll()
        {
            return SqlHelper.ExecuteReader(SP_GET_ALL_USERS, null, ReadUsers);
        }

        private List<User> ReadUsers(IDataReader reader)
        {
            var result = new List<User>();
            var houseDA = new HouseDataAdapter();
            var roleDA = new RoleDataAdapter();
            while (reader.Read())
            {
                var user = new User
                {
                    Id = reader["Id"].ToString(),
                    Email = reader["Email"]?.ToString() ?? "",
                    Phone = reader["Phone"]?.ToString() ?? "",
                    Password = reader["Password"].ToString(),
                    Username = reader["Username"].ToString(),
                    Role = new Role
                    {
                        Id = reader["RoleId"].ToString(),
                    },
                    Status = reader["Status"]?.ToString() ?? "",
                    House = new House
                    {
                        Id = reader["HouseId"].ToString()
                    }
                };
                if (reader["HouseId"] != null && !string.IsNullOrEmpty(reader["HouseId"].ToString()))
                {
                    user.House = houseDA.GetById(user.House.Id);
                }
                user.Role = roleDA.GetById(user.Role.Id);
                result.Add(user);
            }
            return result;
        }
    }
}
