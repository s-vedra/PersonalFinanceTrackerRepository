using Microsoft.EntityFrameworkCore;
using PFA_DM.Models;

namespace PFA_DAL
{
    public class DataContext : DbContext
    {
        public DbSet<AccountBalance> AccountBalances { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var accountBalance = new AccountBalance()
            {
                AccountBalanceId = 1, 
                Amount = 10000, 
                Currency = "MKD", 
                LastDateAddedMoney = DateTime.Today, 
                LastDateDrawMoney = DateTime.Today.AddDays(-2),
                UserContractId = 1
            };

            modelBuilder.Entity<AccountBalance>()
            .ToTable("AccountBalance")
            .HasData(accountBalance);
        }
    }
}
