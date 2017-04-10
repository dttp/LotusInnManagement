using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using LotusInn.Management.Core;
using LotusInn.Management.Model;

namespace LotusInn.Management.Services
{
    public class CustomerService
    {
        private static string SP_CUSTOMER_SELECT = "CustomerSelect";
        private static string SP_CUSTOMER_GETBYID = "CustomerGetById";
        private static string SP_CUSTOMER_INSERT = "CustomerInsert";
        private static string SP_CUSTOMER_UPDATE = "CustomerUpdate";
        private static string SP_CUSTOMER_DELETE = "CustomerDelete";
        private static string SP_CUSTOMER_GETCOUNT = "CustomerGetCount";
        private static string SP_CUSTOMER_SELECT_CHECKOUT_BY_DATE = "CustomerSelectCheckoutByDate";
        private static string SP_CUSTOMER_SELECT_ACTIVE = "CustomerSelectActive";
        private static string SP_CUSTOMER_SELECT_RESERVED = "CustomerSelectReserved";
        private static string SP_CUSTOMER_ISUSINGINORDER = "CustomerIsUsingInOrder";

        public static PaginationResult<Customer> GetCustomers(string houseId, string name = "", string email = "", string passportOrId = "", int pageSize = 30, int pageIdx = 1)
        {
            return SqlHelper.ExecuteReader(SP_CUSTOMER_SELECT, new []
                {
                    new SqlParameter("@name", name),
                    new SqlParameter("@email", email),
                    new SqlParameter("@passportOrId", passportOrId),
                    new SqlParameter("@pageSize", pageSize),
                    new SqlParameter("@pageIndex", pageIdx),
                    new SqlParameter("@houseId", houseId), 
                }, Parse);
        }

        public static PaginationResult<ActiveCustomer> GetActiveCustomers(string houseId, string name = "",
            string email = "", string room = "", string passportOrId = "", int pageSize = 30, int pageIdx = 1)
        {
            var list = SqlHelper.ExecuteReader(SP_CUSTOMER_SELECT_ACTIVE, new[]
                {
                    new SqlParameter("@name", name),
                    new SqlParameter("@email", email),
                    new SqlParameter("@passportOrId", passportOrId),
                    new SqlParameter("@room", room),
                    new SqlParameter("@pageSize", pageSize),
                    new SqlParameter("@pageIndex", pageIdx),
                    new SqlParameter("@houseId", houseId),
                }, (reader) => ParseCustomerList(houseId, reader));
            
            return list;
        }

        public static PaginationResult<ActiveCustomer> GetReservedCustomers(string houseId, string name = "",
            string email = "", string room = "", string passportOrId = "", int pageSize = 30, int pageIdx = 1)
        {
            return SqlHelper.ExecuteReader(SP_CUSTOMER_SELECT_RESERVED, new[]
                {
                    new SqlParameter("@name", name),
                    new SqlParameter("@email", email),
                    new SqlParameter("@passportOrId", passportOrId),
                    new SqlParameter("@room", room),
                    new SqlParameter("@pageSize", pageSize),
                    new SqlParameter("@pageIndex", pageIdx),
                    new SqlParameter("@houseId", houseId),
                }, (reader) => ParseCustomerList(houseId, reader));
        }

        private static PaginationResult<ActiveCustomer> ParseCustomerList(string houseId, IDataReader reader)
        {
            var result = new PaginationResult<ActiveCustomer>()
            {
                Data = new List<ActiveCustomer>()
            };
            var allRooms = RoomService.GetRooms(houseId);

            while (reader.Read())
            {

                var item = new ActiveCustomer
                {
                    Id = reader["Id"].ToString(),
                    Address = reader["Address"]?.ToString() ?? "",
                    Country = reader["Country"]?.ToString() ?? "",
                    Email = reader["Email"]?.ToString() ?? "",
                    Name = reader["Name"].ToString(),
                    Phone = reader["Phone"]?.ToString() ?? "",
                    PassportOrId = reader["passportOrId"].ToString(),
                    OrderId = reader["OrderId"].ToString(),
                    CheckinDate = DateTime.Parse(reader["CheckinDate"].ToString()),
                    CheckoutDate = DateTime.Parse(reader["CheckoutDate"].ToString()),
                    Rooms = new List<Room>()
                };

                result.Data.Add(item);
            }
            reader.NextResult();
            reader.Read();
            var total = int.Parse(reader["Total"].ToString());
            result.Total = total;

            reader.NextResult();
            var roomMap = new Dictionary<string, List<string>>();
            while (reader.Read())
            {
                var orderId = reader["OrderId"].ToString();
                var roomId = reader["RoomId"].ToString();
                if (!roomMap.ContainsKey(orderId))
                {
                    roomMap.Add(orderId, new List<string>());
                }
                roomMap[orderId].Add(roomId);
            }

            foreach (var item in result.Data)
            {
                foreach (var roomId in roomMap[item.OrderId])
                {
                    var room = allRooms.Single(r => r.Id.Equals(roomId));
                    item.Rooms.Add(room);
                }
            }

            return result;
        } 

