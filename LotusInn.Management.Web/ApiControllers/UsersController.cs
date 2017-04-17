using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LotusInn.Management.Model;
using LotusInn.Management.Services;

namespace LotusInn.Management.Web.ApiControllers
{
    public class UsersController : BaseApiController
    {
        [AcceptVerbs("GET")]
        public List<User> GetUsers()
        {
            return Execute(session => UserService.GetAll());
        }

        [AcceptVerbs("GET")]
        public User GetUserById(string id)
        {
            return Execute(session => UserService.GetUserById(id));
        }

        [AcceptVerbs("POST")]
        public User Add(User user)
        {
            return Execute(session => UserService.AddUser(user));
        }

        [AcceptVerbs("PUT")]
        public HttpResponseMessage Update(User user)
        {
            return Execute(session =>
            {
                UserService.UpdateUser(user);
                return new HttpResponseMessage(HttpStatusCode.Accepted);
            });
        }

        [AcceptVerbs("POST")]
        public LoginResult Reset(string code, string newpass)
        {
            return Execute(() => UserService.Reset(code, newpass));
        }


        [AcceptVerbs("POST")]
        public HttpResponseMessage ResetPassword(string id)
        {
            return Execute(session =>
            {
                UserService.ResetPassword(id);
                return new HttpResponseMessage(HttpStatusCode.OK);
            });
        }

        [AcceptVerbs("DELETE")]
        public HttpResponseMessage Delete(string id)
        {
            return Execute(session =>
            {
                UserService.DeleteUser(id);
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            });
        }
    }
}