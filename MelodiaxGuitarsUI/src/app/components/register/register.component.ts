import { Component} from '@angular/core';
import { MatFormFieldModule, MatLabel} from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Router, RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { User } from '../../models/User';
import { HttpClientModule } from '@angular/common/http';
import { UserService } from '../../services/user/user.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    HttpClientModule,
    MatFormFieldModule, 
    MatLabel, 
    MatInputModule, 
    RouterModule,
    MatButtonModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})

export class RegisterComponent {
  constructor(private userService:UserService, private router:Router){}

  user:User = {
    firstName: '',
    lastName: '',
    email: '',
    passwordHash: '',
    phoneNumber: '',
    address: '',
    city: '',
    country: ''
  }

  registerForm:FormGroup = new FormGroup({
    firstName: new FormControl('', [Validators.required]),
    lastName: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required, Validators.email]),
    passwordHash: new FormControl('', [Validators.required, Validators.minLength(8)]),
    phoneNumber: new FormControl('', [Validators.required, Validators.maxLength(15)]),
    address: new FormControl('', [Validators.required]),
    city: new FormControl('', [Validators.required]),
    country: new FormControl('', [Validators.required])
  });

  onSubmit(){
    if(this.registerForm.invalid){
      return;
    }

    Object.assign(this.user, this.registerForm.value)

    this.userService.registerUser(this.user).subscribe({
      next: response => {
        console.log('User registration successful', response);
        this.registerForm.reset();
        this.router.navigate(['/login']);
      },
      error: err => {
        console.error('Error during user registration:', err);
        console.log('Error status:', err.status);
        console.log('Error message:', err.message);
      }
    });
  }


}