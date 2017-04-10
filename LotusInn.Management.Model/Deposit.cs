using System;

namespace LotusInn.Management.Model
{
    public class Deposit
    {
        public string Id { get; set; }
        public string Amount { get; set; }
        public DateTime Date { get; set; }
        public string Unit { get; set; }
        public string OrderId { get; set; }
    }
}
