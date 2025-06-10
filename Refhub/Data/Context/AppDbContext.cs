using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Refhub_Ir.Data.Configuration;
using Refhub_Ir.Data.Models;
using Refhub_Ir.Models;
using System.Reflection;

namespace Refhub_Ir.Data.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookKeyword> BookKeywords { get; set; }
        public DbSet<BookRelation> BookRelations { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(AppDbContext)));
            base.OnModelCreating(modelBuilder);
        }
    }
}
