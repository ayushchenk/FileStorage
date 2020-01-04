using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileStorage.DAL.Model
{
    public class FileStorageContext : IdentityDbContext<User, Role, Guid>
    {
        public FileStorageContext(DbContextOptions<FileStorageContext> options): base(options)
        {
            Database.EnsureCreated();
            if (!Roles.AnyAsync().Result)
            {
                Roles.Add(new Role { Name = "User", NormalizedName = "USER" });
                Roles.Add(new Role { Name = "Admin", NormalizedName = "ADMIN" });
                SaveChanges();
            }
        }

        public virtual DbSet<Folder> Folders { set; get; }
        public virtual DbSet<Category> Categories { set; get; }
        public virtual DbSet<File> Files { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<File>(b =>
            {
                b.HasKey(f => f.Id);
                b.Property(f => f.Id);
                b.Ignore(f => f.Path);
                b.Property(f => f.FileName).IsRequired().HasMaxLength(256);
                b.Property(f => f.ShortLink).IsRequired(false).HasMaxLength(32);
            });

            modelBuilder.Entity<Category>(b => 
            {
                b.HasKey(c => c.Id);
                b.Property(c => c.Id);
                b.Property(c => c.CategoryName).IsRequired().HasMaxLength(64);
            });

            modelBuilder.Entity<Folder>(b =>
            {
                b.HasKey(f => f.Id);
                b.Property(f => f.Id);
                b.Property(f => f.FolderName).IsRequired().HasMaxLength(64);
                b.Ignore(f => f.ParentPath);
            });

            modelBuilder.Entity<User>(b =>
            {
                b.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");
                b.Property(u => u.FirstName).HasMaxLength(32).IsRequired(false);
                b.Property(u => u.LastName).HasMaxLength(32).IsRequired(false);
            });

            modelBuilder.Entity<Role>()
                .Property(u => u.Id)
                .HasDefaultValueSql("newsequentialid()");

            base.OnModelCreating(modelBuilder);
        }
    }
}
