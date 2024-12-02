import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { exchangeRatesEnvironment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class ExchangeRatesService {
  private exchangeRatesApi = exchangeRatesEnvironment;
  constructor(private http: HttpClient) {}

  getCurrencies(): Observable<any> {
    return this.http.get<any>(this.exchangeRatesApi.currencies);
  }
}
