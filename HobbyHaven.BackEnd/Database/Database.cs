using HobbyHaven.BackEnd.Database.Models.Users;
using SQLite;


namespace HobbyHaven.BackEnd.Database
{
    public class Database
    {
        public SQLiteConnection connection;


        public Database() { }

        public void EstablishDatabase()
        {
            connection = new SQLiteConnection(@"./database.db");
        }

        public void resetTables()
        {
            SQLiteCommand command;
            foreach (string table in new List<string>() { "users", "havens", "hobby" })
            {
                command = connection.CreateCommand($"DROP TABLE {table}");
                command.ExecuteNonQuery();
            }
        }

        public void VerifyTablesPresence()
        {

            if (connection.GetTableInfo("hobbies").Count() == 0) CreateHobbyTable();
            if (connection.GetTableInfo("users").Count() == 0) CreateUserTable();
            if (connection.GetTableInfo("havens").Count() == 0) CreateHavenTable();
            if (connection.GetTableInfo("userPersonalityJunction").Count() == 0) CreateUserPersonalityJunctionTable();

            connection.Commit();
        }

        public void CreateTable(string tableDetails) { connection.CreateCommand(tableDetails).ExecuteNonQuery(); }


        public void CreateUserTable()
        {
            CreateTable("CREATE TABLE users (Username TEXT, Password TEXT, Email TEXT, Id BIGINT, ADMIN BIT, ProfilePicture TEXT, Bio TEXT, AuthorizationToken TEXT)");
        }

        public void CreateUserPersonalityJunctionTable()
        {
            CreateTable("Create TABLE userPersonalityJunction (UserID BIGINT, PersonalityTag TEXT)");
        }

        public void CreateHavenTable()
        {
            CreateTable("CREATE TABLE havens (Name TEXT, Description TEXT, Id BIGINT, Location TEXT, AssociatedHobbies TEXT, OwnerID BIGINT)");
        }

        public void CreateHobbyTable()
        {
            CreateTable("CREATE TABLE hobbies (Name TEXT, Description TEXT, Id BIGINT)");
        }

        public void CreateHobbyPersonalityJunctionTable()
        {
            CreateTable("CREATE TABLE hobbyPersonalityJunction (HobbyId BIGINT, PersonalityTag TEXT)");
        }

        private long generateId(Random rand)
        {
            byte[] buf = new byte[8];
            rand.NextBytes(buf);

            long longRand = BitConverter.ToInt64(buf, 0);
            return (Math.Abs(longRand % (999999999999999999 - 111111111111111111)) + 111111111111111111);
        }

        public long randomId(string? TableCheck = null)
        {
            long chosen = generateId(new Random());

            if (TableCheck != null)
            {
                bool found = false;
                while (!found)
                {
                    List<long> ids = connection.Query<long>($"SELECT Id FROM {TableCheck} WHERE Id={chosen} LIMIT 1");
                    if (ids.Count() == 0) return chosen;
                    chosen = generateId(new Random());
                }
            }

            return chosen;
        }


    }
}

