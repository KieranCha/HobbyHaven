﻿using HobbyHaven.Shared.DTOs.Administration.Hobbies;
using HobbyHaven.Shared.DTOs.Administration.PersonalityTags;
using HobbyHaven.Shared.DTOs.Hobbies;
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
            List<DTOHobby> revisedHobbies = new();
            Hobbies.ForEach(h => revisedHobbies.Add(h.ToDTO()));

            return new()
			{
				Name = Name,
				Description = Description,
				Id = PersonalityTagID,
				Hobbies = revisedHobbies
			};
		}

        public DTOAdminPersonalityTagViewBasic ToAdminDTOBasic()
        {
            return new()
            {
                Name = Name,
                Description = Description,
                Id = PersonalityTagID,
				TotalUsers = Users.Count,
				TotalHobbies = Hobbies.Count
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

        public Guid PersonalityTagID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
		public List<User> Users { get; set; } = new();
		public List<Hobby> Hobbies { get; set; } = new();
	}

}
