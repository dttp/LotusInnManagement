using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Configuration;
using LotusInn.Management.Core;
using LotusInn.Management.Model;

namespace LotusInn.Management.Services
{
    public class OrderService
    {
        private static string SP_CUSTOMER_GETALL = "CustomerGetAll";
        private static string SP_CUSTOMER_GETBYID = "CustomerGetById";
        private static string SP_CUSTOMER_INSERT = "CustomerInsert";
        private static string SP_CUSTOMER_UPDATE = "CustomerUpdate";
        private static string SP_CUSTOMER_DELETE = "CustomerDelete";

        private static string SP_ORDER_INSERT = "OrderInsert";        
        private static string SP_ORDER_GET_BY_ID = "OrderGetById";

        private static string SP_ORDER_ROOM_INSERT = "OrderRoomInsert";
        private static string SP_ORDER_CUSTOMER_INSERT = "OrderCustomerInsert";
        private static string SP_ORDER_PAYMENT_INSERT = "OrderPaymentInsert";
        private static string SP_DEPOSIT_INSERT = "DepositInsert";
        private static string SP_DEPOSIT_UPDATE = "DepositUpdate";
        private static string SP_DISCOUNT_INSERT = "DiscountInsert";
        private static string SP_DISCOUNT_DELETE = "DiscountDelete";
        private static string SP_DISCOUNT_UPDATE = "DiscountUpdate";
        private static string SP_PAYMENT_INSERT = "PaymentInsert";

        private static string SP_ORDER_UPDATE = "OrderUpdate";
        private static string SP_PAYMENT_UPDATE = "PaymentUpdate";
        private static string SP_PAYMENT_DELETE = "PaymentDelete";

        private static string SP_ORDER_DELETE = "OrderDelete";

        private static string SP_ORDER_SELECT_IN_RANGE = "OrderSelectInRange";

        private static SqlParameter[] CreateParams(BookingOrder order)
        {
            return new[]
            {
                new SqlParameter("@id", order.Id),
                new SqlParameter("@note", order.Note),
                new SqlParameter("@checkinDate", order.CheckinDate),
                new SqlParameter("@checkoutDate", order.CheckOutDate),
                new SqlParameter("@totalGuest", order.TotalGuest),
                new SqlParameter("@stayLength", order.StayLength),
                new SqlParameter("@status", order.Status),
                new SqlParameter("@houseId", order.HouseId),
                new SqlParameter("@paymentCycle", order.PaymentCycle.ToString()),
            };
        }

        private static SqlParameter[] CreateParams(Customer customer)
        {
            return new[]
            {
                new SqlParameter("@id", customer.Id),
                new SqlParameter("@name", customer.Name),
                new SqlParameter("@phone", customer.Phone),
                new SqlParameter("@email", customer.Email),
                new SqlParameter("@address", customer.Address),
                new SqlParameter("@country", customer.Country),
                new SqlParameter("@passportOrId", customer.PassportOrId),
            };
        }

        private static SqlParameter[] CreateParams(Deposit deposit, string orderId)
        {
            return new[]
            {
                new SqlParameter("@id", deposit.Id),
                new SqlParameter("@orderId", orderId),
                new SqlParameter("@date", deposit.Date),
                new SqlParameter("@amount", deposit.Amount),
                new SqlParameter("@unit", deposit.Unit),
            };
        }

