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
  ],
  templateUrl: './acoustic-guitars.component.html',
  styleUrl: './acoustic-guitars.component.scss'
})
export class AcousticGuitarsComponent implements OnInit {
  constructor(
    private brandService:BrandService,
    private categoryService:CategoryService,
    private productService:ProductService
  ){}
  brands:Brand[] = [];
  categories:Category[] = [];
  products:Product[] = [];

  ngOnInit():void{
    this.brandService.getBrands().subscribe(
      (brands) => {
        this.brands = brands;
      }
    )

    this.categoryService.getCategories().subscribe(
      (categories) => {
        this.categories = categories;
      }
    )

    this.productService.getProducts().subscribe(
      (products) => {
        this.products = products;
      }
    )
  }
}
