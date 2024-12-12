using PS.Domain.Models.Person;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


namespace PS.Persistence.Contexts
{
    public class PSDBContext: DbContext
    {

        public PSDBContext(DbContextOptions<PSDBContext> options)
            : base(options)
        {

        }

        public DbSet<PersonInfo> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "Person.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);
            optionsBuilder.UseSqlite(connection);
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PersonInfo>().HasQueryFilter(p => !p.IsRemoved);

        }

    
}
}
