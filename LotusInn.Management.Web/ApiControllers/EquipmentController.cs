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
    public class EquipmentController : BaseApiController
    {
        [AcceptVerbs("GET")]
        public List<Equipment> SelectAll(string houseId, string roomId = "", string name = "", string category = "", string status = "")
        {
            return Execute(session => EquipmentService.GetAll(houseId, roomId, name, category, status));
        }

        [AcceptVerbs("GET")]
        public Equipment GetById(string id)
        {
            return Execute(session => EquipmentService.GetById(id));
        }

        [AcceptVerbs("POST")]
        public Equipment Insert(Equipment equipment)
        {
            return Execute(session => EquipmentService.Insert(equipment));
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage Update(Equipment equipment)
        {
            return Execute(session =>
            {
                EquipmentService.Update(equipment);
                return Request.CreateResponse(HttpStatusCode.OK);
            });
        }

        [AcceptVerbs("DELETE")]
        public HttpResponseMessage Delete(string id)
        {
            return Execute(session =>
            {
                EquipmentService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.NoContent);
            });
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage Copy(EquipmentCopyRequest request)
        {
            return Execute(session =>
            {
                EquipmentService.Copy(request);
                return Request.CreateResponse(HttpStatusCode.OK);
            });
        }
    }
}