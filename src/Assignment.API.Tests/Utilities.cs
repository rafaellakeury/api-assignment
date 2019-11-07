using Assignment.API.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment.API.Tests
{
    public class Utilities
    {
        public static DbContextOptions<AppDbContext> TestDbContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("assignment-api-in-memory")
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}