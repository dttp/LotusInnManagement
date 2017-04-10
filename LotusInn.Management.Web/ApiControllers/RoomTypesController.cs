using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LotusInn.Management.Model;
using LotusInn.Management.Services;

namespace LotusInn.Management.Web.ApiControllers
{
    public class RoomTypesController : BaseApiController
    {
        [AcceptVerbs("GET")]
        public List<RoomType> GetRoomTypes(string houseId)
        {
            return Execute(session => RoomTypeService.GetRoomTypes(houseId));
        }

        [AcceptVerbs("GET")]
        public RoomType GetById(string id)
        {
            return Execute(session => RoomTypeService.GetById(id));
        }

        [AcceptVerbs("POST")]
        public RoomType Add(RoomType roomType)
        {
            return Execute(session => RoomTypeService.Add(roomType));
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage Update(RoomType roomType)
        {
            return Execute(session =>
            {
                RoomTypeService.Update(roomType);
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            });
        }

        [AcceptVerbs("DELETE")]
        public HttpResponseMessage Delete(string id)
        {
            return Execute(session =>
            {
                RoomTypeService.Delete(id);
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            });
        }
    }
}