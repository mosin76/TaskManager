using Microsoft.EntityFrameworkCore;
using System.TaskItem.API.Model.ApplicationModel;
namespace System.TaskItem.API.Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
    options.UseSqlite("DataSource = TaskDb; Cache=Shared");
        public DbSet<SprintTask> SprintTask { get; set; }
    }
}
