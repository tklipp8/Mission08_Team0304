using Microsoft.EntityFrameworkCore;

namespace Mission08_Team0304.Models
{
    public class TasksContext : DbContext
    {
        public TasksContext(DbContextOptions<TasksContext> options) : base(options)
        {

        }
        public DbSet<Task> Tasks { get; set; }
    }
}
