namespace HobbyHaven.BackEnd.Decorators.Authentication
{
    public interface IDataController
    {
        public DataContext _context { get; set; }
    }
}
