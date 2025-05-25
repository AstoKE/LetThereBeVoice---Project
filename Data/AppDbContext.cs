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
        public DbSet<UserRecentActivity> UserRecentActivity { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<VoiceSession> VoiceSessions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ServerRole> ServerRoles { get; set; }


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

            modelBuilder.Entity<UserRecentActivity>().HasNoKey().ToView("UserRecentActivity");
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.Requester)
                .WithMany()
                .HasForeignKey(f => f.RequesterId)
                .OnDelete(DeleteBehavior.Restrict); // ✅

            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.Receiver)
                .WithMany()
                .HasForeignKey(f => f.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict); // ✅

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserServer>()
                .HasKey(us => new { us.UserID, us.ServerID });
        }

    }
}
