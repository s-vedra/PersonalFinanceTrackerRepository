﻿using MediatR;
using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_DTO.DtoModels;
using PersonalFinanceApplication_Exceptions.Exceptions;
using PersonalFinanceApplication_Mappers.ExpenseMapper;

namespace PersonalFinanceApplication_Services.QueryHandlers.ExpenseQueryHandlers
{
    public class GetExpensesQuery : IRequest<List<ExpenseDto>>
    {
    }

    public class GetExpensesQueryHandler : IRequestHandler<GetExpensesQuery, List<ExpenseDto>>
    {
        private readonly IExpenseRepository _expenseRepository;
        public GetExpensesQueryHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<List<ExpenseDto>> Handle(GetExpensesQuery request, CancellationToken cancellationToken)
        {
            var expenses = _expenseRepository.GetAllEntities();
            if (!expenses.Any())
            {
                throw new CoreException("No expenses found!");
            }
            return expenses.Select(x => x.ToDto()).ToList();
        }
    }
}
