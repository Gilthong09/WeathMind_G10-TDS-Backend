using Microsoft.EntityFrameworkCore;
using WealthMind.Core.Domain.Common;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        //public DbSet<CategoryType> CategoryTypes { get; set; }
        public DbSet<ChatbotMessage> ChatbotMessages { get; set; }
        public DbSet<ChatbotSession> ChatbotSessions { get; set; }
        public DbSet<FinancialGoal> FinancialGoals { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<PaymentPlan> PaymentPlans { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Product> Products { get; set; }

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
            //modelBuilder.Entity<CategoryType>().HasKey(ct => ct.Name);
            modelBuilder.Entity<ChatbotMessage>().HasKey(cm => cm.Id);
            modelBuilder.Entity<ChatbotSession>().HasKey(cs => cs.Id);
            modelBuilder.Entity<FinancialGoal>().HasKey(fg => fg.Id);
            modelBuilder.Entity<Recommendation>().HasKey(r => r.Id);
            modelBuilder.Entity<Report>().HasKey(rp => rp.Id);
            modelBuilder.Entity<Transaction>().HasKey(t => t.Id);
            modelBuilder.Entity<Payment>().HasKey(t => t.Id);
            modelBuilder.Entity<PaymentPlan>().HasKey(t => t.Id);
            modelBuilder.Entity<Product>().HasKey(t => t.Id);

            modelBuilder.Entity<Product>()
            .HasDiscriminator<string>("ProductType")
            .HasValue<CreditCard>("CreditCard")
            .HasValue<Loan>("Loan")
            .HasValue<Saving>("Saving")
            .HasValue<Investment>("Investment")
            .HasValue<Cash>("Cash");

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.FromProduct)
                .WithMany()
                .HasForeignKey(t => t.FromProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.ToProduct)
                .WithMany()
                .HasForeignKey(t => t.ToProductId)
                .OnDelete(DeleteBehavior.Restrict);

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

            modelBuilder.Entity<Recommendation>()
                .HasOne(cm => cm.Report)
                .WithMany(cs => cs.Recommendations)
                .HasForeignKey(cm => cm.ReportId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
