using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;
using LotusInn.Management.Core;
using LotusInn.Management.Data;
using LotusInn.Management.Model;

namespace LotusInn.Management.Services
{
    public class MoneySourceService
    {
        private readonly MoneySourceDataAdapter _adapter = new MoneySourceDataAdapter();
        public List<MoneySource> GetAll(User user)
        {
            var result =_adapter.GetAll(user.Id);
            return result;
        }

        public MoneySource Insert(MoneySource moneySource)
        {
            var source = _adapter.Insert(moneySource);

            var userPermissionAdapter = new UserObjectPermissionDataAdapter();
            userPermissionAdapter.Insert(new UserObjectPermission
            {
                ObjectType = "MoneySource",
                ObjectId = source.Id,
                Permission = PermissionEnum.Read | PermissionEnum.Create | PermissionEnum.Edit | PermissionEnum.Delete,
                User = source.Owner
            });

            return source;            
        }

        public void Update(MoneySource moneySource, SqlConnection con = null, SqlTransaction trans = null)
        {
            _adapter.Update(moneySource, con, trans);
        }

        public void Delete(string id)
        {
            _adapter.Delete(id);
            var ropAdapter = new RoleObjectPermissionDataAdapter();
            var roleList = ropAdapter.GetByObjectId(id);
            foreach (var item in roleList)
            {
                ropAdapter.Delete(item.Id);
            }

            var uopAdapter = new UserObjectPermissionDataAdapter();
            var userList = uopAdapter.GetByObjectId(id);
            foreach (var item in userList)
            {
                uopAdapter.Delete(item.Id);
            }
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

        public ObjectPermissions GetPermissions(string moneySourceId)
        {
            var result = new ObjectPermissions
            {
                ObjectId = moneySourceId,
                ObjectType = "MoneySource",
            };

            var ropAdapter = new RoleObjectPermissionDataAdapter();
            result.RolesPermissions = ropAdapter.GetByObjectId(moneySourceId);

            var uopAdapter = new UserObjectPermissionDataAdapter();
            result.UsersPermissions = uopAdapter.GetByObjectId(moneySourceId);

            return result;
        }

        public void SetPermission(ObjectPermissions objectPermissions)
        {
            var oldPerm = GetPermissions(objectPermissions.ObjectId);

            var ropAdapter = new RoleObjectPermissionDataAdapter();
            foreach (var item in oldPerm.RolesPermissions)
            {
                ropAdapter.Delete(item.Id);
            }

            var uopAdapter = new UserObjectPermissionDataAdapter();
            foreach (var item in oldPerm.UsersPermissions)
            {
                uopAdapter.Delete(item.Id);
            }

            foreach (var item in objectPermissions.RolesPermissions)
            {
                ropAdapter.Insert(item);
            }
            foreach (var item in objectPermissions.UsersPermissions)
            {
                uopAdapter.Insert(item);
            }
        }
    }
}
