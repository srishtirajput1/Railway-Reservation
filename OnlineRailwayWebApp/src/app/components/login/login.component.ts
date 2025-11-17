import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  userId = '';
  password = '';
  email = '';

  constructor(private authService: AuthService, private router: Router) {}

  login() {
    const userData = {
      userId: this.userId,
      password: this.password,
      email: this.email,
    };

    this.authService.login(userData).subscribe(
      (response) => {
        alert('✅ Login successful!');

        // Store the token
        this.authService.storeToken(response.token);

        // Get user role from response
        const userRole = response.user.role;

        // Navigate based on role
        if (userRole === 'Admin') {
          this.router.navigate(['/admin-dashboard']);
        } else {
          this.router.navigate(['/user-dashboard']);
        }
      },
      (error) => {
        alert('❌ Invalid credentials, please try again.');
        console.error(error);
      }
    );
  }
}
