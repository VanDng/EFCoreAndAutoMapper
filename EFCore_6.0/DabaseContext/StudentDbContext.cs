using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore6
{
    public class StudentDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Class> Classes { get; set; }

        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
        }

        public StudentDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.Config();
        }
    }

    public class StudentDbContextFactory : IDesignTimeDbContextFactory<StudentDbContext>
    {
        public StudentDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StudentDbContext>();
            optionsBuilder.Config();

            return new StudentDbContext(optionsBuilder.Options);
        }
    }

    public static class DBContextBuilder
    {
        public static void Config(this DbContextOptionsBuilder dbContextOptionBuilder)
        {
            var connectionString = CommonConfig.ConnectionString.Get();
            dbContextOptionBuilder.UseSqlServer(connectionString);

            dbContextOptionBuilder.EnableSensitiveDataLogging();
            dbContextOptionBuilder.LogTo(Console.WriteLine);
        }
    }
}
