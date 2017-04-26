using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LotusInn.Management.Core;
using LotusInn.Management.Data;
using LotusInn.Management.Model;

namespace LotusInn.Management.Services
{
    public class MoneySourceService
    {
        private readonly MoneySourceDataAdapter _adapter = new MoneySourceDataAdapter();
        public List<MoneySource> GetAll()
        {
            return _adapter.GetAll();
        }

        public MoneySource Insert(MoneySource moneySource)
        {
            return _adapter.Insert(moneySource);
        }

        public void Update(MoneySource moneySource, SqlConnection con = null, SqlTransaction trans = null)
        {
            _adapter.Update(moneySource, con, trans);
        }

        public void Delete(string id)
        {
            _adapter.Delete(id);
        }

        public MoneySource GetById(string id)
        {
            return _adapter.GetById(id);
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

                _adapter.Update(source, con, trans);
            }
            else
            {
                SqlHelper.StartTransaction(ConfigManager.ConnectionString, (connection, transaction) =>
                {
                    payment = ps.Insert(payment, connection, transaction);

                    _adapter.Update(source, con, trans);
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

                _adapter.Update(source, con, trans);
            }
            else
            {
                SqlHelper.StartTransaction(ConfigManager.ConnectionString, (connection, transaction) =>
                {
                    ps.Update(payment, connection, transaction);

                    _adapter.Update(source, con, trans);
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

                _adapter.Update(source, con, trans);
            }
            else
            {
                SqlHelper.StartTransaction(ConfigManager.ConnectionString, (connection, transaction) =>
                {
                    ps.Delete(id, connection, transaction);

                    _adapter.Update(source, con, trans);
                });
            }
        }

        
    }
}
