using HobbyHaven.Shared.DTOs.Administration.Havens;
using HobbyHaven.Shared.DTOs.Administration.Hobbies;
using HobbyHaven.Shared.DTOs.Havens;
using HobbyHaven.Shared.DTOs.Hobbies;

namespace HobbyHaven.BackEnd.Database.Models
{
    public class Hobby
    {

        public Hobby() { }

        public Hobby(DTOAdminCreateHobby hobby)
        {
            Name = hobby.Name;
            Description = hobby.Description;
        }

        public DTOAdminHobbyView ToAdminDTO()
        {
            return new()
            {
                Name = Name,
                Description = Description,
                Id = HobbyID
            };
        }

        public DTOHobby ToDTO()
        {
            return new()
            {
                Name = Name,
                Description = Description,
                Id = HobbyID
            };
        }

        public Guid HobbyID { get; set; }
		public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
	}

}
