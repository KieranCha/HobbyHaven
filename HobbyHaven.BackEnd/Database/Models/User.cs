namespace HobbyHaven.BackEnd.Database.Models
{
    public class User
    {
        public User () { }
        public string UserID { get; set; }
        public bool Admin { get; set; } = false;
    }

}
