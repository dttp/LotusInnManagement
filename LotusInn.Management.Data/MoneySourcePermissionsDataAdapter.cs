using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LotusInn.Management.Core;
using LotusInn.Management.Model;

namespace LotusInn.Management.Data
{
    public class MoneySourcePermissionsDataAdapter
    {
        private const string SP_MONEYSOURCEPERMISSION_GET_BY_MONEYSOURCEID = "MoneySourcePermissionsGetByMoneySourceId";
        public List<MoneySourcePermission> GetByMoneySourceId(string moneySourceId)
        {
            var param = new[]
            {
                new SqlParameter("@moneySourceId", moneySourceId),
            };
            return SqlHelper.ExecuteReader(SP_MONEYSOURCEPERMISSION_GET_BY_MONEYSOURCEID, param, reader =>
            {
                var list = new List<MoneySourcePermission>();
                var userDA = new UserDataAdapter();
                while (reader.Read())
                {
                    var item = new MoneySourcePermission
                    {
                        MoneySourceId = reader["MoneySourceId"].ToString(),
                        User = new User
                        {
                            Id = reader["UserId"].ToString()
                        },
                        Permissions =
                            reader["Permissions"].ToString().FromJson<Dictionary<MoneySourcePermissionName, bool>>()
                    };

                    item.User = userDA.GetById(item.User.Id);

                    list.Add(item);
                }

                return list;
            });
        } 
    }
}
