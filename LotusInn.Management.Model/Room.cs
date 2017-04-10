namespace LotusInn.Management.Model
{
    public class Room
    {
        public string Id { get; set; }
        public string HouseId { get; set; }
        public string RoomNumber { get; set; }
        public RoomType RoomType { get; set; }
    }

    public class RoomWithStatus : Room
    {
        public string Status { get; set; }
    }
}
