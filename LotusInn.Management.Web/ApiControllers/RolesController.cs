using System.Collections.Generic;
using System.Web.Http;
using LotusInn.Management.Model;
using LotusInn.Management.Services;
using LotusInn.Management.Web.ApiControllers;

public class RolesController : BaseApiController
{
    [AcceptVerbs("GET")]
    public List<Role> GetRoles()
    {
        return Execute(session => RoleService.GetRoles());
    }
}