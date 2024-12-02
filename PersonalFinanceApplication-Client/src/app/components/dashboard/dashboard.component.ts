import { Component, OnInit } from '@angular/core';
import { IncomeServiceService } from 'src/app/services/income-service.service';
import { IncomeCategory } from 'src/app/viewModels/enums/incomeCategory';
import { AccountBalanceDto } from 'src/app/viewModels/models/accountBalanceModel';
import { IncomeDto } from 'src/app/viewModels/models/incomeModel';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  incomes!: Array<IncomeDto>;
  categories!: Array<string>;
  balance!: AccountBalanceDto;
  currencies: any = {};
  selectedCurrency: string = 'MKD';
  objectKeys: string[] = [];
  isSidebarActive = false;

  constructor(private incomeService: IncomeServiceService) {}

  ngOnInit(): void {
    this.getDataFromApi();
  }

  logout() {
    console.log('Logout clicked');
    // Implement your logout logic here
  }
  toggleSidebar() {
    this.isSidebarActive = !this.isSidebarActive;
  }

  getDataFromApi(): void {
    this.incomeService.getAllIncomes().subscribe({
      next: (incomes) => {
        this.incomes = incomes;
        // this.categories = this.getIncomeCategoryFromEnum();
      },
      error: (message) => console.log(message),
    });
  }

  // getIncomeCategoryFromEnum(): Array<string> {
  //   return this.incomes.map((income) => {
  //     switch (income.category) {
  //       case 1:
  //         return (income.incomeCategory = IncomeCategory.Allowance);
  //       case 2:
  //         return (income.incomeCategory = IncomeCategory.Salary);
  //       case 3:
  //         return (income.incomeCategory = IncomeCategory.PettyCash);
  //       case 4:
  //         return (income.incomeCategory = IncomeCategory.Bonus);
  //       case 5:
  //         return (income.incomeCategory = IncomeCategory.Other);
  //       default:
  //         return 'Unknown';
  //     }
  //   });
  // }

  getBalance(userContractId: string) {
    this.incomeService.getBalance(userContractId).subscribe({
      next: (balance) => {
        this.balance = balance;
      },
      error: (err) => console.error(err),
    });
  }
}
