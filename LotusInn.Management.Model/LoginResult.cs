namespace LotusInn.Management.Model
{
    public class LoginResult    
    {
        public bool Success { get; set; }
        public string AuthId { get; set; }
        public string Message { get; set; }
    }

    public class LoginInfo
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
}
