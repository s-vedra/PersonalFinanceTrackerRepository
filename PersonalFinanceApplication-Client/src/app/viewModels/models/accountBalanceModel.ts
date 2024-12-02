export interface AccountBalanceDto {
  accountBalanceId: number;
  amount: number;
  currency: string;
  lastDateAddedMoney: Date;
  lastDateDrawMoney: Date;
  userContractId: number;
}
