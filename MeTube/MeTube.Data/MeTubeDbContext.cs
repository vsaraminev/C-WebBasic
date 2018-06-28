namespace MeTube.Data
{
    using Models;
    using Microsoft.EntityFrameworkCore;

    public class MeTubeDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Tube> Tubes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(ServerConstants.ConnectionString);
            }

            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            builder.Entity<User>()
                .HasMany(u => u.Tubes)
                .WithOne(t => t.Uploader)
                .HasForeignKey(t => t.UploaderId);
        }
    }
}
