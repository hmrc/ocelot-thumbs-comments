using Microsoft.EntityFrameworkCore;

namespace ThumbsComments.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
           : base(options)
        {
           // Database.EnsureCreated();
        }

        /// <summary>
        /// Comments
        /// </summary>
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                        .HasIndex(p => p.LineOfBusiness)
                        .IsUnique();         
        }
    }
}
