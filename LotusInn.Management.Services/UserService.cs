using System.Collections.Generic;
using System.Linq;
using LotusInn.Management.Core;
using LotusInn.Management.Data;
using LotusInn.Management.Model;

namespace LotusInn.Management.Services
{
    public class UserService
    {
        
        private static readonly UserDataAdapter _adapter = new UserDataAdapter();

        public static User GetUserByName(string username)
        {
            return _adapter.GetByName(username);
        }

        public static User GetUserById(string id)
        {
            return _adapter.GetById(id);
        }

        public static bool VerifyPassword(User user, string password)
        {
            var encodedPass = IdHelper.Encode(password);
            return user.Password == encodedPass;
        }

        public static List<User> GetAll()
        {
            return _adapter.GetAll();
        }

        public static User AddUser(User user)
        {
            user = _adapter.Insert(user);

            var code = IdHelper.Encode(new ResetCode { UserId = user.Id }.SerializeToJson());
            var link = $"{ConfigManager.LMSDomain}users/reset?code={code}";
            var emailBodyFmt = EmailService.LoadTemplate("WelcomeUser.txt");
            var emailBody = string.Format(emailBodyFmt, user.Username, link);

            EmailService.SendMail(new MailInfo
            {
                To = new string[] { user.Email },
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
            _adapter.Update(user);
        }

        public static void DeleteUser(string id)
        {
            _adapter.Delete(id);
        }

        public static void ChangePassword(string id, string newpass)
        {
            _adapter.ChangePassword(id, newpass);
        }

        public static void UpdateStatus(string id, string status)
        {
            _adapter.UpdateStatus(id, status);
        }

        public static UserPermissions GetPermissions(string userId)
        {
            var userObjectPermDA = new UserObjectPermissionDataAdapter();
            var list = userObjectPermDA.GetByUserId(userId);
            if (list != null && list.Count > 0)
            {
                return new UserPermissions
                {
                    Permissions = list,
                    InheriteFromRole = false
                };

            }

            var user = GetUserById(userId);
            var roleSvc = new RoleService();
            var rolePermissionList = roleSvc.GetPermissions(user.Role.Id);
            var result = rolePermissionList.Select(item => new UserObjectPermission()
            {
                Id = item.Id, ObjectType = item.ObjectType, User = user, Permission = item.Permission
            }).ToList();

            return new UserPermissions
            {
                InheriteFromRole = true,
                Permissions = result
            };
        }
        public static void SetPermissions(string userId, UserPermissions permissions)
        {
            var userObjectPermDA = new UserObjectPermissionDataAdapter();
            if (permissions.InheriteFromRole)
            {
                var list = userObjectPermDA.GetByUserId(userId);
                foreach (var item in list)
                {
                    userObjectPermDA.Delete(item.Id);
                }
            }
            else
            {
                foreach (var item in permissions.Permissions)
                {
                    userObjectPermDA.Insert(item);
                }
            }
        }
    }
}
