import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ExpenseDto } from '../viewModels/models/expenseModel';
import { expenseEnvironment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class ExpenseService {
  private expenseApi = expenseEnvironment;
  constructor(private http: HttpClient) {}

  addExpense(expense: ExpenseDto): Observable<any> {
    const requestBody = { ExpenseDto: expense };
    return this.http.post(this.expenseApi.addExpenditure, requestBody);
  }
}
