import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { IncomeFormComponent } from './components/income-form/income-form.component';
import { ExpenseFormComponent } from './components/expense-form/expense-form.component';

const routes: Routes = [
  { path: '', component: DashboardComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'transactions/income', component: IncomeFormComponent },
  { path: 'transactions/expense', component: ExpenseFormComponent },
  // { path: 'analytics', component: DashboardComponent },
  // { path: 'settings', component: DashboardComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
