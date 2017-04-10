using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LotusInn.Management.Core;
using LotusInn.Management.Model;

namespace LotusInn.Management.Services
{
    public class UserService
    {
        public const string SP_GET_USER_BY_NAME = "UserGetByName";
        public const string SP_GET_USER_BY_ID = "UserGetById";
        private const string SP_GET_ALL_USERS = "UserGetAll";
        private const string SP_USER_INSERT = "UserInsert";
        private const string SP_USER_UPDATE = "UserUpdate";
        private const string SP_USER_UPDATE_STATUS = "UserUpdateStatus";
        private const string SP_USER_DELETE = "UserDelete";
        private const string SP_USER_CHANGE_PASSWORD = "UserChangePassword";


        public static User GetUserByName(string username)
        {
            var users = SqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.StoredProcedure, SP_GET_USER_BY_NAME,
                new[] {new SqlParameter {ParameterName = "@username", Value = username}}, ParseUser);
            if (users == null || users.Count != 1) 
                throw new Exception($"Cannot find user '{username}'");
            return users[0];
        }

        public static User GetUserById(string id)
        {
            var users = SqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.StoredProcedure, SP_GET_USER_BY_ID,
                new[] { new SqlParameter { ParameterName = "@id", Value = id } }, ParseUser);
            if (users == null || users.Count != 1)
                throw new Exception($"Cannot find user with id = '{id}'");
            return users[0];
        }

        

        public static bool VerifyPassword(User user, string password)
        {
            var encodedPass = IdHelper.Encode(password);
            return user.Password == encodedPass;
        }

        public static List<User> GetUsers()
        {
            var users = SqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.StoredProcedure, SP_GET_ALL_USERS,
                null, ParseUser);
            return users;
        }

        public static User AddUser(User user)
        {
            var id = IdHelper.Generate();
            user.Id = "u-" + id;
            user.Password = IdHelper.Generate();
            SqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.StoredProcedure, SP_USER_INSERT,
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

            var code = IdHelper.Encode(new ResetCode {UserId = id}.SerializeToJson());
            var link = $"{ConfigManager.LMSDomain}users/reset?code={code}";
            var emailBodyFmt = EmailService.LoadTemplate("WelcomeUser.txt");
            var emailBody = string.Format(emailBodyFmt, user.Username, link);

            EmailService.SendMail(new MailInfo
            {
                To = new string[] {user.Email},
                Body = emailBody,
                IsBodyHtml = true,
                Subject = "Welcome to LotusInn Management System"
            });

            return user;
        }

        public static LoginResult Reset(string code, string newpass)
        {
            var resetCode = IdHelper.Decode(code).FromJson<ResetCode>();            
            ChangePassword(resetCode.UserId, IdHelper.Encode(newpass));
            UpdateStatus(resetCode.UserId, "Verified");
            var user = GetUserById(resetCode.UserId);
            var result = new LoginResult
            {
                AuthId = SessionManager.GenerateAuthId(user),
                Message = string.Empty,
                Success = true
            };

            return result;
        }

        public static void ResetPassword(string id)
        {
            UpdateStatus(id, "NotVerified");

            var code = IdHelper.Encode(new ResetCode { UserId = id }.SerializeToJson());
            var link = $"{ConfigManager.LMSDomain}users/reset?code={code}";
            var emailBodyFmt = EmailService.LoadTemplate("Resetpassword.txt");
            var user = GetUserById(id);
            var emailBody = string.Format(emailBodyFmt, user.Username, link);

            EmailService.SendMail(new MailInfo
            {
                To = new string[] { user.Email },
                Body = emailBody,
                IsBodyHtml = true,
                Subject = "Welcome to LotusInn Management System"
            });
        }

        public static void UpdateUser(User user)
        {
            SqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.StoredProcedure, SP_USER_UPDATE,
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

        public static void DeleteUser(string id)
        {
            SqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.StoredProcedure, SP_USER_DELETE,
                new[]
                {
                    new SqlParameter("@id", id)                    
                });
        }

        public static void ChangePassword(string id, string newpass)
        {
            SqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.StoredProcedure, SP_USER_CHANGE_PASSWORD,
                new[]
                {
                    new SqlParameter("@id", id),
                    new SqlParameter("@password", newpass)
                });
        }

        public static void UpdateStatus(string id, string status)
        {
            SqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.StoredProcedure, SP_USER_UPDATE_STATUS,
                new[]
                {
                    new SqlParameter("@id", id),
                    new SqlParameter("@status", status)
                });
        }

        private static List<User> ParseUser(SqlDataReader reader)
        {
            var result = new List<User>();
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
                        Name = reader["RoleName"].ToString()
                    },
                    Status = reader["Status"]?.ToString() ?? ""
                };
                if (reader["HouseId"] != null)
                {
                    user.House = new House
                    {
                        Id = reader["HouseId"].ToString(),
                        Name = reader["HouseName"].ToString(),
                        Address = reader["HouseAddress"]?.ToString() ?? ""
                    };
                }
                result.Add(user);
            }
            return result;
        }
    }
}
