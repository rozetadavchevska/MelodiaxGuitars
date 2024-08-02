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
  selector: 'app-amps',
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
  templateUrl: './amps.component.html',
  styleUrl: './amps.component.scss'
})
export class AmpsComponent implements OnInit{
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

  selectedBrand:string | null = null;
  ampsCategoryId:string | null = null;

  ngOnInit(): void {
    this.brandService.getBrands().subscribe(
      (brands) => {
        this.brands = brands;
      } 
    )

    this.categoryService.getCategories().subscribe(
      (categories) => {
        this.categories = categories;
        const ampsCategory = this.categories.find(category => category.name.toLowerCase() === "amps");
        if(ampsCategory){
          this.ampsCategoryId = ampsCategory.id;
          this.loadProducts();
        }
      }
    )
  }

  loadProducts(): void{
    this.productService.getProducts().subscribe(
      (products) => {
        if(this.ampsCategoryId){
          this.products = products.filter(product => product.categoryId === this.ampsCategoryId);
          this.filteredProducts = [...products];
        }
      }
    )
  }

  filterProducts(): void{
    this.filteredProducts = this.products.filter(product => {
      return !this.selectedBrand || product.brandId.toString() === this.selectedBrand.toString();
    })
  }

  goToProductDetails(productId:string): void {
    this.router.navigate(['/product', productId]);
  }
}
