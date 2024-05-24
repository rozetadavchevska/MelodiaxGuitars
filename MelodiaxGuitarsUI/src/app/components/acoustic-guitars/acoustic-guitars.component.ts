import { Component } from '@angular/core';
import { MatOptionModule } from '@angular/material/core';
import { MatFormField, MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';

@Component({
  selector: 'app-acoustic-guitars',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatSelectModule,
    MatOptionModule,
  ],
  templateUrl: './acoustic-guitars.component.html',
  styleUrl: './acoustic-guitars.component.scss'
})
export class AcousticGuitarsComponent {
  selected = 'option2';
}
