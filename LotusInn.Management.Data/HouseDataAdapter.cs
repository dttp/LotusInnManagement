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
    public class HouseDataAdapter
    {
        private const string SP_GET_HOUSES = "HouseGetAll";
        private const string SP_GET_BY_ID = "HouseGetById";
        private const string SP_ADD_NEW_HOUSE = "HouseInsert";
        private const string SP_UPDATE_HOUSE = "HouseUpdate";
        private const string SP_DELETE_HOUSE = "HouseDelete";

        public House Insert(House house)
        {
            house.Id = "h-" + IdHelper.Generate();
            SqlHelper.ExecuteNonQuery(SP_ADD_NEW_HOUSE,
                new[]
                {
                    new SqlParameter("@id", house.Id),
                    new SqlParameter("@name", house.Name),
                    new SqlParameter("@address", house.Address),
                });
            return house;
        }

        public void Update(House house)
        {
            SqlHelper.ExecuteNonQuery(SP_UPDATE_HOUSE,
                new[]
                {
                    new SqlParameter("@id", house.Id),
                    new SqlParameter("@name", house.Name),
                    new SqlParameter("@address", house.Address),
                });
        }

        public void Delete(string id)
        {
            SqlHelper.ExecuteNonQuery(SP_DELETE_HOUSE,
                new[]
                {
                    new SqlParameter("@id", id)
                });
        }

        public House GetById(string id)
        {
            var list = SqlHelper.ExecuteReader(SP_GET_BY_ID,
                new[]
                {
                    new SqlParameter("@id", id),
                }, Read);
            if (list == null || list.Count == 0)
                throw new Exception("Cannot find house with id '" + id);
            return list[0];
        }

        public List<House> GetHouses(string id)
        {
            return SqlHelper.ExecuteReader(SP_GET_HOUSES, new[]
            {
                new SqlParameter("@houseId", id),
            }, Read);
        } 

        private List<House> Read(IDataReader reader)
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
