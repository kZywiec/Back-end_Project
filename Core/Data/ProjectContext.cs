using Core.Entities.Document;
using Core.Entities.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data
{
    public class ProjectContext : DbContext
    {
        public DbSet<Document> Documents { get; set; }
        public DbSet<User> Users { get; set; }

        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=DocumentsDb;Trusted_Connection=True;");
            }
        }
    }
}
