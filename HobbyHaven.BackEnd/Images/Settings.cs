namespace HobbyHaven.BackEnd.Images
{
	public class ImageSettings
	{
		public string hobbyImagePath { get; set; }
		public HavenImageSettings havenImagePath { get; set; }
	}


	public class HavenImageSettings
	{
		public string logo { get; set; }
		public string banner { get; set; }
	}
}
