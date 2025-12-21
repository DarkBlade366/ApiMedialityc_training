using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ApiMedialityc_training.Data
{
    public class MedialitycDBContextFactory 
        : IDesignTimeDbContextFactory<MedialitycDBContext>
    {
        public MedialitycDBContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<MedialitycDBContext>();

            optionsBuilder.UseNpgsql(
                configuration.GetConnectionString("DbApiMedialityc")
            );

            return new MedialitycDBContext(optionsBuilder.Options);
        }
    }
}
