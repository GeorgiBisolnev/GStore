namespace StorKoorespondencii.Data
{
   
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using StorKoorespondencii.Data.Models;

    public class StoreDbContext : DbContext
    {
        public StoreDbContext() { }

        public StoreDbContext(DbContextOptions options)
            : base(options) {
        
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .AddJsonFile("Data\\appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                var connectionString = configuration.GetConnectionString("StoreConnectionString");
                optionsBuilder.UseSqlServer(connectionString);
            }            
        }

        public DbSet<User> Login { get; set; }
        public DbSet<UserPermition> USCCPerm { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
