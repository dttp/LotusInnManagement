using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LotusInn.Management.Core;
using LotusInn.Management.Data;
using LotusInn.Management.Model;

namespace LotusInn.Management.Services
{
    public class HouseService
    {

        private static readonly HouseDataAdapter _adapter = new HouseDataAdapter();
        public static List<House> GetHouses(string houseId)
        {
            return _adapter.GetHouses(houseId);
        }

        public static House AddHouse(House house)
        {
            return _adapter.Insert(house);
        }

        public static void UpdateHouse(House house)
        {
            _adapter.Update(house);
        }

        public static void DeleteHouse(string id)
        {
            _adapter.Delete(id);
        }

        public static House GetById(string id)
        {
            return _adapter.GetById(id);
        }

        
    }
}
