using System.Web;
using System.Web.Http;
using LotusInn.Management.Model;
using System.IO;
using LotusInn.Management.Core;

namespace LotusInn.Management.Web.ApiControllers
{
    public class ConfigController : BaseApiController
    {
        [AcceptVerbs("GET")]
        public SidebarMenu GetSidebarMenu()
        {
            return Execute(session =>
            {
                var filePath = HttpContext.Current.Server.MapPath("~/App_Data/SidebarMenu.json");
                var menu = File.ReadAllText(filePath).FromJson<SidebarMenu>();
                foreach (var item in menu.Items)
                {
                    var refererUrl = Request.Headers.Referrer.AbsolutePath;
                    if ((refererUrl == "/" && item.Url == "/") ||
                        (refererUrl.StartsWith(item.Url) && item.Url != "/"))                    
                    {
                        item.Selected = true;
                        break;
                    }
                }
                return menu;
            });            
        }
    }
}
