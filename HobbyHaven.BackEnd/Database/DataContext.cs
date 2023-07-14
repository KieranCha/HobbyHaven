
using Microsoft.EntityFrameworkCore;

using HobbyHaven.BackEnd.Database.Models;

namespace HobbyHaven.BackEnd.Database
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options) { 

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
				.HasMany(user => user.PersonalityTags)
				.WithMany(tag => tag.Users);

            modelBuilder.Entity<User>()
                .HasMany(user => user.Hobbies)
                .WithMany(hobby => hobby.Users);


            modelBuilder.Entity<Hobby>()
                .HasMany(hobby => hobby.PersonalityTags)
                .WithMany(tag => tag.Hobbies);
        }

		public DbSet<Haven> Havens { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Hobby> Hobbies { get; set; }
		public DbSet<PersonalityTag> PersonalityTags { get; set; }
	}

}
