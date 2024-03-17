import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { UserService } from '../../services/user/user.service';
import { HttpClientModule } from '@angular/common/http';
import { Router, RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { AuthService } from '../../services/auth/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatLabel,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    RouterModule,
    MatButtonModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  constructor(private userService:UserService, private authService:AuthService, private router:Router){}

  loginForm : FormGroup = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    passwordHash: new FormControl('', [Validators.required]) 
  });

  onSubmit(){
    if(this.loginForm.invalid){
      return;
    }

    const formData = this.loginForm.value;
    this.userService.loginUser(formData.email, formData.passwordHash).subscribe({
      next: (response) => {
        this.authService.storeToken(response);
        console.log('Logged in successfully. Token stored. ', response);
        const isAdmin = this.authService.isAdmin();
        if (isAdmin) {
          // Redirect to admin dashboard
          this.router.navigate(['/acoustic']);
        } else {
          // Redirect to user dashboard
          this.router.navigate(['/electric']);
        }
      },
      error: (err) => {
        console.error("Login failed. ", err);
      }
    })
  }
}
