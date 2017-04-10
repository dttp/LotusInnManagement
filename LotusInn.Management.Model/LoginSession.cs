using System;

namespace LotusInn.Management.Model
{
    public class LoginSession
    {
        public User User { get; set; }
        public DateTime LoginTime { get; set; }
    }
}
