using Microsoft.EntityFrameworkCore;

namespace ThumbsComments.Models
{
    /// <summary>
    /// Database context
    /// </summary>
    public class Context : DbContext
    {
        /// <summary>
        /// Context constructor
        /// </summary>
        /// <param name="options">Options</param>
        public Context(DbContextOptions<Context> options)
           : base(options)
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Comments
        /// </summary>
        public DbSet<Comment> Comments { get; set; }

        /// <summary>
        /// Runs on creation
        /// </summary>
        /// <param name="modelBuilder">Model builder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                        .HasIndex(p => p.LineOfBusiness)
                        .IsUnique();         
        }
    }
}
