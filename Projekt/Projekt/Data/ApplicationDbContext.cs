using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;

namespace Projekt.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Post>()
                .HasMany(c => c.comments)
                .WithOne(e => e.post)
                .IsRequired();

        }

        public DbSet<Post> Post { get; set; }

        public DbSet<Comment> Comment { get; set; }

    }
}