        public static BookingOrder Create(string mode, BookingOrder order)
        {
            var ms = new MoneySourceService();
            var ps = new PaymentRecordService();
            var id = IdHelper.Generate();

            order.Id = "o-" + id;            

            if (order.Deposit == null)
            {
                order.Deposit = new Deposit();
            }
            
            order.Deposit.Id = "d-" + IdHelper.Generate();
            order.Deposit.Date = DateTime.Now;
            order.Deposit.OrderId = order.Id;

            order.TotalGuest = order.Customers.Count;

            order.Status = mode.Equals("Checkin", StringComparison.CurrentCultureIgnoreCase) ? "Active" : "Reserved";


            SqlHelper.StartTransaction(ConfigManager.ConnectionString, (connection, transaction) =>
            {
                // insert order
                SqlHelper.ExecuteCommand(connection, transaction, CommandType.StoredProcedure, SP_ORDER_INSERT, CreateParams(order));

                // insert customer
                foreach (var customer in order.Customers)
                {
                    string spName;
                    if (customer.Id.StartsWith("customer-"))
                    {
                        customer.Id = "c-" + IdHelper.Generate();
                        spName = SP_CUSTOMER_INSERT;
                    }
                    else
                    {
                        spName = SP_CUSTOMER_UPDATE;
                    }

                    SqlHelper.ExecuteCommand(connection, transaction, CommandType.StoredProcedure, spName, CreateParams(customer));
                    SqlHelper.ExecuteCommand(connection,transaction, CommandType.StoredProcedure, SP_ORDER_CUSTOMER_INSERT,
                        new[]
                        {
                            new SqlParameter("@orderId", order.Id),
                            new SqlParameter("@customerId", customer.Id),
                        });
                }                

                // insert room
                foreach (var room in order.Rooms)
                {
                    SqlHelper.ExecuteCommand(connection, transaction,CommandType.StoredProcedure, SP_ORDER_ROOM_INSERT,
                        new []
                        {
                            new SqlParameter("@orderId", order.Id),
                            new SqlParameter("@roomId", room.Id),
                        });
                }
                // insert deposit
                SqlHelper.ExecuteCommand(connection, transaction, CommandType.StoredProcedure, SP_DEPOSIT_INSERT, CreateParams(order.Deposit, order.Id));


                var dict = new Dictionary<string, MoneySource>();
                foreach (var d in order.Discounts)
                {
                    if (!dict.ContainsKey(d.MoneySourceId))
                    {
                        dict.Add(d.MoneySourceId, ms.GetById(d.MoneySourceId));
                    }
                }
                foreach (var p in order.Payments)
                {
                    if (!dict.ContainsKey(p.MoneySourceId))
                    {
                        dict.Add(p.MoneySourceId, ms.GetById(p.MoneySourceId));
                    }
                }

                // insert discount
                foreach (var discount in order.Discounts)
                {
                    discount.Date = DateTime.Now;
                    discount.OrderId = order.Id;
                    discount.Type = "Order-Discount";

                    var source = dict[discount.MoneySourceId];
                    UpdateSource(source, discount);
                    ps.Insert(discount, connection, transaction);
                }

                
                // insert payment
                foreach (var payment in order.Payments)
                {
                    payment.OrderId = order.Id;
                    payment.Date = DateTime.Now;
                    payment.Type = "Order-Payment";
                    var source = dict[payment.MoneySourceId];
                    UpdateSource(source, payment);
                    ps.Insert(payment, connection, transaction);
                }

                foreach (var source in dict.Values)
                {
                    ms.Update(source, connection, transaction);
                }
            });
            
            NotificationService.Insert(order);        

            return order;
        }

        private static void UpdateSource(MoneySource source, PaymentRecord payment)
        {
            var amount = Convert.ToSingle(payment.Amount);
            if (payment.Type == "Order-Discount") amount = -amount;
            if (payment.Unit == "VND")
            {
                source.BalanceVND += amount;
            }
            else
            {
                source.BalanceUSD += amount;
            }
        }

        private static void ReverseSource(MoneySource source, PaymentRecord payment)
        {
            var amount = Convert.ToSingle(payment.Amount);
            if (payment.Type == "Order-Payment") amount = -amount;
            if (payment.Unit == "VND")
            {
                source.BalanceVND += amount;
            }
            else
            {
                source.BalanceUSD += amount;
            }
        }

