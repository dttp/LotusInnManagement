using System;
using System.Text;
using LotusInn.Management.Core;
using LotusInn.Management.Model;

namespace LotusInn.Management.Services
{
    public class SessionManager
    {
        public static LoginResult Login(LoginInfo account)
        {
            var result = new LoginResult();

            try
            {
                // get the account by name
                var user = UserService.GetUserByName(account.UserName);
                if (user == null)
                    throw new Exception("Cannot find user '" + account.UserName + "'.");
                if (!UserService.VerifyPassword(user, account.Password))
                    throw new Exception("Username and password does not match");

                result.Success = true;
                result.AuthId = GenerateAuthId(user);
                result.Message = string.Empty;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.AuthId = string.Empty;
            }

            return result;
        }

        public static string GenerateAuthId(User user)
        {
            var loginSession = new LoginSession {User = user, LoginTime = DateTime.Now};
            var authId = Convert.ToBase64String(Encoding.UTF8.GetBytes(loginSession.SerializeToJson()));
            return authId;
        }

        public static LoginSession DecodeCookie(string cookieValue)
        {
            var value = cookieValue.Replace("%3D", "=");
            return Encoding.UTF8.GetString(Convert.FromBase64String(value)).FromJson<LoginSession>();
        }
    }
}
