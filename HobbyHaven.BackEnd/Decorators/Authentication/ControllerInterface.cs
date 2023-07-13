namespace HobbyHaven.BackEnd.Decorators.Authentication
{
    public interface IDataController
    {
        public DataContext _context { get; set; }
        public AuthenticationLinkSettings _authenticationLinkSettings { get; set; }

    }
}
