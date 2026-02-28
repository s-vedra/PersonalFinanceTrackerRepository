namespace PersonalFinanceTracker_Contracts.FinancialTrackerContracts
{
    public enum IncomeCategory
    {
        Allowance = 1,
        Salary,
        PettyCash,
        Bonus,
        Other
    }

    public enum ExpenseCategory
    {
        Shopping = 1,
        FoodDrinks,
        BillsUtilities,
        Others
    }

    public enum ContractType
    {
        CurrentAccount = 1,
        CreditAccount,
        SavingsAccount,
        TermDepositAccount,
        InvestmentAccount,
        RetirementAccount,
        BusinessAccount,
        JointAccount
    }

    public enum PaymentIssue
    {
        Cash = 1,
        Card
    }

    public enum TransactionType
    {
        Income = 1,
        Expense
    }

    public enum BalanceOperation
    {
        InitializeBalance = 1,
        AdjustBalance,
        RemoveBalance,
        RetrieveBalance
    }

    public enum UserContractStatus
    {
        New = 1,
        Active,
        Closed,
        Inactive,
        Terminated
    }
}
