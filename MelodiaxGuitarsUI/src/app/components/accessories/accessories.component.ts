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
import { Category } from '../../models/Category';
import { BrandService } from '../../services/brand/brand.service';
import { ProductService } from '../../services/product/product.service';
import { CategoryService } from '../../services/category/category.service';
import { Router } from '@angular/router';

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
  constructor(
    private brandService:BrandService,
    private productService:ProductService,
    private categoryService:CategoryService,
    private router:Router
  ){}

  brands:Brand[] = [];
  categories:Category[] = [];
  products:Product[] = [];
  filteredProducts:Product[] = [];

  selectedBrand: string | null = null;
  accessoriesCategoryId: string | null = null;

  ngOnInit():void{
    this.brandService.getBrands().subscribe(
      (brands) => {
        this.brands = brands;
      }
    )

    this.categoryService.getCategories().subscribe(
      (categories) => {
        this.categories = categories;
        const accessoriesCategory = this.categories.find(category => category.name.toLowerCase() === "accessories");
        if(accessoriesCategory){
          this.accessoriesCategoryId = accessoriesCategory.id;
          this.loadProducts();
        }
      }
    )
  }

  loadProducts():void{
    this.productService.getProducts().subscribe(
      (products) => {
        if(this.accessoriesCategoryId){
          this.products = products.filter(product => product.categoryId === this.accessoriesCategoryId);
          this.filteredProducts = [...products];
        }
      }
    )
  }

  filterProducts():void{
    this.filteredProducts = this.products.filter(product => {
      return !this.selectedBrand || product.brandId.toString() === this.selectedBrand.toString();
    })
  }

  goToProductDetails(productId:string){
    this.router.navigate(['/product', productId]);
  }
}