        private static void UpdatePaymentForOrder(BookingOrder order, BookingOrder originalOrder, SqlConnection connection, SqlTransaction transaction)
        {
            var ms = new MoneySourceService();
            var ps = new PaymentRecordService();

            var msDict = new Dictionary<string, MoneySource>();
            foreach (var discount in order.Discounts)
            {
                if (!msDict.ContainsKey(discount.MoneySourceId))
                    msDict.Add(discount.MoneySourceId, ms.GetById(discount.MoneySourceId));
            }
            foreach (var payment in order.Payments)
            {
                if (!msDict.ContainsKey(payment.MoneySourceId))
                    msDict.Add(payment.MoneySourceId, ms.GetById(payment.MoneySourceId));
            }
            foreach (var discount in originalOrder.Discounts.Where(d => !order.Discounts.Exists(d1 => d1.Id == d.Id)))
            {
                if (!msDict.ContainsKey(discount.MoneySourceId))
                    msDict.Add(discount.MoneySourceId, ms.GetById(discount.MoneySourceId));
            }
            foreach (var payment in originalOrder.Payments.Where(p => !order.Payments.Exists(p1 => p1.Id == p.Id)))
            {
                if (!msDict.ContainsKey(payment.MoneySourceId))
                    msDict.Add(payment.MoneySourceId, ms.GetById(payment.MoneySourceId));
            }

            foreach (var discount in order.Discounts)
            {
                if (discount.Id.StartsWith("discount-"))
                {
                    var id = IdHelper.Generate();
                    discount.Id = "d-" + id;
                    discount.Date = DateTime.Now;
                    discount.OrderId = order.Id;
                    ps.Insert(discount, connection, transaction);
                    UpdateSource(msDict[discount.MoneySourceId], discount);
                }
                else
                {
                    ps.Update(discount, connection, transaction);
                    var source = msDict[discount.MoneySourceId];
                    var oldItem = ps.GetById(discount.Id);
                    ReverseSource(source, oldItem);
                    UpdateSource(source, discount);
                }
            }
            foreach (var discount in originalOrder.Discounts.Where(d => !order.Discounts.Exists(d1 => d1.Id == d.Id)))
            {
                var source = msDict[discount.MoneySourceId];
                ReverseSource(source, discount);
                ps.Delete(discount.Id, connection, transaction);
            }

            // update payment
            foreach (var payment in order.Payments)
            {
                if (payment.Id.StartsWith("payment-"))
                {
                    var id = IdHelper.Generate();
                    payment.Id = "p-" + id;
                    payment.Date = DateTime.Now;
                    payment.OrderId = order.Id;
                    ps.Insert(payment, connection, transaction);
                    UpdateSource(msDict[payment.MoneySourceId], payment);
                }
                else
                {
                    ps.Update(payment, connection, transaction);
                    var source = msDict[payment.MoneySourceId];
                    var oldItem = ps.GetById(payment.Id);
                    ReverseSource(source, oldItem);
                    UpdateSource(source, payment);
                }

            }
            foreach (var payment in originalOrder.Payments.Where(p => !order.Payments.Exists(p1 => p1.Id == p.Id)))
            {
                var source = msDict[payment.MoneySourceId];
                ReverseSource(source, payment);
                ps.Delete(payment.Id, connection, transaction);
            }

            foreach (var source in msDict.Values)
            {
                ms.Update(source, connection, transaction);
            }
        }


        public static void Update(BookingOrder order)
        {
            order.TotalGuest = order.Customers.Count;
            var originalOrder = GetById(order.Id);

            SqlHelper.StartTransaction(ConfigManager.ConnectionString, (connection, transaction) =>
            {
                // update order
                SqlHelper.ExecuteCommand(connection, transaction, CommandType.StoredProcedure, SP_ORDER_UPDATE, CreateParams(order));

                // update customer
                foreach (var customer in order.Customers)
                {
                    string spName;
                    if (customer.Id.StartsWith("customer-"))
                    {
                        customer.Id = "c-" + IdHelper.Generate();
                        spName = SP_CUSTOMER_INSERT;

                        SqlHelper.ExecuteCommand(connection, transaction, CommandType.StoredProcedure, SP_ORDER_CUSTOMER_INSERT,
                        new[]
                        {
                            new SqlParameter("@orderId", order.Id),
                            new SqlParameter("@customerId", customer.Id),
                        });
                    }
                    else
                    {
                        spName = SP_CUSTOMER_UPDATE;
                    }

                    SqlHelper.ExecuteCommand(connection, transaction, CommandType.StoredProcedure, spName,CreateParams(customer));
                }

                // update deposit
                SqlHelper.ExecuteCommand(connection, transaction, CommandType.StoredProcedure, SP_DEPOSIT_UPDATE,
                    new[]
                    {
                        new SqlParameter("@id", order.Deposit.Id),
                        new SqlParameter("@date", order.Deposit.Date),
                        new SqlParameter("@amount", order.Deposit.Amount),
                        new SqlParameter("@unit", order.Deposit.Unit),
                    });

                UpdatePaymentForOrder(order, originalOrder, connection, transaction);

            });

            NotificationService.DeleteByOrderId(order.Id);
            NotificationService.Insert(order);
        }

        public static BookingOrder GetById(string id)
        {
            return SqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.StoredProcedure,
                SP_ORDER_GET_BY_ID,
                new[]
                {
                    new SqlParameter("@id", id)
                }, Parse);
        }

