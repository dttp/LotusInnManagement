using LotusInn.Management.Model;
using System.Collections.Generic;
using System.Data;
using LotusInn.Management.Core;

namespace LotusInn.Management.Services
{
    public class RoleService
    {
        private const string SP_ROLE_GET_ALL = "RoleGetAll";

        public static List<Role> GetRoles()
        {
            return SqlHelper.ExecuteReader(ConfigManager.ConnectionString, CommandType.StoredProcedure, SP_ROLE_GET_ALL, null,
                Parse);
        }

        private static List<Role> Parse(IDataReader reader)
        {
            var result = new List<Role>();
            while (reader.Read())
            {
                var role = new Role
                {
                    Id = reader["Id"].ToString(),
                    Name = reader["Name"].ToString()
                };
                result.Add(role);
            }
            return result;
        } 

    }
}
