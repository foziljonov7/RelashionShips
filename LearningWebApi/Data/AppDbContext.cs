using LearningWebApi.Controllers;
using LearningWebApi.Entity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace LearningWebApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Car> Cars { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base (options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<User>()
                .Property(e => e.Username)
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasIndex(e => e.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(e => e.Name)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasOne(d => d.DriverLicense)
                .WithOne(u => u.Holder)
                .HasForeignKey<DriverLicense>(u => u.Id)
                .HasPrincipalKey<User>(u => u.Id);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Cars)
                .WithOne(c => c.Owner)
                .HasPrincipalKey(c => c.Id)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasMany(u => u.Courses)
                .WithMany(c => c.Users);

            modelBuilder.Entity<Car>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<Car>()
                .Property(e => e.Color)
                .HasMaxLength(20);
            modelBuilder.Entity<Car>()
                .Property(e => e.Brand)
                .HasMaxLength(20)
                .IsRequired();
            modelBuilder.Entity<Car>()
                .Property(e => e.Model)
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<Course>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<Course>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Course>()
                .Property(e => e.Name)
                .IsRequired();
        }
    }
}
