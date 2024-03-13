import { Component} from '@angular/core';
import { MatFormFieldModule, MatLabel} from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { RouterModule } from '@angular/router';
import {MatButtonModule} from '@angular/material/button';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    MatFormFieldModule, 
    MatLabel, 
    MatInputModule, 
    RouterModule,
    MatButtonModule
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})

export class RegisterComponent {

}