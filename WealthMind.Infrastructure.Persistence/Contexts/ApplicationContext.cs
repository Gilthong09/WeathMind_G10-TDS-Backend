using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<ChatbotMessage> ChatbotMessages { get; set; }
        public DbSet<ChatbotSession> ChatbotSessions { get; set; }
        public DbSet<FinancialGoal> FinancialGoals { get; set; }
        public DbSet<Investment> Investments { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Saving> Savings { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de claves primarias
            modelBuilder.Entity<Category>().HasKey(c => c.Id);
            modelBuilder.Entity<ChatbotMessage>().HasKey(cm => cm.Id);
            modelBuilder.Entity<ChatbotSession>().HasKey(cs => cs.Id);
            modelBuilder.Entity<FinancialGoal>().HasKey(fg => fg.Id);
            modelBuilder.Entity<Investment>().HasKey(i => i.Id);
            modelBuilder.Entity<Recommendation>().HasKey(r => r.Id);
            modelBuilder.Entity<Report>().HasKey(rp => rp.Id);
            modelBuilder.Entity<Saving>().HasKey(s => s.Id);
            modelBuilder.Entity<Transaction>().HasKey(t => t.Id);

            // Configuración de relaciones
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ChatbotMessage>()
                .HasOne(cm => cm.Session)
                .WithMany(cs => cs.Messages)
                .HasForeignKey(cm => cm.SessionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