        private static BookingOrder Parse(IDataReader reader)
        {
            var ps = new PaymentRecordService();
            reader.Read();
            var order = new BookingOrder
            {
                Id = reader["Id"].ToString(),
                CheckinDate = DateTime.Parse(reader["CheckinDate"].ToString()),
                CheckOutDate = DateTime.Parse(reader["CheckoutDate"].ToString()),
                Note = reader["Note"]?.ToString() ?? string.Empty,
                Status = reader["Status"].ToString(),
                TotalGuest = int.Parse(reader["TotalGuest"].ToString()),
                StayLength = reader["StayLength"].ToString(),
                HouseId = reader["HouseId"].ToString(),
                PaymentCycle = new PaymentCycle(reader["PaymentCycle"].ToString())
            };

            order.Deposit = new Deposit
            {
                OrderId = order.Id,
                Id = reader["DepositId"].ToString(),
                Amount = reader["DepositAmount"].ToString(),
                Unit = reader["DepositUnit"].ToString(),
                Date = DateTime.Parse(reader["DepositDate"].ToString())
            };

            reader.NextResult();            
            order.Customers = new List<Customer>();
            while (reader.Read())
            {
                var customer = CustomerService.GetById(reader["CustomerId"].ToString());
                order.Customers.Add(customer);
            }

            reader.NextResult();
            order.Rooms = new List<Room>();
            while (reader.Read())
            {
                var room = RoomService.GetById(reader["RoomId"].ToString());
                order.Rooms.Add(room);
            }

            order.Discounts = ps.GetByOrderId(order.Id, "Order-Discount");
            order.Payments = ps.GetByOrderId(order.Id, "Order-Payment");

            return order;
        }

        public static void Checkout(BookingOrder order)
        {
            order.Status = "Completed";
            var originalOrder = GetById(order.Id);
            SqlHelper.StartTransaction(ConfigManager.ConnectionString, (connection, transaction) =>
            {
                // update order
                SqlHelper.ExecuteCommand(connection, transaction, CommandType.StoredProcedure, SP_ORDER_UPDATE, CreateParams(order));

                UpdatePaymentForOrder(order, originalOrder, connection, transaction);
            });

            NotificationService.DeleteByOrderId(order.Id);
        }

        public static void Delete(string id)
        {
            var ms = new MoneySourceService();
            var ps = new PaymentRecordService();

            SqlHelper.StartTransaction(ConfigManager.ConnectionString, (connection, transaction) =>
            {
                var order = GetById(id);

                var msDict = new Dictionary<string, MoneySource>();
                var pmList = new List<PaymentRecord>();
                pmList.AddRange(order.Discounts);
                pmList.AddRange(order.Payments);
                foreach (var item in pmList)
                {
                    if (!msDict.ContainsKey(item.MoneySourceId))
                    {
                        msDict.Add(item.MoneySourceId, ms.GetById(item.MoneySourceId));
                    }
                }

                foreach (var item in pmList)
                {
                    ReverseSource(msDict[item.MoneySourceId], item);
                    ps.Delete(item.Id, connection, transaction);
                }

                foreach (var source in msDict.Values)
                {
                    ms.Update(source, connection, transaction);
                }

                SqlHelper.ExecuteCommand(connection, transaction, CommandType.StoredProcedure, SP_ORDER_DELETE,
                    new[]
                    {
                        new SqlParameter("@id", id),
                    });                                
            });
            
        }

        public static List<BookingOrder> GetOrdersInRange(string houseId, DateTime startDate, DateTime endDate)
        {
            return SqlHelper.ExecuteReader(SP_ORDER_SELECT_IN_RANGE,
                new[]
                {
                    new SqlParameter("@houseId", houseId),
                    new SqlParameter("@startDate", startDate),
                    new SqlParameter("@endDate", endDate),
                }, reader =>
                {
                    var result = new List<BookingOrder>();
                    while (reader.Read())
                    {
                        var order = new BookingOrder
                        {
                            Id = reader["Id"].ToString(),
                            CheckinDate = DateTime.Parse(reader["CheckinDate"].ToString()),
                            CheckOutDate = DateTime.Parse(reader["CheckoutDate"].ToString()),
                            Note = reader["Note"]?.ToString() ?? string.Empty,
                            Status = reader["Status"].ToString(),
                            TotalGuest = int.Parse(reader["TotalGuest"].ToString()),
                            StayLength = reader["StayLength"].ToString(),
                            HouseId = reader["HouseId"].ToString(),
                            Rooms = new List<Room>()
                        };
                        result.Add(order);
                    }
                    reader.NextResult();

                    var mapping = result.ToDictionary(o => o.Id, o => o);
                    var rooms = RoomService.GetRooms(houseId);
                    var roomMapping = rooms.ToDictionary(r => r.Id, r => r);
                    while (reader.Read())
                    {
                        var order = mapping[reader["OrderId"].ToString()];
                        order.Rooms.Add(roomMapping[reader["RoomId"].ToString()]);
                    }

                    return result;
                });
        } 
    }
}
