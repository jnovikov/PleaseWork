using BackendApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendApi.Models
{
    public class MyContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Deadline> Deadlines { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public MyContext(DbContextOptions<MyContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Deadline>().HasMany<Task>(d => d.Tasks)
                .WithOne(t => t.Deadline).IsRequired();

            modelBuilder.Entity<Task>().HasOne<Deadline>(t => t.Deadline)
                .WithMany(d => d.Tasks);

            modelBuilder.Entity<Deadline>()
                .Property(f => f.Finish)
                .HasColumnType("datetime2");

            modelBuilder.Entity<Task>()
                .Property(f => f.WorkTime)
                .HasColumnType("datetime2");
        }
    }
}