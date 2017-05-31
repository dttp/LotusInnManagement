using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LotusInn.Management.Model
{
    
    public enum RoleType
    {
        System,
        Custom,
    }
    public class Role
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public RoleType Type { get; set; } 
    }
}
