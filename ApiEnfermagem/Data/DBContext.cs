using ApiEnfermagem.Models.Content;
using ApiEnfermagem.Models.Security;
using Microsoft.EntityFrameworkCore;

namespace ApiEnfermagem.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<TopicImage> TopicImages { get; set; }
        public DbSet<ForumPost> ForumPosts { get; set; }
        public DbSet<ForumReply> ForumReplies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Admin>()
                .HasIndex(a => a.Username)
                .IsUnique();

            modelBuilder.Entity<Article>()
                .HasOne(a => a.Topic)
                .WithMany(t => t.Articles)
                .HasForeignKey(a => a.TopicID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ForumReply>()
                .HasOne(r => r.Post)
                .WithMany(p => p.Replies)
                .HasForeignKey(r => r.PostID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}