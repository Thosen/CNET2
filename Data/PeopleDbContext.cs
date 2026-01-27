
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class PeopleDbContext : DbContext
    {
        public DbSet<Model.Person> People { get; set; }

        public DbSet<Model.Address> Addresses { get; set; }

        public DbSet<Model.Contract> Contracts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PeopleDatabase;Trusted_Connection=True;");
        }
    }
}
