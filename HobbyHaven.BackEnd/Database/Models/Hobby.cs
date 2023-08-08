using HobbyHaven.BackEnd.Controllers.Hobbies;
using HobbyHaven.Shared.DTOs.Administration.Havens;
using HobbyHaven.Shared.DTOs.Administration.Hobbies;
using HobbyHaven.Shared.DTOs.Havens;
using HobbyHaven.Shared.DTOs.Hobbies;
using HobbyHaven.Shared.DTOs.PersonalityTag;

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
            List<DTOPersonalityTag> revisedTags = new();
            PersonalityTags.ForEach(t => revisedTags.Add(t.ToDTO()));

            return new()
            {
                Name = Name,
                Description = Description,
                Id = HobbyID,
                HasImage = HasImage,
                PersonalityTags = revisedTags
            };
        }

        public DTOHobby ToDTO()
        {

            List<DTOPersonalityTag> revisedTags = new();
            PersonalityTags.ForEach(t => revisedTags.Add(t.ToDTO()));

            return new()
            {
                Name = Name,
                Description = Description,
                Id = HobbyID,
                HasImage = HasImage,
                PersonalityTags = revisedTags
            };
        }

        public DTOAdminHobbyViewBasic ToAdminDTOBasic()
        {
            return new()
            {
                Name = Name,
                Description = Description,
                Id = HobbyID,
                HasImage= HasImage,
                TotalUsers = Users.Count,
                TotalPersonalityTags = PersonalityTags.Count
            };
        }

        public Guid HobbyID { get; set; }
		public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool HasImage { get; set; } = false;
        public List<User> Users { get; set; } = new();
        public List<PersonalityTag> PersonalityTags { get; set; } = new();
	}

}


