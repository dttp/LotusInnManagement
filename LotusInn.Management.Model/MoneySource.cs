using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace LotusInn.Management.Model
{
    public class MoneySource
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public House House { get; set; }
        public float BalanceUSD { get; set; }
        public float BalanceVND { get; set; }
        public User Owner { get; set; }
    }
}
