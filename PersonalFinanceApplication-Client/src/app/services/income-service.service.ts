import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IncomeDto } from '../viewModels/models/incomeModel';
import {
  incomeEnvironment,
  userContractEnvironment,
} from 'src/environments/environment.development';
import { AccountBalanceDto } from '../viewModels/models/accountBalanceModel';

@Injectable({
  providedIn: 'root',
})
export class IncomeServiceService {
  private incomeApi = incomeEnvironment;
  private userContractApi = userContractEnvironment;
  constructor(private http: HttpClient) {}

  getBalance(userContractId: string): Observable<AccountBalanceDto> {
    return this.http.get<AccountBalanceDto>(
      this.userContractApi.balance + userContractId
    );
  }

  getAllIncomes(): Observable<Array<IncomeDto>> {
    return this.http.get<Array<IncomeDto>>(this.incomeApi.incomes);
  }

  addIncome(income: IncomeDto): Observable<any> {
    const requestBody = { IncomeDto: income };
    return this.http.post(this.incomeApi.addIncome, requestBody);
  }
}
