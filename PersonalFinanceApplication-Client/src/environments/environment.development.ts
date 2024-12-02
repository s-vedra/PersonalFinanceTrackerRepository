const baseEnvironment = {
  baseUrl: 'https://localhost:7189/api/',
};

export const incomeEnvironment = {
  incomes: baseEnvironment.baseUrl + 'dashboard/income/incomes',
  income: baseEnvironment.baseUrl + 'dashboard/income/income/',
  deleteIncome: baseEnvironment.baseUrl + 'dashboard/income/income/',
  addIncome: baseEnvironment.baseUrl + 'dashboard/income/income',
  updateIncome: baseEnvironment.baseUrl + 'dashboard/income/income/update',
};

export const expenseEnvironment = {
  expenditures: baseEnvironment.baseUrl + 'dashboard/expenses/expenditures',
  expenditure: baseEnvironment.baseUrl + 'dashboard/expenses/expenditure/',
  deleteExpenditure:
    baseEnvironment.baseUrl + 'dashboard/expenses/expenditure/',
  addExpenditure: baseEnvironment.baseUrl + 'dashboard/expenses/expenditure',
  updateExpenditure:
    baseEnvironment.baseUrl + 'dashboard/expenses/expenditure/update',
};

export const exchangeRatesEnvironment = {
  latestCurrencies:
    baseEnvironment.baseUrl + 'exchange-rates/latest-currencies/',
  historicalCurrencies:
    baseEnvironment.baseUrl + 'exchange-rates/historical-currencies',
  availableCurrencies:
    baseEnvironment.baseUrl + 'exchange-rates/available-currencies/',
  convert: baseEnvironment.baseUrl + 'exchange-rates/convert',
  currencies: baseEnvironment.baseUrl + 'exchange-rates/currencies',
};

export const userContractEnvironment = {
  balance: baseEnvironment.baseUrl + 'dashboard/user-contract/balance/',
};
