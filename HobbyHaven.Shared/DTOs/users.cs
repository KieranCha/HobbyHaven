using HobbyHaven.Shared.DTOs.Hobbies;

namespace HobbyHaven.Shared.DTOs.User
{
    public class DTOUser
    {
        public string Username { get; set; }
        public long Id { get; set; } // Randomly generated unique identifier assigned to each user.
        public string? ProfilePicture { get; set; } = null; // Profile picture image url.
        public List<string> PersonalityTags { get; set; } = new(); // Tags used to match the users personality/characteristics to a hobby.
        public List<DTOHaven> Havens { get; set; } = new(); // List of hobbies havens the user is enrolled in
        public string Bio { get; set; } = "";
    }

    public class DTOCreateUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

}