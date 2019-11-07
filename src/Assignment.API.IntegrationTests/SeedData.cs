using Assignment.API.Persistence.Contexts;
using Assignment.API.Domain.Models;

namespace Assignment.API.IntegrationTests
{
    public class SeedData
    {
        public static void PopulateTestData(AppDbContext dbContext)
        {
            //Seeding database with Equal people of Id = 1
            dbContext.LeftPeople.Add(new LeftPerson() { Id = 1, Name = "Rafa", Age = 28, City = "Denver", Profession = "Developer" });
            dbContext.RightPeople.Add(new RightPerson() { Id = 1, Name = "Rafa", Age = 28, City = "Denver", Profession = "Developer" });
            
            //Seeding database with different sizes people of Id = 4
            dbContext.LeftPeople.Add(new LeftPerson() { Id = 4, Name = "Rafa", Age = 28, City = "Denver", Profession = "Developer" });
            dbContext.RightPeople.Add(new RightPerson() { Id = 4, City = "Denver", Profession = "Developer" });

            //Seeding database with different people of Id = 5
            dbContext.LeftPeople.Add(new LeftPerson() { Id = 5, Name = "Rafa", Age = 28, City = "Denver", Profession = "Developer" });
            dbContext.RightPeople.Add(new RightPerson() { Id = 5, Name = "Keury", Age = 28, City = "Berlin", Profession = "Developer" });

            dbContext.SaveChanges();
        }
    }
}