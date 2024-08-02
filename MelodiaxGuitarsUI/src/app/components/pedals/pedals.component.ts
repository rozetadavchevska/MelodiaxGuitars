import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatCommonModule, MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { Router} from '@angular/router';
import { BrandService } from '../../services/brand/brand.service';
import { CategoryService } from '../../services/category/category.service';
import { ProductService } from '../../services/product/product.service';
import { Brand } from '../../models/Brand';
import { Category } from '../../models/Category';
import { Product } from '../../models/Product';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';

@Component({
  selector: 'app-pedals',
  standalone: true,
  imports: [
    MatButtonModule,
    MatCardModule,
    MatSelectModule,
    MatCommonModule,
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatOptionModule,
  ],
  templateUrl: './pedals.component.html',
  styleUrl: './pedals.component.scss'
})
export class PedalsComponent implements OnInit{
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

  pedalsCategoryId: string | null = null;
  selectedBrand: string | null = null;

  ngOnInit():void{
    this.brandService.getBrands().subscribe(
      (brands) => {
        this.brands = brands;
      }
    );

    this.categoryService.getCategories().subscribe(
      (categories) => {
        this.categories = categories;
        const pedalsCategory = this.categories.find(category => category.name.toLowerCase() === 'pedals');
        if(pedalsCategory){
          this.pedalsCategoryId = pedalsCategory.id;
          this.loadProducts();
        }
      }
    );
  };

  loadProducts(): void{
    this.productService.getProducts().subscribe(
      (products) => {
        this.products = products.filter(product => product.categoryId === this.pedalsCategoryId);
        this.filteredProducts = [...this.products];
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
