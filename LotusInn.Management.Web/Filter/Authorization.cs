using System;
using System.Web;
using System.Web.Mvc;
using LotusInn.Management.Services;

namespace LotusInn.Management.Web.Filter
{
    public class Authorization : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var authIdCookie = request.Cookies["AuthId"];           
            if (IsUserNeedLogin(authIdCookie))
            {                                
                filterContext.Result = new RedirectResult($"/login?ref={HttpUtility.HtmlEncode(request.RawUrl)}");                
            }
            base.OnActionExecuted(filterContext);
        }

        private bool IsUserNeedLogin(HttpCookie authIdCookie)
        {
            if (authIdCookie == null) return true;

            try
            {
                var loginSession = SessionManager.DecodeCookie(authIdCookie.Value);
                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }
    }
}