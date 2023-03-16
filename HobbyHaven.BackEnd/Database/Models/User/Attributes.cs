using HobbyHaven.BackEnd.Database.Models.Havens;
using HobbyHaven.BackEnd.Database.Models.PersonalityTags;
using HobbyHaven.Shared.DTOs.User;
using SQLite;
using static MudBlazor.CategoryTypes;

// https://jasonwatmore.com/post/2021/05/27/net-5-hash-and-verify-passwords-with-bcrypt

namespace HobbyHaven.BackEnd.Database.Models.Users
{


    public class UserAttributes
    {
        // Instance of the database object, where you can interact and commit to the local SQLite database.
        public Database database;

        // Username of the user
        public string? _Username { get; set; } = null;
        public string Username
        {
            get { return _Username; }
            set { if (_Username == null) { _Username = value; } else { UpdateProperty("Username", value); _Username = value; } }
        }

        // Boolean value if the user instance has administrator permissions
        public bool Admin { get; set; } = false;

        // Hashed value of the password using brypt w/ salts.
        public string Password { get; set; }

        // Email of the user instance
        public string Email { get; set; }

        // String representing the URL where the user instances' profile picture is stored.
        public string ProfilePicture { get; set; } = "";

        // The authorization token of the user instance, used to make requests to the API.
        public string? AuthorizationToken { get; set; } = null;

        // The user instances unique id. Used for identifying the user on some endpoints, and for database junctions.
        public long Id { get; set; } = 0; // Randomly generated unique identifier assigned to each user.

        // The biography of the user instance displayed on the profile page.
        public string? _Bio = null;
        public string Bio
        {
            get { return _Bio; }
            set { if (_Bio == null) { _Bio = value;  } else { UpdateProperty("Bio", value); _Bio = value; } }
        }


        private void UpdateProperty(string field, object value)
        {
            SQLiteCommand command = database.connection.CreateCommand($"UPDATE users SET {field}='{value}' WHERE Id={this.Id}");
            command.ExecuteNonQuery();
        }






        // Bool showing if the user exists or not.
        public bool exists = false;

        // Represent the current user instance as a DTOUser object instead :)
        public DTOUser DTOUser
        {
            get
            {
                DTOUser tmpuser = new DTOUser();
                tmpuser.Username = Username;
                tmpuser.Id = Id;
                tmpuser.ProfilePicture = ProfilePicture;
                tmpuser.Havens = new(); // Iterate through the list and convert each of them to its DTO alternate.
                tmpuser.PersonalityTags = PersonalityTags;
                tmpuser.Bio = Bio;

                foreach (Haven haven in Havens) tmpuser.Havens.Add(haven.DTOHaven);

                return tmpuser;
            }
            set { }
        }






        // Store value to _PersonalityTags, when you use the variable/attribute "PersonalityTags" it checks if the data has been handled, if not then it handles the data
        // and returns the value. Not always required for 99% of endpoints so save time by not doing it unless the API actually needs it.

        public List<string>? _PersonalityTags = null;

        public List<string> PersonalityTags
        {
            get
            {
                if (_PersonalityTags == null) _PersonalityTags = GetPersonalityTags();
                return _PersonalityTags;
            }
            set
            {
                _PersonalityTags = value;
            }
        }


        public List<string> GetPersonalityTags()
        {
            List<string> tags = new();

            foreach (UserPersonalityTag tag in database.connection.Query<UserPersonalityTag>($"SELECT * FROM userPersonalityJunction WHERE UserID={this.Id}")) tags.Add(tag.PersonalityTag);

            return tags;
        }







        // Store value to _Havens, when you use the variable/attribute "Havens" it checks if the data has been handled, if not then it handles the data
        // and returns the value. Not always required for 99% of endpoints so save time by not doing it unless the API actually needs it.

        public List<Haven>? _Havens { get; set; } = null;

        public List<Haven> Havens
        {
            get
            {
                if (_Havens == null) _Havens = GetHavens();
                return _Havens;
            }
            set
            {
                _Havens = value;
            }
        }

        public List<Haven> GetHavens()
        {
            return database.connection.Query<Haven>($"SELECT * FROM havens WHERE OwnerID={this.Id}");
        }









    }
}
