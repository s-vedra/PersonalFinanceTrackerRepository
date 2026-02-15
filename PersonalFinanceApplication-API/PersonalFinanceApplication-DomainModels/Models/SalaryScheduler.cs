namespace PersonalFinanceApplication_DomainModels.Models
{
    public class SalaryScheduler
    {
        public Guid ReferenceId { get; set; }
        public int SalarySchedulerId { get; set; }
        public int UserContractId { get; set; }
        public decimal Amount { get; set; }
        public int DayOfMonth { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? LastExecutedAt { get; set; }
        public string Notes { get; set; }
    }
}
