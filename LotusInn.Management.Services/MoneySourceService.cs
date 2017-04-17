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
    public class MoneySourceService
    {
        private const string SP_MONEYSOURCE_GETALL = "MoneySourceGetAll";
        private const string SP_MONEYSOURCE_GETBYID = "MoneySourceGetById";
        private const string SP_MONEYSOURCE_INSERT = "MoneySourceInsert";
        private const string SP_MONEYSOURCE_UPDATE = "MoneySourceUpdate";
        private const string SP_MONEYSOURCE_DELETE = "MoneySourceDelete";
        public List<MoneySource> GetAll()
        {
            return SqlHelper.ExecuteReader(SP_MONEYSOURCE_GETALL, null, Read);
        }

        public MoneySource Insert(MoneySource moneySource)
        {
            moneySource.Id = "m-" + IdHelper.Generate();
            var param = CreateParams(moneySource);

            SqlHelper.ExecuteNonQuery(SP_MONEYSOURCE_INSERT, param);
            return moneySource;
        }

        public void Update(MoneySource moneySource, SqlConnection con = null, SqlTransaction trans = null)
        {
            var param = CreateParams(moneySource);
            if (con == null)
            {
                SqlHelper.ExecuteNonQuery(SP_MONEYSOURCE_UPDATE, param);
            }
            else
            {
                SqlHelper.ExecuteCommand(con, trans, CommandType.StoredProcedure, SP_MONEYSOURCE_UPDATE, param);
            }            
        }

        private SqlParameter[] CreateParams(MoneySource moneySource)
        {
            var param = new[]
            {
                new SqlParameter("@id", moneySource.Id),
                new SqlParameter("@houseId", moneySource.House?.Id),
                new SqlParameter("@name", moneySource.Name),
                new SqlParameter("@balanceUSD", moneySource.BalanceUSD),
                new SqlParameter("@balanceVND", moneySource.BalanceVND),
                new SqlParameter("@ownerId", moneySource.Owner.Id),
            };
            return param;            
        }

        public void Delete(string id)
        {
            var param = new[]
            {
                new SqlParameter("@id", id),
            };

            SqlHelper.ExecuteNonQuery(SP_MONEYSOURCE_DELETE, param);
        }

        public MoneySource GetById(string id)
        {
            var param = new[]
            {
                new SqlParameter("@id", id),
            };

            return SqlHelper.ExecuteReader(SP_MONEYSOURCE_GETBYID, param, reader => Read(reader).FirstOrDefault());
        }

        public PaymentRecord CreatePayment(PaymentRecord payment, SqlConnection con, SqlTransaction trans)
        {            
            var ps = new PaymentRecordService();

            var source = GetById(payment.MoneySourceId);
            var amount = Convert.ToSingle(payment.Amount);

            if (payment.Type.Equals("Expense", StringComparison.CurrentCultureIgnoreCase) ||
                payment.Type.Equals("Order-Discount", StringComparison.CurrentCultureIgnoreCase))
            {
                amount = -amount;
            }

            if (payment.Unit.Equals("VND"))
            {
                source.BalanceVND += amount;
            }
            else
            {
                source.BalanceUSD += amount;
            }


            if (con != null && trans != null)
            {
                payment = ps.Insert(payment, con, trans);

                var param = CreateParams(source);
                SqlHelper.ExecuteCommand(con, trans, CommandType.StoredProcedure, SP_MONEYSOURCE_UPDATE, param);
            }
            else
            {
                SqlHelper.StartTransaction(ConfigManager.ConnectionString, (connection, transaction) =>
                {
                    payment = ps.Insert(payment, connection, transaction);

                    var param = CreateParams(source);
                    SqlHelper.ExecuteCommand(connection, transaction, CommandType.StoredProcedure, SP_MONEYSOURCE_UPDATE, param);
                });
            }
            return payment;
        }

        public void UpdatePayment(PaymentRecord payment, SqlConnection con, SqlTransaction trans)
        {
            var ps = new PaymentRecordService();

            var source = GetById(payment.MoneySourceId);
            var amount = Convert.ToSingle(payment.Amount);

            var original = ps.GetById(payment.Id);
            var originalAmount = Convert.ToSingle(original.Amount);

            if (payment.Type.Equals("Expense", StringComparison.CurrentCultureIgnoreCase) ||
                payment.Type.Equals("Order-Discount", StringComparison.CurrentCultureIgnoreCase))
            {
                amount = -amount;
            }

            if (original.Type.Equals("Income", StringComparison.CurrentCultureIgnoreCase) ||
                original.Type.Equals("Order-Payment", StringComparison.CurrentCultureIgnoreCase))
            {
                originalAmount = -originalAmount;
            }

            if (payment.Unit.Equals("VND"))
            {
                source.BalanceVND += amount;
            }
            else
            {
                source.BalanceUSD += amount;
            }

            if (original.Unit.Equals("VND"))
            {
                source.BalanceVND += originalAmount;
            }
            else
            {
                source.BalanceUSD += originalAmount;
            }


            if (con != null && trans != null)
            {
                ps.Update(payment, con, trans);

                var param = CreateParams(source);
                SqlHelper.ExecuteCommand(con, trans, CommandType.StoredProcedure, SP_MONEYSOURCE_UPDATE, param);
            }
            else
            {
                SqlHelper.StartTransaction(ConfigManager.ConnectionString, (connection, transaction) =>
                {
                    ps.Update(payment, connection, transaction);

                    var param = CreateParams(source);
                    SqlHelper.ExecuteCommand(connection, transaction, CommandType.StoredProcedure, SP_MONEYSOURCE_UPDATE, param);
                });
            }
        }

        public void DeletePayment(string id, SqlConnection con, SqlTransaction trans)
        {
            var ps = new PaymentRecordService();

            var payment = ps.GetById(id);

            var source = GetById(payment.MoneySourceId);
            var amount = Convert.ToSingle(payment.Amount);

            if (payment.Type.Equals("Income", StringComparison.CurrentCultureIgnoreCase) ||
                payment.Type.Equals("Order-Payment", StringComparison.CurrentCultureIgnoreCase))
            {
                amount = -amount;
            }

            if (payment.Unit.Equals("VND"))
            {
                source.BalanceVND += amount;
            }
            else
            {
                source.BalanceUSD += amount;
            }

            if (con != null && trans != null)
            {
                ps.Delete(id, con, trans);

                var param = CreateParams(source);
                SqlHelper.ExecuteCommand(con, trans, CommandType.StoredProcedure, SP_MONEYSOURCE_UPDATE, param);
            }
            else
            {
                SqlHelper.StartTransaction(ConfigManager.ConnectionString, (connection, transaction) =>
                {
                    ps.Delete(id, connection, transaction);

                    var param = CreateParams(source);
                    SqlHelper.ExecuteCommand(connection, transaction, CommandType.StoredProcedure, SP_MONEYSOURCE_UPDATE, param);
                });
            }
        }

        private List<MoneySource> Read(IDataReader reader)
        {
            var list = new List<MoneySource>();
            while (reader.Read())
            {
                var item = new MoneySource
                {
                    Id = reader["Id"].ToString(),
                    Name = reader["Name"].ToString(),                    
                    BalanceUSD = Convert.ToSingle(reader["BalanceUSD"]),
                    BalanceVND = Convert.ToSingle(reader["BalanceVND"]),
                    Owner = new User
                    {
                        Id = reader["OwnerId"].ToString()
                    }
                };
                var houseId = reader["HouseId"].ToString();
                if (!string.IsNullOrEmpty(houseId))
                    item.House = HouseService.GetById(houseId);
                item.Owner = UserService.GetUserById(item.Owner.Id);
                list.Add(item);
            }

            return list;
        } 
    }
}
