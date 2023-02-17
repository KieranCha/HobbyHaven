using HobbyHaven.Shared.DTOs.Hobbies;

namespace HobbyHaven.Shared.DTOs.User
{
    class User
    {
        public string Username { get; set; }
        public string? ProfilePicture { get; set; } = null; // Profile picture image url.
        public List<string> PersonalityTags { get; set; } = new(); // Tags used to 
        public List<Haven> Havens { get; set; } = new(); // List of hobbies havens the user is enrolled in.
    }
}