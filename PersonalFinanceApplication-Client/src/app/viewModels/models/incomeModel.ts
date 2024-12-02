import { PaymentIssue } from '../enums/paymentIssue';
import { IncomeCategory } from '../enums/incomeCategory';

export interface IncomeDto {
  incomeId: number;
  date: Date;
  paymentIssue: PaymentIssue;
  category: IncomeCategory;
  purpose: string;
  amount: number;
  currency: string;
  note: string;
  userContractId: number;
}
