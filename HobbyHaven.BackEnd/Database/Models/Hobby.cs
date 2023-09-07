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

            List<DTOHaven> revisedHavens = new();
            Havens.ForEach(h => revisedHavens.Add(h.ToDTO()));

            return new()
            {
                Name = Name,
                Description = Description,
                Id = HobbyID,
                PersonalityTags = revisedTags,
                Havens = revisedHavens,
                HasImage = HasImage
            };
        }

        public DTOHobby ToDTO()
        {

            List<DTOPersonalityTag> revisedTags = new();
            PersonalityTags.ForEach(t => revisedTags.Add(t.ToDTO()));

            List<DTOHavenBasic> revisedHavens = new();
            Havens.ForEach(h => revisedHavens.Add(h.ToDTOBasic()));

            return new()
            {
                Name = Name,
                Description = Description,
                Id = HobbyID,
                PersonalityTags = revisedTags,
                Havens = revisedHavens,
                HasImage = HasImage
            };
        }

        public DTOHobbyBasic ToDTOBasic()
        {

            List<Guid> revisedTags = new();
            PersonalityTags.ForEach(t => revisedTags.Add(t.PersonalityTagID));

            List<Guid> revisedHavens = new();
            Havens.ForEach(h => revisedHavens.Add(h.HavenID));

            return new()
            {
                Name = Name,
                Description = Description,
                Id = HobbyID,
                PersonalityTags = revisedTags,
                Havens = revisedHavens,
                HasImage = HasImage
            };
        }

        public DTOAdminHobbyViewBasic ToAdminDTOBasic()
        {
            return new()
            {
                Name = Name,
                Description = Description,
                Id = HobbyID,
                HasImage = HasImage,
                TotalUsers = Users.Count,
                TotalPersonalityTags = PersonalityTags.Count,
                TotalHavens = Havens.Count
            };
        }

        public Guid HobbyID { get; set; }
		public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool HasImage { get; set; } = false;
        public List<User> Users { get; set; } = new();
        public List<PersonalityTag> PersonalityTags { get; set; } = new();
        public List<Haven> Havens { get; set; } = new();
    }

}


