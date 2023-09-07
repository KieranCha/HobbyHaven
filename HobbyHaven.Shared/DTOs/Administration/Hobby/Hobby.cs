using HobbyHaven.Shared.DTOs.Havens;
using HobbyHaven.Shared.DTOs.Hobbies;
using HobbyHaven.Shared.DTOs.PersonalityTag;
using HobbyHaven.Shared.DTOs.Users;

namespace HobbyHaven.Shared.DTOs.Administration.Hobbies
{

    public class DTOAdminHobbyView
    {
        public string Name { get; set; } = "undefined";
        public string Description { get; set; } = "";
        public Guid Id { get; set; }
        public bool HasImage { get; set; } = false;
        public List<DTOPersonalityTag> PersonalityTags { get; set; } = new(); // Tags used to match the users personality/characteristics to a hobby.
        public List<DTOHaven> Havens { get; set; } = new();
    }

    public class DTOAdminHobbyViewBasic
    {
        public string Name { get; set; } = "undefined";
        public string Description { get; set; } = "";
        public Guid Id { get; set; }
        public bool HasImage { get; set; } = false;
        public int TotalPersonalityTags { get; set; } = 0; 
        public int TotalHavens { get; set; } = 0;
        public int TotalUsers { get; set; } = 0;
    }


    public class DTOAdminCreateHobby
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

}
