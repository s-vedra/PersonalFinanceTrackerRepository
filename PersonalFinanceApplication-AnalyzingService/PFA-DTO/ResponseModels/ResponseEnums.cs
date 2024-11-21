namespace PFA_DTO.ResponseModels
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

    public enum Account
    {
        Cash = 1,
        Card
    }

    public enum TransactionType
    {
        Income = 1, 
        Expense
    }
}
