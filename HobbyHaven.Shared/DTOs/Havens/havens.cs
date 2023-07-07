using HobbyHaven.Shared.DTOs.Hobbies;
using HobbyHaven.Shared.DTOs.Users;

namespace HobbyHaven.Shared.DTOs.Havens
{
    public class DTOHaven
    {
        public string Name { get; set; }
        public List<DTOHobby> AssociatedHobbies { get; set; } = new(); // List of hobbies havens the haven is enrolled in.
        public Guid Id { get; set; } // Unique id for the hobby haven
        public string Location { get; set; }
		public string? Address { get; set; } = null; // Address
		public string Description { get; set; }
        public string OwnerID { get; set; }
    }
}
