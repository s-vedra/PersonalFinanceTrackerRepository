using Microsoft.EntityFrameworkCore;
using PersonalFinanceApplication_DomainModels.Enums;
using PersonalFinanceApplication_DomainModels.Models;

namespace PersonalFinanceApplication_DAL
{
    public class DataContext : DbContext
    {
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var income = new Income()
            {
                IncomeId = 1,
                Date = DateTime.Today,
                Account = Account.Card,
                Category = IncomeCategory.Salary,
                Amount = 20000,
                Currency = "MKD"
            };

            modelBuilder.Entity<Income>()
            .Property(x => x.Note)
            .IsRequired(false);
            modelBuilder.Entity<Income>()
            .Property(x => x.Purpose)
            .IsRequired(false);

            modelBuilder.Entity<Expense>()
           .Property(x => x.Note)
           .IsRequired(false);
            modelBuilder.Entity<Expense>()
            .Property(x => x.Purpose)
            .IsRequired(false);

            modelBuilder.Entity<Income>()
            .ToTable("Income")
            .HasData(income);
        }
    }
}
