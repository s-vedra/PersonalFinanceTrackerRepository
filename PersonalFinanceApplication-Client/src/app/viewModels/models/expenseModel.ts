import { ExpenseCategory } from '../enums/expenseCategory';
import { PaymentIssue } from '../enums/paymentIssue';

export interface ExpenseDto {
  expenseId: number;
  date: Date;
  paymentIssue: PaymentIssue;
  category: ExpenseCategory;
  purpose: string;
  amount: number;
  currency: string;
  note: string;
  userContractId: number;
}
