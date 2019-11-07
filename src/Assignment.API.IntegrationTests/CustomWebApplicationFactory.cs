using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Assignment.API.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.TestHost;

namespace Assignment.API.IntegrationTests
{
    public class CustomWebApplicationFactory<T> : WebApplicationFactory<Startup>
    {
        public TestServer CreateTestServer()
        {
            return new TestServer(new WebHostBuilder().UseStartup<Startup>().ConfigureServices(
                services =>
                {
                    var serviceProvider = new ServiceCollection()
                        .AddEntityFrameworkInMemoryDatabase()
                        .BuildServiceProvider();

                    services.AddDbContext<AppDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("assignment-api-in-memory");
                        options.UseInternalServiceProvider(serviceProvider);
                    });

                    var sp = services.BuildServiceProvider();

                    using (var scope = sp.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        var appDb = scopedServices.GetRequiredService<AppDbContext>();

                        var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<Startup>>>();

                        appDb.Database.EnsureCreated();

                        try
                        {
                            SeedData.PopulateTestData(appDb);
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, "An error occurred seeding the " +
                                                "database with test messages. Error: {ex.Message}");
                        }
                    }
                }
            ));
        }
    }
}