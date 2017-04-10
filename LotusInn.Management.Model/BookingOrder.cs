using System;
using System.Collections.Generic;

namespace LotusInn.Management.Model
{
    public class BookingOrder
    {
        public string Id { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Room> Rooms { get; set; }        
        public DateTime CheckinDate { get; set; }
        public DateTime CheckOutDate { get; set; }                
        public string Note { get; set; }
        public int TotalGuest { get; set; }
        public string StayLength { get; set; }

        public List<PaymentRecord> Payments { get; set; } 
        public Deposit Deposit { get; set; }
        public List<PaymentRecord> Discounts { get; set; }
        public string Status { get; set; }
        public string HouseId { get; set; }
        public PaymentCycle PaymentCycle { get; set; }
    }
}
