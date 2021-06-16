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


        public AuthorsDbContext(DbContextOptions<AuthorsDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().ToTable("Author");
        }
    }
}
