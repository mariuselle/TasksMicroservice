
using System.Data.Entity;
using Tasks.Domain.Entities;

namespace Tasks.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
