using System;
using System.Linq;
using System.Web.Http;
using LotusInn.Management.Model;
using LotusInn.Management.Services;

namespace LotusInn.Management.Web.ApiControllers
{
    public class BaseApiController : ApiController
    {
        public T Execute<T>(Func<LoginSession, T> func)
        {
            if (!Request.Headers.Contains("X-Login-Session"))
                throw new Exception("Missing or Invalid Session. Please logout then login again.");
            var loginSession = Request.Headers.GetValues("X-Login-Session").FirstOrDefault();            
            var sessionObj = SessionManager.DecodeCookie(loginSession);
            return func.Invoke(sessionObj);
        }

        public T Execute<T>(Func<T> func)
        {
            return func.Invoke();
        }
    }
}