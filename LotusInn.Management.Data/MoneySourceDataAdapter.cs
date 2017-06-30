using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LotusInn.Management.Core;
using LotusInn.Management.Model;

namespace LotusInn.Management.Data
{
    public class MoneySourceDataAdapter: IObjectDataAdapter<MoneySource>
    {
        private const string SP_MONEYSOURCE_GETALL = "MoneySourceGetAll";
        private const string SP_MONEYSOURCE_GETBYID = "MoneySourceGetById";
        private const string SP_MONEYSOURCE_INSERT = "MoneySourceInsert";
        private const string SP_MONEYSOURCE_UPDATE = "MoneySourceUpdate";
        private const string SP_MONEYSOURCE_DELETE = "MoneySourceDelete";

        public MoneySource Insert(MoneySource @object)
        {
            @object.Id = IdHelper.Generate();
            var param = CreateParams(@object);

            SqlHelper.ExecuteNonQuery(SP_MONEYSOURCE_INSERT, param);
            return @object;
        }

        public void Update(MoneySource @object)
        {
            Update(@object, null, null);
        }

        public void Update(MoneySource @object, SqlConnection conn, SqlTransaction trans)
        {
            var param = CreateParams(@object);            
            if (conn == null)
            {
                SqlHelper.ExecuteNonQuery(SP_MONEYSOURCE_UPDATE, param);
            }
            else
            {
                SqlHelper.ExecuteCommand(conn, trans, CommandType.StoredProcedure, SP_MONEYSOURCE_UPDATE, param);
            }
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

        public List<MoneySource> GetAll(string userId)
        {
            var param = new[]
            {
                new SqlParameter("@userId", userId),
            };
            return SqlHelper.ExecuteReader(SP_MONEYSOURCE_GETALL, param, Read);
        } 

        private SqlParameter[] CreateParams(MoneySource moneySource)
        {
            var param = new[]
            {
                new SqlParameter("@id", moneySource.Id),
                new SqlParameter("@houseId", moneySource.House != null ? moneySource.House.Id : ""),
                new SqlParameter("@name", moneySource.Name),
                new SqlParameter("@balanceUSD", moneySource.BalanceUSD),
                new SqlParameter("@balanceVND", moneySource.BalanceVND),
                new SqlParameter("@ownerId", moneySource.Owner.Id),
            };
            return param;
        }

        private List<MoneySource> Read(IDataReader reader)
        {
            var list = new List<MoneySource>();
            var houseDA = new HouseDataAdapter();
            var userDA = new UserDataAdapter();
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
                    item.House = houseDA.GetById(houseId);
                item.Owner = userDA.GetById(item.Owner.Id);
                list.Add(item);
            }

            return list;
        }
    }
}
