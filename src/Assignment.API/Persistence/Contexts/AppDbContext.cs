using Assignment.API.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment.API.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<LeftPerson> LeftPeople { get; set; }
        public DbSet<RightPerson> RightPeople { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<LeftPerson>().ToTable("LeftPeople");
            builder.Entity<LeftPerson>().Property(p => p.Id);
            builder.Entity<LeftPerson>().Property(p => p.Name);
            builder.Entity<LeftPerson>().Property(p => p.Age);
            builder.Entity<LeftPerson>().Property(p => p.City);
            builder.Entity<LeftPerson>().Property(p => p.Profession);

            builder.Entity<RightPerson>().ToTable("RightPeople");
            builder.Entity<RightPerson>().Property(p => p.Id);
            builder.Entity<RightPerson>().Property(p => p.Name);
            builder.Entity<RightPerson>().Property(p => p.Age);
            builder.Entity<RightPerson>().Property(p => p.City);
            builder.Entity<RightPerson>().Property(p => p.Profession);
        }
    }
}