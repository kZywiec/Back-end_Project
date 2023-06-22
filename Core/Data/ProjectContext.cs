using Core.Entities.DocumentEntities;
using Core.Entities.LogEntities;
using Core.Entities.UserEntities;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

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
                .WithMany(u => u.Logs)
                .HasForeignKey(l => l.AuthorId)
                .OnDelete(DeleteBehavior.NoAction); // Or DeleteBehavior.Restrict


            modelBuilder.Entity<Log>()
                .HasOne(l => l.Document)
                .WithMany(d => d.Logs)
                .HasForeignKey(l => l.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Document>()
                .HasOne(d => d.Uploader)
                .WithMany(u => u.Documents)
                .HasForeignKey(d => d.UploaderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>().HasData(
                new User("admin", "admin", UserRole.Admin) { Id = 1},
                new User("user", "user", UserRole.User) { Id = 2 },
                new User("guest", "guest", UserRole.Guest) { Id = 3 }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}