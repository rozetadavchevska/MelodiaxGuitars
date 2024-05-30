import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatOptionModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { Brand } from '../../models/Brand';
import { Category } from '../../models/Category';
import { BrandService } from '../../services/brand/brand.service';
import { CategoryService } from '../../services/category/category.service';
import { ProductService } from '../../services/product/product.service';
import { Product } from '../../models/Product';

@Component({
  selector: 'app-electric-guitars',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatFormFieldModule,
    FormsModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatOptionModule,
    MatCardModule,
  ],
  templateUrl: './electric-guitars.component.html',
  styleUrl: './electric-guitars.component.scss'
})
export class ElectricGuitarsComponent implements OnInit {
  constructor(
    private brandService:BrandService,
    private categoryService:CategoryService,
    private productService:ProductService
  ){}

  brands:Brand[] = [];
  categories:Category[] = [];
  products:Product[] = [];
  filteredProducts:Product[] = [];

  electricCategoryId: string | null = null;
  selectedBrand: string | null = null; 

  ngOnInit(): void {
    this.brandService.getBrands().subscribe(
      (brands) => {
        this.brands = brands;
      }
    );

    this.categoryService.getCategories().subscribe(
      (categories) => {
        this.categories = categories;
        const electricCategory = this.categories.find(category => category.name.toLowerCase() === 'electric');
        if(electricCategory){
          this.electricCategoryId = electricCategory.id;
          this.loadProducts();
        }
      }
    );
  }

  loadProducts(): void {
    this.productService.getProducts().subscribe(
      (products) => {
        if(this.electricCategoryId){
          this.products = products.filter(product => product.categoryId === this.electricCategoryId);
          this.filteredProducts = [...this.products];
        }
      }
    )
  }

  filterProducts(): void {
    this.filteredProducts = this.products.filter(product => {
      return !this.selectedBrand || product.brandId.toString() === this.selectedBrand.toString();
    })
  }
}
