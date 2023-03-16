using SQLite;

namespace HobbyHaven.BackEnd.Database.Models.Users
{
    public class DatabaseManager : UserAttributes
    {

        private void EstablishDatabase()
        {
            if (this.database == null)
            {
                database = new Database();
                database.EstablishDatabase();
                database.VerifyTablesPresence();
            }
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

        public void Create()
        {

            if (Id == 0) Id = database.randomId("users");
            if (AuthorizationToken == null) AuthorizationToken = GenerateAuthenticationToken();

            SQLiteCommand command = database.connection.CreateCommand($"INSERT INTO users VALUES ('{Username}', '{Password}', '{Email}', {Id}, {Admin}, '{ProfilePicture}', '{Bio}', '{AuthorizationToken}')");
            command.ExecuteNonQuery();

            database.connection.Commit();
        }

        public void FindUser(string query)
        {
            EstablishDatabase();
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

    }
}
