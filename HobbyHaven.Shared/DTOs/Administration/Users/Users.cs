using HobbyHaven.Shared.DTOs.PersonalityTag;
using HobbyHaven.Shared.DTOs.Hobbies;
using HobbyHaven.Shared.DTOs.Havens;

namespace HobbyHaven.Shared.DTOs.Administration.Users
{
    public class DTOAdminCreateUser
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; } = false;
    }

	public class DTOAdminUserView    {
        public string? Id { get; set; }
        public bool Admin { get; set; } = false;
        public List<DTOHobby> Hobbies { get; set; } = new();
        public List<DTOHaven> Havens { get; set; } = new();
        public List<DTOPersonalityTag> PersonalityTags { get; set; } = new();
    }

}
