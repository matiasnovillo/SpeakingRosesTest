using SpeakingRosesTest.Areas.System.FailureBack.Entities;
using SpeakingRosesTest.Areas.System.FailureBack.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;
using SpeakingRosesTest.Areas.SpeakingRosesTest.TasksBack.Entities;
using SpeakingRosesTest.Areas.SpeakingRosesTest.PriorityBack.Entities;
using SpeakingRosesTest.Areas.SpeakingRosesTest.StatusBack.Entities;
using SpeakingRosesTest.Areas.SpeakingRosesTest.TasksBack.EntitiesConfiguration;
using SpeakingRosesTest.Areas.SpeakingRosesTest.PriorityBack.EntitiesConfiguration;
using SpeakingRosesTest.Areas.SpeakingRosesTest.StatusBack.EntitiesConfiguration;

namespace SpeakingRosesTest.DatabaseContexts
{
    public class TaskDBContext : DbContext
    {
        protected IConfiguration _configuration { get; }

        public DbSet<Failure> Failure { get; set; }

        //SpeakingRosesTest
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Priority> Priority { get; set; }
        public DbSet<Status> Status { get; set; }

        public TaskDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                string ConnectionString = "";
#if DEBUG
                ConnectionString = "data source =.; " +
                    "initial catalog = TaskDB; " +
                    "Integrated Security = SSPI;" +
                    " MultipleActiveResultSets=True;" +
                    "Pooling=false;" +
                    "Persist Security Info=True;" +
                    "App=EntityFramework;" +
                    "TrustServerCertificate=True;";
#else
                ConnectionString = "Password=;" +
                    "Persist Security Info=True;" +
                    "User ID=;" +
                    "Initial Catalog=;" +
                    "Data Source=;" +
                    "TrustServerCertificate=True";
#endif

                optionsBuilder
                    .UseSqlServer(ConnectionString);
            }
            catch (Exception) { throw; }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.ApplyConfiguration(new FailureConfiguration());
                modelBuilder.Entity<Failure>().ToTable("System.Failure");

                //SpeakingRosesTest
                modelBuilder.ApplyConfiguration(new TasksConfiguration());
                modelBuilder.Entity<Tasks>().ToTable("SpeakingRosesTest.Tasks");
                modelBuilder.ApplyConfiguration(new PriorityConfiguration());
                modelBuilder.Entity<Priority>().ToTable("SpeakingRosesTest.Priority");
                modelBuilder.ApplyConfiguration(new StatusConfiguration());
                modelBuilder.Entity<Status>().ToTable("SpeakingRosesTest.Status");
            }
            catch (Exception) { throw; }
        }
    }
}
