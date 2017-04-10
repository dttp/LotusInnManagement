using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using LotusInn.Management.Model;
using LotusInn.Management.Services;
using Microsoft.SqlServer.Server;

namespace LotusInn.Management.Web.ApiControllers
{
    public class CustomersController : BaseApiController
    {
        [AcceptVerbs("GET")]
        public PaginationResult<Customer> GetCustomers(string houseId, string name = "", string email ="", string room = "", string passportOrId = "", int pageSize = 30, int pageIdx = 1)
        {
            return Execute((session) =>
            {
                if (name == null) name = "";
                if (email == null) email = "";
                if (passportOrId == null) passportOrId = "";
                return CustomerService.GetCustomers(houseId, name, email, passportOrId, pageSize, pageIdx);
            });            
        }

        [AcceptVerbs("GET")]
        public PaginationResult<ActiveCustomer> GetActiveCustomers(string houseId, string name = "", string email = "", string room = "", string passportOrId = "", int pageSize = 30, int pageIdx = 1)
        {
            return Execute(session =>
            {
                if (name == null) name = "";
                if (email == null) email = "";
                if (passportOrId == null) passportOrId = "";
                if (room == null) room = "";
                return CustomerService.GetActiveCustomers(houseId, name, email, room, passportOrId, pageSize, pageIdx);
            });
        }

        [AcceptVerbs("GET")]
        public PaginationResult<ActiveCustomer> GetReservedCustomers(string houseId, string name = "", string email = "", string room = "", string passportOrId = "", int pageSize = 30, int pageIdx = 1)
        {
            return Execute(session =>
            {
                if (name == null) name = "";
                if (email == null) email = "";
                if (passportOrId == null) passportOrId = "";
                if (room == null) room = "";
                return CustomerService.GetReservedCustomers(houseId, name, email, room, passportOrId, pageSize, pageIdx);
            });
        }

        [AcceptVerbs("GET")]
        public Customer GetById(string id)
        {
            return Execute(session => CustomerService.GetById(id));
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage Update(Customer customer)
        {
            return Execute(sesion =>
            {
                CustomerService.Update(customer);
                return Request.CreateResponse(HttpStatusCode.OK);
            });
        }        
    }
}