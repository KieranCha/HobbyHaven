using HobbyHaven.BackEnd.Database.Models.Havens;
using HobbyHaven.BackEnd.Database.Models.PersonalityTags;
using HobbyHaven.Shared.DTOs.User;
using SQLite;

namespace HobbyHaven.BackEnd.Database.Models.Users
{


    public class User : DatabaseManager
    {

        public User()
        {

        }


        // Find the user by the email
        public User(string? query, bool findUser = true) {

            if (query == null) return; 

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

        // Find the user by the id
        public User(long Id, bool findUser = true)
        {
            this.Id = Id;
            if (findUser)
            {
                FindUser($"SELECT * FROM users WHERE Id={Id} LIMIT 1");
            }
        }

        // Find the user by a DTO
        public User(DTOUser user, bool findUser = true)
        {
            this.Id = user.Id;
            this.Username = user.Username;
            this.PersonalityTags = user.PersonalityTags;
            this.ProfilePicture = user.ProfilePicture;

            if (findUser)
            {
                FindUser($"SELECT * FROM users WHERE Id={user.Id} LIMIT 1");
            }
        }

        public void Update() { database.connection.Commit(); }
        public void SetPassword(string password) { this.Password = BCrypt.Net.BCrypt.HashPassword(password); }
        public bool AuthenticateByPassword(string password) { return BCrypt.Net.BCrypt.Verify(password, this.Password); }
        public bool AuthenticateByToken(string? token) { return this.AuthorizationToken == token && token != null; }


    }
}
