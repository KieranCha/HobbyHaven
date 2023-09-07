using HobbyHaven.BackEnd.Controllers.Hobbies;
using HobbyHaven.BackEnd.Controllers.PersonalityTags;
using HobbyHaven.Shared.DTOs.Administration.Havens;
using HobbyHaven.Shared.DTOs.Havens;
using HobbyHaven.Shared.DTOs.Hobbies;
using HobbyHaven.Shared.DTOs.PersonalityTag;
using System.Collections.Generic;

namespace HobbyHaven.BackEnd.Database.Models
{

	public class Haven
    {
        public Haven() { }

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
            List<DTOHobbyBasic> revisedHobbies = new();
            Hobbies.ForEach(h => revisedHobbies.Add(h.ToDTOBasic()));

            return new()
            {
                Id = HavenID,
                Name = Name,
                Description = Description,
                Location = Location,
                Address = Address,
                OwnerID = OwnerID,
                Hobbies = revisedHobbies
            };
        }

        public DTOAdminHavenViewBasic ToAdminDTOBasic()
        {
            return new()
            {
                Id = HavenID,
                Name = Name,
                Description = Description,
                Location = Location,
                Address = Address,
                OwnerID = OwnerID,
                TotalHobbies = Hobbies.Count
            };
        }

        public DTOHaven ToDTO()
        {
            List<DTOHobbyBasic> revisedHobbies = new();
            Hobbies.ForEach(h => revisedHobbies.Add(h.ToDTOBasic()));

            return new()
            {
                Id = HavenID,
                Name = Name,
                Description = Description,
                Location = Location,
                Address = Address,
                OwnerID = OwnerID,
                Hobbies = revisedHobbies
            };
        }

        public DTOHavenBasic ToDTOBasic()
        {
            List<Guid> revisedHobbies = new();
            Hobbies.ForEach(h => revisedHobbies.Add(h.HobbyID));

            return new()
            {
                Id = HavenID,
                Name = Name,
                Description = Description,
                Location = Location,
                Address = Address,
                OwnerID = OwnerID,
                Hobbies = revisedHobbies
            };
        }

        public Guid HavenID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
		public string OwnerID { get; set; }
        public List<Hobby> Hobbies { get; set; } = new();

    }

}
