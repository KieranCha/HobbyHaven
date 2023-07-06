
using Microsoft.EntityFrameworkCore;

using HobbyHaven.BackEnd.Database.Models;

namespace HobbyHaven.BackEnd.Database
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options) { 

		}

		public DbSet<Haven> Havens { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Hobby> Hobbies { get; set; }
		public DbSet<PersonalityTag> PersonalityTags { get; set; }
	}

}
