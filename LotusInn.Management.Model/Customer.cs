using System;
using System.Collections.Generic;

namespace LotusInn.Management.Model
{
    public class Customer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string PassportOrId { get; set; }
    }

    public class ActiveCustomer : Customer
    {
        public string OrderId { get; set; }
        public DateTime CheckinDate { get; set; }
        public DateTime CheckoutDate { get; set; }
        public List<Room> Rooms { get; set; }

        public ActiveCustomer(Customer c)
        {
            Id = c.Id;
            Name = c.Name;
            Email = c.Email;
            Phone = c.Phone;
            Country = c.Country;
            Address = c.Address;
            PassportOrId = c.PassportOrId;
        }

        public ActiveCustomer()
        {
            
        }
    }
}
