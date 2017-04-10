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
    public class WarehouseService
    {
        private const string SP_WAREHOUSE_INSERT = "WarehouseInsert";
        private const string SP_WAREHOUSE_UPDATE = "WarehouseUpdate";
        private const string SP_WAREHOUSE_DELETE = "WarehouseDelete";
        private const string SP_WAREHOUSE_GETALL = "WarehouseGetAll";

        private const string SP_WAREHOUSEITEM_SELECT = "WarehouseItemSelect";
        private const string SP_WAREHOUSEITEM_GETBYID = "WarehouseItemGetById";
        private const string SP_WAREHOUSEITEM_INSERT = "WarehouseItemInsert";
        private const string SP_WAREHOUSEITEM_UPDATE = "WarehouseItemUpdate";
        private const string SP_WAREHOUSEITEM_DELETE = "WarehouseItemDelete";

        private const string SP_WAREHOUSEACTIVITY_SELECT = "WarehouseActivitySelect";
        private const string SP_WAREHOUSEACTIVITY_INSERT = "WarehouseActivityInsert";
        private const string SP_WAREHOUSEACTIVITY_UPDATE = "WarehouseActivityUpdate";
        private const string SP_WAREHOUSEACTIVITY_DELETE = "WarehouseActivityDelete";

        public static List<Warehouse> GetAll()
        {
            return SqlHelper.ExecuteReader(SP_WAREHOUSE_GETALL, null, reader =>
            {
                var list = new List<Warehouse>();
                while (reader.Read())
                {
                    var item = new Warehouse
                    {
                        Id = reader["Id"].ToString(),
                        Manager = reader["Manager"].ToString(),
                        Name = reader["Name"].ToString()
                    };
                    list.Add(item);
                }
                return list;
            });
        }

        public static Warehouse Insert(Warehouse warehouse)
        {
            var id = "w-" + IdHelper.Generate();
            warehouse.Id = id;

            var param = new[]
            {
                new SqlParameter("@id", warehouse.Id),
                new SqlParameter("@name", warehouse.Name),
                new SqlParameter("@manager", warehouse.Manager)
            };

            SqlHelper.ExecuteNonQuery(SP_WAREHOUSE_INSERT, param);

            return warehouse;
        }

        public static void Update(Warehouse warehouse)
        {
            var param = new[]
            {
                new SqlParameter("@id", warehouse.Id),
                new SqlParameter("@name", warehouse.Name),
                new SqlParameter("@manager", warehouse.Manager)
            };

            SqlHelper.ExecuteNonQuery(SP_WAREHOUSE_UPDATE, param);            
        }

        public static void Delete(string id)
        {
            var param = new[]
            {
                new SqlParameter("@id", id),
            };

            SqlHelper.ExecuteNonQuery(SP_WAREHOUSE_DELETE, param);
        }

        public static PaginationResult<WarehouseItem> GetWarehouseItems(string warehouseId, int pageIndex, int pageSize)
        {
            var param = new[]
            {
                new SqlParameter("@warehouseId", warehouseId),
                new SqlParameter("@pageIndex", pageIndex),
                new SqlParameter("@pageSize", pageSize),
            };
            return SqlHelper.ExecuteReader(SP_WAREHOUSEITEM_SELECT, param, reader =>
            {
                var result = new PaginationResult<WarehouseItem>
                {
                    Data = new List<WarehouseItem>(),
                    Total = 0
                };
                while (reader.Read())
                {
                    var item = ReadWareHouseItem(reader);
                    result.Data.Add(item);
                }
                reader.NextResult();
                reader.Read();
                result.Total = Convert.ToInt32(reader["Total"].ToString());

                return result;
            });
        }

        public static PaginationResult<WarehouseActivity> GetWarehouseActivity(string warehouseId, int pageIndex, int pageSize)
        {
            var param = new[]
            {
                new SqlParameter("@warehouseId", warehouseId),
                new SqlParameter("@pageIndex", pageIndex),
                new SqlParameter("@pageSize", pageSize),
            };
            return SqlHelper.ExecuteReader(SP_WAREHOUSEACTIVITY_SELECT, param, reader =>
            {
                var result = new PaginationResult<WarehouseActivity>
                {
                    Data = new List<WarehouseActivity>(),
                    Total = 0
                };
                while (reader.Read())
                {
                    var item = new WarehouseActivity
                    {
                        Id = reader["Id"].ToString(),
                        WarehouseId = warehouseId,
                        Date = Convert.ToDateTime(reader["Date"]),
                        Description = reader["Description"].ToString(),
                        Name = reader["Name"].ToString(),
                        UserId = reader["UserId"].ToString(),
                    };
                    result.Data.Add(item);
                }
                reader.NextResult();
                reader.Read();
                result.Total = Convert.ToInt32(reader["Total"].ToString());

                return result;
            });
        }

        public static WarehouseItem GetItem(string id)
        {
            var param = new[]
                {
                    new SqlParameter("@id", id),
                };
            return SqlHelper.ExecuteReader(SP_WAREHOUSEITEM_GETBYID, param, reader =>
            {
                reader.Read();
                return ReadWareHouseItem(reader);
            });
        }

        private static WarehouseItem ReadWareHouseItem(SqlDataReader reader)
        {
            var item = new WarehouseItem
            {
                Id = reader["Id"].ToString(),
                WarehouseId = reader["WarehouseId"].ToString(),
                Condition = reader["Condition"].ToString(),
                Count = Convert.ToInt32(reader["Count"].ToString()),
                Name = reader["Name"].ToString(),
                Description = reader["Description"].ToString()
            };
            return item;
        }

        public static void DeleteWarehouseItem(string id, string userId)
        {
            var item = GetItem(id);
            SqlHelper.StartTransaction(ConfigManager.ConnectionString, (connection, transaction) =>
            {
                var param = new[]
                {
                    new SqlParameter("@id", id),
                };
                SqlHelper.ExecuteCommand(connection, transaction, CommandType.StoredProcedure, SP_WAREHOUSEITEM_DELETE, param);

                var activity = new WarehouseActivity
                {
                    Id = "w-" + IdHelper.Generate(),
                    Date = DateTime.Now,
                    Name = "Delete item '" + item.Name + "'",
                    Description = $"Remove item with Id: {item.Id}; Name: {item.Name} from warehouse",
                    WarehouseId = item.WarehouseId,
                    UserId = userId
                };

                param = new[]
                {
                    new SqlParameter("@id", activity.Id),
                    new SqlParameter("@warehouseId", activity.WarehouseId),
                    new SqlParameter("@name", activity.Name),
                    new SqlParameter("@description", activity.Description),
                    new SqlParameter("@userId", activity.UserId),
                };
                SqlHelper.ExecuteCommand(connection, transaction, CommandType.StoredProcedure, SP_WAREHOUSEACTIVITY_INSERT, param);
            });
        }

        public static WarehouseItem InsertWarehouseItem(WarehouseItem item, string userId)
        {
            item.Id = "w-" +IdHelper.Generate();
            SqlHelper.StartTransaction(ConfigManager.ConnectionString, (connection, transaction) =>
            {
                var param = new[]
                {
                    new SqlParameter("@id", item.Id),
                    new SqlParameter("@warehouseId", item.WarehouseId),
                    new SqlParameter("@condition", item.Condition),
                    new SqlParameter("@count", item.Count),
                    new SqlParameter("@name", item.Name),
                    new SqlParameter("@description", item.Description),
                };

                SqlHelper.ExecuteCommand(connection, transaction, CommandType.StoredProcedure, SP_WAREHOUSEITEM_INSERT, param);

                var activity = new WarehouseActivity
                {
                    Id = "w-" + IdHelper.Generate(),
                    Date = DateTime.Now,
                    Name = "Insert item '" + item.Name + "'",
                    Description = $"Adding {item.Count} items '{item.Name}' in {item.Condition} condition",
                    WarehouseId = item.WarehouseId,
                    UserId = userId
                };

                param = new[]
                {
                    new SqlParameter("@id", activity.Id),
                    new SqlParameter("@warehouseId", activity.WarehouseId),
                    new SqlParameter("@name", activity.Name),
                    new SqlParameter("@description", activity.Description),
                    new SqlParameter("@userId", activity.UserId),
                };
                SqlHelper.ExecuteCommand(connection, transaction, CommandType.StoredProcedure, SP_WAREHOUSEACTIVITY_INSERT, param);
            });
            

            return item;
        }

        public static void UpdateWarehouseItem(WarehouseItem item)
        {
            var param = new[]
            {
                new SqlParameter("@id", item.Id),
                new SqlParameter("@warehouseId", item.WarehouseId),
                new SqlParameter("@condition", item.Condition),
                new SqlParameter("@count", item.Count),
                new SqlParameter("@name", item.Name),
                new SqlParameter("@description", item.Description),
            };

            SqlHelper.ExecuteNonQuery(SP_WAREHOUSEITEM_UPDATE, param);
        }
    }
}
