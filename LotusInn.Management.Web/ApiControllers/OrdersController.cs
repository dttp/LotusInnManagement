using System.Net;
using System.Net.Http;
using System.Web.Http;
using LotusInn.Management.Model;
using LotusInn.Management.Services;

namespace LotusInn.Management.Web.ApiControllers
{
    public class OrdersController : BaseApiController
    {

        [AcceptVerbs("POST")]
        public BookingOrder Create(string mode, BookingOrder order)
        {
            return Execute<BookingOrder>((session) => OrderService.Create(mode, order));
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage Update(BookingOrder order)
        {
            return Execute(session =>
            {
                OrderService.Update(order);
                return Request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [AcceptVerbs("GET")]
        public BookingOrder GetById(string id)
        {
            return Execute<BookingOrder>(session => OrderService.GetById(id));
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage Checkout(BookingOrder order)
        {
            return Execute(session =>
            {
                OrderService.Checkout(order);
                return Request.CreateResponse(HttpStatusCode.NoContent);
            });
        }

        [AcceptVerbs("DELETE")]
        public HttpResponseMessage Delete(string id)
        {
            return Execute(session =>
            {
                OrderService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.NoContent);
            });
        }
    }
}