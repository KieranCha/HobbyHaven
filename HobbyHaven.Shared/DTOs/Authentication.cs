namespace HobbyHaven.Shared.DTOs.Authentication
{
    public class Authentication
    {
        public bool Exists { get; set; } = false;
        public bool Admin { get; set; } = false;
        public string Token { get; set; }
        public long? id { get; set; } = null;
    }

    public class LoginInformation
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}