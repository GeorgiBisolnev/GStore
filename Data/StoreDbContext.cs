namespace StorKoorespondencii.Data
{
   
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using StorKoorespondencii.Data.Models;
    using StorKoorespondencii.DataProcessing;

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
                Configuration.ConnectionString = connectionString;
                optionsBuilder.UseSqlServer(connectionString);
            }            
        }

        public DbSet<User> Login { get; set; }
        public DbSet<UserPermition> USCCPerm { get; set; }
        public DbSet<Charger> SCharger { get; set; }
        public DbSet<SDOType> SDOType { get; set; }

        public DbSet<SpResults> Sp_SetUpPermUserId { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
