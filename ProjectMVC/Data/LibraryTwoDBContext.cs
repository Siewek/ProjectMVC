using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectMVC.Data
{
    public class LibraryTwoDBContext : IdentityDbContext<LibraryTwoUser>
    {
        public LibraryTwoDBContext(DbContextOptions<LibraryTwoDBContext> options) 
            : base(options) { }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }
        public DbSet<BookTag> BookTags { get; set; }

        public DbSet<Order> Orders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AuthorBook>().HasKey(ab => new { ab.BookID, ab.AuthorID });
            modelBuilder.Entity<AuthorBook>().HasOne(ab => ab.Author).WithMany(b => b.BooksByAuthor).HasForeignKey(ab => ab.AuthorID);
            modelBuilder.Entity<AuthorBook>().HasOne(ab => ab.Book).WithMany(a => a.BooksByAuthor).HasForeignKey(ab => ab.BookID);

            modelBuilder.Entity<BookTag>().HasKey(bt => new { bt.BookID, bt.TagID });
            modelBuilder.Entity<BookTag>().HasOne(bt => bt.Book).WithMany(t => t.BooksByTags).HasForeignKey(bt => bt.BookID);
            modelBuilder.Entity<BookTag>().HasOne(bt => bt.Tag).WithMany(b => b.BooksByTags).HasForeignKey(bt => bt.TagID);
        }

    }
}

