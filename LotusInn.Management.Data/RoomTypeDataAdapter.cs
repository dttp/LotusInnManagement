using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LotusInn.Management.Core;
using LotusInn.Management.Model;

namespace LotusInn.Management.Data
{
    public class RoomTypeDataAdapter : IObjectDataAdapter<RoomType>
    {
        private const string SP_GET_ROOMTYPES = "RoomTypeGetAll";
        private const string SP_GET_BY_ID = "RoomTypeGetById";
        private const string SP_INSERT_ROOMTYPE = "RoomTypeInsert";
        private const string SP_UPDATE_ROOMTYPE = "RoomTYpeUpdate";
        private const string SP_DELETE_ROOMTYPE = "RoomTypeDelete";
        public RoomType Insert(RoomType @object)
        {
            @object.Id = IdHelper.Generate();
            SqlHelper.ExecuteNonQuery(SP_INSERT_ROOMTYPE, ConvertToParams(@object));
            return @object;
        }

        public void Update(RoomType @object)
        {
            SqlHelper.ExecuteNonQuery(SP_UPDATE_ROOMTYPE, ConvertToParams(@object));
        }

        public void Delete(string id)
        {
            SqlHelper.ExecuteNonQuery(SP_DELETE_ROOMTYPE,
                  new[]
                  {
                    new SqlParameter("@id", id),
                  });
        }

        public RoomType GetById(string id)
        {
            var list = SqlHelper.ExecuteReader(SP_GET_BY_ID,
                new[]
                {
                    new SqlParameter("@id", id),
                }, Read);
            if (list.Count != 1) throw new Exception($"Cannot find Room Type with id = '{id}'");
            return list[0];
        }

        public List<RoomType> GetByHouse(string houseId)
        {
            return SqlHelper.ExecuteReader(SP_GET_ROOMTYPES,
                 new[]
                {
                    new SqlParameter("@houseId", houseId),
                }, Read);
        } 

        private SqlParameter[] ConvertToParams(RoomType roomType)
        {
            return new[]
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
            };
        }

        private static List<RoomType> Read(IDataReader reader)
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
