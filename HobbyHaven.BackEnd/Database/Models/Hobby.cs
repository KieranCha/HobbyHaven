using HobbyHaven.Shared.DTOs.Hobbies;

namespace HobbyHaven.BackEnd.Database.Models.Hobbies
{

    public class Hobby
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public long Id { get; set; }
        public List<string> PersonalityTags { get; set; } = new();

        public DTOHobby DTOHobby
        {
            get
            {
                DTOHobby tmp = new DTOHobby();
                tmp.Name = Name;
                tmp.Description = Description;
                tmp.Id = Id;
                tmp.PersonalityTags = PersonalityTags;
                return tmp;
            }
            set { }
        }
    }

}
