namespace HobbyHaven.BackEnd.Database.Models
{
    public class PersonalityTag
    {
        public PersonalityTag () { }

        public long PersonalityTagID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
	}

}
