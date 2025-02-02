import { Component } from '@angular/core';
import { AuthServiceService } from 'src/app/services/auth-service.service';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from '../custom-dialogs/error-dialog.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  errorMessage: string = '';

  constructor(
    private authService: AuthServiceService,
    private dialog: MatDialog,
    private router: Router
  ) {}

  onSubmit() {
    if (this.email && this.password) {
      this.authService
        .loginWithEmail(this.email, this.password)
        .then(() => {
          this.authService.getToken().subscribe(
            () => {
              this.router.navigate(['/dashboard']);
            },
            () => {
              this.errorMessage = 'Token verification failed';
              this.showErrorDialog(this.errorMessage);
            }
          );
        })
        .catch(() => {
          this.errorMessage = 'Email or Password is incorrect';
          this.showErrorDialog(this.errorMessage);
        });
    }
  }

  loginWithGoogle() {
    this.authService
      .loginWithGoogle()
      .then((user) => {
        this.errorMessage = '';
      })
      .catch((error) => {
        this.errorMessage = 'Email or Password is incorrect';
        this.showErrorDialog(this.errorMessage);
      });
  }

  showErrorDialog(message: string) {
    this.dialog.open(ErrorDialogComponent, {
      data: { message },
    });
  }
}
