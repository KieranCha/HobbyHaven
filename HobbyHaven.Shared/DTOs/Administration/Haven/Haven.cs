using HobbyHaven.Shared.DTOs.Hobbies;

namespace HobbyHaven.Shared.DTOs.Administration.Havens
{

    public class DTOAdminHavenView
    {

        public string Name { get; set; } = "undefined";
        public string Description { get; set; }
        public Guid Id { get; set; }
        public string? OwnerID { get; set; }
        public int TotalUsers { get; set; }
        public string Location { get; set; }
		public string? Address { get; set; } = null; // Address
		public List<DTOHobbyBasic> Hobbies { get; set; } = new();
    }
    public class DTOAdminHavenViewBasic
    {

        public string Name { get; set; } = "undefined";
        public string Description { get; set; }
        public Guid Id { get; set; }
        public string? OwnerID { get; set; }
        public int TotalUsers { get; set; }
        public string Location { get; set; }
        public string? Address { get; set; } = null; // Address
        public int TotalHobbies = 0;
    }

    public class DTOAdminCreateHaven
    {
        public string Name { get; set; } = "undefined";
        public string Description { get; set; }
        public string Location { get; set; } // GPS location
        public string? OwnerID { get; set; } // As ID
        public string? Address { get; set; } = null; // Address
    }

}
