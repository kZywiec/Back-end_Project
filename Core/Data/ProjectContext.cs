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
                new User("user2", "user2", UserRole.User) { Id = 3 },
                new User("guest", "guest", UserRole.Guest) { Id = 4 }
                );

            modelBuilder.Entity<Document>().HasData(
                new Document("Public", "pdf", "This's the public test file", 2, DocumentAccessStatus.Public) { Id = 1 },
                new Document("Private", "pdf", "This's the private test file", 2, DocumentAccessStatus.Private) { Id = 2 },
                new Document("Confidential", "pdf", "This's the confidential test file", 2, DocumentAccessStatus.Confidential) { Id = 3 }
                );

            modelBuilder.Entity<Log>().HasData(
                new Log(ActionLog.Upload, 2, 1) { Id = 1 },
                new Log(ActionLog.Upload, 2, 2) { Id = 2 },
                new Log(ActionLog.Upload, 2, 3) { Id = 3 }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}