import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ExchangeRatesService } from 'src/app/services/exchange-rates.service';
import { IncomeServiceService } from 'src/app/services/income-service.service';
import { PaymentIssue } from 'src/app/viewModels/enums/paymentIssue';
import { IncomeCategory } from 'src/app/viewModels/enums/incomeCategory';
import { IncomeDto } from 'src/app/viewModels/models/incomeModel';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from '../custom-dialogs/error-dialog.component';

@Component({
  selector: 'app-income-form',
  templateUrl: './income-form.component.html',
  styleUrls: ['./income-form.component.css'],
})
export class IncomeFormComponent {
  incomeForm!: FormGroup;
  incomeDto!: IncomeDto;
  paymentIssueEnumValues = this.getEnumLabels(PaymentIssue);
  categoryEnumValues = this.getEnumLabels(IncomeCategory);
  currencies: any = {};
  currencyKeys: string[] = [];
  isSidebarActive = false;

  constructor(
    private incomeService: IncomeServiceService,
    private fb: FormBuilder,
    private exchangeRatesService: ExchangeRatesService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.getAllCurrencies();
    this.incomeForm = this.fb.group({
      date: [null, Validators.required],
      paymentIssue: [PaymentIssue.Card, Validators.required],
      category: [IncomeCategory.Allowance, Validators.required],
      purpose: [null, Validators.required],
      amount: [null, [Validators.required, Validators.min(1)]],
      currency: [this.currencyKeys[0], Validators.required],
      note: [''],
      userContractId: [11],
    });
  }

  showErrorDialog(message: string) {
    // this.dialog.open(ErrorDialogComponent, {
    //   data: { message },
    // });
  }

  showSuccessDialog(message: string) {
    this.dialog.open(ErrorDialogComponent, {
      data: { message },
    });
  }

  toggleSidebar() {
    this.isSidebarActive = !this.isSidebarActive;
  }

  submitIncome() {
    if (this.incomeForm.valid) {
      this.incomeDto = { ...this.incomeForm.value };
      this.incomeDto.paymentIssue = Number(this.incomeDto.paymentIssue);
      this.incomeDto.category = Number(this.incomeDto.category);
      this.incomeService.addIncome(this.incomeDto).subscribe({
        next: () => {
          this.showSuccessDialog('Income saved!');
        },
        error: (error) => {
          this.showErrorDialog(error);
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
