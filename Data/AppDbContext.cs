using Microsoft.EntityFrameworkCore;
using LetThereBeVoice.Models;

namespace LetThereBeVoice.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserServer> UserServer { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.RegistrationDate)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<User>()
                .Property(u => u.Status)
                .HasDefaultValue("Active");

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.SenderID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Server>()
                .HasOne(s => s.Creator)
                .WithMany(u => u.CreatedServers)
                .HasForeignKey(s => s.CreatorID)
                .OnDelete(DeleteBehavior.Restrict);

            // ✅ Composite primary key tanımı
            modelBuilder.Entity<UserServer>()
                .HasKey(us => new { us.UserID, us.ServerID });
        }

    }
}
