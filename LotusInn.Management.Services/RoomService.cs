using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using LotusInn.Management.Core;
using LotusInn.Management.Data;
using LotusInn.Management.Model;

namespace LotusInn.Management.Services
{
    public class RoomService
    {
        private static RoomDataAdapter _adapter = new RoomDataAdapter();

        public static List<Room> GetRooms(string houseId)
        {
            return _adapter.GetAll(houseId);
        }

        public static Room GetById(string id)
        {
            return _adapter.GetById(id);
        }

        public static List<RoomWithStatus> GetRoomStatus(string houseId, string startDate, string endDate)
        {
            startDate = startDate.Replace("-", "/");
            endDate = endDate.Replace("-", "/");
            var dateStart = DateTime.ParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var dateEnd = DateTime.ParseExact(endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            return _adapter.GetRoomsStatus(houseId, dateStart, dateEnd);
        }

        public static List<RoomWithStatus> GetRoomsWithStatuses(string houseId)
        {
            return _adapter.GetRoomWithStatuses(houseId);
        }

        public static Room Add(Room room)
        {
            var rooms = GetRooms(room.HouseId);
            if (rooms.Any(r => r.RoomNumber == room.RoomNumber))
                throw new Exception("There is already a room with same number. Please choose a difference number");
            return _adapter.Insert(room);
        }

        public static void Update(Room room)
        {                     
            _adapter.Update(room);
        }

        public static void Delete(string id)
        {
            _adapter.Delete(id);
        }
    }
}
