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
                Currency = "MKD",
                UserContractId = 1
            };

            var userContract = new UserContract()
            {
                UserContractId = 1,
                UserId = 1,
                AccountBalanceId = 1,
                ContractType = ContractType.CurrentAccount
            };

            var user = new User()
            {
                UserId = 1,
                FirstName = "admin",
                LastName = "admin",
                UserName = "admin@admin",
                Password = "admin123"
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

            modelBuilder.Entity<UserContract>()
            .ToTable("UserContract")
            .HasData(userContract);

            modelBuilder.Entity<User>()
           .ToTable("User")
           .HasData(user);

            modelBuilder.Entity<UserContract>()
            .HasOne(uc => uc.User)
            .WithMany(u => u.UserContracts)
            .HasForeignKey(uc => uc.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
