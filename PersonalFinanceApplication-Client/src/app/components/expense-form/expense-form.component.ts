import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ExchangeRatesService } from 'src/app/services/exchange-rates.service';
import { ExpenseService } from 'src/app/services/expense.service';
import { PaymentIssue } from 'src/app/viewModels/enums/paymentIssue';
import { ExpenseDto } from 'src/app/viewModels/models/expenseModel';
import { ExpenseCategory } from 'src/app/viewModels/enums/expenseCategory';
import { User } from 'firebase/auth';
import { AuthServiceService } from 'src/app/services/auth-service.service';

@Component({
  selector: 'app-expense-form',
  templateUrl: './expense-form.component.html',
  styleUrls: ['./expense-form.component.css'],
})
export class ExpenseFormComponent {
  expenseForm!: FormGroup;
  expenseDto!: ExpenseDto;
  paymentIssueEnumValues = this.getEnumLabels(PaymentIssue);
  categoryEnumValues = this.getEnumLabels(ExpenseCategory);
  currencies: any = {};
  currencyKeys: string[] = [];
  isSidebarActive = false;
  isLoggedIn: boolean = false;
  user: User | null = null;

  constructor(
    private expenseService: ExpenseService,
    private fb: FormBuilder,
    private exchangeRatesService: ExchangeRatesService,
    private authService: AuthServiceService
  ) {}

  ngOnInit(): void {
    this.authService.getAuthState().subscribe((user) => {
      this.isLoggedIn = !!user;
      this.user = user;

      if (this.isLoggedIn) {
        this.getAllCurrencies();
        this.expenseForm = this.fb.group({
          date: [null, Validators.required],
          paymentIssue: [PaymentIssue.Card, Validators.required],
          category: [ExpenseCategory.Others, Validators.required],
          purpose: [null, Validators.required],
          amount: [null, [Validators.required, Validators.min(1)]],
          currency: [this.currencyKeys[0], Validators.required],
          note: [''],
          userContractId: [11],
        });
      }
    });
  }

  toggleSidebar() {
    this.isSidebarActive = !this.isSidebarActive;
  }

  submitExpense() {
    if (this.expenseForm.valid) {
      this.expenseDto = { ...this.expenseForm.value };
      this.expenseDto.paymentIssue = Number(this.expenseDto.paymentIssue);
      this.expenseDto.category = Number(this.expenseDto.category);
      this.expenseService.addExpense(this.expenseDto).subscribe({
        next: () => {
          console.log('Expense saved successfully!');
        },
        error: (error) => {
          console.error(error, this.expenseDto);
        },
      });
    }
  }

  getAllCurrencies() {
    this.exchangeRatesService.getCurrencies().subscribe({
      next: (currencies) => {
        this.currencies = currencies;
        this.currencyKeys = Object.keys(this.currencies);
      },
      error: (err) => console.error(err),
    });
  }

  getEnumLabels(enumValues: any): { label: string; value: number }[] {
    return Object.keys(enumValues)
      .filter((key) => isNaN(Number(key)))
      .map((key) => ({
        label: key,
        value: enumValues[key],
      }));
  }
}
