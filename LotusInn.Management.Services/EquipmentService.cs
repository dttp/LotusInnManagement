using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LotusInn.Management.Core;
using LotusInn.Management.Model;

namespace LotusInn.Management.Services
{
    public class EquipmentService
    {
        private static string SP_EQUIPMENT_SELECT_ALL = "EquipmentSelectAll";
        private static string SP_EQUIPMENT_GET_BY_ID = "EquipmentGetById";
        private static string SP_EQUIPMENT_INSERT = "EquipmentInsert";
        private static string SP_EQUIPMENT_UPDATE = "EquipmentUpdate";
        private static string SP_EQUIPMENT_DELETE = "EquipmentDelete";

        public static List<Equipment> GetAll(string houseId, string roomId, string name, string category, string status)
        {
            return SqlHelper.ExecuteReader( SP_EQUIPMENT_SELECT_ALL,
                new[]
                {
                    new SqlParameter("@houseId", houseId),
                    new SqlParameter("@roomId", roomId),
                    new SqlParameter("@name", name),
                    new SqlParameter("@category", category),
                    new SqlParameter("@status", status),
                }, Parse);
        }

        public static Equipment GetById(string id)
        {
            var list = SqlHelper.ExecuteReader(SP_EQUIPMENT_GET_BY_ID,
                new[]
                {
                    new SqlParameter("@id", id)                    
                }, Parse);

            if (list.Count != 1)
                throw new Exception($"Cannot find Equipment with id = {id}");

            return list[0];
        }

        public static Equipment Insert(Equipment equipment)
        {
            var id = "e-" + IdHelper.Generate();
            equipment.Id = id;
            equipment.CreatedDate = DateTime.Now;
            SqlHelper.ExecuteNonQuery(SP_EQUIPMENT_INSERT,
                new []
                {
                    new SqlParameter("@id", equipment.Id),
                    new SqlParameter("@name", equipment.Name),
                    new SqlParameter("@category", equipment.Category),
                    new SqlParameter("@houseId", equipment.HouseId),
                    new SqlParameter("@roomId", equipment.RoomId),
                    new SqlParameter("@quantity", equipment.Quantity),
                    new SqlParameter("@status", equipment.Status),
                    new SqlParameter("@price", equipment.Price),
                    new SqlParameter("@unit", equipment.Unit),
                    new SqlParameter("@createdDate", equipment.CreatedDate),
                });
            return equipment;
        }

        public static void Update(Equipment equipment)
        {
            SqlHelper.ExecuteNonQuery(SP_EQUIPMENT_UPDATE,
                new[]
                {
                    new SqlParameter("@id", equipment.Id),
                    new SqlParameter("@name", equipment.Name),
                    new SqlParameter("@category", equipment.Category),
                    new SqlParameter("@houseId", equipment.HouseId),
                    new SqlParameter("@roomId", equipment.RoomId),
                    new SqlParameter("@quantity", equipment.Quantity),
                    new SqlParameter("@status", equipment.Status),
                    new SqlParameter("@price", equipment.Price),
                    new SqlParameter("@unit", equipment.Unit),
                });
        }

        public static void Delete(string id)
        {
            SqlHelper.ExecuteNonQuery(SP_EQUIPMENT_DELETE,
                new[]
                {
                    new SqlParameter("@id", id)                    
                });
        }

        public static void Copy(EquipmentCopyRequest request)
        {
            var list = GetAll(request.FromRoom.HouseId, request.FromRoom.Id, string.Empty, string.Empty, string.Empty);
            foreach (var room in request.Target)
            {
                foreach (var equipment in list)
                {
                    var item = new Equipment
                    {
                        Name = equipment.Name,
                        Category = equipment.Category,
                        HouseId = equipment.HouseId,
                        RoomId = room.Id,
                        Price = equipment.Price,
                        Unit = equipment.Unit,
                        Quantity = equipment.Quantity,
                        Status = equipment.Status
                    };
                    Insert(item);
                }
            }
        }

        private static List<Equipment> Parse(SqlDataReader reader)
        {
            var list = new List<Equipment>();
            while (reader.Read())
            {
                var item = new Equipment
                {
                    Id = reader["Id"].ToString(),
                    Name = reader["Name"].ToString(),
                    HouseId = reader["HouseId"].ToString(),
                    RoomId = reader["RoomId"]?.ToString(),
                    Category = reader["Category"]?.ToString(),
                    Price = reader["Price"].ToString(),
                    Quantity = Convert.ToInt32(reader["Quantity"].ToString()),
                    Status = reader["Status"].ToString(),
                    Unit = reader["Unit"].ToString(),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                };
                list.Add(item);
            }

            return list;
        } 
    }
}
