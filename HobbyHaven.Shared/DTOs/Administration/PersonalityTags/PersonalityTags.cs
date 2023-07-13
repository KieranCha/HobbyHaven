using HobbyHaven.Shared.DTOs.Administration.Users;
using HobbyHaven.Shared.DTOs.Hobbies;
using HobbyHaven.Shared.DTOs.Users;

namespace HobbyHaven.Shared.DTOs.Administration.PersonalityTags
{
    public class DTOAdminCreatePersonalityTag
    {
        public string Name { get; set; } = "undefined";
        public string Description { get; set; } = "";
    }

    public class DTOAdminPersonalityTagView
    {
        public string Name { get; set; } = "undefined";
        public string Description { get; set; } = "";
        public Guid Id { get; set; }
        public List<DTOUser> Users { get; set; }
        public List<DTOHobby> Hobbies { get; set; }
    }

    public class DTOAdminPersonalityTagViewBasic
    {
        public string Name { get; set; } = "undefined";
        public string Description { get; set; } = "";
        public Guid Id { get; set; }
        public int TotalUsers { get; set; }
        public int TotalHobbies { get; set; }


    }
}
