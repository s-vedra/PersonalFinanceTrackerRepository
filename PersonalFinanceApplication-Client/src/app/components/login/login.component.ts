import { Component } from '@angular/core';
import { AuthServiceService } from 'src/app/services/auth-service.service';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from '../custom-dialogs/error-dialog.component';
import { Router } from '@angular/router';
import { LoginDto } from 'src/app/viewModels/models/loginModel';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { catchError, throwError } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  errorMessage: string = '';
  loginForm!: FormGroup;

  constructor(
    private authService: AuthServiceService,
    private dialog: MatDialog,
    private router: Router,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
  }

  onSubmit() {
    if (this.loginForm.invalid) {
      return;
    }
    const loginDto: LoginDto = this.loginForm.value;
    const requestPayload = { loginRequestDto: loginDto };
    this.authService.loginWithEmail(requestPayload).subscribe({
      next: (response) => {
        console.log('Login response:', response); // Log the response to verify
        localStorage.setItem('authToken', response);
        this.router.navigate(['dashboard']);
      },
      error: () => {
        this.handleError('Email or Password is incorrect');
      },
    });
  }

  private handleError(message: string) {
    this.errorMessage = message;
    this.showErrorDialog(this.errorMessage);
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
