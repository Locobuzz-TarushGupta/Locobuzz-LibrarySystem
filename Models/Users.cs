namespace library_management_system.Models
{

    public class UserDetails
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
    public class Users
    {
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }

    public class ClientToken
    {
        public string Token { get; set; }
        public DateTime DateExpiration { get; set; }

    }
}
