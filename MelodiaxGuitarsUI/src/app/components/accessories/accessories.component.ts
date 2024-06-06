import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatOptionModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { Brand } from '../../models/Brand';
import { Product } from '../../models/Product';

@Component({
  selector: 'app-accessories',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatSelectModule,
    MatOptionModule,
    MatCardModule,
    MatFormFieldModule,
    FormsModule,
    ReactiveFormsModule
  ],
  templateUrl: './accessories.component.html',
  styleUrl: './accessories.component.scss'
})
export class AccessoriesComponent implements OnInit {
  brands:Brand[] = [];

  filteredProducts:Product[] = [];

  selectedBrand: string | null = null;

  ngOnInit(): void {
    
  }

  filterProducts():void{

  }
}
