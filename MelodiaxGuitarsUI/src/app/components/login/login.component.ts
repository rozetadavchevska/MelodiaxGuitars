import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { UserService } from '../../services/user/user.service';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { User } from '../../models/User';

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
  constructor(private userService:UserService){}

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
        console.log("loged in ", response);
        console.log(formData.email, formData.passwordHash);
      },
      error: (err) => {
        console.error("Error ", err);
        console.log('Error status:', err.status);
        console.log('Error message:', err.message);
        console.log(formData.email, formData.passwordHash);
      }
    })
  }
}
