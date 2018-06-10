using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Core
{
    public class Context : DbContext
    {
        public Context() : base("TeamProject") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Deadline> Deadlines { get; set; }
        public DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasMany<Deadline>(u => u.Deadlines);
            modelBuilder.Entity<Deadline>().HasMany<Task>(d => d.Tasks);
        }
    }
}
