using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using NetHacksPack.Hosting.Environment;

namespace DoceGrao.Database.Infrastructure.Context
{
    public class MigrationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

            var migrationEnvironmentVariable = $"ASPNET_CORE_{Assembly.GetExecutingAssembly().GetName().Name}_MIGRATION".Replace(".", "_");
            optionsBuilder.UseSqlServer(migrationEnvironmentVariable.GetString());
            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}
