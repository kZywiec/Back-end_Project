using Core.Entities.DocumentEntities;
using Core.Entities.LogEntities;
using Core.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;

namespace Core.Data
{
    public class ProjectContext : DbContext
    {
        public DbSet<Document> Documents { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Log> Logs { get; set; }

        public ProjectContext(DbContextOptions<ProjectContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>()
               .HasOne(l => l.Author)
               .WithMany()
               .HasForeignKey(l => l.Id)
               .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}