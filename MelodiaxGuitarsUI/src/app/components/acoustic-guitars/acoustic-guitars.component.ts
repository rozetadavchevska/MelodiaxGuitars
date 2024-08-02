import { Component, OnInit } from '@angular/core';
import { MatOptionModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatSliderModule } from '@angular/material/slider';
import { MatCardModule } from '@angular/material/card';
import { Brand } from '../../models/Brand';
import { Category } from '../../models/Category';
import { BrandService } from '../../services/brand/brand.service';
import { CategoryService } from '../../services/category/category.service';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { Product } from '../../models/Product';
import { ProductService } from '../../services/product/product.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';


@Component({
  selector: 'app-acoustic-guitars',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatSelectModule,
    MatOptionModule,
    MatSliderModule,
    CommonModule,
    MatCardModule,
    MatButtonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  templateUrl: './acoustic-guitars.component.html',
  styleUrl: './acoustic-guitars.component.scss'
})
export class AcousticGuitarsComponent implements OnInit {
  constructor(
    private brandService:BrandService,
    private categoryService:CategoryService,
    private productService:ProductService,
    private router:Router
  ){}
  brands:Brand[] = [];
  categories:Category[] = [];
  products:Product[] = [];
  filteredProducts:Product[] = [];

  acousticCategoryId:string | null = null;
  selectedBrand: string | null = null;
  

  ngOnInit(): void {
    this.brandService.getBrands().subscribe((brands) => {
      this.brands = brands;
    });

    this.categoryService.getCategories().subscribe((categories) => {
      this.categories = categories;
      const acousticCategory = this.categories.find(category => category.name.toLowerCase() === 'acoustic');
      if (acousticCategory) {
        this.acousticCategoryId = acousticCategory.id;
        this.loadProducts();
      }
    });
  }

  loadProducts(): void {
    this.productService.getProducts().subscribe((products) => {
      if (this.acousticCategoryId) {
        this.products = products.filter(product => product.categoryId === this.acousticCategoryId);
        this.filteredProducts = [...this.products]; 
      }
    });
  }

  filterProducts(): void {
    this.filteredProducts = this.products.filter(product => {
      return !this.selectedBrand || product.brandId.toString() === this.selectedBrand.toString();
    });
  }

  goToProductDetails(productId: string): void{
    this.router.navigate(['/product', productId]);
  }

}
