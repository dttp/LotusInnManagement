using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusInn.Management.Model
{
    public class PaymentCycle
    {
        public int Value { get; set; }
        public string TimeUnit { get; set; }
        public PaymentCycle()
        {
            Value = 1;
            TimeUnit = "M";
        }

        public PaymentCycle(string cycle)
        {
            var parts = cycle.Split('-');
            if (parts.Length < 2) return;
            Value = Convert.ToInt32(parts[0]);
            TimeUnit = parts[1];
        }

        public override string ToString()
        {
            return $"{Value}-{TimeUnit}";
        }        
    }
}
