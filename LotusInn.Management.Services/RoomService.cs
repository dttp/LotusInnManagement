using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using LotusInn.Management.Core;
using LotusInn.Management.Model;

namespace LotusInn.Management.Services
{
    public class RoomService
    {
        private const string SP_ROOM_GETALL = "RoomGetAll";
        private const string SP_ROOM_GETBYID = "RoomGetById";
        private const string SP_ROOM_INSERT = "RoomInsert";
        private const string SP_ROOM_UPDATE = "RoomUpdate";
        private const string SP_ROOM_DELETE = "RoomDelete";
        private const string SP_ROOM_GETAVAILABLE = "RoomGetAvailable";
        private const string SP_ROOM_GETWITHSTATUS = "RoomSelectWithStatus";

        public static List<Room> GetRooms(string houseId)
        {
            return SqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.StoredProcedure, SP_ROOM_GETALL,
                 new[]
                {
                    new SqlParameter("@houseId", houseId),
                }, Parse);
        }

        public static Room GetById(string id)
        {
            var list = SqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.StoredProcedure,
                SP_ROOM_GETBYID,
                new[]
                {
                    new SqlParameter("@id", id),
                }, Parse);
            if (list.Count != 1) throw new Exception($"Cannot find room with id='{id}'");
            return list[0];
        }

        public static List<RoomWithStatus> GetRoomStatus(string houseId, string startDate, string endDate)
        {
            startDate = startDate.Replace("-", "/");
            endDate = endDate.Replace("-", "/");
            var availableRooms =SqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.StoredProcedure, SP_ROOM_GETAVAILABLE,
                 new[]
                {
                    new SqlParameter("@houseId", houseId),
                    new SqlParameter("@startDate", DateTime.ParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)),
                    new SqlParameter("@endDate", DateTime.ParseExact(endDate, "dd/MM/yyyy",CultureInfo.InvariantCulture)),
                }, Parse);

            var allRooms = GetRooms(houseId);
            var result = allRooms.Select(r => new RoomWithStatus
            {
                Id = r.Id,
                HouseId = r.HouseId,
                RoomNumber = r.RoomNumber,
                RoomType = r.RoomType,
                Status = availableRooms.Exists(ar => ar.Id == r.Id) ? "Available" : "Busy"
            }).ToList();
            return result;
        }

        public static List<RoomWithStatus> GetRoomsWithStatuses(string houseId)
        {
            var roomTypes = RoomTypeService.GetRoomTypes(houseId);
            var rooms = SqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.StoredProcedure, SP_ROOM_GETWITHSTATUS,
                 new[]
                {
                    new SqlParameter("@houseId", houseId),
                }, ParseRoomWithStatus);
            foreach (var r in rooms)
            {
                r.RoomType = roomTypes.Find(type => r.RoomType.Id.Equals(type.Id));
            }
            return rooms;
        }

        public static Room Add(Room room)
        {
            var rooms = GetRooms(room.HouseId);
            if (rooms.Any(r => r.RoomNumber == room.RoomNumber))
                throw new Exception("There is already a room with same number. Please choose a difference number");

            var id = IdHelper.Generate();
            room.Id = "r-" + id;
            SqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.StoredProcedure, SP_ROOM_INSERT,
                new []
                {
                    new SqlParameter("@id", room.Id),                    
                    new SqlParameter("@houseId", room.HouseId),
                    new SqlParameter("@roomNumber", room.RoomNumber),
                    new SqlParameter("@roomTypeId", room.RoomType.Id)                   
                });
            return room;
        }

        public static void Update(Room room)
        {                     
            SqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.StoredProcedure, SP_ROOM_UPDATE,
                new[]
                {
                    new SqlParameter("@id", room.Id),
                    new SqlParameter("@houseId", room.HouseId),
                    new SqlParameter("@roomNumber", room.RoomNumber),
                    new SqlParameter("@roomTypeId", room.RoomType.Id)
                });
        }

        public static void Delete(string id)
        {
            SqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.StoredProcedure, SP_ROOM_DELETE,
                  new[]
                  {
                    new SqlParameter("@id", id),
                  });
        }

        private static List<Room> Parse(IDataReader reader)
        {
            var result = new List<Room>();
            while (reader.Read())
            {
                var item = new Room
                {
                    Id = reader["Id"].ToString(),
                    HouseId = reader["HouseId"].ToString(),
                    RoomNumber = reader["RoomNumber"].ToString(),
                    RoomType = new RoomType
                    {
                        Id=reader["RoomTypeId"].ToString(),
                        HouseId = reader["HouseId"].ToString(),
                        Name = reader["RoomTypeName"].ToString(),
                        Price = reader["RoomTypePrice"].ToString(),
                        Unit = reader["RoomTypeUnit"].ToString()
                    }
                };
                result.Add(item);
            }
            return result;
        }

        private static List<RoomWithStatus> ParseRoomWithStatus(IDataReader reader)
        {
            var result = new List<RoomWithStatus>();
            while (reader.Read())
            {
                var item = new RoomWithStatus
                {
                    Id = reader["Id"].ToString(),
                    HouseId = reader["HouseId"].ToString(),
                    RoomNumber = reader["RoomNumber"].ToString(),
                    RoomType = new RoomType
                    {
                        Id = reader["RoomTypeId"].ToString(),
                    },
                    Status = reader["Status"].ToString()
                };
                result.Add(item);
            }
            return result;
        } 
    }
}
