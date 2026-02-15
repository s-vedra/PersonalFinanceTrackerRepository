using Microsoft.EntityFrameworkCore;
using PersonalFinanceApplication_DomainModels.Enums;
using PersonalFinanceApplication_DomainModels.Models;

namespace PersonalFinanceApplication_DAL
{
    public class DataContext : DbContext
    {
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserContract> UserContracts { get; set; }
        public DbSet<AccountBalance> AccountBalances { get; set; }
        public DbSet<SalaryScheduler> ScheduledSalaries { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var income = new Income()
            {
                ReferenceId = Guid.NewGuid(),
                IncomeId = 1,
                Date = DateTime.Today,
                PaymentIssue = PaymentIssue.Card,
                Category = IncomeCategory.Salary,
                Amount = 20000,
                Currency = "MKD",
                UserContractId = 1
            };

            var userContract = new UserContract()
            {
                UserContractId = 1,
                UserId = 1,
                ContractType = ContractType.CurrentAccount,
                ContractName = "PCB CA-4879",
                DateOpened = DateTime.Today,
                UserContractStatus = UserContractStatus.Active
            };

            var user = new User()
            {
                UserId = 1,
                FirstName = "admin",
                LastName = "admin",
                UserName = "admin@admin",
                Password = "admin123"
            };

            var accountBalance = new AccountBalance()
            {
                AccountBalanceId = 1,
                Amount = 1070000,
                Currency = "MKD",
                LastDateAddedMoney = DateTime.Parse("2025-12-22 20:14:18.5860000"),
                LastDateDrawMoney = DateTime.Parse("2025-12-22 20:14:18.5860000"),
                UserContractId = 1
            };

            var scheduledSalary = new SalaryScheduler()
            {
                SalarySchedulerId = 1,
                UserContractId = 1,
                Amount = 57890m,
                DayOfMonth = 1,
                IsActive = true,
                LastExecutedAt = DateTime.Today,
                ReferenceId = Guid.NewGuid()
            };

            modelBuilder.Entity<Income>()
            .HasIndex(x => x.ReferenceId)
            .IsUnique();
            modelBuilder.Entity<Income>()
            .Property(x => x.Note)
            .IsRequired(false);
            modelBuilder.Entity<Income>()
            .Property(x => x.Purpose)
            .IsRequired(false);
            modelBuilder.Entity<Income>()
            .Property(e => e.Amount)
            .HasPrecision(18, 6);

            modelBuilder.Entity<Expense>()
            .HasIndex(x => x.ReferenceId)
            .IsUnique();
            modelBuilder.Entity<Expense>()
           .Property(x => x.Note)
           .IsRequired(false);
            modelBuilder.Entity<Expense>()
           .Property(x => x.Purpose)
           .IsRequired(false);
            modelBuilder.Entity<Expense>()
            .Property(e => e.Amount)
            .HasPrecision(18, 6);

            modelBuilder.Entity<Income>()
            .ToTable("Income")
            .HasData(income);

            modelBuilder.Entity<UserContract>()
            .ToTable("UserContract")
            .HasData(userContract);

            modelBuilder.Entity<User>()
           .ToTable("User")
           .HasData(user);

            modelBuilder.Entity<Expense>()
            .ToTable("Expense");

            modelBuilder.Entity<AccountBalance>()
            .ToTable("AccountBalance")
            .HasData(accountBalance);

            modelBuilder.Entity<UserContract>()
            .HasOne(uc => uc.User)
            .WithMany(u => u.UserContracts)
            .HasForeignKey(uc => uc.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalaryScheduler>()
            .ToTable("ScheduledSalary")
            .HasData(scheduledSalary);

            modelBuilder.Entity<SalaryScheduler>()
            .Property(x => x.Notes)
            .IsRequired(false);

            modelBuilder.Entity<SalaryScheduler>()
            .HasIndex(x => x.ReferenceId)
            .IsUnique();
        }
    }
}
