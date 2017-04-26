using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LotusInn.Management.Core;
using LotusInn.Management.Data;
using LotusInn.Management.Model;

namespace LotusInn.Management.Services
{
    public class RoomTypeService
    {
        public static RoomTypeDataAdapter _adapter = new RoomTypeDataAdapter();

        public static List<RoomType> GetRoomTypes(string houseId)
        {
            return _adapter.GetByHouse(houseId);
        }

        public static RoomType GetById(string id)
        {
            return _adapter.GetById(id);
        }

        public static RoomType Add(RoomType roomType)
        {
            return  _adapter.Insert(roomType);
        }

        public static void Update(RoomType roomType)
        {                     
            _adapter.Update(roomType);
        }

        public static void Delete(string id)
        {
            _adapter.Delete(id);
        }
    }
}
