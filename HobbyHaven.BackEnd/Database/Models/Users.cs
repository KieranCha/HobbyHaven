using HobbyHaven.BackEnd.Database.Models.Havens;
using HobbyHaven.BackEnd.Database.Models.PersonalityTags;
using HobbyHaven.Shared.DTOs.User;
using SQLite;
using static MudBlazor.CategoryTypes;

// https://jasonwatmore.com/post/2021/05/27/net-5-hash-and-verify-passwords-with-bcrypt

namespace HobbyHaven.BackEnd.Database.Models.Users
{


    public class User
    {
        public string? Username { get; set; } = null;
        public bool Admin { get; set; } = false;
        public string Password { get; set; }
        public string Email { get; set; }
        public string ProfilePicture { get; set; } = "";
        public string? AuthorizationToken { get; set; } = null;
        public long Id { get; set; } = 0; // Randomly generated unique identifier assigned to each user.
        public string Bio { get; set; } = "";

        public Database database;

        public bool exists = false;

        public List<string>? _PersonalityTags = null; 
        
        public List<string> PersonalityTags {
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


        public DTOUser DTOUser
        {
            get {
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
            set { }}


        public User()
        {

        }


        // find the user by the email
        public User(string query, bool findUser = true) {
            EstablishDatabase();

            if (query.Contains("@"))
            {
                this.Email = query;
                if (findUser)
                {
                    FindUser($"SELECT * FROM users WHERE Email='{query}' LIMIT 1");
                }
            } else
            {
                this.AuthorizationToken = query;
                if (findUser)
                {
                    FindUser($"SELECT * FROM users WHERE AuthorizationToken='{AuthorizationToken}' LIMIT 1");
                }
            }

        }

        // find the user by the id
        public User(int Id, bool findUser = true)
        {
            EstablishDatabase();
            this.Id = Id;
            if (findUser)
            {
                FindUser($"SELECT * FROM users WHERE Id={Id} LIMIT 1");
            }
        }

        public User(DTOUser user, bool findUser = true)
        {
            EstablishDatabase();
            this.Id = user.Id;
            this.Username = user.Username;
            this.PersonalityTags = user.PersonalityTags;
            this.ProfilePicture = user.ProfilePicture;

            if (findUser)
            {
                FindUser($"SELECT * FROM users WHERE Id={user.Id} LIMIT 1");
            }
        }

        private void EstablishDatabase()
        {
            if (this.database == null)
            {
                database = new Database();
                database.EstablishDatabase();
                database.VerifyTablesPresence();
            }
        }
        
        public void FindUser(string query)
        {
            List<User> users = database.connection.Query<User>(query);
            
            if (users.Count == 0)
            {
                this.exists = false;
                return;
            }

            this.exists = true;

            User targetUser = users[0];

            this.Username = targetUser.Username;
            this.Email = targetUser.Email;
            this.Id = targetUser.Id;
            this.ProfilePicture = targetUser.ProfilePicture;
            this.Password = targetUser.Password;
            this.AuthorizationToken = targetUser.AuthorizationToken;
            this.Admin = targetUser.Admin;
            this.Bio = targetUser.Bio;
        }

        private string GenerateAuthenticationToken()
        {
            // https://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[90];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
        }

        public void Create() {

            if (Id == 0) Id = database.randomId("users");
            if (AuthorizationToken == null) AuthorizationToken = GenerateAuthenticationToken();

            SQLiteCommand command;
            command = database.connection.CreateCommand($"INSERT INTO users VALUES ('{Username}', '{Password}', '{Email}', {Id}, {Admin}, '{ProfilePicture}', '{Bio}', '{AuthorizationToken}')");
            command.ExecuteNonQuery();

            database.connection.Commit();
        }

        public void Update() { database.connection.Commit(); }
        public void SetPassword(string password) { this.Password = BCrypt.Net.BCrypt.HashPassword(password); }
        public bool AuthenticateByPassword(string password) { return BCrypt.Net.BCrypt.Verify(password, this.Password); }
        public List<string> GetPersonalityTags()
        {
            List<string> tags = new();

            foreach (UserPersonalityTag tag in database.connection.Query<UserPersonalityTag>($"SELECT * FROM userPersonalityJunction WHERE UserID={this.Id}")) tags.Add(tag.PersonalityTag);

            return tags;
        }

        public List<Haven> GetHavens()
        {
            return database.connection.Query<Haven>($"SELECT * FROM havens WHERE OwnerID={this.Id}");
        }

    }
}
