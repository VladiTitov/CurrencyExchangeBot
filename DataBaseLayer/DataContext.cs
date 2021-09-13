using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataBaseLayer
{
    public class DataContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Quotation> Quotations { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<UserState> States { get; set; }

        public DataContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => 
            optionsBuilder.UseNpgsql("Host=10.5.0.2;Port=5432;Database=postgres;Username=postgres;Password=postgres");
    }
}
