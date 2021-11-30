using Entities.Configuration;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
           //var books = Books.Include(a => a.Authors).ToList();
        }   
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
              .HasMany<Author>(b => b.Authors)
              .WithMany(a => a.Books)
              .UsingEntity(x => x.ToTable("BookAuthor"));
            modelBuilder.Entity<Book>().HasKey(b => b.Id);
            modelBuilder.Entity<Book>()
                .Property(b => b.Id)
                .HasColumnName("BookId");
            modelBuilder.Entity<Book>()
                .Property(b => b.Name)
                .HasMaxLength(30)
                .IsRequired();
            modelBuilder.Entity<Book>()
                .Property(b => b.Year)
                .IsRequired();

            modelBuilder.Entity<Author>().HasKey(a => a.Id);
            modelBuilder.Entity<Author>()
                .Property(a => a.Id)
                .HasColumnName("AuthorId");
            modelBuilder.Entity<Author>()
                .Property(a => a.Surname)
                .IsRequired();
            //modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            //modelBuilder.ApplyConfiguration(new BookConfiguration());
        }
    }
}
