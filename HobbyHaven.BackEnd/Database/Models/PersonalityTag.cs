using HobbyHaven.Shared.DTOs.Administration.Hobbies;
using HobbyHaven.Shared.DTOs.Administration.PersonalityTags;
using HobbyHaven.Shared.DTOs.PersonalityTag;

namespace HobbyHaven.BackEnd.Database.Models
{
    public class PersonalityTag
    {

		public PersonalityTag() { }

		public PersonalityTag(DTOAdminCreatePersonalityTag tag)
		{
			Name = tag.Name;
			Description = tag.Description;
		}

		public DTOAdminPersonalityTagView ToAdminDTO()
		{
			return new()
			{
				Name = Name,
				Description = Description,
				Id = PersonalityTagID
			};
		}

		public DTOPersonalityTag ToDTO()
		{
			return new()
			{
				Name = Name,
				Description = Description,
				Id = PersonalityTagID
			};
		}

        public long PersonalityTagID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
	}

}
