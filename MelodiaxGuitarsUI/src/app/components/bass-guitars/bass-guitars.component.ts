import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatOptionModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { BrandService } from '../../services/brand/brand.service';
import { CategoryService } from '../../services/category/category.service';
import { ProductService } from '../../services/product/product.service';
import { Brand } from '../../models/Brand';
import { Category } from '../../models/Category';
import { Product } from '../../models/Product';
import { Router } from '@angular/router';

@Component({
  selector: 'app-bass-guitars',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatCardModule,
    MatFormFieldModule,
    FormsModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatOptionModule
  ],
  templateUrl: './bass-guitars.component.html',
  styleUrl: './bass-guitars.component.scss'
})
export class BassGuitarsComponent implements OnInit{
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

  bassCategoryId: string | null = null;
  selectedBrand: string | null = null;

  ngOnInit(): void {
    this.brandService.getBrands().subscribe(
      (brands) => {
        this.brands = brands;
      }
    )

    this.categoryService.getCategories().subscribe(
      (categories) => {
        this.categories = categories;
        const bassCategory = this.categories.find(category => category.name.toLowerCase() === 'bass');
        if(bassCategory){
          this.bassCategoryId = bassCategory.id;
          this.loadProducts();
        } 
      }
    )
  }

  loadProducts(): void {
    this.productService.getProducts().subscribe(
      (products) => {
        if(this.bassCategoryId){
          this.products = products.filter(product => product.categoryId === this.bassCategoryId);
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

  goToProductDetails(productId:string): void{
    this.router.navigate(['/product', productId]);
  }
}
