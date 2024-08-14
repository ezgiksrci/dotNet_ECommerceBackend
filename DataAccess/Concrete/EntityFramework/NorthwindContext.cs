using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    // Context: DB Tabloları <--> Class bağlamak
    public class NorthwindContext : DbContext
    {
        // OnConfiguring(): Proje hangi DB ile ilişkili
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // @ : string içerisindeki ters slash'ın (\) bir anlamı vardır. Ama bizim stringimiz de \ içeriyorsa onu özel olarak algılama demek. 
            // @"Connection_String"
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Northwind;Trusted_Connection=true");
        }

        // DbSet: DB'de hangi tabloya ne karşılık gelecek...
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
