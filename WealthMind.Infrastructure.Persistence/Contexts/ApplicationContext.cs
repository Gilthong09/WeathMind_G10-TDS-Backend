using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WealthMind.Core.Domain.Common;
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
        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<PaymentPlan> PaymentPlans { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Investment> Investments { get; set; }
        public DbSet<Saving> Savings { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "DefaultAppUser";
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasKey(c => c.Id);
            modelBuilder.Entity<ChatbotMessage>().HasKey(cm => cm.Id);
            modelBuilder.Entity<ChatbotSession>().HasKey(cs => cs.Id);
            modelBuilder.Entity<FinancialGoal>().HasKey(fg => fg.Id);
            modelBuilder.Entity<Investment>().HasKey(i => i.Id);
            modelBuilder.Entity<Recommendation>().HasKey(r => r.Id);
            modelBuilder.Entity<Report>().HasKey(rp => rp.Id);
            modelBuilder.Entity<Saving>().HasKey(s => s.Id);
            modelBuilder.Entity<Transaction>().HasKey(t => t.Id);
            modelBuilder.Entity<Payment>().HasKey(t => t.Id);
            modelBuilder.Entity<PaymentPlan>().HasKey(t => t.Id);
            modelBuilder.Entity<Cash>().HasKey(t => t.Id);
            modelBuilder.Entity<CreditCard>().HasKey(t => t.Id);
            modelBuilder.Entity<Loan>().HasKey(t => t.Id);

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

            modelBuilder.Entity<Payment>()
                .HasOne(cm => cm.PaymentPlan)
                .WithMany(cs => cs.Payments)
                .HasForeignKey(cm => cm.PaymentPlanId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
