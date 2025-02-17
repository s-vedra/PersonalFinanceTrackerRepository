export const baseEnvironment = {
  baseUrl: 'https://localhost:7189/api/',
  firebase: {
    apiKey: 'AIzaSyDV0EhNFqNn3W0B8kO2myJRiuJO1Sg2bfA',
    authDomain: 'financetracker-auth.firebaseapp.com',
    projectId: 'financetracker-auth',
    storageBucket: 'financetracker-auth.firebasestorage.app',
    messagingSenderId: '123415401093',
    appId: '1:123415401093:web:eefb88c6e86d7bc58e8fec',
    measurementId: 'G-D91S5847MY',
  },
  gateawayUrl: 'https://localhost:7046/api/',
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

export const gateawayServiceEnvironment = {
  verifyLoginToken:
    baseEnvironment.gateawayUrl + 'gateway-service/verify-login-token',
  login: baseEnvironment.gateawayUrl + 'gateway-service/login',
};
