using DoceGrao.Database.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using NetHacksPack.Hosting.Abstractions.Providers;

namespace DoceGrao.Database.Infrastructure.Context
{
    public class ApplicationContext : DbContext
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        public ApplicationContext(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (optionsBuilder.Options.FindExtension<SqlServerOptionsExtension>() == null)
                optionsBuilder.UseSqlServer(_connectionStringProvider.GetConnectionString("DefaultConnection"));
        }
    }
}
