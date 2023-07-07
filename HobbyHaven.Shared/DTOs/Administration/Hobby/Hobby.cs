using HobbyHaven.Shared.DTOs.PersonalityTag;

namespace HobbyHaven.Shared.DTOs.Administration.Hobbies
{

    public class DTOAdminHobbyView
    {
        public string Name { get; set; }
        public string Description { get; set; } = "";
        public Guid Id { get; set; }
        public List<DTOPersonalityTag> PersonalityTags { get; set; } = new(); // Tags used to match the users personality/characteristics to a hobby.
        public int TotalHavens { get; set; } = new();
        public int TotalUsers { get; set; } = new();
    }

    public class DTOAdminCreateHobby
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

}
