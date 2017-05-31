using System.Collections.Generic;

namespace LotusInn.Management.Model
{
    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public House House { get; set; }        
        public string Status { get; set; }
        public Role Role { get; set; }
    }
}
