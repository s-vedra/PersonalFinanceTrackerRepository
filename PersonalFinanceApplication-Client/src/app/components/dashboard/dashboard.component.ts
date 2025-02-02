import { Component, OnInit } from '@angular/core';
import { IncomeServiceService } from 'src/app/services/income-service.service';
import { IncomeCategory } from 'src/app/viewModels/enums/incomeCategory';
import { AccountBalanceDto } from 'src/app/viewModels/models/accountBalanceModel';
import { IncomeDto } from 'src/app/viewModels/models/incomeModel';
import { AuthServiceService } from 'src/app/services/auth-service.service';
import { User } from 'firebase/auth';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from '../custom-dialogs/error-dialog.component';

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
  isLoggedIn: boolean = false;
  user: User | null = null;

  constructor(
    private incomeService: IncomeServiceService,
    private authService: AuthServiceService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    // this.authService.getAuthState().subscribe((user) => {
    //   this.isLoggedIn = !!user;
    //   this.user = user;
    //   if (this.isLoggedIn) {
    //     this.getDataFromApi();
    //   }
    // });
  }

  showErrorDialog(message: string) {
    this.dialog.open(ErrorDialogComponent, {
      data: { message },
    });
  }
  toggleSidebar() {
    this.isSidebarActive = !this.isSidebarActive;
  }

  getDataFromApi(): void {
    this.incomeService.getAllIncomes().subscribe({
      next: (incomes) => {
        this.incomes = incomes;
      },
      error: (message) => console.log(message),
    });
  }

  getBalance(userContractId: string) {
    this.incomeService.getBalance(userContractId).subscribe({
      next: (balance) => {
        this.balance = balance;
      },
      error: (err) => console.error(err),
    });
  }
}