        private static PaginationResult<Customer> Parse(IDataReader reader)
        {
            var result = new PaginationResult<Customer>()
            {
                Data = new List<Customer>()
            };
            while (reader.Read())
            {
                var item = ReadCustomer(reader);
                result.Data.Add(item);
            }
            reader.NextResult();
            reader.Read();
            var total = int.Parse(reader["Total"].ToString());
            result.Total = total;
            return result;
        }

        private static Customer ReadCustomer(IDataReader reader)
        {
            var item = new Customer
            {
                Id = reader["Id"].ToString(),
                Address = reader["Address"]?.ToString() ?? "",
                Country = reader["Country"]?.ToString() ?? "",
                Email = reader["Email"]?.ToString() ?? "",
                Name = reader["Name"].ToString(),
                Phone = reader["Phone"]?.ToString() ?? "",
                PassportOrId = reader["passportOrId"].ToString()
            };
            return item;
        }

        public static Customer GetById(string id)
        {
            var list = SqlHelper.ExecuteReader(SP_CUSTOMER_GETBYID, new []
                {
                    new SqlParameter("@id", id), 
                }, reader =>
                {
                    var result = new List<Customer>();
                    while (reader.Read())
                    {
                        result.Add(ReadCustomer(reader));
                    }
                    return result;
                });
            if (list.Count != 1) throw new Exception($"Cannot find customer with id = '{id}'");
            return list[0];
        }

        public static int CountByHouse(string houseId)
        {
            return SqlHelper.ExecuteScalar<int>(ConfigManager.ConnectionString, CommandType.StoredProcedure,
                SP_CUSTOMER_GETCOUNT, new[]
                {
                    new SqlParameter("@houseId", houseId)
                });
        }

        public static List<ActiveCustomer> GetCustomerCheckoutByDate(string houseId, DateTime date)
        {
            return SqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.StoredProcedure,
                SP_CUSTOMER_SELECT_CHECKOUT_BY_DATE,
                new[]
                {
                    new SqlParameter("@houseId", houseId), 
                    new SqlParameter("@date", date)
                }, reader =>
                {
                    var list = new List<ActiveCustomer>();
                    var allRooms = RoomService.GetRooms(houseId);
                    while (reader.Read())
                    {
                        var item = new ActiveCustomer
                        {
                            Id = reader["Id"].ToString(),
                            Address = reader["Address"]?.ToString() ?? "",
                            Country = reader["Country"]?.ToString() ?? "",
                            Email = reader["Email"]?.ToString() ?? "",
                            Name = reader["Name"].ToString(),
                            Phone = reader["Phone"]?.ToString() ?? "",
                            PassportOrId = reader["passportOrId"].ToString(),
                            Rooms = new List<Room>(),
                            OrderId = reader["OrderId"].ToString(),
                            CheckinDate = DateTime.Parse(reader["CheckinDate"].ToString()),
                            CheckoutDate = DateTime.Parse(reader["CheckoutDate"].ToString())
                        };
                        var roomId = reader["RoomId"].ToString();
                        var activeItem = list.FirstOrDefault(c => c.Id == item.Id);
                        if (activeItem == null)
                        {
                            item.Rooms.Add(allRooms.Single(r => r.Id == roomId));
                            list.Add(item);
                        }
                        else
                        {
                            activeItem.Rooms.Add(allRooms.Single(r => r.Id == roomId));
                        }                        
                    }

                    return list;
                });
        }

        public static bool IsUsingInOrder(string id)
        {
            var customerId = SqlHelper.ExecuteScalar<string>(ConfigManager.ConnectionString, CommandType.StoredProcedure,
                SP_CUSTOMER_ISUSINGINORDER, new[] {new SqlParameter("@id", id)});
            if (customerId == id) return true;
            return false;
        }

        public static void Delete(string id)
        {
            SqlHelper.ExecuteNonQuery(SP_CUSTOMER_DELETE, new []{new SqlParameter("@id", id) });
        }

        public static Customer Insert(Customer customer)
        {
            var id = IdHelper.Generate();
            customer.Id = "c-" + id;
            SqlHelper.ExecuteNonQuery(SP_CUSTOMER_INSERT, 
                new []
                {
                    new SqlParameter("@id", customer.Id),
                    new SqlParameter("@name", customer.Name),
                    new SqlParameter("@phone", customer.Phone),
                    new SqlParameter("@email", customer.Email),
                    new SqlParameter("@address", customer.Address),
                    new SqlParameter("@country", customer.Country),
                    new SqlParameter("@passportOrId", customer.PassportOrId),
                });
            return customer;
        }

        public static void Update(Customer customer)
        {
            SqlHelper.ExecuteNonQuery(SP_CUSTOMER_UPDATE,
                 new[]
                 {
                    new SqlParameter("@id", customer.Id),
                    new SqlParameter("@name", customer.Name),
                    new SqlParameter("@phone", customer.Phone),
                    new SqlParameter("@email", customer.Email),
                    new SqlParameter("@address", customer.Address),
                    new SqlParameter("@country", customer.Country),
                    new SqlParameter("@passportOrId", customer.PassportOrId),
                 });
        }
    }
}
