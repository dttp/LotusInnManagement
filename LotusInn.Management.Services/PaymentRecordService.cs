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
    public class PaymentRecordService
    {
        private const string SP_PAYMENT_INSERT = "PaymentRecordInsert";
        private const string SP_PAYMENT_UPDATE = "PaymentRecordUpdate";
        private const string SP_PAYMENT_DELETE = "PaymentRecordDelete";
        private const string SP_PAYMENT_SELECT = "PaymentRecordSelect";
        private const string SP_PAYMENT_GETBYORDERID = "PaymentRecordGetByOrderId";
        private const string SP_PAYMENT_GETBYID = "PaymentRecordGetById";
        public PaymentRecord Insert(PaymentRecord record, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            record.Id = "p-" + IdHelper.Generate();

            var param = CreateParam(record);

            if (connection == null && transaction == null)
            {
                SqlHelper.ExecuteNonQuery(SP_PAYMENT_INSERT, param);
            }
            else
            {
                SqlHelper.ExecuteCommand(connection, transaction, CommandType.StoredProcedure, SP_PAYMENT_INSERT, param);
            }
            return record;
        }

        public void Update(PaymentRecord record, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var param = CreateParam(record);

            if (connection == null && transaction == null)
            {
                SqlHelper.ExecuteNonQuery(SP_PAYMENT_UPDATE, param);
            }
            else
            {
                SqlHelper.ExecuteCommand(connection, transaction, CommandType.StoredProcedure, SP_PAYMENT_UPDATE, param);
            }
        }

        public void Delete(string id, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            var param = new[]
            {
                new SqlParameter("@id", id),
            };

            if (connection == null && transaction == null)
            {
                SqlHelper.ExecuteNonQuery(SP_PAYMENT_DELETE, param);
            }
            else
            {
                SqlHelper.ExecuteCommand(connection, transaction, CommandType.StoredProcedure, SP_PAYMENT_DELETE, param);
            }
        }

        public List<PaymentRecord> GetByOrderId(string orderId, string type)
        {
            var param = new[]
            {
                new SqlParameter("@orderId", orderId),
                new SqlParameter("@type", type),
            };

            return SqlHelper.ExecuteReader(SP_PAYMENT_GETBYORDERID, param, Read);
        }

        public PaymentRecord GetById(string id)
        {
            var param = new[]
            {
                new SqlParameter("@id", id),
            };

            return SqlHelper.ExecuteReader(SP_PAYMENT_GETBYID, param, Read).FirstOrDefault();
        }

        public PaginationResult<PaymentRecord> Select(int pageIndex, int pageSize, string moneySourceId, string type, DateTime startDate, DateTime endDate) 
        {
            var param = new[]
            {
                new SqlParameter("@pageIndex", pageIndex),
                new SqlParameter("@pageSize", pageSize),
                new SqlParameter("@moneySourceId", moneySourceId),
                new SqlParameter("@type", type),
                new SqlParameter("@startDate", startDate),
                new SqlParameter("@endDate", endDate),
            };

            return SqlHelper.ExecuteReader(SP_PAYMENT_SELECT, param, reader =>
            {
                var result = new PaginationResult<PaymentRecord>
                {
                    Data = Read(reader)
                };

                reader.NextResult();
                reader.Read();
                result.Total = Convert.ToInt32(reader["Total"]);
                return result;
            });
        } 

        private SqlParameter[] CreateParam(PaymentRecord record)
        {
            if (record.OrderId == null) record.OrderId = "";
            return new[]
            {
                new SqlParameter("@id", record.Id),
                new SqlParameter("@amount", record.Amount),
                new SqlParameter("@date", record.Date),
                new SqlParameter("@method", record.Method),
                new SqlParameter("@moneySourceId", record.MoneySourceId),
                new SqlParameter("@name", record.Name),
                new SqlParameter("@note", record.Note),
                new SqlParameter("@orderId", record.OrderId),
                new SqlParameter("@type", record.Type),
                new SqlParameter("@unit", record.Unit),
            };
        }

        private List<PaymentRecord> Read(IDataReader reader)
        {
            var list = new List<PaymentRecord>();

            while (reader.Read())
            {
                var item = new PaymentRecord
                {
                    Id = reader["Id"].ToString(),
                    Amount = reader["Amount"].ToString(),
                    Date = Convert.ToDateTime(reader["Date"]),
                    Method = reader["Method"].ToString(),
                    Name = reader["Name"].ToString(),
                    MoneySourceId = reader["MoneySourceId"].ToString(),
                    Note = reader["Note"].ToString(),
                    OrderId = reader["OrderId"].ToString(),
                    Type = reader["Type"].ToString(),
                    Unit = reader["Unit"].ToString()
                };
                list.Add(item);
            }
            return list;
        } 
    }
}
