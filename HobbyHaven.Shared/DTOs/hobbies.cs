namespace HobbyHaven.Shared.DTOs.Hobbies
{
    public class DTOHaven
    {
        public string Name { get; set; }
        public List<DTOHobby> AssociatedHobbies { get; set; } = new(); // List of hobbies havens the haven is enrolled in.
        public long Id { get; set; } // Unique id for the hobby haven
        public string Location { get; set; }
        public string Description { get; set; }
    }

    public class DTOHobby
    {
        public string Name { get; set; }
        public string Description { get; set; } = "";
        public long Id { get; set; }
        public List<string> PersonalityTags { get; set; } = new(); // Tags used to match the users personality/characteristics to a hobby.
    }
}