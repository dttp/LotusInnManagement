using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LotusInn.Management.Core;
using LotusInn.Management.Model;

namespace LotusInn.Management.Services
{
    public class HouseService
    {
        private const string SP_GET_HOUSES = "HouseGetAll";
        private const string SP_GET_BY_ID = "HouseGetById";
        private const string SP_ADD_NEW_HOUSE = "HouseInsert";
        private const string SP_UPDATE_HOUSE = "HouseUpdate";
        private const string SP_DELETE_HOUSE = "HouseDelete";

        public static List<House> GetHouses(string houseId)
        {
            return SqlHelper.ExecuteReader(SP_GET_HOUSES, new []
            {
                new SqlParameter("@houseId", houseId), 
            }, Parse);
        }

        public static House AddHouse(House house)
        {
            var id = "h-" + IdHelper.Generate();
            SqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, CommandType.StoredProcedure,
                SP_ADD_NEW_HOUSE,
                new[]
                {
                    new SqlParameter("@id", id),
                    new SqlParameter("@name", house.Name),
                    new SqlParameter("@address", house.Address),
                });
            house.Id = id;
            return house;
        }

        public static void UpdateHouse(House house)
        {
            SqlHelper.ExecuteNonQuery(SP_UPDATE_HOUSE,
                new[]
                {
                    new SqlParameter("@id", house.Id),
                    new SqlParameter("@name", house.Name),
                    new SqlParameter("@address", house.Address),
                });
        }

        public static void DeleteHouse(string id)
        {
            SqlHelper.ExecuteNonQuery(SP_DELETE_HOUSE,
                new[]
                {
                    new SqlParameter("@id", id)
                });
        }

        public static House GetById(string id)
        {
            var list = SqlHelper.ExecuteReader(SP_GET_BY_ID,
                new[]
                {
                    new SqlParameter("@id", id),
                }, Parse);
            if (list == null || list.Count == 0)
                throw new Exception("Cannot find house with id '" + id);
            return list[0];
        }

        private static List<House> Parse(SqlDataReader reader)
        {
            var result = new List<House>();
            while (reader.Read())
            {
                var house = new House
                {
                    Id = reader["Id"].ToString(),
                    Name = reader["Name"].ToString(),
                    Address = reader["Address"].ToString()
                };

                result.Add(house);
            }
            return result;
        } 
    }
}
