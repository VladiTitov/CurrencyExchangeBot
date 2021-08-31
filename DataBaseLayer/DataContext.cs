using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
            optionsBuilder.UseSqlite(@"Data Source=..\..\..\..\\CurrencyExchangeBotDB.db");
        //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=banks;Username=postgres;Password=]]]]"); 
        //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=bankDB;Trusted_Connection=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Branch>().HasMany(m => m.Quotations).WithOne(o => o.Branch).HasForeignKey(k => k.BranchId);
            modelBuilder.Entity<Branch>().HasMany(m => m.Phones).WithOne(o => o.Branch).IsRequired().HasForeignKey(k => k.BranchId);
            modelBuilder.Entity<Branch>().HasOne(o => o.Bank).WithMany(m => m.Branches).HasForeignKey(k => k.BankId);
            modelBuilder.Entity<Branch>().HasOne(o => o.City).WithMany(m => m.Branches).HasForeignKey(k => k.CityId);
            modelBuilder.Entity<Bank>().HasMany(m => m.Branches).WithOne(o => o.Bank).HasForeignKey(k => k.BankId);
            modelBuilder.Entity<Currency>().HasMany(m => m.Quotations).WithOne(o => o.Currency).HasForeignKey(k => k.CurrencyId);
        }
        
    }
}
