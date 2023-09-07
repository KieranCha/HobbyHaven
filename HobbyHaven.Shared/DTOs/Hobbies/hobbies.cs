using HobbyHaven.Shared.DTOs.PersonalityTag;
using HobbyHaven.Shared.DTOs.Havens;

namespace HobbyHaven.Shared.DTOs.Hobbies
{
    public class DTOHobby
    {
        public string Name { get; set; }
        public string Description { get; set; } = "";
        public bool HasImage { get; set; } = false;
        public Guid Id { get; set; }
        public List<DTOPersonalityTag> PersonalityTags { get; set; } = new(); // Tags used to match the users personality/characteristics to a hobby.
        public List<DTOHavenBasic> Havens { get; set; } = new();
    }

    public class DTOHobbyBasic
    {
        public string Name { get; set; }
        public string Description { get; set; } = "";
        public Guid Id { get; set; }
        public List<Guid> PersonalityTags { get; set; } = new(); // Tags used to match the users personality/characteristics to a hobby.
        public List<Guid> Havens { get; set; } = new();
    }
}