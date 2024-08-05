using Finances.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Finances.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<News> News { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<ScrapedDataYahooCrypto> ScrapedDataYahooCrypto { get; set; }
        public DbSet<UserInRole> UserInRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Category>().HasData(
                new Category{Id = 1, Name = "Action", DisplayOrder = 1},
                new Category{ Id = 2, Name = "Sci-fi", DisplayOrder = 2 },
                new Category{ Id = 3, Name = "History", DisplayOrder = 3 }

                );

            modelBuilder.Entity<News>().HasData(
                new News { Id = 1, Author = "Admin", Description = "description0", CreatedDate = new DateTime(), Title = "News0"},
                new News { Id = 2, Author = "Admin", Description = "description1", CreatedDate = new DateTime(), Title = "News1" },
                new News { Id = 3, Author = "Admin", Description = "description2", CreatedDate = new DateTime(), Title = "News2" },
                new News { Id = 4, Author = "Admin", Description = "description3", CreatedDate = new DateTime(), Title = "News3" },
                new News { Id = 5, Author = "Admin", Description = "description4", CreatedDate = new DateTime(), Title = "News4" },
                new News { Id = 6, Author = "Admin", Description = "description5", CreatedDate = new DateTime(), Title = "News5" },
                new News { Id = 7, Author = "Admin", Description = "description6", CreatedDate = new DateTime(), Title = "News6" }
                );

        }

    }
}
