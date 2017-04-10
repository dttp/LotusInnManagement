using System.Configuration;

namespace LotusInn.Management.Core
{
    public class ConfigManager
    {
        public static string ConnectionString => ConfigurationManager.AppSettings["ConnectionString"];
        public static string LMSDomain => ConfigurationManager.AppSettings["LMSDomain"];
    }
}
