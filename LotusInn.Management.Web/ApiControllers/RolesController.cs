using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LotusInn.Management.Model;
using LotusInn.Management.Services;

namespace LotusInn.Management.Web.ApiControllers
{
    public class RolesController : BaseApiController
    {
        [AcceptVerbs("GET")]
        public List<Role> GetRoles()
        {
            return Execute(session =>
            {
                var svc = new RoleService();
                return svc.GetRoles();
            });
        }

        [AcceptVerbs("GET")]
        public Role GetById(string id)
        {
            return Execute(session =>
            {
                var svc = new RoleService();
                return svc.GetById(id);
            });
        }

        [AcceptVerbs("POST")]
        public Role Insert(Role role)
        {
            return Execute(session =>
            {
                var svc = new RoleService();
                return svc.Insert(role);
            });
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage Update(Role role)
        {
            return Execute(session =>
            {
                var svc = new RoleService();
                svc.Update(role);
                return Request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [AcceptVerbs("DELETE")]
        public HttpResponseMessage Delete(string id)
        {
            return Execute(session =>
            {
                var svc = new RoleService();
                svc.Delete(id);
                return Request.CreateResponse(HttpStatusCode.NoContent);
            });
        }

        [AcceptVerbs("GET")]
        public List<RoleObjectPermission> GetPermissions(string roleId)
        {
            return Execute(session =>
            {
                var svc = new RoleService();
                return svc.GetPermissions(roleId);
            });
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage SetPermissions(List<RoleObjectPermission> permissionsList)
        {
            return Execute(session =>
            {
                var svc = new RoleService();
                svc.SetPermissions(permissionsList);
                return Request.CreateResponse(HttpStatusCode.OK);
            });
        }
    }
}