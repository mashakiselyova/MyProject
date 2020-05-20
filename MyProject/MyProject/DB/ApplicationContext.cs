using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyProject.Models;

namespace MyProject.DB
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Dictionary> Dictionaries { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<RevisionWord> RevisionWords { get; set; }
        public DbSet<Translation> Translations { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "0",
                Name = "admin",
                NormalizedName = "ADMIN"
            });
        }
    }
}
