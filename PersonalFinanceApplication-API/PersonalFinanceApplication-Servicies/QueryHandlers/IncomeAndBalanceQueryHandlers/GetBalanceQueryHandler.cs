﻿using MediatR;
using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_DTO.DtoModels;
using PersonalFinanceApplication_Exceptions.Exceptions;
using PersonalFinanceApplication_Services.ExtensionMethods;

namespace PersonalFinanceApplication_Services.QueryHandlers.IncomeAndBalanceQueryHandlers
{
    public class GetBalanceQuery : IRequest<BalanceDto>
    {
        public string Currency { get; set; }
    }

    public class GetBalanceQueryHandler : IRequestHandler<GetBalanceQuery, BalanceDto>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IExpenseRepository _expenseRepository;
        public GetBalanceQueryHandler(IIncomeRepository incomeRepository, IExpenseRepository expenseRepository)
        {
            _incomeRepository = incomeRepository;
            _expenseRepository = expenseRepository;
        }

        public async Task<BalanceDto> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
        {
            var incomes = _incomeRepository.GetAllEntities().Where(x => x.Currency.ToLower().Equals(request.Currency.ToLower()));
            var expenses = _expenseRepository.GetAllEntities().Where(x => x.Currency.ToLower().Equals(request.Currency.ToLower()));

            if (!incomes.Any() && !expenses.Any())
            {
                throw new CoreException("You dont't have balance on your account");
            }

            var incomeBalance = incomes.Select(x => x.Amount).Sum();
            var expenseBalance = expenses.Select(x => x.Amount).Sum();

            var balanceDto = new BalanceDto
            {
                Amount = incomeBalance.Subtract(expenseBalance),
                Currency = request.Currency,
                LastDateAddedMoney = incomes.Last().Date,
                LastDateDrawMoney = expenses.Last().Date
            };

            return balanceDto;
        }
    }
}
