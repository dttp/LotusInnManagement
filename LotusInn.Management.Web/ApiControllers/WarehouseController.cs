using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using LotusInn.Management.Model;
using LotusInn.Management.Services;

namespace LotusInn.Management.Web.ApiControllers
{
    public class WarehouseController : BaseApiController
    {
        [AcceptVerbs("GET")]
        public List<Warehouse> GetAll()
        {
            return Execute(session => WarehouseService.GetAll());
        }

        [AcceptVerbs("POST")]
        public Warehouse Insert(Warehouse warehouse)
        {
            return Execute(session => WarehouseService.Insert(warehouse));
        }

        [AcceptVerbs("POST","PUT")]
        public HttpResponseMessage Update(Warehouse warehouse)
        {
            return Execute(session =>
            {
                WarehouseService.Update(warehouse);
                return Request.CreateResponse(HttpStatusCode.NoContent);
            });            
        }

        [AcceptVerbs("DELETE")]
        public HttpResponseMessage Delete(string id)
        {
            return Execute(session =>
            {
                WarehouseService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.NoContent);
            });
        }

        [AcceptVerbs("GET")]
        public PaginationResult<WarehouseItem> GetItems(string warehouseId, int pageIndex = 1, int pageSize = 25)
        {
            return Execute(session => WarehouseService.GetWarehouseItems(warehouseId, pageIndex, pageSize));
        }

        [AcceptVerbs("GET")]
        public PaginationResult<WarehouseActivity> GetActivities(string warehouseId, int pageIndex = 1, int pageSize = 25)
        {
            return Execute(session => WarehouseService.GetWarehouseActivity(warehouseId, pageIndex, pageSize));
        }

        [AcceptVerbs("DELETE")]
        public HttpResponseMessage DeleteItem(string id)
        {
            return Execute(session =>
            {
                WarehouseService.DeleteWarehouseItem(id, session.User.Id);
                return Request.CreateResponse(HttpStatusCode.NoContent);
            });
        }

        [AcceptVerbs("POST")]
        public WarehouseItem AddItem(WarehouseItem item)
        {
            return Execute(session => WarehouseService.InsertWarehouseItem(item, session.User.Id));
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage UpdateItem(WarehouseItem item)
        {
            return Execute(session =>
            {
                WarehouseService.UpdateWarehouseItem(item);
                return Request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [AcceptVerbs("GET")]
        public WarehouseItem GetItem(string id)
        {
            return Execute(session => WarehouseService.GetItem(id));
        }
    }
}