using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Departments;
using WebAppServer.Models.Employees;
using WebAppServer.Models.Users;

namespace WebAppServer.DbContexts
{
    public class ApplicationContext: DbContext
    {
        public DbSet<User> Users { get; set; } = null;
        public DbSet<Department> Departments { get; set; } = null;
        public DbSet<Employee> Employees { get; set; } = null;
        public DbSet<Education> Educations { get; set; } = null;
        public DbSet<WorkExperience> WorkExperience { get; set; } = null;
        public DbSet<UserFile> UserFiles { get; set; } = null;

        public ApplicationContext() 
        { 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
            var connectionString = configuration.GetConnectionString("ApplicationContext");
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 28)));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
