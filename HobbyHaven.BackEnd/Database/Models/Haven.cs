using HobbyHaven.Shared.DTOs.Administration.Havens;
using HobbyHaven.Shared.DTOs.Havens;
using HobbyHaven.Shared.DTOs.Hobbies;

namespace HobbyHaven.BackEnd.Database.Models
{

	public class Haven
    {
        public Haven() { }

        public Haven(DTOHaven haven)
        {
            HavenID = haven.Id;
            Name = haven.Name;
            Description = haven.Description;
            Location = haven.Location;
            Address = haven.Address;
            OwnerID = haven.OwnerID;
        }

        public Haven(DTOAdminCreateHaven haven)
        {
            Name = haven.Name;
            Description = haven.Description;   
            Location = haven.Location;
            Address = haven.Address;
            OwnerID = haven.OwnerID;
        }

        public DTOAdminHavenView ToAdminDTO()
        {
            return new()
            {
                Id = HavenID,
                Name = Name,
                Description = Description,
                Location = Location,
                Address = Address,
                OwnerID = OwnerID
            };
        }

        public DTOHaven ToDTO()
        {
            return new()
            {
                Id = HavenID,
                Name = Name,
                Description = Description,
                Location = Location,
                Address = Address,
                OwnerID = OwnerID
            };
        }

        public long HavenID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
		public string OwnerID { get; set; } 

    }

}
