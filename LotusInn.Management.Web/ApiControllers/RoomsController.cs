using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LotusInn.Management.Model;
using LotusInn.Management.Services;

namespace LotusInn.Management.Web.ApiControllers
{
    public class RoomsController : BaseApiController
    {
        [AcceptVerbs("GET")]
        public List<Room> GetRooms(string houseId)
        {
            return Execute(session => RoomService.GetRooms(houseId));
        }

        [AcceptVerbs("GET")]
        public Room GetById(string id)
        {
            return Execute(session => RoomService.GetById(id));
        }

        [AcceptVerbs("GET")]
        public List<RoomWithStatus> GetRoomStatus(string houseId, string startDate, string endDate)
        {
            return Execute(session => RoomService.GetRoomStatus(houseId, startDate, endDate));
        }

        [AcceptVerbs("GET")]
        public List<RoomWithStatus> GetRoomWithStatuses(string houseId)
        {
            return Execute(session => RoomService.GetRoomsWithStatuses(houseId));
        }

        [AcceptVerbs("POST")]
        public Room Add(Room room)
        {
            return Execute(session =>
            {
                var numbers = room.RoomNumber.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
                foreach (var number in numbers)
                {
                    var r = new Room
                    {
                        HouseId = room.HouseId,
                        Id = room.Id,
                        RoomNumber = number,
                        RoomType = room.RoomType
                    };
                    RoomService.Add(r);
                }

                return new Room();
            });
        }

        [AcceptVerbs("PUT")]
        public HttpResponseMessage Update(Room room)
        {
            return Execute(session =>
            {
                RoomService.Update(room);
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            });
        }

        [AcceptVerbs("DELETE")]
        public HttpResponseMessage Delete(string id)
        {
            return Execute(session =>
            {
                RoomService.Delete(id);
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            });
        }
    }
}