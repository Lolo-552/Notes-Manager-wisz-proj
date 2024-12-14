using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Notes_Manager.Models;

namespace Notes_Manager.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Notes_Manager.Models.Notes> Notes { get; set; }
        public DbSet<Category> Category { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Notes)
                .WithOne(n => n.user)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Category)
                .WithOne(c => c.user)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Notes>()
                .HasOne(n => n.Category)
                .WithMany(c => c.Notes)
                .HasForeignKey(n => n.CategoryId);
        }
    }
}