using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LotusInn.Management.Core;
using LotusInn.Management.Model;

namespace LotusInn.Management.Data
{
    public class RoomDataAdapter : IObjectDataAdapter<Room>
    {
        private const string SP_ROOM_GETALL = "RoomGetAll";
        private const string SP_ROOM_GETBYID = "RoomGetById";
        private const string SP_ROOM_INSERT = "RoomInsert";
        private const string SP_ROOM_UPDATE = "RoomUpdate";
        private const string SP_ROOM_DELETE = "RoomDelete";
        private const string SP_ROOM_GETAVAILABLE = "RoomGetAvailable";
        private const string SP_ROOM_GETWITHSTATUS = "RoomSelectWithStatus";

        public Room Insert(Room @object)
        {
            @object.Id = IdHelper.Generate();
            
            SqlHelper.ExecuteNonQuery(SP_ROOM_INSERT, ConvertToParams(@object));

            return @object;
        }

        public void Update(Room @object)
        {
            SqlHelper.ExecuteNonQuery(SP_ROOM_UPDATE, ConvertToParams(@object));
        }

        public void Delete(string id)
        {
            SqlHelper.ExecuteNonQuery(SP_ROOM_DELETE,
                  new[]
                  {
                    new SqlParameter("@id", id),
                  });
        }

        public Room GetById(string id)
        {
            var list = SqlHelper.ExecuteReader(SP_ROOM_GETBYID,
                new[]
                {
                    new SqlParameter("@id", id),
                }, Read);
            if (list.Count != 1) throw new Exception($"Cannot find room with id='{id}'");
            return list[0];
        }

        public List<Room> GetAll(string houseId)
        {
            return SqlHelper.ExecuteReader(SP_ROOM_GETALL,
                 new[]
                {
                    new SqlParameter("@houseId", houseId),
                }, Read);
        }

        public List<RoomWithStatus> GetRoomsStatus(string houseId, DateTime startDate, DateTime endDate)
        {
            var availableRooms = SqlHelper.ExecuteReader(SP_ROOM_GETAVAILABLE,
                 new[]
                {
                    new SqlParameter("@houseId", houseId),
                    new SqlParameter("@startDate", startDate),
                    new SqlParameter("@endDate", endDate),
                }, Read);

            var allRooms = GetAll(houseId);
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

        public List<RoomWithStatus> GetRoomWithStatuses(string houseId)
        {
            var rooms = SqlHelper.ExecuteReader(SP_ROOM_GETWITHSTATUS,
                new[]
                {
                    new SqlParameter("@houseId", houseId),
                }, ParseRoomWithStatus);
            return rooms;
        } 

        private List<Room> Read(IDataReader reader)
        {
            var result = new List<Room>();
            var rtDA = new RoomTypeDataAdapter();
            while (reader.Read())
            {
                var item = new Room
                {
                    Id = reader["Id"].ToString(),
                    HouseId = reader["HouseId"].ToString(),
                    RoomNumber = reader["RoomNumber"].ToString(),
                    RoomType = new RoomType
                    {
                        Id = reader["RoomTypeId"].ToString(),
                    }
                };
                item.RoomType = rtDA.GetById(item.RoomType.Id);
                result.Add(item);
            }
            return result;
        }

        private List<RoomWithStatus> ParseRoomWithStatus(IDataReader reader)
        {
            var result = new List<RoomWithStatus>();
            var rtDA = new RoomTypeDataAdapter();
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
                item.RoomType = rtDA.GetById(item.RoomType.Id);
                result.Add(item);
            }
            return result;
        }

        private SqlParameter[] ConvertToParams(Room room)
        {
            return new[]
            {
                new SqlParameter("@id", room.Id),
                new SqlParameter("@houseId", room.HouseId),
                new SqlParameter("@roomNumber", room.RoomNumber),
                new SqlParameter("@roomTypeId", room.RoomType.Id)
            };
        }
    }
}
