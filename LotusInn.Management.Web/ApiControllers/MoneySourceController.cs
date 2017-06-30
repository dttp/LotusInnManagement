using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using LotusInn.Management.Model;
using LotusInn.Management.Services;

namespace LotusInn.Management.Web.ApiControllers
{
    public class MoneySourceController : BaseApiController
    {
        [AcceptVerbs("GET")]
        public List<MoneySource> GetAll()
        {
            return Execute(session =>
            {
                var svc = new MoneySourceService();
                return svc.GetAll(session.User);
            });
        }

        [AcceptVerbs("GET")]
        public MoneySource GetById(string id)
        {
            return Execute(session =>
            {
                var svc = new MoneySourceService();
                return svc.GetById(id);
            });
        }

        [AcceptVerbs("POST")]
        public MoneySource Insert(MoneySource source)
        {
            return Execute(session =>
            {
                var svc = new MoneySourceService();
                source.Owner = session.User;

                return svc.Insert(source);
            });
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage Update(MoneySource source)
        {
            return Execute(session =>
            {
                var svc = new MoneySourceService();
                svc.Update(source);
                return Request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [AcceptVerbs("DELETE")]
        public HttpResponseMessage Delete(string id)
        {
            return Execute(session =>
            {
                var svc = new MoneySourceService();
                svc.Delete(id);

                return Request.CreateResponse(HttpStatusCode.NoContent);
            });
        }

        [AcceptVerbs("GET")]
        public PaginationResult<PaymentRecord> Query(string sourceId, int pageIndex, int pageSize, string type,
            string startDate, string endDate)
        {
            return Execute(session =>
            {
                var dateStart = DateTime.ParseExact(startDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                var dateEnd = DateTime.ParseExact(endDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                var svc = new PaymentRecordService();
                return svc.Select(pageIndex, pageSize, sourceId, type, dateStart, dateEnd);
            });
        }

        [AcceptVerbs("POST")]
        public PaymentRecord CreatePayment(PaymentRecord payment)
        {
            return Execute(session =>
            {
                var svc = new MoneySourceService();
                return svc.CreatePayment(payment, null, null);
            });
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage UpdatePayment(PaymentRecord payment)
        {
            return Execute(session =>
            {
                var svc = new MoneySourceService();
                svc.UpdatePayment(payment, null, null);
                return Request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [AcceptVerbs("DELETE")]
        public HttpResponseMessage DeletePayment(string id)
        {
            return Execute(session =>
            {
                var svc = new MoneySourceService();
                svc.DeletePayment(id, null, null);
                return Request.CreateResponse(HttpStatusCode.NoContent);
            });
        }

        [AcceptVerbs("GET")]
        public ObjectPermissions GetObjectPermissions(string moneySourceId)
        {
            return Execute(session =>
            {
                var svc = new MoneySourceService();
                return svc.GetPermissions(moneySourceId);
            });
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage SetObjectPermissions(ObjectPermissions permissions)
        {
            return Execute(session =>
            {
                var svc = new MoneySourceService();
                svc.SetPermission(permissions);
                return Request.CreateResponse(HttpStatusCode.OK);
            });
        }
    }
}