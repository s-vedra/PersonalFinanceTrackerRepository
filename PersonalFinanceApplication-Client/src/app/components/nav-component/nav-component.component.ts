import { Component } from '@angular/core';
import { AuthServiceService } from 'src/app/services/auth-service.service';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from '../custom-dialogs/error-dialog.component';
import { User } from 'firebase/auth';

@Component({
  selector: 'app-nav-component',
  templateUrl: './nav-component.component.html',
  styleUrls: ['./nav-component.component.css'],
})
export class NavComponentComponent {
  constructor(
    private authService: AuthServiceService,
    private dialog: MatDialog
  ) {}
  isSidebarActive = false;
  isLoggedIn: boolean = false;
  user: User | null = null;
  logout() {
    this.authService
      .logout()
      .then(() => {
        this.isLoggedIn = false;
        this.user = null;
      })
      .catch((error) => {
        this.showErrorDialog(error);
      });
  }

  toggleSidebar() {
    this.isSidebarActive = !this.isSidebarActive;
  }

  showErrorDialog(message: string) {
    this.dialog.open(ErrorDialogComponent, {
      data: { message },
    });
  }
}
