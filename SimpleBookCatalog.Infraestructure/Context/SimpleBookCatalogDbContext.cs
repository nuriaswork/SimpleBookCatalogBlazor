using Microsoft.EntityFrameworkCore;
using SimpleBookCatalog.Domain.Entities;

namespace SimpleBookCatalog.Infraestructure.Context
{
    public class SimpleBookCatalogDbContext : DbContext
    {
        public SimpleBookCatalogDbContext(DbContextOptions<SimpleBookCatalogDbContext> options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }


        //If we were to use Fluent API to configure the model (instead of DataAnnotations in Book.cs),
        //we would override the OnModelCreating method and use the ModelBuilder to configure the entities and their relationships, like this:
        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Author).IsRequired().HasMaxLength(100);
            });
           //OR:
            //modelBuilder.Entity<Book>().Property(e=>e.Title).IsRequired().HasMaxLength(100);
            //modelBuilder.Entity<Book>().Property(e => e.Author).IsRequired().HasMaxLength(100);
           
        }*/
    }
}
