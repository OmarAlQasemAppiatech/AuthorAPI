using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Author_API.Entities
{
    public class AuthorsDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }



        public AuthorsDbContext(DbContextOptions<AuthorsDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Author>()
            //            .HasMany(c => c.Books)
            //            .WithMany(e => e.Authors);
            //modelBuilder.Entity<Book>()
            //            .HasMany(c => c.Authors)
            //            .WithMany(e => e.Books);
            //modelBuilder.Entity<Publisher>()
            //            .HasMany(c => c.Books)
            //            .WithOne(e => e.Publisher);

        }
    }
}
