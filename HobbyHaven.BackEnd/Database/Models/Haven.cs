using HobbyHaven.BackEnd.Database.Models.Hobbies;
using HobbyHaven.BackEnd.Database.Models.Users;
using HobbyHaven.Shared.DTOs.Hobbies;

namespace HobbyHaven.BackEnd.Database.Models.Havens
{
    public class Haven
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public List<Hobby> AssociatedHobbies { get; set; } = new();
        public long Id { get; set; }
        public string Location { get; set; } = "";
        public User Owner { get; set; }

        public DTOHaven DTOHaven { get
            {
                DTOHaven tmpHaven = new();
                tmpHaven.Name = Name;
                tmpHaven.Description = Description;
                tmpHaven.Id = Id;
                tmpHaven.Location = Location;
                tmpHaven.AssociatedHobbies = new();

                foreach (Hobby hobby in AssociatedHobbies)
                    tmpHaven.AssociatedHobbies.Add(hobby.DTOHobby);

                return tmpHaven;
            }
            set { } }
    }
}
