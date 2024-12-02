import { UserContractStatus } from '../enums/userContractStatus';
import { AccountBalanceDto } from './accountBalanceModel';
import { ExpenseDto } from './expenseModel';
import { IncomeDto } from './incomeModel';

export interface UserContractDto {
  userContractId: number;
  accountBalance: AccountBalanceDto;
  contractName: string;
  incomes: IncomeDto[];
  expenses: ExpenseDto[];
  userId: number;
  userContractStatus: UserContractStatus;
  dateOpened: Date;
}
