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
    public class RoleDataAdapter : IObjectDataAdapter<Role>
    {
        private const string SP_ROLE_INSERT = "RoleInsert";
        private const string SP_ROLE_UPDATE = "RoleUpdate";
        private const string SP_ROLE_DELETE = "RoleDelete";
        private const string SP_ROLE_GETBYID = "RoleGetById";
        private const string SP_ROLE_GETALL = "RoleGetAll";

        public Role Insert(Role @object)
        {
            @object.Id = IdHelper.Generate();
            var param = new[]
            {
                new SqlParameter("@id", @object.Id),
                new SqlParameter("@name", @object.Name),
                new SqlParameter("@type", @object.Type.ToString()),
            };
            SqlHelper.ExecuteNonQuery(SP_ROLE_INSERT, param);
            return @object;
        }

        public void Update(Role @object)
        {
            var param = new[]
            {
                new SqlParameter("@id", @object.Id),
                new SqlParameter("@name", @object.Name),
            };
            SqlHelper.ExecuteNonQuery(SP_ROLE_UPDATE, param);
        }

        public void Delete(string id)
        {
            var param = new[]
            {
                new SqlParameter("@id", id),
            };
            SqlHelper.ExecuteNonQuery(SP_ROLE_DELETE, param);
        }

        public Role GetById(string id)
        {
            var param = new[]
            {
                new SqlParameter("@id", id),
            };
            return SqlHelper.ExecuteReader(SP_ROLE_GETBYID, param, Read).FirstOrDefault();
        }

        public List<Role> GetAll()
        {
            return SqlHelper.ExecuteReader(SP_ROLE_GETALL, null, Read);
        }

        private List<Role> Read(IDataReader reader)
        {
            var list = new List<Role>();
            while (reader.Read())
            {
                var item = new Role
                {
                    Id = reader["Id"].ToString(),
                    Name = reader["Name"].ToString(),
                    Type = (RoleType) Enum.Parse(typeof (RoleType), reader["Type"].ToString())
                };
                list.Add(item);
            }
            return list;
        } 
    }
}
