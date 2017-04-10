using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LotusInn.Management.Core;
using LotusInn.Management.Model;

namespace LotusInn.Management.Services
{
    public class RoomTypeService
    {
        private const string SP_GET_ROOMTYPES = "RoomTypeGetAll";
        private const string SP_GET_BY_ID = "RoomTypeGetById";
        private const string SP_INSERT_ROOMTYPE = "RoomTypeInsert";
        private const string SP_UPDATE_ROOMTYPE = "RoomTYpeUpdate";
        private const string SP_DELETE_ROOMTYPE = "RoomTypeDelete";

        public static List<RoomType> GetRoomTypes(string houseId)
        {
            return SqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.StoredProcedure, SP_GET_ROOMTYPES,
                 new[]
                {
                    new SqlParameter("@houseId", houseId),
                }, Parse);
        }

        public static RoomType GetById(string id)
        {
            var list = SqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.StoredProcedure, SP_GET_BY_ID,
                new []
                {
                    new SqlParameter("@id", id), 
                }, Parse);
            if (list.Count != 1) throw new Exception($"Cannot find Room Type with id = '{id}'");
            return list[0];
        }

        public static RoomType Add(RoomType roomType)
        {
            var id = IdHelper.Generate();
            roomType.Id = "t-" + id;
            SqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.StoredProcedure, SP_INSERT_ROOMTYPE,
                new []
                {
                    new SqlParameter("@id", roomType.Id),
                    new SqlParameter("@name", roomType.Name),
                    new SqlParameter("@houseId", roomType.HouseId),
                    new SqlParameter("@price", roomType.Price),
                    new SqlParameter("@unit", roomType.Unit),
                    new SqlParameter("@pricePerWeek", roomType.PricePerNight),
                    new SqlParameter("@unitPerWeek", roomType.UnitPerWeek),
                    new SqlParameter("@pricePerNight", roomType.PricePerNight),
                    new SqlParameter("@unitPerNight", roomType.UnitPerNight),
                    new SqlParameter("@square", roomType.Square),
                });
            return roomType;
        }

        public static void Update(RoomType roomType)
        {                     
            SqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.StoredProcedure, SP_UPDATE_ROOMTYPE,
                new[]
                {
                    new SqlParameter("@id", roomType.Id),
                    new SqlParameter("@name", roomType.Name),
                    new SqlParameter("@houseId", roomType.HouseId),
                    new SqlParameter("@price", roomType.Price),
                    new SqlParameter("@unit", roomType.Unit),
                    new SqlParameter("@pricePerWeek", roomType.PricePerNight),
                    new SqlParameter("@unitPerWeek", roomType.UnitPerWeek),
                    new SqlParameter("@pricePerNight", roomType.PricePerNight),
                    new SqlParameter("@unitPerNight", roomType.UnitPerNight),
                    new SqlParameter("@square", roomType.Square),
                });
        }

        public static void Delete(string id)
        {
            SqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.StoredProcedure, SP_DELETE_ROOMTYPE,
                  new[]
                  {
                    new SqlParameter("@id", id),
                  });
        }

        private static List<RoomType> Parse(IDataReader reader)
        {
            var result = new List<RoomType>();
            while (reader.Read())
            {
                var item = new RoomType
                {
                    Id = reader["Id"].ToString(),
                    HouseId = reader["HouseId"].ToString(),
                    Name = reader["Name"].ToString(),
                    Price = reader["Price"].ToString(),
                    Unit = reader["Unit"].ToString(),
                    PricePerWeek = reader["PricePerWeek"].ToString(),
                    UnitPerWeek = reader["UnitPerWeek"].ToString(),
                    PricePerNight = reader["PricePerNight"].ToString(),
                    UnitPerNight = reader["UnitPerNight"].ToString(),
                    Square = reader["Square"].ToString()
                };
                result.Add(item);
            }
            return result;
        }
    }
}
