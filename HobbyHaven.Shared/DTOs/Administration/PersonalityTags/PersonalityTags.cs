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
        public int TotalUsers { get; set; }
        public int TotalHobbies { get; set; }

    }
}
