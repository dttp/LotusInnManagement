using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusInn.Management.Model
{
    public class HouseOverviewModel : House
    {
        public int TotalGuest { get; set; }
        public int RoomCoverRating { get; set; }
        public List<RoomWithStatus> Rooms { get; set; } 
        public List<ActiveCustomer> CustomersCheckoutList { get; set; } 


        public List<RoomStateRange> RoomsState { get; set; } 

        public HouseOverviewModel() { }

        public HouseOverviewModel(House baseHouse)
        {
            Id = baseHouse.Id;
            Address = baseHouse.Address;
            Name = baseHouse.Name;
        }

    }

    public class Dashboard
    {
        public List<HouseOverviewModel> Houses { get; set; } 
    }
}
