using HobbyHaven.BackEnd.Controllers.PersonalityTags;
using HobbyHaven.Shared.DTOs.Administration.PersonalityTags;
using HobbyHaven.Shared.DTOs.Administration.Users;
using HobbyHaven.Shared.DTOs.Hobbies;
using HobbyHaven.Shared.DTOs.PersonalityTag;
using HobbyHaven.Shared.DTOs.Users;
using System.Xml.Linq;

namespace HobbyHaven.BackEnd.Database.Models
{
    public class User
    {
		public User() { }

		public DTOAdminUserView ToAdminDTO()
		{
			List<DTOPersonalityTag> revisedTags = new();
			PersonalityTags.ForEach(tag => revisedTags.Add(tag.ToDTO()));


            List<DTOHobby> revisedHobbies = new();
            Hobbies.ForEach(hobby => revisedHobbies.Add(hobby.ToDTO()));

            return new()
			{
				UserID = UserID,
				Admin = Admin,
				PersonalityTags = revisedTags,
				Hobbies = revisedHobbies
            };
		}

		public DTOAdminUserViewBasic ToAdminDTOBasic()
		{
			return new()
			{
				UserID = UserID,
				Admin = Admin,
				TotalPersonalityTags = PersonalityTags.Count,
				TotalHobbies = Hobbies.Count
			};
		}

		public DTOUser ToDTO()
		{
			List<DTOPersonalityTag> revisedTags = new();
			PersonalityTags.ForEach(tag => revisedTags.Add(tag.ToDTO()));

			return new()
			{
				UserID = UserID,
				PersonalityTags = revisedTags,
			};
		}

        public string UserID { get; set; }
        public bool Admin { get; set; } = false;
		public List<PersonalityTag> PersonalityTags { get; set; } = new();
        public List<Hobby> Hobbies { get; set; } = new();
    }

}
