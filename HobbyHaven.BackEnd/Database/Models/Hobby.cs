namespace HobbyHaven.BackEnd.Database.Models
{
    public class Hobby
    {

        public Hobby() { }

        public long HobbyID { get; set; }
		public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
	}

}
