using HobbyHaven.BackEnd.Controllers.PersonalityTags;
using HobbyHaven.Shared.DTOs.Administration.PersonalityTags;
using HobbyHaven.Shared.DTOs.Administration.Users;
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
			return new()
			{
				UserID = UserID,
				Admin = Admin
			};
		}

		public DTOUser ToDTO()
		{
			return new()
			{
				UserID = UserID
			};
		}

        public string UserID { get; set; }
        public bool Admin { get; set; } = false;
    }

}
