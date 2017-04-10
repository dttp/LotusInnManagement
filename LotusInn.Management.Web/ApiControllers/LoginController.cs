using System.Web.Http;
using LotusInn.Management.Model;
using LotusInn.Management.Services;

namespace LotusInn.Management.Web.ApiControllers
{
    public class LoginController : ApiController
    {
        [AcceptVerbs("POST")]
        public LoginResult Verify(LoginInfo loginInfo)
        {
            var result = SessionManager.Login(loginInfo);
            return result;
        }     
        
    }
}