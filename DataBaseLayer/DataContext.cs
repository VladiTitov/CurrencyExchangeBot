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
            optionsBuilder.UseSqlite(@"Data Source=..\\banks.db");
            //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=bankDB;Trusted_Connection=True;");
    }
}
