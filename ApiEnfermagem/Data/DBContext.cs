using ApiEnfermagem.Models.Security;
using Microsoft.EntityFrameworkCore;

namespace ApiEnfermagem.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        // DbSets representam as tabelas
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Garante unicidade do Username via código também
            modelBuilder.Entity<Admin>()
                .HasIndex(a => a.Username)
                .IsUnique();

            // Configuração do Delete Cascade (Se apagar Tópico, apaga Artigos)
            modelBuilder.Entity<Article>()
                .HasOne(a => a.Topic)
                .WithMany(t => t.Articles)
                .HasForeignKey(a => a.TopicID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}