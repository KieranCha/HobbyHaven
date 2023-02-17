namespace HobbyHaven.Shared.DTOs.Hobbies
{
    class Haven
    {
        public string Name { get; set; }
        public List<Hobby> AssociatedHobbies { get; set; } = new(); // List of hobbies havens the user is enrolled in.
        public int Id { get; set; } // Unique id for the hobby haven
        public string location { get; set; }
    }

    class Hobby
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public List<string> PersonalityTags { get; set; } = new();
    }
}