using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusInn.Management.Model
{
    public class PaymentRecord
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Amount { get; set; }
        public string Unit { get; set; }
        public string Method { get; set; }
        public string Note { get; set; }
        public string Type { get; set; }
        public string MoneySourceId { get; set; }
        public string OrderId { get; set; }
    }
}
